using DataLayer.Models;
using DataLayer.Utilities;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        Window childForm;
        Window parentWindow = null;
        Categories? startingUserSettings = null;

        public bool SettingsUpdated { get; set; }
        //hope you're having a great day :>
        public SettingsWindow(Window window)
        {
            parentWindow = window;

            SetLocalization();
            startingUserSettings = ConfigService.UserSettings.category;
            InitializeComponent();
            InitializeComboBoxes();
        }

        public SettingsWindow()
        {
            SetLocalization();
            if (ConfigService.CheckConfigFile() && ConfigService.UserSettings.WpfResolution != null)
            {
                ShowChild();
            }
            
            InitializeComponent();
            InitializeComboBoxes();
        }

        private void SetLocalization()
        {
            if (ConfigService.UserSettings.language == Languages.Hrvatski)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("hr");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("hr");
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en");
            }
        }

        private void InitializeComboBoxes()
        {
            cbCategory.ItemsSource = Enum.GetNames(typeof(Categories));
            cbLanguage.ItemsSource = Enum.GetNames(typeof(Languages));
            foreach (Resolutions item in Enum.GetValues<Resolutions>())
            {
                FieldInfo field = item.GetType().GetField(item.ToString());
                DescriptionAttribute descriptionAttribute = field.GetCustomAttribute(typeof(DescriptionAttribute), false) as DescriptionAttribute;
                cbResolution.Items.Add(descriptionAttribute.Description);
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (cbResolution.SelectedIndex != -1 && cbLanguage.SelectedIndex != -1 && cbCategory.SelectedIndex != -1)
            {
                GetUserSettings();
                ShowChild();
            }
        }

        private void ShowChild()
        {
            if (parentWindow != null)
            {
                childForm = new ConfirmWindow(this);
                childForm.ShowDialog();
            } else
            {
                childForm = new MainWindow(this);
                childForm.Show();
                Hide();
            }
        }

        private void GetUserSettings()
        {
            if (ConfigService.UserSettings.category != (Categories)cbCategory.SelectedIndex)
            {
                ConfigService.UserSettings.favPlayers.Clear();
            }
            ConfigService.UserSettings.categoryName = ((Categories)cbCategory.SelectedIndex).ToString();
            ConfigService.UserSettings.category = (Categories)cbCategory.SelectedIndex;
            ConfigService.UserSettings.languageName = ((Languages)cbLanguage.SelectedIndex).ToString();
            ConfigService.UserSettings.language = (Languages)cbLanguage.SelectedIndex;
            ConfigService.UserSettings.WpfResolution = (Resolutions)cbResolution.SelectedIndex;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (SettingsUpdated && parentWindow != null)
            {
                MainWindow parentMainWindow = (MainWindow)parentWindow;
                parentMainWindow.UpdateSettings(startingUserSettings);
            }
        }
    }
}
