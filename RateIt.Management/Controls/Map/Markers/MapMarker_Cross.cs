using System.Drawing;
using System.IO;
using System.Reflection;
using GMap.NET;
using GMap.NET.WindowsForms;

namespace RateIt.Management.Controls.Map.Markers
{
    public class MapMarker_Cross : GMapMarker
    {

#region Static private fields

        private static readonly Bitmap _markerIcon;

#endregion

#region Static ctor

        static MapMarker_Cross()
        {
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

        public MapMarker_Cross(PointLatLng p) 
            : base(p)
        {
            IsHitTestVisible = false;
        }

#endregion

#region Class methods

        public override void OnRender(Graphics g)
        {
            g.DrawImageUnscaled(_markerIcon,
                                LocalPosition.X - _markerIcon.Width/2,
                                LocalPosition.Y - _markerIcon.Height/2);
        }

#endregion

    }
}
