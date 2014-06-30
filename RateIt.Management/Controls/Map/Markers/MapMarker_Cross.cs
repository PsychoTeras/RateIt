using System.Drawing;
using GMap.NET;
using GMap.NET.WindowsForms;

namespace RateIt.Management.Controls.Map.Markers
{
    public class MapMarker_Cross : GMapMarker
    {
        private static readonly Pen _pen = new Pen(Brushes.Red, 1);

        public MapMarker_Cross(PointLatLng p) 
            : base(p)
        {
            IsHitTestVisible = false;
        }

        public override void OnRender(Graphics g)
        {
            Point p1 = new Point(LocalPosition.X, LocalPosition.Y);
            p1.Offset(0, -10);
            Point p2 = new Point(LocalPosition.X, LocalPosition.Y);
            p2.Offset(0, 10);

            Point p3 = new Point(LocalPosition.X, LocalPosition.Y);
            p3.Offset(-10, 0);
            Point p4 = new Point(LocalPosition.X, LocalPosition.Y);
            p4.Offset(10, 0);

            g.DrawLine(_pen, p1.X, p1.Y, p2.X, p2.Y);
            g.DrawLine(_pen, p3.X, p3.Y, p4.X, p4.Y);
        }
    }
}
