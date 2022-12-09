using HTML_Crawler.Routers;
using HTML_Crawler.UI;
namespace HTML_Crawler
{
    public partial class FormMain : Form
    {
        Router _router = new Router();
        public FormMain()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(38, 38, 46);
            this.textBoxOutput.BackColor = Color.FromArgb(61, 61, 77);
            this.textBoxInput.BackColor = Color.FromArgb(61, 61, 77);
            WindowState = FormWindowState.Maximized;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadDocument loadForm = new LoadDocument(_router);
            loadForm.ShowDialog();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

           textBoxOutput.Text =  _router.ParseInput(textBoxInput.Text);
        }
    }
}