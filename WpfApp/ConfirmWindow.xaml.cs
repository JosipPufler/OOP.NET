using DataLayer.Utilities;
using System.Windows;
using System.Windows.Input;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for ConfirmWindow.xaml
    /// </summary>
    public partial class ConfirmWindow : Window
    {
        Window parentWindow;
        public ConfirmWindow(Window parentWindow)
        {
            InitializeComponent();
            this.parentWindow = parentWindow;
        }

        private void noButton_Click(object sender, RoutedEventArgs e)
        {
            CloseConfirmWindow();
        }

        private void yesButton_Click(object sender, RoutedEventArgs e)
        {
            Confirm();
        }

        private void Confirm()
        {
            if (parentWindow.GetType() == typeof(SettingsWindow))
            {
                SettingsWindow parentSettingsWindow = (SettingsWindow)parentWindow;
                ConfigService.SaveConfig();
                parentSettingsWindow.SettingsUpdated = true;
                CloseParentWindow();
            }
            else if (parentWindow.GetType() == typeof(MainWindow))
            {
                MainWindow parentMainWindow = (MainWindow)parentWindow;
                parentMainWindow.LicenceToDie = true;
                CloseConfirmWindow();
            }
        }

        private void CloseParentWindow()
        {
            Close();
            parentWindow.Close();
        }

        private void CloseConfirmWindow()
        {
            Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Confirm();
            }
            else if(e.Key == Key.Escape)
            {
                CloseConfirmWindow();
            }
        }
    }
}
