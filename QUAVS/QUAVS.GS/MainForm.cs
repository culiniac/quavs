using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using WeifenLuo.WinFormsUI.Docking;

namespace QUAVS.GS
{
    public partial class MainForm : Form
    {

        private bool _bSaveLayout = true;
        private DeserializeDockContent _deserializeDockContent;

        private VideoForm _videoForm = new VideoForm();
        private MapForm _mapForm = new MapForm();
        private SettingsForm _settingsForm = new SettingsForm();
        private OutputForm _outputForm = new OutputForm();

        public MainForm()
        {
            InitializeComponent();
            _deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");

            if (File.Exists(configFile))
                DockingPanel.LoadFromXml(configFile, _deserializeDockContent);
        }

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
        /// Gets the content from persist string. !!! Needs update anytime we add another form to the project !!!
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
                _videoForm = new VideoForm();
            _videoForm.Show(DockingPanel, DockState.Document);
            if (_mapForm.IsDisposed == true)
                _mapForm = new MapForm();
            _mapForm.Show(DockingPanel, DockState.DockRight);
            if (_settingsForm.IsDisposed == true)
                _settingsForm = new SettingsForm();
            _settingsForm.Show(DockingPanel, DockState.DockLeft);
            if (_outputForm.IsDisposed == true)
                _outputForm = new OutputForm();
            _outputForm.Show(DockingPanel, DockState.DockLeft);
            
            //save current layout
            _deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);

            DockingPanel.ResumeLayout(true, true);
        }
    }
}
