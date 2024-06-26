namespace FormsApp
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            lblCategory = new Label();
            lblLanguage = new Label();
            cbLanguage = new ComboBox();
            cbCategory = new ComboBox();
            btnSubmit = new Button();
            SuspendLayout();
            // 
            // lblCategory
            // 
            resources.ApplyResources(lblCategory, "lblCategory");
            lblCategory.Name = "lblCategory";
            // 
            // lblLanguage
            // 
            resources.ApplyResources(lblLanguage, "lblLanguage");
            lblLanguage.Name = "lblLanguage";
            // 
            // cbLanguage
            // 
            cbLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            cbLanguage.FormattingEnabled = true;
            resources.ApplyResources(cbLanguage, "cbLanguage");
            cbLanguage.Name = "cbLanguage";
            // 
            // cbCategory
            // 
            cbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCategory.FormattingEnabled = true;
            resources.ApplyResources(cbCategory, "cbCategory");
            cbCategory.Name = "cbCategory";
            // 
            // btnSubmit
            // 
            resources.ApplyResources(btnSubmit, "btnSubmit");
            btnSubmit.Name = "btnSubmit";
            btnSubmit.UseVisualStyleBackColor = true;
            btnSubmit.Click += BtnSubmit_Click;
            // 
            // Settings
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnSubmit);
            Controls.Add(lblLanguage);
            Controls.Add(cbLanguage);
            Controls.Add(lblCategory);
            Controls.Add(cbCategory);
            Name = "Settings";
            FormClosing += Settings_FormClosing;
            ResumeLayout(false);
        }

        #endregion
        private Label lblCategory;
        private Label lblLanguage;
        private ComboBox cbLanguage;
        private ComboBox cbCategory;
        private Button btnSubmit;
    }
}