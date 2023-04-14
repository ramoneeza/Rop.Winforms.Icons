using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Rop.Winforms.DuotoneIcons
{
    public static class Converter
    {
        public static byte[] From32bTo2b(byte[] bmp)
        {
            var res=new byte[bmp.Length/16]; // 4 pixels per byte
            var i = 0;
            var f = 0;
            while(i<bmp.Length)
            {
                var g0 = GetGray2b(i);
                i += 4;
                var g1 = GetGray2b(i);
                i += 4;
                var g2 = GetGray2b(i);
                i += 4;
                var g3 = GetGray2b(i);
                i += 4;
                var bytepack = g0 + g1 * 4 + g2 * 16 + g3 * 64;
                res[f] = (byte)bytepack;
                f++;
            }
            return res;
            // Local functions
            int GetGray2b(int i)
            {
                var a = bmp[i + 3];
                if (a!=255) return 0;
                var gr = (bmp[i + 0] + bmp[i + 1] + bmp[i + 2]) / 3;
                if (gr==0) return 1;
                if (gr==255) return 0;
                return 2;
            }

        }
        public static byte[] From2bTo32b(byte[] bmp)
        {
            var res=new byte[bmp.Length*16];
            var f = 0;
            var i = 0;
            var ccs = new byte[][]
            {
                new byte[]{0,0,0,0},
                new byte[]{0,0,0,255},
                new byte[]{128,128,128,255},
                new byte[]{255,255,255,255}
            };
            while (f < bmp.Length)
            {
                var v= bmp[f];
                var bytepack0 = (v&0x3);
                var bytepack1 = (v&0xC)>>2;
                var bytepack2 = (v&0x30)>>4;
                var bytepack3 = (v&0xC0)>>6;
                res[i] = ccs[bytepack0][0];
                res[i+1] = ccs[bytepack0][1];
                res[i+2] = ccs[bytepack0][2];
                res[i+3] = ccs[bytepack0][3];
                res[i+4] = ccs[bytepack1][0];
                res[i+5] = ccs[bytepack1][1];
                res[i+6] = ccs[bytepack1][2];
                res[i+7] = ccs[bytepack1][3];
                res[i+8] = ccs[bytepack2][0];
                res[i+9] = ccs[bytepack2][1];
                res[i+10] = ccs[bytepack2][2];
                res[i+11] = ccs[bytepack2][3];
                res[i+12] = ccs[bytepack3][0];
                res[i+13] = ccs[bytepack3][1];
                res[i+14] = ccs[bytepack3][2];
                res[i+15] = ccs[bytepack3][3];
                i += 16;
                f++;
            }
            return res;
        }
        public static byte[] From2bTo4b(byte[] bmp)
        {
            var res=new byte[bmp.Length*2];
            var f = 0;
            var i = 0;
            while (f<bmp.Length)
            {
                var v= bmp[f];
                var bytepack0 = (v&0x3);
                var bytepack1 = (v&0xC)>>2;
                var bytepack2 = (v&0x30)>>4;
                var bytepack3 = (v&0xC0)>>6;
                res[i] = (byte)(bytepack0+bytepack1*16);
                res[i+1] = (byte)(bytepack2+bytepack3*16);
                i += 2;
                f++;
            }
            return res;
        }

        public static byte[] From4bTo2b(byte[] bmp)
        {
            var res=new byte[bmp.Length/2];
            var f = 0;
            var i = 0;
            while (f < bmp.Length)
            {
                var v= bmp[f];
                var v2= bmp[f+1];
                var bytepack0 = (v&0x3);
                var bytepack1 = (v&0x30)>>4;
                var bytepack2 = (v2&0x3);
                var bytepack3 = (v2&0x30)>>4;
                res[i] = (byte)(bytepack0+bytepack1*4+bytepack2*16+bytepack3*64);
                i += 1;
                f+=2;
            }
            return res;
        }

        public static byte[] FromBmpTo32b(Bitmap bmp)
        {
            var sz=bmp.Size;
            var w = sz.Width * sz.Height*4;
            var res = new byte[w];
            var bmpData=bmp.LockBits(new System.Drawing.Rectangle(0,0,sz.Width,sz.Height),System.Drawing.Imaging.ImageLockMode.ReadOnly,PixelFormat.Format32bppArgb);
            if (bmpData.Stride == sz.Width*4)
            {
                var ptr = bmpData.Scan0;
                Marshal.Copy(ptr, res, 0, w);
            }
            else
            {
                var ptr = bmpData.Scan0;
                var i = 0;
                for (var y = 0; y < sz.Height; y++)
                {
                    Marshal.Copy(ptr, res, i, sz.Width*4);
                    i += sz.Width*4;
                    ptr += bmpData.Stride;
                }
            }
            bmp.UnlockBits(bmpData);
            return res;
        }
        public static byte[] FromBmpTo2b(System.Drawing.Bitmap bmp)
        {
            var res = FromBmpTo32b(bmp);
            return From32bTo2b(res);
        }

        public static System.Drawing.Bitmap From2bToBmp(byte[] bmp, int width, int height)
        {
            var res = From2bTo4b(bmp);
            var b = new System.Drawing.Bitmap(width, height, PixelFormat.Format4bppIndexed);
            var pal = b.Palette;
            pal.Entries[0] = System.Drawing.Color.Transparent;
            pal.Entries[1] = System.Drawing.Color.Black;
            pal.Entries[2] = System.Drawing.Color.Gray;
            b.Palette = pal;
            var rect = new System.Drawing.Rectangle(0, 0, width, height);
            var bmpData = b.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format4bppIndexed);
            if (bmpData.Stride == width/2)
            {
                var ptr = bmpData.Scan0;
                Marshal.Copy(res, 0, ptr, res.Length);
            }
            else
            {
                var ptr = bmpData.Scan0;
                var i = 0;
                for (var y = 0; y < height; y++)
                {
                    Marshal.Copy(res, i, ptr, width/2);
                    i += width / 2;
                    ptr += bmpData.Stride;
                }
            }
            b.UnlockBits(bmpData);
            return b;
        }
    }
}