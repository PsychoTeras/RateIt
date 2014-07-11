using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.ToolTips;
using RateIt.Common.Core.Classes;
using RateIt.Common.Core.Entities.Stores;
using RateIt.Management.Helpers;

namespace RateIt.Management.Controls.Map.Markers
{
    public class MapMarker_Store : GMapMarker
    {

#region Constants

        public const int MIN_ZOOM_LEVEL_FOR_VISIBILITY = 14;

#endregion

#region Static private fields

        private static readonly Pen _pen;
        private static readonly Brush _brush;
        private static readonly GraphicsPath _graphicsPath;

        private static readonly Bitmap _storeIconBig;
        private static readonly Bitmap _storeIconSmall;

#endregion

#region Private fields

        private readonly MapViewer _map;

#endregion

#region Properties

        public Store Store { get; private set; }

#endregion

#region Static ctor

        static MapMarker_Store()
        {
            _pen = new Pen(Brushes.DarkGreen, 1);
            _brush = new SolidBrush(Color.FromArgb(50, Color.ForestGreen));
            _graphicsPath = new GraphicsPath(FillMode.Winding);

            string imageResName = "RateIt.Management.Resources.marker_shop_big.png";
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(imageResName))
            {
                if (stream != null)
                {
                    _storeIconBig = new Bitmap(stream);
                }
            }
            imageResName = "RateIt.Management.Resources.marker_shop_small.png";
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(imageResName))
            {
                if (stream != null)
                {
                    _storeIconSmall = new Bitmap(stream);
                }
            }
        }

#endregion

#region Ctor

        public MapMarker_Store(MapViewer map, Store store) : base(new PointLatLng())
        {
            _map = map;
            ToolTipMode = MarkerTooltipMode.OnMouseOver;
            ToolTip = new GMapBaloonToolTip(this);
            ToolTip.Foreground = new SolidBrush(Color.FromArgb(20, 20, 20));
            ToolTip.Fill = new SolidBrush(Color.AntiqueWhite);
            ToolTip.Font = new Font(_map.Font, FontStyle.Regular);
            Update(store);
        }

#endregion

#region Class methods

        public void Update()
        {
            Update(Store);
        }

        public void Update(Store store)
        {
            Store = store;
            Position = Store.Location.ToPointLatLng();
            ToolTipText = string.Format("{0}\r\n{1}", Store.StoreName, Store.Address).Trim();
        }

        public override void OnRender(Graphics g)
        {
            if (IsVisible)
            {
                //Get store geoares
                GeoArea storeArea = Store.Size.ToGeoArea(Store.Location);

                //Convert geocordinates to local coordinates
                int cnt = storeArea.Points.Count;
                GPoint[] points = new GPoint[cnt];
                for (int i = 0; i < cnt; i++)
                {
                    GeoPoint point = storeArea.Points[i];
                    points[i] = _map.FromLatLngToLocal(point.ToPointLatLng());
                }

                //Make array of local points to draw area rectangle line-by-line
                PointF[] fPoints = new PointF[cnt];
                for (int i = 0; i < cnt; i++)
                {
                    fPoints[i] = new PointF(points[i].X, points[i].Y);
                }

                //Setup marker size
                int markerWidth = (int)(fPoints[2].X - fPoints[0].X) + 1;
                int markerHeight = (int)(fPoints[0].Y - fPoints[1].Y) + 1;
                if (Size.Width != markerWidth || Size.Height != markerHeight)
                {
                    Size = new Size(markerWidth, markerHeight);
                }

                //Setup marker position
                GPoint markerLocation = _map.FromLatLngToLocal(Store.Location.ToPointLatLng());
                LocalPosition = new Point
                    (
                        markerLocation.X - markerWidth / 2,
                        markerLocation.Y - markerHeight / 2
                    );

                //Draw area rectangle
                _graphicsPath.Reset();
                _graphicsPath.AddLines(fPoints);
                g.FillPath(_brush, _graphicsPath);
                _graphicsPath.CloseFigure();
                g.DrawPath(_pen, _graphicsPath);

                //Draw store icon
                Bitmap storeIcon = _map.Zoom > 16
                                       ? _storeIconBig
                                       : _storeIconSmall;
                GeoPoint storeAreaCenter = storeArea.CenterPoint;
                GPoint gStoreAreaCenter = _map.FromLatLngToLocal(storeAreaCenter.ToPointLatLng());
                g.DrawImageUnscaled(storeIcon,
                                    gStoreAreaCenter.X - storeIcon.Width / 2 + 1,
                                    gStoreAreaCenter.Y - storeIcon.Height);
            }
        }

#endregion

    }
}
