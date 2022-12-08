using HTML_Crawler.UI;

namespace HTML_Crawler
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(38, 38, 46);
            this.textBox1.BackColor = Color.FromArgb(61, 61, 77);
            this.textBox2.BackColor = Color.FromArgb(61, 61, 77);
            WindowState = FormWindowState.Maximized;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadDocument loadForm = new LoadDocument();
            loadForm.ShowDialog();
        }
    }
}