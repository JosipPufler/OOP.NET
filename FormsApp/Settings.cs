using DataLayer.Models;
using DataLayer.Utilities;

namespace FormsApp
{
    public partial class Settings : Form
    {
        private ConfirmForm childForm = null;
        UserSettings inheritedSettings = null;
        MainForm parentForm = null;
        public Settings()
        {
            InitializeComponent();
            InitializeComboBoxes();
        }

        public Settings(MainForm mainForm)
        {
            InitializeComponent();
            InitializeComboBoxes();
            cbCategory.SelectedItem = ConfigService.UserSettings.categoryName;
            cbLanguage.SelectedItem = ConfigService.UserSettings.languageName;
            inheritedSettings = ConfigService.UserSettings;
            parentForm = mainForm;
        }

        private void InitializeComboBoxes()
        {
            cbCategory.Items.AddRange(Enum.GetNames(typeof(Categories)));
            cbLanguage.Items.AddRange(Enum.GetNames(typeof(Languages)));
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (cbCategory.SelectedIndex == -1 || cbLanguage.SelectedIndex == -1)
            {
                MessageBox.Show("Sve vrijednosti trebaju biti odabrane",
                                "Nemere",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            else if (inheritedSettings != null
                && (inheritedSettings.language != (Languages)cbLanguage.SelectedIndex
                    || inheritedSettings.category != (Categories)cbCategory.SelectedIndex))
            {
                if (childForm == null)
                {
                    childForm = new ConfirmForm();
                    childForm.Response += HandleResponse;
                    childForm.ShowDialog();
                }
                else
                {
                    childForm.BringToFront();
                }
            }
            else if (inheritedSettings != null
                && inheritedSettings.language == (Languages)cbLanguage.SelectedIndex
                    && inheritedSettings.category == (Categories)cbCategory.SelectedIndex)
            {
                parentForm.Show();
                Close();
            }
            else if (inheritedSettings == null)
            {
                GetUserSettings();
                UpdateParent();
            }

        }

        private void UpdateParent()
        {
            ConfigService.SaveConfig();
            if (parentForm != null)
            {
                parentForm.UpdateSettings();
                parentForm.Show();
                Close();
            }
            else
            {
                var newForm = new MainForm();
                newForm.Show();
                Hide();
            }
        }

        private void HandleResponse(bool dialogResult)
        {
            if (dialogResult)
            {
                GetUserSettings();

                if (inheritedSettings.category == ConfigService.UserSettings.category)
                {
                    ConfigService.UserSettings.favCountry = inheritedSettings.favCountry;
                    ConfigService.UserSettings.favPlayers = inheritedSettings.favPlayers;
                }

                UpdateParent();
            }
            childForm = null;
        }

        private void GetUserSettings()
        {
            ConfigService.UserSettings.categoryName = ((Categories)cbCategory.SelectedIndex).ToString();
            ConfigService.UserSettings.category = (Categories)cbCategory.SelectedIndex;
            ConfigService.UserSettings.languageName = ((Languages)cbLanguage.SelectedIndex).ToString();
            ConfigService.UserSettings.language = (Languages)cbLanguage.SelectedIndex;

            ConfigService.UserSettings.favCountry = null;
            ConfigService.UserSettings.favPlayers.Clear();
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (parentForm != null)
            {
                parentForm.Show();
            }
        }
    }
}
