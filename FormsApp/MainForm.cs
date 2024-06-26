using DataLayer.Models;
using DataLayer.Repository;
using DataLayer.Utilities;
using FormsApp.Properties;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Globalization;

namespace FormsApp
{

    public partial class MainForm : Form
    {
        private Settings childForm;
        private ConfirmForm confirmForm;
        readonly IRepo repository = RepoFactory.getInstance();
        private List<Control> loadingControls;
        private List<PlayerPanel> selectedPlayers = new();
        private List<PlayerPanel> selectedFavPlayers = new();
        private Dictionary<string, string> playerPictureDictionary;
        private List<Player> playerList = new();
        List<Bitmap> bitmapList = new();
        string defaultCulture;

        public MainForm()
        {
            defaultCulture = "en-US";

            playerPictureDictionary = FileUtils.GetPlayerPictures() == null ? new() : FileUtils.GetPlayerPictures();
            var nes = CultureInfo.CurrentCulture;
            if (ConfigService.UserSettings.languageName == Languages.Hrvatski.ToString())
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("hr");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("hr");
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(defaultCulture);
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(defaultCulture);
            }
            InitializeComponent();

            ApplySettings();
        }

        public void UpdateSettings()
        {
            childForm.Close();
            ClearData();
            UpdateCulture();

            ApplySettings();
        }

        private void UpdateCulture()
        {
            if (ConfigService.UserSettings.languageName == Languages.Hrvatski.ToString())
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("hr");
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("hr");
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(defaultCulture);
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(defaultCulture);
            }

