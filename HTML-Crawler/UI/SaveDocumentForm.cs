using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HTML_Crawler.Routers;

namespace HTML_Crawler.UI
{
    public partial class SaveDocumentForm : Form
    {
        Router _router;
        public SaveDocumentForm(Router router)
        {
            InitializeComponent();
            _router = router;
            this.BackColor = Color.FromArgb(38, 38, 46);
            textBoxInput.BackColor = Color.FromArgb(61, 61, 77);
            SaveDocumentLabel.BackColor = Color.FromArgb(32, 190, 125);

        }

        private void SaveDocumentLabel_Click(object sender, EventArgs e)
        {
            _router.SaveDocument(textBoxInput.Text);
            this.Close();
        }

    }
}
