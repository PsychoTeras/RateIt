using System;
using System.Collections.Generic;
using RateIt.Common.Helpers;

namespace RateIt.Common.Core.Classes
{
    [Serializable]
    public sealed class GeoArea
    {

#region Public fields

        public List<GeoPoint> Points;

#endregion

#region Properties

        //!!! Make it fastes AFAP
        public GeoPoint CenterPoint
        {
            get
            {
                switch (Points.Count)
                {
                    case 0:
                        {
                            return null;
                        }
                    case 1:
                        {
                            return Points[0];
                        }
                    case 2:
                        {
                            double lat = (Points[0].Latitude + Points[1].Latitude)/2;
                            double lng = (Points[0].Longitude + Points[1].Longitude)/2;
                            return new GeoPoint(lat, lng);
                        }
                    default:
                        {
                            GeoPoint topLeft, bottomRight;
                            GetCornerPoints(out topLeft, out bottomRight);
                            double lat = (topLeft.Latitude + bottomRight.Latitude) / 2;
                            double lng = (topLeft.Longitude + bottomRight.Longitude) / 2;
                            return new GeoPoint(lat, lng);

                        }
                }
            }
        }

#endregion

#region Ctor

        public GeoArea()
        {
            Points = new List<GeoPoint>();
        }

#endregion

#region Class methods

        private void GetCornerPoints(out GeoPoint topLeft, out GeoPoint bottomRight)
        {
            topLeft = new GeoPoint(Points[0]);
            bottomRight = new GeoPoint(Points[0]);
            foreach (GeoPoint geoPoint in Points)
            {
                //Top-left
                if (geoPoint.Latitude > topLeft.Latitude)
                {
                    topLeft.Latitude = geoPoint.Latitude;
                }
                if (geoPoint.Longitude < topLeft.Longitude)
                {
                    topLeft.Longitude = geoPoint.Longitude;
                }

                //Bottom-right
                if (geoPoint.Latitude < bottomRight.Latitude)
                {
                    bottomRight.Latitude = geoPoint.Latitude;
                }
                if (geoPoint.Longitude > bottomRight.Longitude)
                {
                    bottomRight.Longitude = geoPoint.Longitude;
                }
            }
        }

        public GeoPoint AddPoint(double latitude, double longitude)
        {
            GeoPoint geoPoint = new GeoPoint(latitude, longitude);
            Points.Add(geoPoint);
            return geoPoint;
        }

        public void AddPoint(GeoPoint geoPoint)
        {
            Points.Add(geoPoint);
        }

        public GeoRectangle ToRectangle()
        {
            if (Points.Count < 4)
            {
                throw new Exception("Error while create georectangle: at least 4 geopoints are required");
            }

            GeoPoint topLeft, bottomRight;
            GetCornerPoints(out topLeft, out bottomRight);

            return new GeoRectangle
                (
                    topLeft.Latitude, 
                    topLeft.Longitude,
                    bottomRight.Latitude - topLeft.Latitude,
                    bottomRight.Longitude - topLeft.Longitude
                );
        }

#endregion

#region Static methods

        public static GeoArea Rectangle(GeoPoint atLocation, uint widthInMeters, uint heightInMeters)
        {
            int halfWidth = (int) (widthInMeters/2);
            int halfHeight = (int) (heightInMeters/2);

            GeoArea geoArea = new GeoArea();
            geoArea.AddPoint(GeoHelper.ShiftCoordinates(atLocation, -halfWidth,  halfHeight));
            geoArea.AddPoint(GeoHelper.ShiftCoordinates(atLocation,  halfWidth,  halfHeight));
            geoArea.AddPoint(GeoHelper.ShiftCoordinates(atLocation,  halfWidth, -halfHeight));
            geoArea.AddPoint(GeoHelper.ShiftCoordinates(atLocation, -halfWidth, -halfHeight));

            return geoArea;
        }

        public static GeoArea Rectangle(GeoPoint atLocation, double width, double height)
        {
            double halfWidth = width / 2f;
            double halfHeight = width / 2f;

            GeoArea geoArea = new GeoArea();
            geoArea.AddPoint(atLocation.Latitude + halfHeight, atLocation.Longitude - halfWidth);
            geoArea.AddPoint(atLocation.Latitude + halfHeight, atLocation.Longitude + halfWidth);
            geoArea.AddPoint(atLocation.Latitude - halfHeight, atLocation.Longitude + halfWidth);
            geoArea.AddPoint(atLocation.Latitude - halfHeight, atLocation.Longitude - halfWidth);

            return geoArea;
        }

#endregion

    }
}
