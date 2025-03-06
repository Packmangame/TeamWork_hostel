namespace WindowsFormsApp1
{
    partial class Caretaker
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose (bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose( );
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ()
        {
            this.CardsPanel = new System.Windows.Forms.Panel();
            this.panelBuilders = new System.Windows.Forms.Panel();
            this.panelWashers = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // CardsPanel
            // 
            this.CardsPanel.Location = new System.Drawing.Point(12, 0);
            this.CardsPanel.Name = "CardsPanel";
            this.CardsPanel.Size = new System.Drawing.Size(364, 623);
            this.CardsPanel.TabIndex = 0;
            // 
            // panelBuilders
            // 
            this.panelBuilders.AutoScroll = true;
            this.panelBuilders.Location = new System.Drawing.Point(414, 339);
            this.panelBuilders.Name = "panelBuilders";
            this.panelBuilders.Size = new System.Drawing.Size(771, 284);
            this.panelBuilders.TabIndex = 1;
            // 
            // panelWashers
            // 
            this.panelWashers.AutoScroll = true;
            this.panelWashers.Location = new System.Drawing.Point(414, 12);
            this.panelWashers.Name = "panelWashers";
            this.panelWashers.Size = new System.Drawing.Size(771, 286);
            this.panelWashers.TabIndex = 2;
            // 
            // Caretaker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(253)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1206, 635);
            this.Controls.Add(this.panelBuilders);
            this.Controls.Add(this.panelWashers);
            this.Controls.Add(this.CardsPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Caretaker";
            this.Text = "Form2";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Caretaker_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel CardsPanel;
        private System.Windows.Forms.Panel panelBuilders;
        private System.Windows.Forms.Panel panelWashers;
    }
}