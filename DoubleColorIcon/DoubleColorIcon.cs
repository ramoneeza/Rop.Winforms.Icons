using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rop.Winforms.Icons;

namespace DoubleColorIcon
{
    public class DoubleColorIcon
    {
        private readonly IEmbeddedIcons _icons;
        private const int basewidth = 128;
        private readonly Font _iconfont;
        private readonly Size _masksize;
        private readonly ConcurrentDictionary<int, Bitmap> _dic = new ConcurrentDictionary<int, Bitmap>();

        internal DoubleColorIcon(IEmbeddedIcons icons)
        {
            _icons = icons;
            _iconfont = _icons.GetFont(basewidth, FontStyle.Regular, GraphicsUnit.Pixel);
            var ch = _icons.GetChar(20);
            _masksize = TextRenderer.MeasureText(ch, _iconfont);
        }

        public Bitmap GetBaseIcon(int index)
        {
            if (_dic.TryGetValue(index, out var bmp)) return bmp;
            var ch = _icons.GetChar(index);
            var bmpmask = MakeIconMask(ch);
            if (_dic.Count > 1000)
            {
                var values=_dic.Values.ToArray();
                _dic.Clear();
                foreach (var bitmap in values)
                {
                    bitmap.Dispose();
                }
            }
            _dic.TryAdd(index, bmpmask);
            return bmpmask;
        }
        public Bitmap GetColoredIcon(int index,Color c1, Color c2)
        {
            var baseicon = GetBaseIcon(index);
            var newbmp = (Bitmap)baseicon.Clone();
            var pal = newbmp.Palette;
            pal.Entries[0] = c1;
            pal.Entries[1] = c2;
            newbmp.Palette = pal;
            return newbmp;
        }

        public Bitmap? GetColoredIcon(string code, Color c1, Color c2)
        {
            var index=_icons.GetIndex(code);
            if (index < 0) return null;
            return GetColoredIcon(index, c1, c2);
        }
        public void DrawColoredIconIndex(Graphics g,int i, float x, float y, Color c1, Color c2, Font font)
        {
            var ch = _icons.GetChar(i);
            if (ch == null) return;
            var bmp = GetColoredIcon(i, c1, c2);
            var sz = g.MeasureString(ch, font);
            var scale = sz.Height / _masksize.Height;
            var w = bmp.Width * scale;
            var h = bmp.Height * scale;
            g.DrawImage(bmp, x, y, w, h);
        }

        public void DrawColoredIconCode(Graphics g,string code, float x,float y, Color c1, Color c2,Font font)
        {
            var i=_icons.GetIndex(code);
            DrawColoredIconIndex(g,i,x,y,c1,c2,font);
        }
        public void DrawColoredIconChar(Graphics g, string ch, float x, float y, Color c1, Color c2, Font font)
        {
            var i = _icons.GetIndex(ch);
            DrawColoredIconIndex(g, i, x, y, c1, c2, font);
        }

        private Bitmap MakeIconMask(string ch)
        {
            var sz = _masksize;
            using var bmp = new Bitmap(sz.Width, sz.Height, PixelFormat.Format24bppRgb);
            using (var g = Graphics.FromImage(bmp))
            {
                g.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;
                g.Clear(Color.White);
                var br = new SolidBrush(Color.Black);
                g.DrawString(ch, _iconfont, br, 0, 0);
            }

            var gray = Bmp24To8(bmp);
            CalculateMask(gray, sz.Width);
            return ByteToIndex(gray, sz);
            // Locals

            void CalculateMask(byte[] gray, int width)
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

            byte[] Bmp24To8(Bitmap bmp)
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
                        grayvalues[i] = (byte) ((rgbValues[f] > 0) ? 1 : 0);
                        i++;
                        f += 3;
                    }
                }

                return grayvalues;
            }

            Bitmap ByteToIndex(byte[] gray, Size sz)
            {
                var bmp = new Bitmap(sz.Width, sz.Height, PixelFormat.Format8bppIndexed);
                var pal = bmp.Palette;
                var colors = new Color[] {Color.Black, Color.White, Color.Transparent};
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
        }


    }

    public static class DoubleColorIconRepository
    {
        private static readonly ConcurrentDictionary<string,DoubleColorIcon> _dic=new ConcurrentDictionary<string,DoubleColorIcon>();
        public static DoubleColorIcon GetIcon(IEmbeddedIcons icons)
        {
            var name = icons.FontName;
            if (_dic.TryGetValue(name, out var icon)) return icon;
            icon = new DoubleColorIcon(icons);
            _dic.TryAdd(name, icon);
            return icon;
        }
    }
}
