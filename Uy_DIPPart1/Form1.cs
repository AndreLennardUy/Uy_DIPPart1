using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Drawing.Imaging;

namespace Uy_DIPPart1
{
    public partial class Form1 : Form
    {
        Bitmap loaded, processed , bg , green;
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
            int width = Convert.ToInt32(pictureBox3.Width);
            int height = Convert.ToInt32(pictureBox3.Height);
            using (Bitmap bmp = new Bitmap(width, height))
            {
                pictureBox3.DrawToBitmap(bmp, new Rectangle(0, 0, width, height));
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

            pictureBox3.Image = processed;
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

            pictureBox3.Image = processed;
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

            pictureBox3.Image = histImage;
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

            pictureBox3.Image = processed;
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

            pictureBox3.Image = processed;
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

            pictureBox3.Image = processed;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog3.ShowDialog();
        }

        private void openFileDialog2_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bg = new Bitmap(openFileDialog2.FileName);
            pictureBox2.Image = bg;
        }

        private void openFileDialog3_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            green = new Bitmap(openFileDialog3.FileName);
            pictureBox1.Image = green;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
        }

        private void subtractToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap resultImage = new Bitmap(bg.Width, bg.Height);

            Color mygreen = Color.FromArgb(53, 255, 33);
            int greygreen = (mygreen.R + mygreen.G + mygreen.B) / 3;
            int threshold = 5;
            
            for (int x = 0; x < green.Width; x++)
            {
                for (int y = 0; y < green.Height; y++)
                {
                    Color pixel = green.GetPixel(x, y);
                    Color backpixel = bg.GetPixel(x, y);
                    int grey = (pixel.R + pixel.G + pixel.B) / 3;
                    int subtractValue = Math.Abs(grey - greygreen);
                    if (subtractValue < threshold)
                    {
                        resultImage.SetPixel(x, y, backpixel);
                    }
                    else
                    {
                        resultImage.SetPixel(x, y, pixel);
                    }
                }
            }

            pictureBox3.Image = resultImage;
        }   

        private void basicCopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            processed = loaded;
            pictureBox3.Image = processed;
        }
    }
}