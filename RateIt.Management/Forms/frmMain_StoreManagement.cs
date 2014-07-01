using System;
using System.Collections.Generic;
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
            //Create map overlays
            _mapMainOverlay = new GMapOverlay(map, null);
            map.Overlays.Add(_mapMainOverlay);

            //Show map
            cbMapStyle.SelectedIndex = 0;
            trackMapZoom.Value = 16;
            TbMapZoomScroll(trackMapZoom, null);
            DoMapNavigate(true);

            //Create main marker
            _mapMainMarker = new GMapMarkerCross(map.Position);
            _mapMainOverlay.Markers.Add(_mapMainMarker);
            UpdateMapMarkerTbValues();

            //Initialize store markers
            _mapStoreMarkers = new List<GMapMarker>();
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
