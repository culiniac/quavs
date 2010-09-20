namespace QUAVS.GroundStation
{
    partial class MapForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.MainMap = new GMap.NET.WindowsForms.GMapControl();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.labelLat = new System.Windows.Forms.Label();
            this.textBoxLat = new System.Windows.Forms.TextBox();
            this.labelLng = new System.Windows.Forms.Label();
            this.textBoxLng = new System.Windows.Forms.TextBox();
            this.trackBarMapZoom = new System.Windows.Forms.TrackBar();
            this.labelProviderType = new System.Windows.Forms.Label();
            this.comboBoxProviderType = new System.Windows.Forms.ComboBox();
            this.comboBoxProviderMode = new System.Windows.Forms.ComboBox();
            this.labelProviderMode = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMapZoom)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.MainMap);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.flowLayoutPanel1);
            this.splitContainer1.Panel2.Controls.Add(this.trackBarMapZoom);
            this.splitContainer1.Size = new System.Drawing.Size(1303, 602);
            this.splitContainer1.SplitterDistance = 945;
            this.splitContainer1.TabIndex = 0;
            // 
            // MainMap
            // 
            this.MainMap.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.MainMap.CanDragMap = true;
            this.MainMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainMap.GrayScaleMode = false;
            this.MainMap.LevelsKeepInMemmory = 5;
            this.MainMap.Location = new System.Drawing.Point(0, 0);
            this.MainMap.MapType = GMap.NET.MapType.GoogleTerrain;
            this.MainMap.MarkersEnabled = true;
            this.MainMap.MaxZoom = 17;
            this.MainMap.MinZoom = 1;
            this.MainMap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.MainMap.Name = "MainMap";
            this.MainMap.PolygonsEnabled = true;
            this.MainMap.RetryLoadTile = 0;
            this.MainMap.RoutesEnabled = true;
            this.MainMap.ShowTileGridLines = false;
            this.MainMap.Size = new System.Drawing.Size(945, 602);
            this.MainMap.TabIndex = 0;
            this.MainMap.Zoom = 4D;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.labelProviderType);
            this.flowLayoutPanel1.Controls.Add(this.comboBoxProviderType);
            this.flowLayoutPanel1.Controls.Add(this.labelProviderMode);
            this.flowLayoutPanel1.Controls.Add(this.comboBoxProviderMode);
            this.flowLayoutPanel1.Controls.Add(this.labelLat);
            this.flowLayoutPanel1.Controls.Add(this.textBoxLat);
            this.flowLayoutPanel1.Controls.Add(this.labelLng);
            this.flowLayoutPanel1.Controls.Add(this.textBoxLng);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(45, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(309, 602);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // labelLat
            // 
            this.labelLat.AutoSize = true;
            this.labelLat.Location = new System.Drawing.Point(3, 80);
            this.labelLat.Name = "labelLat";
            this.labelLat.Size = new System.Drawing.Size(45, 13);
            this.labelLat.TabIndex = 0;
            this.labelLat.Text = "Latitude";
            // 
            // textBoxLat
            // 
            this.textBoxLat.Location = new System.Drawing.Point(3, 96);
            this.textBoxLat.Name = "textBoxLat";
            this.textBoxLat.ReadOnly = true;
            this.textBoxLat.Size = new System.Drawing.Size(220, 20);
            this.textBoxLat.TabIndex = 1;
            // 
            // labelLng
            // 
            this.labelLng.AutoSize = true;
            this.labelLng.Location = new System.Drawing.Point(3, 119);
            this.labelLng.Name = "labelLng";
            this.labelLng.Size = new System.Drawing.Size(54, 13);
            this.labelLng.TabIndex = 2;
            this.labelLng.Text = "Longitude";
            // 
            // textBoxLng
            // 
            this.textBoxLng.Location = new System.Drawing.Point(3, 135);
            this.textBoxLng.Name = "textBoxLng";
            this.textBoxLng.ReadOnly = true;
            this.textBoxLng.Size = new System.Drawing.Size(220, 20);
            this.textBoxLng.TabIndex = 3;
            // 
            // trackBarMapZoom
            // 
            this.trackBarMapZoom.Dock = System.Windows.Forms.DockStyle.Left;
            this.trackBarMapZoom.Location = new System.Drawing.Point(0, 0);
            this.trackBarMapZoom.Maximum = 17;
            this.trackBarMapZoom.Minimum = 1;
            this.trackBarMapZoom.Name = "trackBarMapZoom";
            this.trackBarMapZoom.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBarMapZoom.Size = new System.Drawing.Size(45, 602);
            this.trackBarMapZoom.TabIndex = 0;
            this.trackBarMapZoom.Value = 1;
            this.trackBarMapZoom.Scroll += new System.EventHandler(this.trackBarMapZoom_Scroll);
            // 
            // labelProviderType
            // 
            this.labelProviderType.AutoSize = true;
            this.labelProviderType.Location = new System.Drawing.Point(3, 0);
            this.labelProviderType.Name = "labelProviderType";
            this.labelProviderType.Size = new System.Drawing.Size(73, 13);
            this.labelProviderType.TabIndex = 4;
            this.labelProviderType.Text = "Provider Type";
            // 
            // comboBoxProviderType
            // 
            this.comboBoxProviderType.FormattingEnabled = true;
            this.comboBoxProviderType.Location = new System.Drawing.Point(3, 16);
            this.comboBoxProviderType.Name = "comboBoxProviderType";
            this.comboBoxProviderType.Size = new System.Drawing.Size(220, 21);
            this.comboBoxProviderType.TabIndex = 5;
            this.comboBoxProviderType.DropDownClosed += new System.EventHandler(this.comboBoxProviderType_DropDownClosed);
            // 
            // comboBoxProviderMode
            // 
            this.comboBoxProviderMode.FormattingEnabled = true;
            this.comboBoxProviderMode.Location = new System.Drawing.Point(3, 56);
            this.comboBoxProviderMode.Name = "comboBoxProviderMode";
            this.comboBoxProviderMode.Size = new System.Drawing.Size(220, 21);
            this.comboBoxProviderMode.TabIndex = 6;
            this.comboBoxProviderMode.DropDownClosed += new System.EventHandler(this.comboBoxProviderMode_DropDownClosed);
            // 
            // labelProviderMode
            // 
            this.labelProviderMode.AutoSize = true;
            this.labelProviderMode.Location = new System.Drawing.Point(3, 40);
            this.labelProviderMode.Name = "labelProviderMode";
            this.labelProviderMode.Size = new System.Drawing.Size(76, 13);
            this.labelProviderMode.TabIndex = 7;
            this.labelProviderMode.Text = "Provider Mode";
            // 
            // MapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1303, 602);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MapForm";
            this.Text = "MapForm";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMapZoom)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private GMap.NET.WindowsForms.GMapControl MainMap;
        private System.Windows.Forms.TrackBar trackBarMapZoom;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label labelLat;
        private System.Windows.Forms.TextBox textBoxLat;
        private System.Windows.Forms.Label labelLng;
        private System.Windows.Forms.TextBox textBoxLng;
        private System.Windows.Forms.Label labelProviderType;
        private System.Windows.Forms.ComboBox comboBoxProviderType;
        private System.Windows.Forms.Label labelProviderMode;
        private System.Windows.Forms.ComboBox comboBoxProviderMode;
    }
}