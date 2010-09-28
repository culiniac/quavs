namespace QUAVS.GS
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.MainMap = new GMap.NET.WindowsForms.GMapControl();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.labelProviderType = new System.Windows.Forms.Label();
            this.comboBoxProviderType = new System.Windows.Forms.ComboBox();
            this.labelProviderMode = new System.Windows.Forms.Label();
            this.comboBoxProviderMode = new System.Windows.Forms.ComboBox();
            this.labelLat = new System.Windows.Forms.Label();
            this.textBoxLat = new System.Windows.Forms.TextBox();
            this.labelLng = new System.Windows.Forms.Label();
            this.textBoxLng = new System.Windows.Forms.TextBox();
            this.trackBarMapZoom = new System.Windows.Forms.TrackBar();
            this.gMapControl1 = new GMap.NET.WindowsForms.GMapControl();
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
            this.splitContainer1.Panel1.Controls.Add(this.flowLayoutPanel1);
            this.splitContainer1.Panel1.Controls.Add(this.MainMap);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.trackBarMapZoom);
            this.splitContainer1.Size = new System.Drawing.Size(1049, 461);
            this.splitContainer1.SplitterDistance = 738;
            this.splitContainer1.TabIndex = 0;
            // 
            // MainMap
            // 
            this.MainMap.Bearing = 0F;
            this.MainMap.CanDragMap = true;
            this.MainMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainMap.GrayScaleMode = false;
            this.MainMap.LevelsKeepInMemmory = 5;
            this.MainMap.Location = new System.Drawing.Point(0, 0);
            this.MainMap.MapType = GMap.NET.MapType.BingSatellite;
            this.MainMap.MarkersEnabled = true;
            this.MainMap.MaxZoom = 17;
            this.MainMap.MinZoom = 2;
            this.MainMap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.MainMap.Name = "MainMap";
            this.MainMap.PolygonsEnabled = true;
            this.MainMap.RetryLoadTile = 0;
            this.MainMap.RoutesEnabled = true;
            this.MainMap.ShowTileGridLines = false;
            this.MainMap.Size = new System.Drawing.Size(738, 461);
            this.MainMap.TabIndex = 0;
            this.MainMap.VirtualSizeEnabled = false;
            this.MainMap.Zoom = 2D;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.labelProviderType);
            this.flowLayoutPanel1.Controls.Add(this.comboBoxProviderType);
            this.flowLayoutPanel1.Controls.Add(this.labelProviderMode);
            this.flowLayoutPanel1.Controls.Add(this.comboBoxProviderMode);
            this.flowLayoutPanel1.Controls.Add(this.labelLat);
            this.flowLayoutPanel1.Controls.Add(this.textBoxLat);
            this.flowLayoutPanel1.Controls.Add(this.labelLng);
            this.flowLayoutPanel1.Controls.Add(this.textBoxLng);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(218, 183);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(211, 158);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // labelProviderType
            // 
            this.labelProviderType.AutoSize = true;
            this.labelProviderType.Location = new System.Drawing.Point(3, 0);
            this.labelProviderType.Name = "labelProviderType";
            this.labelProviderType.Size = new System.Drawing.Size(73, 13);
            this.labelProviderType.TabIndex = 0;
            this.labelProviderType.Text = "Provider Type";
            // 
            // comboBoxProviderType
            // 
            this.comboBoxProviderType.FormattingEnabled = true;
            this.comboBoxProviderType.Location = new System.Drawing.Point(3, 16);
            this.comboBoxProviderType.Name = "comboBoxProviderType";
            this.comboBoxProviderType.Size = new System.Drawing.Size(205, 21);
            this.comboBoxProviderType.TabIndex = 1;
            this.comboBoxProviderType.DropDownClosed += new System.EventHandler(this.comboBoxProviderType_DropDownClosed);
            // 
            // labelProviderMode
            // 
            this.labelProviderMode.AutoSize = true;
            this.labelProviderMode.Location = new System.Drawing.Point(3, 40);
            this.labelProviderMode.Name = "labelProviderMode";
            this.labelProviderMode.Size = new System.Drawing.Size(76, 13);
            this.labelProviderMode.TabIndex = 2;
            this.labelProviderMode.Text = "Provider Mode";
            // 
            // comboBoxProviderMode
            // 
            this.comboBoxProviderMode.FormattingEnabled = true;
            this.comboBoxProviderMode.Location = new System.Drawing.Point(3, 56);
            this.comboBoxProviderMode.Name = "comboBoxProviderMode";
            this.comboBoxProviderMode.Size = new System.Drawing.Size(205, 21);
            this.comboBoxProviderMode.TabIndex = 3;
            this.comboBoxProviderMode.DropDownClosed += new System.EventHandler(this.comboBoxProviderMode_DropDownClosed);
            // 
            // labelLat
            // 
            this.labelLat.AutoSize = true;
            this.labelLat.Location = new System.Drawing.Point(3, 80);
            this.labelLat.Name = "labelLat";
            this.labelLat.Size = new System.Drawing.Size(45, 13);
            this.labelLat.TabIndex = 4;
            this.labelLat.Text = "Latitude";
            // 
            // textBoxLat
            // 
            this.textBoxLat.Location = new System.Drawing.Point(3, 96);
            this.textBoxLat.Name = "textBoxLat";
            this.textBoxLat.ReadOnly = true;
            this.textBoxLat.Size = new System.Drawing.Size(205, 20);
            this.textBoxLat.TabIndex = 5;
            // 
            // labelLng
            // 
            this.labelLng.AutoSize = true;
            this.labelLng.Location = new System.Drawing.Point(3, 119);
            this.labelLng.Name = "labelLng";
            this.labelLng.Size = new System.Drawing.Size(54, 13);
            this.labelLng.TabIndex = 6;
            this.labelLng.Text = "Longitude";
            // 
            // textBoxLng
            // 
            this.textBoxLng.Location = new System.Drawing.Point(3, 135);
            this.textBoxLng.Name = "textBoxLng";
            this.textBoxLng.ReadOnly = true;
            this.textBoxLng.Size = new System.Drawing.Size(205, 20);
            this.textBoxLng.TabIndex = 7;
            // 
            // trackBarMapZoom
            // 
            this.trackBarMapZoom.Dock = System.Windows.Forms.DockStyle.Left;
            this.trackBarMapZoom.LargeChange = 2;
            this.trackBarMapZoom.Location = new System.Drawing.Point(0, 0);
            this.trackBarMapZoom.Maximum = 17;
            this.trackBarMapZoom.Minimum = 1;
            this.trackBarMapZoom.Name = "trackBarMapZoom";
            this.trackBarMapZoom.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBarMapZoom.Size = new System.Drawing.Size(45, 461);
            this.trackBarMapZoom.TabIndex = 1;
            this.trackBarMapZoom.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBarMapZoom.Value = 1;
            this.trackBarMapZoom.Scroll += new System.EventHandler(this.trackBarMapZoom_Scroll);
            // 
            // gMapControl1
            // 
            this.gMapControl1.Bearing = 0F;
            this.gMapControl1.CanDragMap = true;
            this.gMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gMapControl1.GrayScaleMode = false;
            this.gMapControl1.LevelsKeepInMemmory = 5;
            this.gMapControl1.Location = new System.Drawing.Point(0, 0);
            this.gMapControl1.MarkersEnabled = true;
            this.gMapControl1.MaxZoom = 2;
            this.gMapControl1.MinZoom = 2;
            this.gMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMapControl1.Name = "gMapControl1";
            this.gMapControl1.PolygonsEnabled = true;
            this.gMapControl1.RetryLoadTile = 0;
            this.gMapControl1.RoutesEnabled = true;
            this.gMapControl1.ShowTileGridLines = false;
            this.gMapControl1.Size = new System.Drawing.Size(714, 461);
            this.gMapControl1.TabIndex = 0;
            this.gMapControl1.VirtualSizeEnabled = false;
            this.gMapControl1.Zoom = 0D;
            // 
            // MapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1049, 461);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "MapForm";
            this.Text = "Map";
            this.DockStateChanged += new System.EventHandler(this.MapForm_DockStateChanged);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
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
        private System.Windows.Forms.Label labelProviderType;
        private System.Windows.Forms.ComboBox comboBoxProviderType;
        private System.Windows.Forms.Label labelProviderMode;
        private System.Windows.Forms.ComboBox comboBoxProviderMode;
        private System.Windows.Forms.Label labelLat;
        private System.Windows.Forms.TextBox textBoxLat;
        private System.Windows.Forms.Label labelLng;
        private System.Windows.Forms.TextBox textBoxLng;
        private GMap.NET.WindowsForms.GMapControl gMapControl1;
    }
}