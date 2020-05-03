namespace PlaylistEditor
{
    partial class CmdOutput
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
            this.textbox_cmdout = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textbox_cmdout
            // 
            this.textbox_cmdout.Location = new System.Drawing.Point(0, 0);
            this.textbox_cmdout.Multiline = true;
            this.textbox_cmdout.Name = "textbox_cmdout";
            this.textbox_cmdout.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textbox_cmdout.Size = new System.Drawing.Size(803, 375);
            this.textbox_cmdout.TabIndex = 0;
            this.textbox_cmdout.TextChanged += new System.EventHandler(this.textbox_cmdout_TextChanged);
            // 
            // CmdOutput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 376);
            this.Controls.Add(this.textbox_cmdout);
            this.Name = "CmdOutput";
            this.Text = "youtube-dl Output";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox textbox_cmdout;
    }
}