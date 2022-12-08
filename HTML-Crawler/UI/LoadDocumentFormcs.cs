using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HTML_Crawler.UI
{
    public partial class LoadDocument : Form
    {
        public LoadDocument()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(38, 38, 46);
            TextBoxInput.BackColor = Color.FromArgb(61, 61, 77);
            LoadButton.BackColor = Color.FromArgb(32, 190, 125);
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
