using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using RateIt.Common.Classes;
using RateIt.Common.Core.Entities.Stores;
using RateIt.Management.Controls.Map.Markers;
using RateIt.Management.Helpers;

namespace RateIt.Management.Forms
{
    partial class frmMain
    {

#region Private members

        private GMapOverlay _mapMainOverlay;
        private GMapMarker _mapMainMarker;
        private List<GMapMarker> _mapStoreMarkers;

        private bool _mapIsMouseDown;

#endregion

#region Store management methods

        private void InitializeStoreManagement()
        {
            //Show map
            cbMapStyle.SelectedIndex = 0;
            trackMapZoom.Value = 16;
            TbMapZoomScroll(trackMapZoom, null);
            DoMapNavigate(true);

            //Create map overlay
            _mapMainOverlay = new GMapOverlay(map, null);

            //Create main marker
            _mapMainMarker = new GMapMarkerCross(map.Position);
            _mapMainOverlay.Markers.Add(_mapMainMarker);
            UpdateMapMarkerTbValues();

            //Initialize map store markers
            _mapStoreMarkers = new List<GMapMarker>();

            //Initialize map
            map.Overlays.Add(_mapMainOverlay);
        }
        
        private void DoMapNavigate(bool force = false)
        {
            if (Visible || force)
            {
                double latitude, longtitude;
                if (double.TryParse(tbMapLatitude.Text.Trim(), out latitude) &&
                    double.TryParse(tbMapLongtitude.Text.Trim(), out longtitude))
                {
                    switch (cbMapStyle.SelectedIndex)
                    {
                        case 0:
                        {
                            map.MapProvider = GMapProviders.YandexMap;
                            break;
                        }
                    }
                    map.Position = new PointLatLng(latitude, longtitude);
                    map.MinZoom = trackMapZoom.Minimum;
                    map.MaxZoom = trackMapZoom.Maximum;
                    map.Zoom = trackMapZoom.Value;
                }
            }
        }

        private void BtnMapNavigateClick(object sender, EventArgs e)
        {
            DoMapNavigate();
        }

        private void TbMapZoomScroll(object sender, EventArgs e)
        {
            lblMapZoom.Text = string.Format("Zoom [{0}]:", trackMapZoom.Value);
            DoMapNavigate();
        }

        private void UpdateMapMarkerTbValues()
        {
            tbMapMarkerLatitude.Text = _mapMainMarker.Position.Lat.ToString(CultureInfo.InvariantCulture);
            tbMapMarkerLongtitude.Text = _mapMainMarker.Position.Lng.ToString(CultureInfo.InvariantCulture);
        }

        private void SetMapMarkerPosition(MouseEventArgs e)
        {
            _mapMainMarker.Position = map.FromLocalToLatLng(e.X, e.Y);
            UpdateMapMarkerTbValues();
            map.Refresh();
        }

        private void BtnStoreRegisterClick(object sender, EventArgs e)
        {
            GeoPoint atLocation = _mapMainMarker.Position.ToGeoPoint();
            Store store = frmStoreRegister.DoRegister(_mainController, atLocation);
            if (store != null)
            {
                AddStoreMarker(store, true);
            }
        }

#endregion

#region Store markers methods

        private void AddStoreMarker(Store store, bool refresh)
        {
            if (store != null)
            {
                MapMarker_Store marker = new MapMarker_Store(map, store);
                _mapMainOverlay.Markers.Add(marker);
                if (refresh)
                {
                    map.Refresh();
                }
            }
        }

#endregion

    }
}
