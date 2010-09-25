namespace QUAVS.Base
{
    partial class VideoControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelVideo = new System.Windows.Forms.Panel();
            this.labelQUAVS = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // panelVideo
            // 
            this.panelVideo.BackColor = System.Drawing.Color.Black;
            this.panelVideo.Location = new System.Drawing.Point(166, 125);
            this.panelVideo.Name = "panelVideo";
            this.panelVideo.Size = new System.Drawing.Size(446, 321);
            this.panelVideo.TabIndex = 0;
            // 
            // labelQUAVS
            // 
            this.labelQUAVS.AutoSize = true;
            this.labelQUAVS.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelQUAVS.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelQUAVS.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelQUAVS.Location = new System.Drawing.Point(0, 581);
            this.labelQUAVS.Name = "labelQUAVS";
            this.labelQUAVS.Size = new System.Drawing.Size(74, 13);
            this.labelQUAVS.TabIndex = 1;
            this.labelQUAVS.Text = "QUAVS Video";
            // 
            // VideoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Controls.Add(this.labelQUAVS);
            this.Controls.Add(this.panelVideo);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "VideoControl";
            this.Size = new System.Drawing.Size(783, 594);
            this.Resize += new System.EventHandler(this.VideoControl_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelVideo;
        private System.Windows.Forms.Label labelQUAVS;
    }
}
