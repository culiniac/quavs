namespace QUAVS.GS.Forms
{
    partial class GEForm
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
            this.geStatusStrip1 = new FC.GEPluginCtrls.GEStatusStrip();
            this.geToolStrip1 = new FC.GEPluginCtrls.GEToolStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.geWebBrowser1 = new FC.GEPluginCtrls.GEWebBrowser();
            this.kmlTreeView1 = new FC.GEPluginCtrls.KmlTreeView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // geStatusStrip1
            // 
            this.geStatusStrip1.Enabled = false;
            this.geStatusStrip1.Location = new System.Drawing.Point(0, 483);
            this.geStatusStrip1.Name = "geStatusStrip1";
            this.geStatusStrip1.Size = new System.Drawing.Size(611, 22);
            this.geStatusStrip1.TabIndex = 0;
            this.geStatusStrip1.Text = "geStatusStrip1";
            // 
            // geToolStrip1
            // 
            this.geToolStrip1.Enabled = false;
            this.geToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.geToolStrip1.Name = "geToolStrip1";
            this.geToolStrip1.Size = new System.Drawing.Size(611, 27);
            this.geToolStrip1.TabIndex = 1;
            this.geToolStrip1.Text = "geToolStrip1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 27);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.kmlTreeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.geWebBrowser1);
            this.splitContainer1.Size = new System.Drawing.Size(611, 456);
            this.splitContainer1.SplitterDistance = 203;
            this.splitContainer1.TabIndex = 2;
            // 
            // geWebBrowser1
            // 
            this.geWebBrowser1.AllowNavigation = false;
            this.geWebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.geWebBrowser1.IsWebBrowserContextMenuEnabled = false;
            this.geWebBrowser1.Location = new System.Drawing.Point(0, 0);
            this.geWebBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.geWebBrowser1.Name = "geWebBrowser1";
            this.geWebBrowser1.ScrollBarsEnabled = false;
            this.geWebBrowser1.Size = new System.Drawing.Size(404, 456);
            this.geWebBrowser1.TabIndex = 0;
            this.geWebBrowser1.WebBrowserShortcutsEnabled = false;
            // 
            // kmlTreeView1
            // 
            this.kmlTreeView1.BalloonMinimumHeight = 200;
            this.kmlTreeView1.BalloonMinimumWidth = 200;
            this.kmlTreeView1.CheckBoxes = true;
            this.kmlTreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kmlTreeView1.ImageIndex = 0;
            this.kmlTreeView1.Location = new System.Drawing.Point(0, 0);
            this.kmlTreeView1.Name = "kmlTreeView1";
            this.kmlTreeView1.SelectedImageIndex = 0;
            this.kmlTreeView1.ShowLines = false;
            this.kmlTreeView1.ShowNodeToolTips = true;
            this.kmlTreeView1.Size = new System.Drawing.Size(203, 456);
            this.kmlTreeView1.TabIndex = 0;
            // 
            // GEForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 505);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.geToolStrip1);
            this.Controls.Add(this.geStatusStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "GEForm";
            this.Text = "GEForm";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FC.GEPluginCtrls.GEStatusStrip geStatusStrip1;
        private FC.GEPluginCtrls.GEToolStrip geToolStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private FC.GEPluginCtrls.KmlTreeView kmlTreeView1;
        private FC.GEPluginCtrls.GEWebBrowser geWebBrowser1;
    }
}