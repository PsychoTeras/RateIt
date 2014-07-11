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

        private static readonly Pen _penNormal;
        private static readonly Pen _penSelected;

        private static readonly Brush _brushNormal;
        private static readonly Brush _brushSelected;

        private static readonly Bitmap _iconBigNormal;
        private static readonly Bitmap _iconSmallNormal;

        private static readonly Bitmap _iconBigSelected;
        private static readonly Bitmap _iconSmallSelected;

        private static readonly GraphicsPath _graphicsPath;

#endregion

#region Private fields

        private readonly MapViewer _map;
        private bool _selected;

#endregion

#region Properties

        public Store Store { get; private set; }

        public bool Selected
        {
            get { return _selected; }
            set
            {
                if (_selected != value)
                {
                    _selected = value;
                    _map.Refresh();
                }
            }
        }

#endregion

#region Static ctor

        static MapMarker_Store()
        {
            _graphicsPath = new GraphicsPath();

            _penNormal = new Pen(Brushes.DarkGreen, 1);
            _brushNormal = new SolidBrush(Color.FromArgb(50, Color.ForestGreen));

            _penSelected = new Pen(Brushes.DarkOrange, 1);
            _brushSelected = new SolidBrush(Color.FromArgb(50, Color.Orange));

            string imageResName = "RateIt.Management.Resources.marker_shop_big_normal.png";
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(imageResName))
            {
                if (stream != null)
                {
                    _iconBigNormal = new Bitmap(stream);
                }
            }
            imageResName = "RateIt.Management.Resources.marker_shop_small_normal.png";
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(imageResName))
            {
                if (stream != null)
                {
                    _iconSmallNormal = new Bitmap(stream);
                }
            }

            imageResName = "RateIt.Management.Resources.marker_shop_big_selected.png";
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(imageResName))
            {
                if (stream != null)
                {
                    _iconBigSelected = new Bitmap(stream);
                }
            }
            imageResName = "RateIt.Management.Resources.marker_shop_small_selected.png";
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(imageResName))
            {
                if (stream != null)
                {
                    _iconSmallSelected = new Bitmap(stream);
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

                //Get actual drawing style
                Pen pen = _selected ? _penSelected : _penNormal;
                Brush brush = _selected ? _brushSelected : _brushNormal;
                Bitmap iconBig = _selected ? _iconBigSelected : _iconBigNormal;
                Bitmap iconSmall = _selected ? _iconSmallSelected : _iconSmallNormal;

                //Draw area rectangle
                _graphicsPath.Reset();
                _graphicsPath.AddLines(fPoints);
                g.FillPath(brush, _graphicsPath);
                _graphicsPath.CloseFigure();
                g.DrawPath(pen, _graphicsPath);

                //Draw store icon
                Bitmap storeIcon = _map.Zoom > 16
                                       ? iconBig
                                       : iconSmall;
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
