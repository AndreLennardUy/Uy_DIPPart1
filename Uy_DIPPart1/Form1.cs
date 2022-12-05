using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Drawing.Imaging;

namespace Uy_DIPPart1
{
    public partial class Form1 : Form
    {
        Bitmap loaded, processed;
        Color c;
        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            loaded = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Image = loaded;
        }

        private void saveFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            int width = Convert.ToInt32(pictureBox2.Width);
            int height = Convert.ToInt32(pictureBox2.Height);
            using (Bitmap bmp = new Bitmap(width, height))
            {
                pictureBox2.DrawToBitmap(bmp, new Rectangle(0, 0, width, height));
                bmp.Save(saveFileDialog1.FileName, ImageFormat.Jpeg);
                label1.Text = "Image saved";
            }
        }

        private void greyscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded.Width, loaded.Height);

            for (int i = 0; i < loaded.Width; i++)
            {
                for (int j = 0; j < loaded.Height; j++)
                {
                    c = loaded.GetPixel(i, j);
                    int avg = (c.R + c.G + c.B) / 3;
                    processed.SetPixel(i, j, Color.FromArgb(avg, avg, avg));
                }
            }

            pictureBox2.Image = processed;
        }

        private void colorInversionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded.Width, loaded.Height);

            for (int i = 0; i < loaded.Width; i++)
            {
                for (int j = 0; j < loaded.Height; j++)
                {
                    c = loaded.GetPixel(i, j);
                    int r = 255 - c.R;
                    int g = 255 - c.G;
                    int b = 255 - c.B;
                    processed.SetPixel(i, j, Color.FromArgb(r, g, b));
                }
            }

            pictureBox2.Image = processed;
        }

        private void histrogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded.Width, loaded.Height);
            //process image to histogram
            int[] hist = new int[256];
            for (int i = 0; i < loaded.Width; i++)
            {
                for (int j = 0; j < loaded.Height; j++)
                {
                    c = loaded.GetPixel(i, j);
                    int avg = (c.R + c.G + c.B) / 3;
                    hist[avg]++;
                }
            }

            //draw histogram
            int max = hist.Max();
            int scale = 100;
            int width = 256;
            int height = 100;
            Bitmap histImage = new Bitmap(width, height);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (j < (hist[i] * scale / max))
                    {
                        histImage.SetPixel(i, j, Color.Black);
                    }
                    else
                    {
                        histImage.SetPixel(i, j, Color.White);
                    }
                }
            }

            pictureBox2.Image = histImage;
        }

        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded.Width, loaded.Height);

            for (int i = 0; i < loaded.Width; i++)
            {
                for (int j = 0; j < loaded.Height; j++)
                {
                    c = loaded.GetPixel(i, j);
                    int r = (int)(c.R * .393 + c.G * .769 + c.B * .189);
                    int g = (int)(c.R * .349 + c.G * .686 + c.B * .168);
                    int b = (int)(c.R * .272 + c.G * .534 + c.B * .131);
                    if (r > 255) r = 255;
                    if (g > 255) g = 255;
                    if (b > 255) b = 255;
                    processed.SetPixel(i, j, Color.FromArgb(r, g, b));
                }
            }

            pictureBox2.Image = processed;
        }

        private void horizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded.Width, loaded.Height);

            for (int i = 0; i < loaded.Width; i++)
            {
                for (int j = 0; j < loaded.Height; j++)
                {
                    c = loaded.GetPixel(i, j);
                    processed.SetPixel((loaded.Width - 1) - i, j, c);
                }
            }

            pictureBox2.Image = processed;
        }

        private void verticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = new Bitmap(loaded.Width, loaded.Height);

            for (int i = 0; i < loaded.Width; i++)
            {
                for (int j = 0; j < loaded.Height; j++)
                {
                    c = loaded.GetPixel(i, j);
                    processed.SetPixel(i, (loaded.Height - 1) - j, c);
                }
            }

            pictureBox2.Image = processed;
        }

        private void basicCopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = loaded;
            pictureBox2.Image = processed;
        }
    }
}