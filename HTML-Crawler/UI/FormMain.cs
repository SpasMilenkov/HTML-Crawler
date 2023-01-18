using HTML_Crawler.Routers;
using HTML_Crawler.UI;
namespace HTML_Crawler
{
    public partial class FormMain : Form
    {
        Router _router = new Router();
        Point _point = new Point();
        int _accumulatedHeight = 0;
        int maxWidth = 0;
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

        private void SaveDocumentLabel_Click(object sender, EventArgs e)
        {
            SaveDocumentForm saveForm = new SaveDocumentForm(_router);
            saveForm.ShowDialog();
        }

        private void VisualizeLabel_Click(object sender, EventArgs e)
        {
            var html = _router.GetTree();
            Bitmap bmp = new Bitmap(Canvas.Width, Canvas.Height);

            if(Canvas.Image != null) Canvas.Image.Dispose();

            Bitmap result = DFSGraphic(html, ref bmp);
            Canvas.Image = result;
            Canvas.Invalidate();
            _point = new Point();
            _accumulatedHeight = 10;
            maxWidth = 10;
            //GC.Collect();
        }
        private Bitmap DFSGraphic(GTree<string> node, ref Bitmap plot)
        {
            var firstChild = node._childNodes.First();
            if (firstChild != null)
            {

                while (firstChild != null)
                {
                    if (firstChild.Value._childNodes.First != null)
                    {
                        DFSGraphic(firstChild.Value, ref plot);
                    }
                    else
                    {
                        RenderHtml(firstChild.Value, ref plot);
                    }

                    firstChild = firstChild.Next;
                }

            }
            else
            {
               RenderHtml(node, ref plot);
            }

            return plot;
        }

        private void RenderHtml(GTree<String>node, ref Bitmap plot)
        {
            Font f = new Font("Courier New", 16, FontStyle.Regular);
            _point.X = 30;
            _point.Y = _point.Y + 30;

            if (node.Value != "")
            {
                Graphics g = Graphics.FromImage(plot);

                SizeF size = g.MeasureString(node.Value, f);

                RectangleF rectf = new RectangleF(_point, size);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                _accumulatedHeight += (int)size.Height + 30;

                if (maxWidth < size.Width)
                    maxWidth = (int)size.Width;

                if (_accumulatedHeight > Canvas.Height || maxWidth > Canvas.Width)
                {
                    ResizeBitmap(ref plot, ref g);
                }
                if (node.Tag == "p" || node.Tag == "h" || node.Tag == "div")
                {
                    g.DrawString(node.Value, new Font("Courier New", 16), Brushes.Black, rectf);
                }

                if (node.Tag == "td")
                {
                    g.DrawString(node.Value, new Font("Courier New", 16), Brushes.Black, rectf);
                    Rectangle rectangle = new Rectangle(_point.X, _point.Y, (int)rectf.Width, (int)rectf.Height);
                    g.DrawRectangle(Pens.Black, rectangle);
                }

                if (node.Tag == "a")
                    g.DrawString(node.Value, new Font("Courier New", 16, FontStyle.Underline), Brushes.Blue, rectf);

                if (node.Tag == "img")
                {
                    var prop = node.Props.First();
                    string path = _router.Directory;
                    while (prop != null)
                    {
                        string[] keyValue = Helper.Split(prop.Value, '=');
                        if (keyValue.Length > 0 && keyValue[0] == "src")
                        {
                            path += Helper.Slice(keyValue[1], 1, keyValue[1].Length - 1);
                            break;
                        }
                    }

                    if (File.Exists(path))
                    {
                        using (Bitmap image = new Bitmap(path))
                        {
                            _accumulatedHeight += image.Height;
                            if (maxWidth < image.Width)
                            {
                                maxWidth = image.Width;
                            }
                            if (_accumulatedHeight > Canvas.Height || maxWidth > Canvas.Width)
                            {
                                ResizeBitmap(ref plot, ref g);

                            }
                            g.DrawImage(image, _point);
                            _point.Y += image.Height;
                        }
                    }
                }
                g.Dispose();
            }
            
        }
        public void ResizeBitmap(ref Bitmap bmp, ref Graphics g)
        {
            Bitmap result = new Bitmap(maxWidth, _accumulatedHeight);
            g = Graphics.FromImage(result);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.DrawImage(bmp, 0, 0);
            

            bmp = result;

        }
    }
}