using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using MongoDB.Driver;
using RateIt.Common;
using RateIt.Common.Classes;
using RateIt.Common.Core.Controller;
using RateIt.Common.Core.Entities.Stores;
using RateIt.Common.Core.QueryParams;
using RateIt.Common.Core.QueryResults;
using RateIt.Management.Helpers;
using RateIt.Management.Properties;

namespace RateIt.Management.Forms
{
    public partial class frmMain : Form
    {

#region Constants

        private const string LOG_SEPARATOR = "------------------------------------------------";

#endregion

#region Private members

        private IController _mainController;

#endregion

#region Class methods

        public frmMain()
        {
            InitializeComponent();

            try
            {
                //Begin initialize application
                WriteLog("Initialize application");

                //Start MongoDB
                try
                {
                    InitializeApplication();
                }
                catch (MongoConnectionException)
                {
                    if (!TryToStartMongoDB())
                    {
                        throw;
                    }
                    InitializeApplication();
                }

                //Out initialization information
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0}   ▪ Version:\t{1}",
                    Environment.NewLine,
                    Assembly.GetExecutingAssembly().GetName().Version);
                sb.AppendFormat("{0}   ▪ MongoDB:\thost '{1}', port '{2}'",
                    Environment.NewLine,
                    Configuration.DBHost,
                    Configuration.DBPort);
                WriteLog(string.Format("Application initialized{0}{1}{2}",
                    Environment.NewLine, LOG_SEPARATOR, sb));
                WriteLog(LOG_SEPARATOR, true, false);
            }

            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0}\n\nApplication will closed.", ex.Message), 
                                "Fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Process.GetCurrentProcess().Kill();
            }
        }

        private bool TryToStartMongoDB()
        {
            string mongoDBStartPath = Path.GetFullPath((string) Settings.Default["MongoDBStartPath"]);
            if (File.Exists(mongoDBStartPath))
            {
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.WorkingDirectory = Path.GetDirectoryName(mongoDBStartPath);
                    startInfo.FileName = mongoDBStartPath;
                    startInfo.WindowStyle = ProcessWindowStyle.Minimized;
                    Process.Start(startInfo);
                    return true;
                }
                catch {}
            }
            return false;
        }

        public void InitializeApplication()
        {
            _mainController = new MainController();
            InitializeUserManagement();
            InitializeStoreManagement();
        }

        private void WriteLog(string logMessage)
        {
            WriteLog(logMessage, true);
        }

        private void WriteLog(string logMessage, bool writeLine)
        {
            WriteLog(logMessage, writeLine, true);
        }

        private void WriteLog(string logMessage, bool writeLine, bool writeTime)
        {
            if (writeTime)
            {
                logMessage = string.Format("[{0}] {1}", DateTime.Now, logMessage ?? string.Empty);
            }
            if (writeLine)
            {
                tbOutput.Text += string.Format("{0}{1}", logMessage, Environment.NewLine);
            }
            else
            {
                tbOutput.Text += logMessage;
            }
        }

        private void FrmMainFormClosed(object sender, FormClosedEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

#endregion

#region Map methods

        private void map_OnTileLoaded(GPoint pos, int zoom)
        {
            lock (_onTileLoadedObject)
            {
                GSize tileSize = map.MapProvider.Projection.TileSize;
                GPoint tilePos = map.MapProvider.Projection.FromTileXYToPixel(pos);

                PointLatLng latLngLeftTop = map.MapProvider.Projection.FromPixelToLatLng(tilePos, zoom);
                tilePos.Offset(tileSize.Width, tileSize.Height);
                PointLatLng latLngRightBottom = map.MapProvider.Projection.FromPixelToLatLng(tilePos, zoom);

                GeoRectangle rectangle = new GeoRectangle
                    (
                    latLngLeftTop.Lat,
                    latLngLeftTop.Lng,
                    latLngRightBottom.Lat - latLngLeftTop.Lat,
                    latLngRightBottom.Lng - latLngLeftTop.Lng
                    );
                StoreListQueryResult result = _mainController.GetStoresAtLocationSys(QuerySysRequestID.Instance,
                    rectangle);

                //Something failed
                if (!Helper.CheckOnValidQueryResult(result))
                {
                    return;
                }

                //Place each of the stores on screen
                foreach (Store store in result.StoreList)
                {
                    AddStoreMarker(store, false);
                }
            }
        }

        private void map_OnTileLoadComplete(long ElapsedMilliseconds)
        {
            lock (_onTileLoadedObject)
            {
                //BeginInvoke(new Action(() => map.Refresh()));
            }
        }

        private void mapStores_OnMapZoomChanged()
        {
            trackMapZoom.Value = (int)map.Zoom;
            lblMapZoom.Text = string.Format("Zoom [{0}]:", trackMapZoom.Value);
        }

        private void MapMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _mapIsMouseDown = true;
                SetMapMarkerPosition(e);
            }
        }

        private void MapMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _mapIsMouseDown = false;
            }
        }

        private void MapMouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && _mapIsMouseDown)
            {
                SetMapMarkerPosition(e);
            }
        }

        private void map_OnPositionChanged(PointLatLng point)
        {
            tbMapLatitude.Text = point.Lat.ToString(CultureInfo.InvariantCulture);
            tbMapLongitude.Text = point.Lng.ToString(CultureInfo.InvariantCulture);
        }

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

        private void BtnMapNavigateClick(object sender, EventArgs e)
        {
            if (Visible)
            {
                //Navigate map
                DoMapNavigate();

                //Set main marker position
                _mapMainMarker.Position = map.Position;
                UpdateMapMarkerTbValues();

                //Refresh the map
                map.Refresh();
            }
        }

        private void TbMapZoomScroll(object sender, EventArgs e)
        {
            lblMapZoom.Text = string.Format("Zoom [{0}]:", trackMapZoom.Value);
            DoMapNavigate();
        }

        private void UpdateMapMarkerTbValues()
        {
            tbMapMarkerLatitude.Text = _mapMainMarker.Position.Lat.ToString(CultureInfo.InvariantCulture);
            tbMapMarkerLongitude.Text = _mapMainMarker.Position.Lng.ToString(CultureInfo.InvariantCulture);
        }

        private void SetMapMarkerPosition(MouseEventArgs e)
        {
            _mapMainMarker.Position = map.FromLocalToLatLng(e.X, e.Y);
            UpdateMapMarkerTbValues();
            map.Refresh();
        }

        private void tbMapLatitude_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                BtnMapNavigateClick(null, null);
            }
        }

#endregion

    }
}
