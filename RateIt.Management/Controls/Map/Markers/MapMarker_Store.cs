using System.Drawing;
using GMap.NET;
using GMap.NET.WindowsForms;
using RateIt.Common.Classes;
using RateIt.Common.Core.Entities.Stores;
using RateIt.Management.Helpers;

namespace RateIt.Management.Controls.Map.Markers
{
    public class MapMarker_Store : GMapMarker
    {
        private static readonly Pen _pen = new Pen(Brushes.Blue, 1);

        private Store _store;
        private readonly MapViewer _map;

        public MapMarker_Store(MapViewer map, Store store) : base(new PointLatLng())
        {
            _map = map;
            UpdatePosition(store);
            IsHitTestVisible = false;
        }

        public void UpdatePosition()
        {
            UpdatePosition(_store);
        }

        public void UpdatePosition(Store store)
        {
            _store = store;
            Position = store.Location.ToPointLatLng();
        }

        public override void OnRender(Graphics g)
        {
            //double gndRes = Overlay.Control.MapProvider.Projection.GetGroundResolution((int)Overlay.Control.Zoom, Position.Lat);
            _map.UpdateMarkerLocalPosition(this);
            
            GeoArea storeArea = _store.Size.ToGeoArea(_store.Location);

            int cnt = storeArea.Points.Count;

            GPoint[] points = new GPoint[cnt];
            for (int i = 0; i < cnt; i++)
            {
                GeoPoint point = storeArea.Points[i];
                points[i] = _map.FromLatLngToLocal(point.ToPointLatLng());
            }

            PointF[] fPoints = new PointF[cnt + 1];
            for (int i = 0; i < cnt; i++)
            {
                fPoints[i] = new PointF(points[i].X, points[i].Y);
            }
            fPoints[cnt] = new PointF(points[0].X, points[0].Y);

            g.DrawLines(_pen, fPoints);
        }
    }
}
