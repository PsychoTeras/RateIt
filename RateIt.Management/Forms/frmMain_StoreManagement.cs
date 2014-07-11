using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
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

        private Store _selectedStore;
        private MapMarker_Store _selectedStoreMarker;

#endregion

#region Store methods

        private void PrintStoresInfo(Store[] stores, GeoPoint atLocation)
        {
            StringBuilder sb = new StringBuilder();
            if (stores.Length == 0)
            {
                sb.AppendFormat("There are no stores found at location {0}", atLocation);
            }
            else
            {
                sb.AppendFormat("There are {0} store(s) found at location {1}:", stores.Length, atLocation);
                for (int i = 0; i < stores.Length; i++)
                {
                    sb.AppendFormat("{0}   {1}. {2}", Environment.NewLine, i + 1, stores[i]);
                }
            }
            WriteLog(sb.ToString());
        }

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

            //Initialize other controls
            UpdateSelectedStoreActionControls();
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

        private void UpdateSelectedStoreActionControls()
        {
            btnSelectedStoreEdit.Enabled = btnSelectedStoreDelete.Enabled =
                _selectedStore != null;
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

#region Map methods

        private void DoMapNavigate(bool force = false)
        {
            if (Visible || force)
            {
                //Parse LatLng from debug string
                if (tbMapLatitude.Text.Contains("x"))
                {
                    string[] latLng = tbMapLatitude.Text.Split(new[] { "x" }, StringSplitOptions.None);
                    tbMapLatitude.Text = latLng[0].Replace("{", "").Trim();
                    tbMapLongitude.Text = latLng[1].Replace("}", "").Trim();
                }
                //or load as is
                else
                {
                    tbMapLatitude.Text = tbMapLatitude.Text.Trim();
                    tbMapLongitude.Text = tbMapLongitude.Text.Trim();
                }

                double latitude, longitude;
                if (double.TryParse(tbMapLatitude.Text, out latitude) &&
                    double.TryParse(tbMapLongitude.Text, out longitude))
                {
                    //Apply map style
                    switch (cbMapStyle.SelectedIndex)
                    {
                        case 0:
                            {
                                map.MapProvider = GMapProviders.GoogleMap;
                                break;
                            }
                    }

                    //Set map coordinates
                    map.Position = new PointLatLng(latitude, longitude);
                    map.MinZoom = trackMapZoom.Minimum;
                    map.MaxZoom = trackMapZoom.Maximum;
                    map.Zoom = trackMapZoom.Value;
                }
            }
        }

        private void UpdateMapMarkerTbValues()
        {
            tbMapMarkerLatitude.Text = _mapMainMarker.Position.Lat.ToString(CultureInfo.InvariantCulture);
            tbMapMarkerLongitude.Text = _mapMainMarker.Position.Lng.ToString(CultureInfo.InvariantCulture);
        }

        private void SetMapMainMarkerPosition(MouseEventArgs e)
        {
            _mapMainMarker.Position = map.FromLocalToLatLng(e.X, e.Y);
            UpdateMapMarkerTbValues();
            map.Refresh();
        }

#endregion

    }
}
