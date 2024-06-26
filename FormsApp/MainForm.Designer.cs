namespace FormsApp
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            cbTeams = new ComboBox();
            btnFavTeamConfirm = new Button();
            lblFavTeam = new Label();
            menuStrip1 = new MenuStrip();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            printToolStripMenuItem1 = new ToolStripMenuItem();
            flpAllPlayers = new FlowLayoutPanel();
            flpFavPlayers = new FlowLayoutPanel();
            cmsAllPlayers = new ContextMenuStrip(components);
            favouriteToolStripMenuItem = new ToolStripMenuItem();
            cmsFavPlayers = new ContextMenuStrip(components);
            unfavouriteToolStripMenuItem = new ToolStripMenuItem();
            dgvRankListsYellowCard = new DataGridView();
            printPreviewDialog1 = new PrintPreviewDialog();
            printDocument1 = new System.Drawing.Printing.PrintDocument();
            pbLoadingFavPlayers = new PictureBox();
            pbLoadingAllPlayers = new PictureBox();
            pbLoadingRankList = new PictureBox();
            dgvRankListsGoals = new DataGridView();
            dgvRankListsGames = new DataGridView();
            menuStrip1.SuspendLayout();
            cmsAllPlayers.SuspendLayout();
            cmsFavPlayers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRankListsYellowCard).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbLoadingFavPlayers).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbLoadingAllPlayers).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbLoadingRankList).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvRankListsGoals).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvRankListsGames).BeginInit();
            SuspendLayout();
            // 
            // cbTeams
            // 
            resources.ApplyResources(cbTeams, "cbTeams");
            cbTeams.FormattingEnabled = true;
            cbTeams.Name = "cbTeams";
            // 
            // btnFavTeamConfirm
            // 
            resources.ApplyResources(btnFavTeamConfirm, "btnFavTeamConfirm");
            btnFavTeamConfirm.Name = "btnFavTeamConfirm";
            btnFavTeamConfirm.UseVisualStyleBackColor = true;
            btnFavTeamConfirm.Click += BtnFavTeamConfirm_Click;
            // 
            // lblFavTeam
            // 
            resources.ApplyResources(lblFavTeam, "lblFavTeam");
            lblFavTeam.Name = "lblFavTeam";
            // 
            // menuStrip1
            // 
            resources.ApplyResources(menuStrip1, "menuStrip1");
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { settingsToolStripMenuItem, printToolStripMenuItem1 });
            menuStrip1.Name = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            resources.ApplyResources(settingsToolStripMenuItem, "settingsToolStripMenuItem");
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Click += SettingsToolStripMenuItem_Click;
            // 
            // printToolStripMenuItem1
            // 
            resources.ApplyResources(printToolStripMenuItem1, "printToolStripMenuItem1");
            printToolStripMenuItem1.Name = "printToolStripMenuItem1";
            printToolStripMenuItem1.Click += btnPrint_Click;
            // 
            // flpAllPlayers
            // 
            resources.ApplyResources(flpAllPlayers, "flpAllPlayers");
            flpAllPlayers.AllowDrop = true;
            flpAllPlayers.BackColor = Color.White;
            flpAllPlayers.Name = "flpAllPlayers";
            flpAllPlayers.DragDrop += Flp_DragDrop;
            flpAllPlayers.DragEnter += Flp_DragEnter;
            // 
            // flpFavPlayers
            // 
            resources.ApplyResources(flpFavPlayers, "flpFavPlayers");
            flpFavPlayers.AllowDrop = true;
            flpFavPlayers.BackColor = Color.White;
            flpFavPlayers.Name = "flpFavPlayers";
            flpFavPlayers.DragDrop += Flp_DragDrop;
            flpFavPlayers.DragEnter += Flp_DragEnter;
            // 
            // cmsAllPlayers
            // 
            resources.ApplyResources(cmsAllPlayers, "cmsAllPlayers");
            cmsAllPlayers.ImageScalingSize = new Size(20, 20);
            cmsAllPlayers.Items.AddRange(new ToolStripItem[] { favouriteToolStripMenuItem });
            cmsAllPlayers.Name = "cmsAllPlayers";
            // 
            // favouriteToolStripMenuItem
            // 
            resources.ApplyResources(favouriteToolStripMenuItem, "favouriteToolStripMenuItem");
            favouriteToolStripMenuItem.Name = "favouriteToolStripMenuItem";
            favouriteToolStripMenuItem.Click += FavouriteToolStripMenuItem_Click;
            // 
            // cmsFavPlayers
            // 
            resources.ApplyResources(cmsFavPlayers, "cmsFavPlayers");
            cmsFavPlayers.ImageScalingSize = new Size(20, 20);
            cmsFavPlayers.Items.AddRange(new ToolStripItem[] { unfavouriteToolStripMenuItem });
            cmsFavPlayers.Name = "cmsFavPlayers";
            // 
            // unfavouriteToolStripMenuItem
            // 
            resources.ApplyResources(unfavouriteToolStripMenuItem, "unfavouriteToolStripMenuItem");
            unfavouriteToolStripMenuItem.Name = "unfavouriteToolStripMenuItem";
            unfavouriteToolStripMenuItem.Click += UnfavouriteToolStripMenuItem_Click;
            // 
            // dgvRankListsYellowCard
            // 
            resources.ApplyResources(dgvRankListsYellowCard, "dgvRankListsYellowCard");
            dgvRankListsYellowCard.AllowUserToOrderColumns = true;
            dgvRankListsYellowCard.BackgroundColor = SystemColors.Control;
            dgvRankListsYellowCard.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRankListsYellowCard.Name = "dgvRankListsYellowCard";
            dgvRankListsYellowCard.RowTemplate.Height = 29;
            // 
            // printPreviewDialog1
            // 
            resources.ApplyResources(printPreviewDialog1, "printPreviewDialog1");
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.Name = "printPreviewDialog1";
            // 
            // printDocument1
            // 
            printDocument1.PrintPage += printDocument1_PrintPage;
            // 
            // pbLoadingFavPlayers
            // 
            resources.ApplyResources(pbLoadingFavPlayers, "pbLoadingFavPlayers");
            pbLoadingFavPlayers.BackColor = Color.White;
            pbLoadingFavPlayers.Image = Properties.Resources.loading_gif;
            pbLoadingFavPlayers.InitialImage = Properties.Resources.loading_gif;
            pbLoadingFavPlayers.Name = "pbLoadingFavPlayers";
            pbLoadingFavPlayers.TabStop = false;
            // 
            // pbLoadingAllPlayers
            // 
            resources.ApplyResources(pbLoadingAllPlayers, "pbLoadingAllPlayers");
            pbLoadingAllPlayers.BackColor = Color.White;
            pbLoadingAllPlayers.Image = Properties.Resources.loading_gif;
            pbLoadingAllPlayers.InitialImage = Properties.Resources.loading_gif;
            pbLoadingAllPlayers.Name = "pbLoadingAllPlayers";
            pbLoadingAllPlayers.TabStop = false;
            // 
            // pbLoadingRankList
            // 
            resources.ApplyResources(pbLoadingRankList, "pbLoadingRankList");
            pbLoadingRankList.BackColor = SystemColors.Control;
            pbLoadingRankList.Image = Properties.Resources.loading_gif;
            pbLoadingRankList.InitialImage = Properties.Resources.loading_gif;
            pbLoadingRankList.Name = "pbLoadingRankList";
            pbLoadingRankList.TabStop = false;
            // 
            // dgvRankListsGoals
            // 
            resources.ApplyResources(dgvRankListsGoals, "dgvRankListsGoals");
            dgvRankListsGoals.AllowUserToOrderColumns = true;
            dgvRankListsGoals.BackgroundColor = SystemColors.Control;
            dgvRankListsGoals.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRankListsGoals.Name = "dgvRankListsGoals";
            dgvRankListsGoals.RowTemplate.Height = 29;
            // 
            // dgvRankListsGames
            // 
            resources.ApplyResources(dgvRankListsGames, "dgvRankListsGames");
            dgvRankListsGames.AllowUserToOrderColumns = true;
            dgvRankListsGames.BackgroundColor = SystemColors.Control;
            dgvRankListsGames.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRankListsGames.Name = "dgvRankListsGames";
            dgvRankListsGames.RowTemplate.Height = 29;
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(dgvRankListsGames);
            Controls.Add(dgvRankListsGoals);
            Controls.Add(pbLoadingRankList);
            Controls.Add(pbLoadingAllPlayers);
            Controls.Add(pbLoadingFavPlayers);
            Controls.Add(dgvRankListsYellowCard);
            Controls.Add(flpFavPlayers);
            Controls.Add(flpAllPlayers);
            Controls.Add(menuStrip1);
            Controls.Add(lblFavTeam);
            Controls.Add(btnFavTeamConfirm);
            Controls.Add(cbTeams);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            FormClosing += MainForm_FormClosing;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            cmsAllPlayers.ResumeLayout(false);
            cmsFavPlayers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvRankListsYellowCard).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbLoadingFavPlayers).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbLoadingAllPlayers).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbLoadingRankList).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvRankListsGoals).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvRankListsGames).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ComboBox cbTeams;
        private Button btnFavTeamConfirm;
        private Label lblFavTeam;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private FlowLayoutPanel flpAllPlayers;
        private FlowLayoutPanel flpFavPlayers;
        private ContextMenuStrip cmsAllPlayers;
        private ToolStripMenuItem favouriteToolStripMenuItem;
        private ContextMenuStrip cmsFavPlayers;
        private ToolStripMenuItem unfavouriteToolStripMenuItem;
        private DataGridView dgvRankListsYellowCard;
        private PrintPreviewDialog printPreviewDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private PictureBox pbLoadingFavPlayers;
        private PictureBox pbLoadingAllPlayers;
        private PictureBox pbLoadingRankList;
        private DataGridView dgvRankListsGoals;
        private DataGridView dgvRankListsGames;
        private ToolStripMenuItem printToolStripMenuItem1;
    }
}
