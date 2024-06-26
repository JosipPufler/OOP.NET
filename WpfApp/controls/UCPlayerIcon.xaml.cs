using DataLayer.Models;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfApp.controls
{
    /// <summary>
    /// Interaction logic for UCPlayerIcon.xaml
    /// </summary>
    public partial class UCPlayerIcon : UserControl
    {
        PlayerPicture player;
        public Player Player
        {
            get { return player.player; }
            set { player.player = value; shirtNumber.Content = value.ShirtNumber; }
        }

        public BitmapImage PlayerPicture
        {
            get { return player.picturePath; }
            set
            {
                player.picturePath = value;
                playerPicture.ImageSource = new TransformedBitmap(value, new ScaleTransform(100 / value.PixelWidth, 100 / value.PixelHeight));
            }
        }

        public UCPlayerIcon()
        {
            player = new();
            InitializeComponent();
        }

        private void OpenPlayerStats(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var playerStatsForm = new PlayerStatsWindow(player);
            playerStatsForm.ShowDialog();
        }
    }
}
