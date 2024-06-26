namespace FormsApp
{
    partial class PlayerPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblPlayerName = new Label();
            lblPosition = new Label();
            lblShirtNumber = new Label();
            pbPlayer = new PictureBox();
            pbStar = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pbPlayer).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbStar).BeginInit();
            SuspendLayout();
            // 
            // lblPlayerName
            // 
            lblPlayerName.AutoSize = true;
            lblPlayerName.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            lblPlayerName.Location = new Point(20, 17);
            lblPlayerName.Name = "lblPlayerName";
            lblPlayerName.Size = new Size(163, 31);
            lblPlayerName.TabIndex = 0;
            lblPlayerName.Text = "lblPlayerName";
            // 
            // lblPosition
            // 
            lblPosition.AutoSize = true;
            lblPosition.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            lblPosition.Location = new Point(20, 106);
            lblPosition.Name = "lblPosition";
            lblPosition.Size = new Size(88, 23);
            lblPosition.TabIndex = 1;
            lblPosition.Text = "lblPosition";
            // 
            // lblShirtNumber
            // 
            lblShirtNumber.AutoSize = true;
            lblShirtNumber.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblShirtNumber.Location = new Point(20, 59);
            lblShirtNumber.Name = "lblShirtNumber";
            lblShirtNumber.Size = new Size(147, 28);
            lblShirtNumber.TabIndex = 2;
            lblShirtNumber.Text = "lblShirtNumber";
            // 
            // pbPlayer
            // 
            pbPlayer.Cursor = Cursors.Hand;
            pbPlayer.Image = Properties.Resources.no_image;
            pbPlayer.Location = new Point(147, 64);
            pbPlayer.Name = "pbPlayer";
            pbPlayer.Size = new Size(128, 128);
            pbPlayer.SizeMode = PictureBoxSizeMode.StretchImage;
            pbPlayer.TabIndex = 3;
            pbPlayer.TabStop = false;
            pbPlayer.Click += pbPlayer_Click;
            // 
            // pbStar
            // 
            pbStar.Image = Properties.Resources.star;
            pbStar.Location = new Point(227, 3);
            pbStar.Name = "pbStar";
            pbStar.Size = new Size(48, 55);
            pbStar.TabIndex = 4;
            pbStar.TabStop = false;
            // 
            // PlayerPanel
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pbStar);
            Controls.Add(pbPlayer);
            Controls.Add(lblShirtNumber);
            Controls.Add(lblPosition);
            Controls.Add(lblPlayerName);
            Name = "PlayerPanel";
            Size = new Size(278, 195);
            ((System.ComponentModel.ISupportInitialize)pbPlayer).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbStar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblPlayerName;
        private Label lblPosition;
        private Label lblShirtNumber;
        private PictureBox pbPlayer;
        private PictureBox pbStar;
    }
}
