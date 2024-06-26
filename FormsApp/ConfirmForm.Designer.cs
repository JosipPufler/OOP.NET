namespace FormsApp
{
    partial class ConfirmForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfirmForm));
            btnYes = new Button();
            btnNo = new Button();
            lblConfirm = new Label();
            SuspendLayout();
            // 
            // btnYes
            // 
            resources.ApplyResources(btnYes, "btnYes");
            btnYes.Name = "btnYes";
            btnYes.TabStop = false;
            btnYes.UseVisualStyleBackColor = true;
            btnYes.Click += btnYes_Click;
            // 
            // btnNo
            // 
            resources.ApplyResources(btnNo, "btnNo");
            btnNo.Name = "btnNo";
            btnNo.TabStop = false;
            btnNo.UseVisualStyleBackColor = true;
            btnNo.Click += btnNo_Click;
            // 
            // lblConfirm
            // 
            resources.ApplyResources(lblConfirm, "lblConfirm");
            lblConfirm.Name = "lblConfirm";
            // 
            // ConfirmForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblConfirm);
            Controls.Add(btnNo);
            Controls.Add(btnYes);
            Name = "ConfirmForm";
            KeyDown += ConfirmForm_KeyDown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnYes;
        private Button btnNo;
        private Label lblConfirm;
    }
}