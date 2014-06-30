using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using GMap.NET;
using RateIt.Common;
using RateIt.Common.Core.Controller;

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
                InitializeApplication();

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
            tbMapLongtitude.Text = point.Lng.ToString(CultureInfo.InvariantCulture);
        }

#endregion

    }
}
