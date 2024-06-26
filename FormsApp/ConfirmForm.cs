namespace FormsApp
{
    public partial class ConfirmForm : Form
    {
        public delegate void ConfirmEvent(bool answer);
        public ConfirmEvent Response;
        
        public ConfirmForm()
        {
            InitializeComponent();
            KeyPreview = true;
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            Response(true);
            Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            Response(false);
            Close();
        }

        private void ConfirmForm_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Response(false);
                    Close();
                    break;
                case Keys.Enter:
                    Response(true);
                    Close();
                    break;
                default:
                    break;
            }
        }
    }
}
