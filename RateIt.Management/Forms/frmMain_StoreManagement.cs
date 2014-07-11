using System;
using System.Collections.Generic;
using System.Drawing;
using GMap.NET;
using GMap.NET.WindowsForms;
using RateIt.Common.Core.Classes;
using RateIt.Common.Core.Entities.Stores;
using RateIt.Management.Controls.Map.Markers;
using RateIt.Management.Helpers;

namespace RateIt.Management.Forms
{
    partial class frmMain
    {

#region Private members

        private object _onTileLoadedObject;

        private GMapOverlay _mapMainOverlay;
        private GMapMarker _mapMainMarker;

        private GMapOverlay _mapStoreOverlay;
        private HashSet<int> _storesOnScreen;
        private HashSet<PointLatLng> _processedTiles;

        private bool _mapIsMouseDown;

#endregion

#region Store management methods

        private void InitializeStoreManagement()
        {
            //Base initialization
            _onTileLoadedObject = new object();

            //Show map
            cbMapStyle.SelectedIndex = 0;
            trackMapZoom.Value = 16;
            TbMapZoomScroll(trackMapZoom, null);
            DoMapNavigate(true);

            //Create map overlays
            _mapStoreOverlay = new GMapOverlay(map, null);
            map.Overlays.Add(_mapStoreOverlay);
            _mapMainOverlay = new GMapOverlay(map, null);
            map.Overlays.Add(_mapMainOverlay);

            //Initialize main marker
            _mapMainMarker = new MapMarker_Cross(map, map.Position);
            _mapMainOverlay.Markers.Add(_mapMainMarker);
            UpdateMapMarkerTbValues();

            //Initialize store markers
            _storesOnScreen = new HashSet<int>();
            _processedTiles = new HashSet<PointLatLng>();

            //Initialize GUI controls
            btnStoresFindAllInL1.BackColor = Color.FromArgb(50, MapMarker_Cross.AreaLevel1Color);
            btnStoresFindAllInL2.BackColor = Color.FromArgb(50, MapMarker_Cross.AreaLevel2Color);
            btnStoresFindAllInL3.BackColor = Color.FromArgb(50, MapMarker_Cross.AreaLevel3Color);
        }

        private void BtnStoreRegisterClick(object sender, EventArgs e)
        {
            GeoPoint atLocation = _mapMainMarker.Position.ToGeoPoint();
            Store store = frmStoreRegister.DoRegister(_mainController, _loggedUserSession, atLocation);
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
                int storeHash = store.GetHashCode();
                if (!_storesOnScreen.Contains(storeHash))
                {
                    _storesOnScreen.Add(storeHash);
                    MapMarker_Store marker = new MapMarker_Store(map, store);
                    _mapStoreOverlay.Markers.Add(marker);
                    if (refresh)
                    {
                        map.Refresh();
                    }
                }
            }
        }

#endregion

    }
}