            Controls.Clear();
            InitializeComponent();
        }

        private async void ApplySettings()
        {
            ShowLoading();
            List<Team> teams = (List<Team>)await repository.Get<Team>(ConfigService.UserSettings.category);
            BindingSource cbTeamsDataBinding = new BindingSource();
            cbTeamsDataBinding.DataSource = teams;


            cbTeams.ValueMember = "FifaCode";
            cbTeams.DisplayMember = "";
            cbTeams.DataSource = cbTeamsDataBinding.DataSource;

            if (ConfigService.UserSettings.favCountry != null)
            {
                await SetTeam(ConfigService.UserSettings.favCountry);

                if (ConfigService.UserSettings.favPlayers != null)
                {
                    var panels = flpAllPlayers.Controls.OfType<PlayerPanel>().ToList();

                    foreach (PlayerPanel playerPanel in panels)
                    {
                        if (ConfigService.UserSettings.favPlayers.Contains(playerPanel.player))
                        {
                            playerPanel.ToggleFavourite();
                            flpFavPlayers.Controls.Add(playerPanel);
                        }
                    }
                }
            }

            HideLoading();
        }

        private void BtnFavTeamConfirm_Click(object sender, EventArgs e)
        {
            if (cbTeams.SelectedValue as string != ConfigService.UserSettings.favCountry)
            {
                ClearData();
                SetTeam(cbTeams.SelectedValue as string);
            }
        }

        private async Task SetTeam(string? favCountry)
        {
            ShowLoading();

            ConfigService.UserSettings.favCountry = favCountry;
            cbTeams.SelectedIndex = cbTeams.FindString(ConfigService.UserSettings.favCountry.ToString());
            ClearData();

            IEnumerable<Match> matches = await repository.GetMatchByFifaCode(ConfigService.UserSettings.category, ConfigService.UserSettings.favCountry);
            if (matches.ElementAt(0).AwayTeam.Code == ConfigService.UserSettings.favCountry)
            {
                playerList = matches.ElementAt(0).AwayTeamStatistics.StartingEleven.Concat(matches.ElementAt(0).AwayTeamStatistics.Substitutes).ToList();
            }
            else
            {
                playerList = matches.ElementAt(0).HomeTeamStatistics.StartingEleven.Concat(matches.ElementAt(0).HomeTeamStatistics.Substitutes).ToList();
            }

            HideLoading();

            foreach (var player in playerList)
            {
                var newPanel = new PlayerPanel(player);
                newPanel.MouseDown += PlayerSelect;
                newPanel.DragEnter += Flp_DragEnter;
                newPanel.MouseDown += Flp_MouseDown;
                newPanel.pictureSet += PlayerPictureSet;

                playerPictureDictionary.TryGetValue(player.ToString(), out string picturePath);
                if (picturePath != null)
                {
                    newPanel.SetPicture(picturePath);
                }

                flpAllPlayers.Controls.Add(newPanel);
            }

            foreach (Match match in matches)
            {
                TeamEvent[] activeEvents;
                if (match.AwayTeam.Code == ConfigService.UserSettings.favCountry)
                {
                    activeEvents = match.AwayTeamEvents;
                }
                else
                {
                    activeEvents = match.HomeTeamEvents;
                }

                foreach (TeamEvent teamEvent in activeEvents)
                {
                    foreach (Player player in playerList)
                    {
                        if (teamEvent.Player == player.Name && teamEvent.TypeOfEvent == TypeOfEvent.Goal)
                        {
                            player.Goals++;
                        }
                        else if (teamEvent.Player == player.Name
                            && (teamEvent.TypeOfEvent == TypeOfEvent.YellowCard || teamEvent.TypeOfEvent == TypeOfEvent.YellowCardSecond))
                        {
                            player.YellowCards++;
                        }
                    }
                }
            }
            SetDGVModelYellowCard();
            SetDGVModelGoal();
            SetDGVModelAttendance(matches);
        }

        private void ClearData()
        {
            flpAllPlayers.Controls.Clear();
            flpFavPlayers.Controls.Clear();
            dgvRankListsYellowCard.Columns.Clear();
        }

        private void PlayerSelect(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                FlowLayoutPanel? parent = ((PlayerPanel)sender).Parent as FlowLayoutPanel;
                PlayerPanel player = (PlayerPanel)sender;
                if (parent == flpAllPlayers)
                {
                    if (player.selected)
                    {
                        selectedPlayers.Remove((player));
                    }
                    else
                    {
                        selectedPlayers.Add((PlayerPanel)sender);
                    }
                }
                else
                {
                    if (player.selected)
                    {
                        selectedFavPlayers.Remove((player));
                    }
                    else
                    {
                        selectedFavPlayers.Add((PlayerPanel)sender);
                    }
                }
                player.ToggleSelect();
            }
        }

        private void Flp_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void Flp_MouseDown(object sender, MouseEventArgs e)
        {
            FlowLayoutPanel? destination = ((PlayerPanel)sender).Parent as FlowLayoutPanel;
            FlowLayoutPanel? source = ((PlayerPanel)sender).Parent as FlowLayoutPanel;
            ContextMenuStrip sourceStrip;
            List<PlayerPanel> sourceList;
            PlayerPanel playerPanel = (PlayerPanel)sender;

            if (source == flpAllPlayers)
            {
                destination = flpFavPlayers;
                sourceStrip = cmsAllPlayers;
                sourceList = selectedPlayers;
            }
            else
            {
                destination = flpAllPlayers;
                sourceStrip = cmsFavPlayers;
                sourceList = selectedFavPlayers;
            }

            if (e.Button == MouseButtons.Left)
            {
                playerPanel.DoDragDrop(playerPanel, DragDropEffects.Copy);
            }
            else if (e.Button == MouseButtons.Right)
            {
                sourceStrip.Show(Cursor.Position);
            }
        }

        private void Flp_DragDrop(object sender, DragEventArgs e)
        {
            FlowLayoutPanel? destination = sender as FlowLayoutPanel;
            FlowLayoutPanel? source = sender as FlowLayoutPanel;
            List<PlayerPanel> playerList;
            if (destination == flpFavPlayers)
            {
                source = flpAllPlayers;
                playerList = selectedPlayers;
            }
            else
            {
                source = flpFavPlayers;
                playerList = selectedFavPlayers;
            }
            MoveItems(source, destination, playerList);
        }

        private void UnfavouriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MoveItems(flpFavPlayers, flpAllPlayers, selectedFavPlayers);
        }

        private void FavouriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MoveItems(flpAllPlayers, flpFavPlayers, selectedPlayers);
        }

        private void MoveItems(FlowLayoutPanel source, FlowLayoutPanel destination, List<PlayerPanel> panels)
        {
            foreach (PlayerPanel panel in panels)
            {
                panel.ToggleSelect();
                panel.ToggleFavourite();
                destination.Controls.Add(panel);
                source.Controls.Remove(panel);
            }
            panels.Clear();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (confirmForm == null && e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                confirmForm = new ConfirmForm();
                confirmForm.Response += HandleCloseResponse;
                confirmForm.ShowDialog();
            }
        }

        private void HandleCloseResponse(bool dialogChoice)
        {
            if (dialogChoice)
            {
                List<Player> favPlayers = new List<Player>();
                foreach (var player in flpFavPlayers.Controls.OfType<PlayerPanel>())
                {
                    favPlayers.Add(player.player);
                }
                ConfigService.UserSettings.favPlayers = favPlayers;
                ConfigService.SaveConfig();


                FileUtils.SavePlayerPicture(playerPictureDictionary);

                Close();
            }
            else
            {
                if (childForm != null)
                {
                    childForm.Close();
                    childForm = null;
                }
                if (confirmForm != null)
                {
                    confirmForm.Close();
                    confirmForm = null;
                }
            }
        }

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            childForm = new Settings(this);
            ShowForm(childForm);
        }

        private void ShowForm(Form form)
        {
            Hide();
            form.Show();
        }

        private void PlayerPictureSet(Player player, string filePath)
        {
            string key = player.ToString();
            if (playerPictureDictionary.ContainsKey(key))
            {
                playerPictureDictionary[key] = filePath;
            }
            else
            {
                playerPictureDictionary.Add(key, filePath);
            }
            SetDGVModelYellowCard();
            SetDGVModelGoal();
        }

        private void SetDGVModelYellowCard()
        {
            playerList.Sort((x, y) => -x.YellowCards.CompareTo(y.YellowCards));
            dgvRankListsYellowCard.Columns.Clear();
            int i = 0;
            dgvRankListsYellowCard.Columns.Add("Rank", "Rank");
            dgvRankListsYellowCard.Columns.Add(new DataGridViewImageColumn());
            dgvRankListsYellowCard.Columns.Add(nameof(Player.Name), nameof(Player.Name));
            dgvRankListsYellowCard.Columns.Add(nameof(Player.YellowCards), nameof(Player.YellowCards));

            foreach (var player in playerList)
            {
                i++;
                playerPictureDictionary.TryGetValue(player.ToString(), out string filePath);
                if (filePath != null)
                {
                    dgvRankListsYellowCard.Rows.Add(i, ResizeImage(Bitmap.FromFile(filePath), 128, 128), player.Name, player.YellowCards);
                }
                else
                {
                    dgvRankListsYellowCard.Rows.Add(i, Resources.no_image, player.Name, player.YellowCards);
                }
            }

            dgvRankListsYellowCard.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvRankListsYellowCard.AutoResizeColumns();

            dgvRankListsYellowCard.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dgvRankListsYellowCard.AutoResizeRows();
        }

        private void SetDGVModelGoal()
        {
            playerList.Sort((x, y) => -x.Goals.CompareTo(y.Goals));
            dgvRankListsGoals.Columns.Clear();
            int i = 0;
            dgvRankListsGoals.Columns.Add("Rank", "Rank");
            dgvRankListsGoals.Columns.Add(new DataGridViewImageColumn());
            dgvRankListsGoals.Columns.Add(nameof(Player.Name), nameof(Player.Name));
            dgvRankListsGoals.Columns.Add(nameof(Player.Goals), nameof(Player.Goals));


            foreach (var player in playerList)
            {
                i++;
                playerPictureDictionary.TryGetValue(player.ToString(), out string filePath);
                if (filePath != null)
                {
                    dgvRankListsGoals.Rows.Add(i, ResizeImage(Bitmap.FromFile(filePath), 128, 128), player.Name, player.Goals);
                }
                else
                {
                    dgvRankListsGoals.Rows.Add(i, Resources.no_image, player.Name, player.Goals);
                }
            }
            dgvRankListsGoals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvRankListsGoals.AutoResizeColumns();

            dgvRankListsGoals.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dgvRankListsGoals.AutoResizeRows();
        }

        private void SetDGVModelAttendance(IEnumerable<Match> matches)
        {
            List<Match> matchList = matches.ToList();
            dgvRankListsGames.Columns.Clear();
            matchList.Sort((x, y) => -x.Attendance.CompareTo(y.Attendance));

            int i = 0;
            dgvRankListsGames.Columns.Add("Rank", "Rank");
            dgvRankListsGames.Columns.Add(nameof(Match.Venue), nameof(Match.Venue));
            dgvRankListsGames.Columns.Add(nameof(Match.Location), nameof(Match.Location));
            dgvRankListsGames.Columns.Add(nameof(Match.Attendance), nameof(Match.Attendance));
            dgvRankListsGames.Columns.Add(nameof(Match.HomeTeamCountry), nameof(Match.HomeTeamCountry));
            dgvRankListsGames.Columns.Add(nameof(Match.AwayTeamCountry), nameof(Match.AwayTeamCountry));

            foreach (var match in matchList)
            {
                i++;
                dgvRankListsGames.Rows.Add(i, match.Venue, match.Location, match.Attendance, match.HomeTeamCountry, match.AwayTeamCountry);
            }

            dgvRankListsGames.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvRankListsGames.AutoResizeColumns();

            dgvRankListsGames.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dgvRankListsGames.AutoResizeRows();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            /*DrawToBitmap(dgvRankListsYellowCard);
            DrawToBitmap(dgvRankListsGoals);
            DrawToBitmap(dgvRankListsGames);

            printPreviewDialog1.PrintPreviewControl.Zoom = 1;
            printPreviewDialog1.ShowDialog();*/
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "pdf files (*.pdf)|*.pdf";
                dialog.FilterIndex = 2;
                dialog.RestoreDirectory = true;
                dialog.DefaultExt = "pdf";
                dialog.AddExtension = true;
                dialog.CheckFileExists = false;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var pdfTableGoals = ExportPlayerTableToPdf(dgvRankListsGoals);
                    var pdfTableCards = ExportPlayerTableToPdf(dgvRankListsGoals);
                    var pdfTableGames = ExportStandardTableToPdf(dgvRankListsGames);

                    using (FileStream stream = new FileStream(dialog.FileName, FileMode.Create))
                    {
                        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                        PdfWriter.GetInstance(pdfDoc, stream);
                        pdfDoc.Open();
                        pdfDoc.Add(pdfTableCards);
                        pdfDoc.NewPage();
                        pdfDoc.Add(pdfTableGoals);
                        pdfDoc.NewPage();
                        pdfDoc.Add(pdfTableGames);
                        pdfDoc.Close();
                        stream.Close();
                    }
                }
            }
        }

        private PdfPTable ExportPlayerTableToPdf(DataGridView dgv)
        {
            PdfPTable pdfTable = new PdfPTable(dgv.ColumnCount);
            pdfTable.DefaultCell.Padding = 3;
            pdfTable.WidthPercentage = 100;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.DefaultCell.BorderWidth = 1;

            foreach (DataGridViewColumn column in dgv.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                pdfTable.AddCell(cell);
            }
            int i = 0;

            int rowCount = 0;
            foreach (DataGridViewRow row in dgv.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    try
                    {
                        if (i == 1 && row.Index < dgv.Rows.Count - 1)
                        {
                            if (playerPictureDictionary.ContainsKey(row.Cells[2].Value.ToString()))
                            {
                                try
                                {
                                    pdfTable.AddCell(iTextSharp.text.Image.GetInstance(playerPictureDictionary[row.Cells[2].Value.ToString()]));
                                }
                                catch (Exception)
                                { }
                            }
                            else
                            {
                                try
                                {
                                    var image = iTextSharp.text.Image.GetInstance(@"../../../Assets/no_image.png");
                                    pdfTable.AddCell(image);
                                }
                                catch (Exception)
                                { }
                            }
                        }
                        else if (row.Index < dgv.Rows.Count - 1)
                        {
                            try
                            {
                                if (cell.Value != null)
                                {
                                    pdfTable.AddCell(cell.Value.ToString());
                                }
                            }
                            catch (Exception)
                            {
                            }

                        }
                    }
                    catch { }

                    i++;
                }

                rowCount++;
                i = 0;
            }
  
            return pdfTable;
        }

    private PdfPTable ExportStandardTableToPdf(DataGridView dgv)
    {
        PdfPTable pdfTable = new PdfPTable(dgv.ColumnCount);
        pdfTable.DefaultCell.Padding = 3;
        pdfTable.WidthPercentage = 100;
        pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
        pdfTable.DefaultCell.BorderWidth = 1;

        foreach (DataGridViewColumn column in dgv.Columns)
        {
            PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
            cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
            pdfTable.AddCell(cell);
        }
        int i = 0;
        foreach (DataGridViewRow row in dgv.Rows)
        {
            foreach (DataGridViewCell cell in row.Cells)
            {
                if (row.Index < dgv.Rows.Count - 1)
                {
                    try
                    {
                        pdfTable.AddCell(cell.Value.ToString());
                    }
                    catch (Exception)
                    {
                    }
                }
            }

        }
        return pdfTable;
    }

    /*private void DrawToBitmap(DataGridView grid) {
        int height = grid.Height;
        int width = grid.Width;
        grid.Height = grid.RowCount * grid.RowTemplate.Height * 2;
        grid.Width = 50;
        foreach (DataGridViewColumn col in grid.Columns)
        {
            grid.Width += col.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
        }
        Bitmap bitmap = new Bitmap(grid.Width, grid.Height);

        grid.DrawToBitmap(bitmap, new System.Drawing.Rectangle(0, 0, grid.Width, grid.Height));
        grid.Height = height;
        grid.Width = width;
        bitmapList.Add(bitmap);
    }*/

    int i = 0;
    private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
    {
        e.Graphics.DrawImage(bitmapList.ElementAt(i), 0, 0);
        if (i == bitmapList.Count() - 1)
        {
            e.HasMorePages = false;
        }
        else
        {
            i++;
            e.HasMorePages = true;
        }
    }
    public static Bitmap ResizeImage(System.Drawing.Image image, int width, int height)
    {
        var destRect = new System.Drawing.Rectangle(0, 0, width, height);
        var destImage = new Bitmap(width, height);

        destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

        using (var graphics = Graphics.FromImage(destImage))
        {
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            using (var wrapMode = new ImageAttributes())
            {
                wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
            }
        }

        return destImage;
    }

    private void ShowLoading()
    {
        pbLoadingFavPlayers.Size = new Size(200, 200);
        pbLoadingAllPlayers.Size = new Size(200, 200);
        pbLoadingRankList.Size = new Size(200, 200);
        pbLoadingFavPlayers.Location = new Point(flpFavPlayers.Location.X + flpFavPlayers.Size.Width / 2 - pbLoadingFavPlayers.Width / 2, flpFavPlayers.Location.Y + flpFavPlayers.Size.Height / 2 - pbLoadingFavPlayers.Height / 2);
        pbLoadingAllPlayers.Location = new Point(flpAllPlayers.Location.X + flpAllPlayers.Size.Width / 2 - pbLoadingFavPlayers.Width / 2, flpAllPlayers.Location.Y + flpAllPlayers.Size.Height / 2 - pbLoadingAllPlayers.Height / 2);
        pbLoadingRankList.Location = new Point(dgvRankListsGoals.Location.X + dgvRankListsYellowCard.Size.Width / 2 - pbLoadingRankList.Width / 2, dgvRankListsYellowCard.Location.Y + dgvRankListsYellowCard.Size.Height / 2 - pbLoadingRankList.Height / 2);
        loadingControls = new List<Control> { pbLoadingFavPlayers, pbLoadingAllPlayers, pbLoadingRankList };

        foreach (Control control in loadingControls)
        {
            control.Visible = true;
        }
    }

    private void HideLoading()
    {
        foreach (Control control in loadingControls)
        {
            control.Visible = false;
        }
    }
}
}