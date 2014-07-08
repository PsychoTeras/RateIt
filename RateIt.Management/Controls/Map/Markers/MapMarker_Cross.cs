using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using GMap.NET;
using GMap.NET.WindowsForms;
using RateIt.Common.Classes;
using RateIt.Common.Core.Constants;
using RateIt.Management.Helpers;

namespace RateIt.Management.Controls.Map.Markers
{
    public class MapMarker_Cross : GMapMarker
    {

#region Static public fields

        public static Color AreaLevel1Color = Color.DarkBlue;
        public static Color AreaLevel2Color = Color.DarkCyan;
        public static Color AreaLevel3Color = Color.DarkGoldenrod;

#endregion
         
#region Static private fields

        private static readonly Bitmap _markerIcon;

        private static readonly Array _areaLevels;
        private static readonly int _areaLevelsCount;
        private static readonly Pen[] _areaLevelPens;
        private static readonly Brush[] _areaLevelBrushes;
        private static readonly string[] _areaLevelCaptions;
        private static readonly Font _areaLevelCaptionFont;
        private static readonly Brush _areaLevelCaptionBrush;
        private static readonly StringFormat _stringFormat;

#endregion

#region Private fields

        private readonly MapViewer _map;

#endregion

#region Static ctor

        static MapMarker_Cross()
        {
            //Initialize array of area levels values
            _areaLevels = Enum.GetValues(typeof(StoreQueryAreaLevel));
            _areaLevelsCount = _areaLevels.Length;

            //Initialize area levels variables, pens
            _areaLevelPens = new[]
            {
                new Pen(Color.FromArgb(150, AreaLevel1Color), 1),
                new Pen(Color.FromArgb(150, AreaLevel2Color), 1),
                new Pen(Color.FromArgb(150, AreaLevel3Color), 1)
            };
            //brushes
            _areaLevelBrushes = new Brush[]
            {
                new SolidBrush(Color.FromArgb(50, AreaLevel1Color)),
                new SolidBrush(Color.FromArgb(50, AreaLevel2Color)),
                new SolidBrush(Color.FromArgb(50, AreaLevel3Color))
            };
            //captions
            _areaLevelCaptions = new string[_areaLevelsCount];
            for (int i = 0; i < _areaLevelsCount; i++)
            {
                StoreQueryAreaLevel areaLevel = (StoreQueryAreaLevel) _areaLevels.GetValue(i);
                _areaLevelCaptions[i] = string.Format("{0}m", (int)areaLevel);
            }
            //captions font
            _areaLevelCaptionFont = new Font("Calibri", 6, FontStyle.Bold);
            //captions brush
            _areaLevelCaptionBrush = new SolidBrush(Color.Black);
            //and caption string format
            _stringFormat = new StringFormat(StringFormatFlags.NoWrap)
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            //Initialize center point image
            string imageResName = "RateIt.Management.Resources.marker_main.png";
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(imageResName))
            {
                if (stream != null)
                {
                    _markerIcon = new Bitmap(stream);
                }
            }
        }

#endregion

#region Ctor

        public MapMarker_Cross(MapViewer map, PointLatLng p) 
            : base(p)
        {
            _map = map;
            IsHitTestVisible = false;
        }

#endregion

#region Class methods

        public override void OnRender(Graphics g)
        {
            //Get current geoposition
            GeoPoint geoPosition = Position.ToGeoPoint();

            //Draw store query area levels
            for (int i = 0; i < _areaLevelsCount; i++)
            {
                //Calculate area outer georectangle
                StoreQueryAreaLevel areaLevel = (StoreQueryAreaLevel) _areaLevels.GetValue(i);
                GeoArea geoArea = GeoArea.Rectangle(geoPosition, (uint) areaLevel, (uint) areaLevel);

                //Translate outer georectangle points to local points
                PointF[] fPoints = new PointF[4];
                for (int y = 0; y < 4; y++)
                {
                    GeoPoint point = geoArea.Points[y];
                    fPoints[y] = _map.FromLatLngToLocal(point.ToPointLatLng()).ToPointF();
                }

                //Calculate local outer rectangle
                RectangleF areaRect = new RectangleF
                (
                    fPoints[0].X, 
                    fPoints[1].Y, 
                    fPoints[2].X - fPoints[1].X,
                    fPoints[0].Y - fPoints[1].Y
                );

                //Draw query area
                if (areaRect.Width > _markerIcon.Width && areaRect.Height > _markerIcon.Height)
                {
                    const int captionHeight = 8;
                    RectangleF captionRect = new RectangleF
                    (
                        fPoints[0].X,
                        fPoints[1].Y - captionHeight, 
                        fPoints[2].X - fPoints[1].X,
                        captionHeight
                    );

                    g.FillEllipse(_areaLevelBrushes[i], areaRect);
                    g.DrawEllipse(_areaLevelPens[i], areaRect);
                    g.DrawString(_areaLevelCaptions[i], _areaLevelCaptionFont,
                        _areaLevelCaptionBrush, captionRect, _stringFormat);
                }
            }

            //Draw center point
            g.DrawImageUnscaled(_markerIcon,
                                LocalPosition.X - _markerIcon.Width/2,
                                LocalPosition.Y - _markerIcon.Height/2);

        }

#endregion

    }
}
