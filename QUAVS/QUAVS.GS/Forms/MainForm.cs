using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using WeifenLuo.WinFormsUI.Docking;
using QUAVS.Base;
using System.IO;
using System.IO.Ports;


namespace QUAVS.GS
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MainForm : Form
    {
        //static private TelemetryDataObject _telemetryDataObject = new TelemetryDataObject();

        static private TelemetryComms _telemetryComms = new TelemetryComms("COM3", 57600, Parity.None, 8, StopBits.One);

        private bool _bSaveLayout = true;
        private DeserializeDockContent _deserializeDockContent;

        static private VideoForm _videoForm;
        static private MapForm _mapForm;
        static private SettingsForm _settingsForm;
        static private OutputForm _outputForm;
        static private TelemetryForm _telemetryForm;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            _telemetryComms.InitializeComPort();
            _videoForm = new VideoForm(_telemetryComms.Data);
            _mapForm = new MapForm();
            _settingsForm = new SettingsForm();
            _outputForm = new OutputForm();
            _telemetryForm = new TelemetryForm(_telemetryComms.Data);

            _deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
        }

        /// <summary>
        /// Handles the Load event of the MainForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            toolStripEndSessionButton.Enabled = false;
            toolStripNewSessionButton.Enabled = true;
            
            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");
            if (File.Exists(configFile))
                DockingPanel.LoadFromXml(configFile, _deserializeDockContent);
        }

        /// <summary>
        /// Handles the FormClosing event of the MainForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");
            if (_bSaveLayout)
                DockingPanel.SaveAsXml(configFile);
            else if (File.Exists(configFile))
                File.Delete(configFile);

            QUAVS.GS.Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Gets the content from persist string. !!! Needs update anytime I add another form to the project !!!
        /// </summary>
        /// <param name="persistString">The persist string.</param>
        /// <returns></returns>
        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(VideoForm).ToString())
                return _videoForm;
            else if (persistString == typeof(MapForm).ToString())
                return _mapForm;
            else if (persistString == typeof(SettingsForm).ToString())
                return _settingsForm;
            else if (persistString == typeof(OutputForm).ToString())
                return _outputForm;
            else if (persistString == typeof(TelemetryForm).ToString())
                return _telemetryForm;
            else
                return null;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void standardLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DockingPanel.SuspendLayout(true);

            //check for null forms - Not working - figure out the disposal of forms.
            if (_videoForm.IsDisposed == true)
                _videoForm = new VideoForm(_telemetryComms.Data);
            _videoForm.Show(DockingPanel, DockState.Document);
            if (_mapForm.IsDisposed == true)
                _mapForm = new MapForm();
            _mapForm.Show(DockingPanel, DockState.DockRight);
            if (_settingsForm.IsDisposed == true)
                _settingsForm = new SettingsForm();
            _settingsForm.Show(DockingPanel, DockState.DockLeft);
            if (_outputForm.IsDisposed == true)
                _outputForm = new OutputForm();
            _outputForm.Show(DockingPanel, DockState.DockBottom);
            if (_telemetryForm.IsDisposed == true)
                _telemetryForm = new TelemetryForm(_telemetryComms.Data);
            _telemetryForm.Show(DockingPanel, DockState.Float);
            
            //save current layout
            _deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);

            DockingPanel.ResumeLayout(true, true);
        }

        private void NewSession()
        {
            if (_videoForm.IsDisposed != true)
            {
                _videoForm.VideoFeed.Record();
            }

            toolStripEndSessionButton.Enabled = true;
            toolStripNewSessionButton.Enabled = false;
        }

        private void EndSession()
        {
            if (_videoForm.IsDisposed != true)
            {
                _videoForm.VideoFeed.Stop();
            }

            toolStripEndSessionButton.Enabled = false;
            toolStripNewSessionButton.Enabled = true;
        }

        private void toolStripNewSessionButton_Click(object sender, EventArgs e)
        {
            NewSession();
        }

        private void toolStripEndSessionButton_Click(object sender, EventArgs e)
        {
            EndSession();
        }
    }
}
