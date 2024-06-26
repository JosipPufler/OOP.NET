using DataLayer.Models;
using System.Resources;

namespace FormsApp
{
    public partial class PlayerPanel : UserControl
    {
        public delegate void PictureSetEvent(Player player, string filePath);
        public PictureSetEvent pictureSet;

        public Player player;
        public bool selected = false;
        public bool favourite = false;
        public PlayerPanel(Player player)
        {
            this.player = player;

            InitializeComponent();
            LoadPlayer();

            BackColor = SystemColors.Control;
        }

        private void LoadPlayer()
        {
            lblPlayerName.Text = player.Name;
            lblPosition.Text = player.Position.ToString();
            lblShirtNumber.Text = player.ShirtNumber.ToString();
            if (player.Captain)
            {
                lblPlayerName.Text += " (C)";
            }
            pbStar.Visible = false;
            ResXResourceWriter resx = new ResXResourceWriter(@".\Properties\Resources.resx");
            
        }

        private void pbPlayer_Click(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = @".\Assets";
                openFileDialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;


                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;

                    /*if (!filePath.StartsWith(Directory.GetCurrentDirectory()))
                    {
                        MessageBox.Show("Slika mora biti unutar projekta", "Kriva putanja", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }*/

                    SetPicture(filePath);
                    pictureSet(player, filePath);
                }
            }
        }

        public void SetPicture(string filePath) { 
            pbPlayer.Image = Image.FromFile(filePath);
        }

        public void ToggleSelect() { 
            selected = !selected;
            if (selected)
            {
                BackColor = SystemColors.ActiveCaption;
            }
            else
            {
                BackColor = SystemColors.Control;
            }
        }

        public void ToggleFavourite() {
            favourite = !favourite;
            pbStar.Visible = favourite;
        }
    }
}
