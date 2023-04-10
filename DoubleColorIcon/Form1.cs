using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using Rop.Winforms.Icons;
using Rop.Winforms.Icons.MaterialDesign;

namespace DoubleColorIcon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private IEmbeddedIcons _icons;
        private Font _iconfont;
        private Size _masksize;
        private void button1_Click(object sender, EventArgs e)
        {
            var bank = new MaterialDesignBank();
            _icons = bank.Bank;
            var width = 128;
            _iconfont = _icons.GetFont(width, FontStyle.Regular, GraphicsUnit.Pixel);
            var ch = _icons.GetChar(20);
            _masksize = TextRenderer.MeasureText(ch.ToString(), _iconfont);
            MakeIconMask(ch);
            pictureBox1.Image = bmpindex;
        }

        private void MakeIconMask(string ch)
        {
            var sz = _masksize;
            var bmp = new Bitmap(sz.Width, sz.Height, PixelFormat.Format24bppRgb);
            using (var g = Graphics.FromImage(bmp))
            {
                g.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;
                g.Clear(Color.White);
                var br = new SolidBrush(Color.Black);
                g.DrawString(ch, _iconfont, br, 0, 0);
            }
            var gray = Bmp24To8(bmp);
            CalculateMask(gray, sz.Width);
            bmpindex = ByteToIndex(gray, sz);
        }

        private void CalculateMask(byte[] gray, int width)
        {
            var queue = new Queue<int>();
            queue.Enqueue(0);
            while (queue.Count > 0)
            {
                var p = queue.Dequeue();
                if (gray[p] != 1) continue;
                gray[p] = 2;
                if (GetValue(p + 1) == 1) queue.Enqueue(p + 1);
                if (GetValue(p + width) == 1) queue.Enqueue(p + width);
                if (GetValue(p - 1) == 1) queue.Enqueue(p - 1);
                if (GetValue(p - width) == 1) queue.Enqueue(p - width);
            }
            // Local
            byte GetValue(int pos)
            {
                if (pos < 0 || pos >= gray.Length) return 0;
                return gray[pos];
            }
        }

        private static byte[] Bmp24To8(Bitmap bmp)
        {
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadOnly, bmp.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            var wwidth = Math.Abs(bmpData.Stride);
            int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            byte[] rgbValues = new byte[bytes];
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);
            bmp.UnlockBits(bmpData);
            var grayvalues = new byte[bmp.Width * bmp.Height];
            for (var y = 0; y < bmp.Height; y++)
            {
                var i = y * bmp.Width;
                var f = y * wwidth;
                for (var g = 0; g < bmp.Width; g++)
                {
                    grayvalues[i] = (byte)((rgbValues[f] > 0) ? 1 : 0);
                    i++;
                    f += 3;
                }
            }
            return grayvalues;
        }

        private static Bitmap ByteToIndex(byte[] gray, Size sz)
        {
            var bmp = new Bitmap(sz.Width, sz.Height, PixelFormat.Format8bppIndexed);
            var pal = bmp.Palette;
            var colors = new Color[] { Color.Black, Color.White, Color.Transparent };
            pal.Entries[0] = colors[0];
            pal.Entries[1] = colors[1];
            pal.Entries[2] = colors[2];
            bmp.Palette = pal;
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.WriteOnly, bmp.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            var wwidth = Math.Abs(bmpData.Stride);
            var i = 0;
            for (var f = 0; f < bmp.Height; f++)
            {
                System.Runtime.InteropServices.Marshal.Copy(gray, i, ptr, bmp.Width);
                ptr += wwidth;
                i += bmp.Width;
            }
            bmp.UnlockBits(bmpData);
            return bmp;
        }

        private Bitmap bmpindex;
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            var ch = _icons.GetChar((int)numericUpDown1.Value);
            MakeIconMask(ch);

            pictureBox1.Image = GetColoredIcon(Color.Black, Color.Yellow);
            DrawIcon(ch);
        }

        private void DrawIcon(string ch)
        {
            var repo = DoubleColorIconRepository.GetIcon(_icons);
            var font = _icons.GetFont(16, FontStyle.Regular);
            var bmp = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            using var g=Graphics.FromImage(bmp);
            g.InterpolationMode=InterpolationMode.HighQualityBicubic;
            repo.DrawColoredIconChar(g,ch,0,0,Color.Black, Color.Yellow, font);
            pictureBox2.Image = bmp;
        }

        private Bitmap GetColoredIcon(Color c1, Color c2)
        {
            var newbmp = (Bitmap)bmpindex.Clone();
            var pal = newbmp.Palette;
            pal.Entries[0] = c1;
            pal.Entries[1] = c2;
            newbmp.Palette = pal;
            return newbmp;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var r = new Random();
            var c = new byte[3];
            r.NextBytes(c);
            pictureBox1.BackColor = Color.FromArgb(c[0], c[1], c[2]);
        }
    }
}