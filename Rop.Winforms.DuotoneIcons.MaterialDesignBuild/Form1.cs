using System.Collections.Concurrent;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.IO.Compression;
using Rop.Bitmap2b;
namespace Rop.Winforms.DuotoneIcons.MaterialDesignBuild
{
    public partial class Form1 : Form
    {
        public const string DouCachePath = "c:\\tmp\\md\\";
        public const int BaseSize = 96;
        public const int Zoom = 6;

        public PreIcon[] Icons { get; set; }
        public Font IconFont { get; set; }

        public Form1()
        {
            InitializeComponent();
            MakeIcons();
        }

        private void MakeIcons()
        {
            IconFont = materialDesignBank1.Bank.GetFont(96, FontStyle.Regular, GraphicsUnit.Pixel);
            var codes = materialDesignBank1.Bank.Codes.OrderBy(c => c, StringComparer.OrdinalIgnoreCase).ToArray();
            Icons = new PreIcon[codes.Length];
            Parallel.ForEach(Enumerable.Range(0, codes.Length), (f, _) =>
            {
                var icon = CreateIcon(f, codes[f]);
                Icons[f] = icon;
            });
            lst.DataSource = Icons;
            lst.DisplayMember = "Name";
            lst.ValueMember = "Name";
        }

        private PreIcon CreateIcon(int i, string code)
        {
            if (File.Exists(DouCachePath + code + ".png"))
            {
                return CreateIconFromCache(i, code);
            }
            else
            {
                if (File.Exists(DouCachePath + code + ".bn.png"))
                {
                    return LoadDefaultIconFromCache(i, code);
                }
                else
                    return CreateDefaultIcon(i, code);
            }
        }

        private PreIcon CreateDefaultIcon(int i, string code)
        {
            var bmp = new Bitmap(96, 96);
            using var g = Graphics.FromImage(bmp);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.Clear(Color.Transparent);
            var cad2 = materialDesignBank1.Bank.GetChar(code);
            var br = new SolidBrush(Color.Black);
            var sz = g.MeasureString(cad2, IconFont, PointF.Empty, StringFormat.GenericTypographic);
            g.DrawString(cad2, IconFont, br, 0, 0, StringFormat.GenericTypographic);
            bmp.Save(DouCachePath + code + ".bn.png");
            return new PreIcon
            {
                Name = code,
                Index = i,
                Icon = bmp,
                IsDuotone = false,
                Size = bmp.Size,
            };
        }

        private PreIcon CreateIconFromCache(int i, string code)
        {
            using FileStream fs = new FileStream(DouCachePath + code + ".png", FileMode.Open, FileAccess.Read);
            var bmp = Image.FromStream(fs) as Bitmap;
            fs.Close();
            return new PreIcon
            {
                Name = code,
                Index = i,
                Icon = bmp,
                IsDuotone = true,
                Size = new SizeF(bmp.Width, bmp.Height),
            };
        }

        private PreIcon LoadDefaultIconFromCache(int i, string code)
        {
            var bmp = Image.FromFile(DouCachePath + code + ".bn.png") as Bitmap;
            return new PreIcon
            {
                Name = code,
                Index = i,
                Icon = bmp,
                IsDuotone = false,
                Size = new SizeF(bmp.Width, bmp.Height),
            };
        }

        private void lst_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (e.Index < 0) return;
            var icon = Icons[e.Index];
            var sz = icon.Size;
            var x = e.Bounds.X;
            var y = e.Bounds.Y;
            var h = e.Bounds.Height;
            var iw = h * icon.Size.Width / icon.Size.Height;

            var br = new SolidBrush(icon.IsDuotone ? Color.Black : Color.Gray);
            e.Graphics.DrawImage(icon.Icon, x, y, iw, h);
            e.Graphics.DrawString(icon.Name, Font, br, x + iw + 4, y);
        }

        private PreIcon? _currentIcon = null;

        public PreIcon? CurrentIcon
        {
            get => _currentIcon;
            set
            {
                _currentIcon = value;
                AjCurrentIcon();
            }
        }

        private DMode _dMode;

        public DMode DMode
        {
            get => _dMode;
            private set
            {
                _dMode = value;
                bmodebox.ForeColor = value == DMode.Box ? Color.Black : Color.Gray;
                bmodecircle.ForeColor = value == DMode.Circle ? Color.Black : Color.Gray;
                bmodeline.ForeColor = value == DMode.Line ? Color.Black : Color.Gray;
                bmmodefill.ForeColor = value == DMode.Fill ? Color.Black : Color.Gray;
            }
        }

        private void AjCurrentIcon()
        {
            if (pbicon.Image != null)
            {
                pbicon.Image.Dispose();
                pbicon.Image = null;
            }

            if (_currentIcon == null)
            {
                return;
            }
            var bmp = new Bitmap(BaseSize * Zoom, BaseSize * Zoom);
            using var g = Graphics.FromImage(bmp);
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = PixelOffsetMode.None;
            g.Clear(Color.White);
            g.DrawImage(_currentIcon?.Icon!, 0, 0, BaseSize * Zoom, BaseSize * Zoom);
            pbicon.SizeMode = PictureBoxSizeMode.Normal;
            pbicon.Image = bmp;
            pbicon.Size = new Size(BaseSize * Zoom, BaseSize * Zoom);
        }

        private void lst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lst.SelectedIndex == -1)
            {
                CurrentIcon = null;
            }
            var icon = Icons[lst.SelectedIndex];
            CurrentIcon = icon;
        }

        private void pbicon_MouseClick(object sender, MouseEventArgs e)
        {
            if (CurrentIcon == null) return;
            var shift = Control.ModifierKeys.HasFlag(Keys.Shift);
            var ctrl = Control.ModifierKeys.HasFlag(Keys.Control);
            if (e.Button == MouseButtons.Left)
            {
                if (firstPoint == null) return;
                lastPoint = e.Location;
                var rx1 = firstPoint.Value.X / Zoom;
                var ry1 = firstPoint.Value.Y / Zoom;
                var rx2 = lastPoint.Value.X / Zoom;
                var ry2 = lastPoint.Value.Y / Zoom;

                switch (DMode)
                {
                    case DMode.Box:
                        if (rx2 < rx1)
                        {
                            (rx1, rx2) = (rx2, rx1);
                        }

                        if (ry2 < ry1)
                        {
                            (ry1, ry2) = (ry2, ry1);
                        }
                        DrawBox(rx1, rx2, ry1, ry2, shift, ctrl);
                        break;

                    case DMode.Circle:
                        if (rx2 < rx1)
                        {
                            (rx1, rx2) = (rx2, rx1);
                        }

                        if (ry2 < ry1)
                        {
                            (ry1, ry2) = (ry2, ry1);
                        }
                        DrawCircle(rx1, rx2, ry1, ry2, shift, ctrl);
                        break;

                    case DMode.Line:
                        DrawLine(rx1, rx2, ry1, ry2, shift, ctrl);
                        break;
                    case DMode.Fill:
                        fill(rx2, ry2, shift, ctrl);
                        break;
                }

                firstPoint = null;
                lastPoint = null;
                CurrentIcon.Dirty = true;
                AjCurrentIcon();
            }
        }

        private void DrawLine(int rx1, int rx2, int ry1, int ry2, bool shift, bool ctrl)
        {

            List<Point> points = new List<Point>();

            float m = (float)(ry2 - ry1) / (float)(rx2 - rx1);
            if (Math.Abs(m) <= 1)
            {
                int x = Math.Min(rx1, rx2);
                int maxX = Math.Max(rx1, rx2);
                while (x <= maxX)
                {
                    int y = (int)Math.Round(ry1 + m * (x - rx1));
                    points.Add(new Point(x, y));
                    x++;
                }
            }
            else
            {
                int y = Math.Min(ry1, ry2);
                int maxY = Math.Max(ry1, ry2);
                while (y <= maxY)
                {
                    int x = (int)Math.Round(rx1 + (y - ry1) / m);
                    points.Add(new Point(x, y));
                    y++;
                }
            }

            foreach (var point in points)
            {
                var x = point.X;
                var y = point.Y;
                var c = CurrentIcon.Icon.GetPixel(x, y);
                c = ChangePixelColor(shift, ctrl, c);
                CurrentIcon.Icon.SetPixel(x, y, c);
            }
        }

        private void DrawBox(int rx1, int rx2, int ry1, int ry2, bool shift, bool ctrl)
        {
            for (var rx = rx1; rx <= rx2; rx++)
            {
                for (var ry = ry1; ry <= ry2; ry++)
                {
                    var c = CurrentIcon.Icon.GetPixel(rx, ry);
                    c = ChangePixelColor(shift, ctrl, c);
                    CurrentIcon.Icon.SetPixel(rx, ry, c);
                }
            }
        }

        private static Color ChangePixelColor(bool shift, bool ctrl, Color c)
        {
            var g = (c.A == 0) ? 255 : (c.R + c.G + c.B) / 3;
            if (shift)
            {
                c = (g != 0) ? (ctrl ? Color.Transparent : Color.Gray) : c;
            }
            else
            {
                switch (g)
                {
                    case 255:
                        c = Color.Gray;
                        break;

                    case 0:
                        break;

                    default:
                        c = Color.Transparent;
                        break;
                }
            }

            return c;
        }

        private void DrawCircle(int rx1, int rx2, int ry1, int ry2, bool shift, bool ctrl)
        {
            var cx = (rx1 + rx2) / 2;
            var cy = (ry1 + ry2) / 2;
            var r = Math.Max(rx2 - rx1, ry2 - ry1) / 2;
            for (var rx = rx1; rx <= rx2; rx++)
            {
                for (var ry = ry1; ry <= ry2; ry++)
                {
                    var dx = rx - cx;
                    var dy = ry - cy;
                    var d = Math.Sqrt(dx * dx + dy * dy);
                    if (d > r) continue;
                    var c = CurrentIcon.Icon.GetPixel(rx, ry);
                    c = ChangePixelColor(shift, ctrl, c);
                    CurrentIcon.Icon.SetPixel(rx, ry, c);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (CurrentIcon == null) return;
            //if (!CurrentIcon.Dirty)
            //{
            //    if (MessageBox.Show("No changes made, save anyway?", "Save", MessageBoxButtons.YesNo) == DialogResult.No)
            //        return;
            //}
            CurrentIcon.Icon.Save(DouCachePath + CurrentIcon.Name + ".png");
            var l = CreateIconFromCache(CurrentIcon.Index, CurrentIcon.Name);
            Icons[CurrentIcon.Index] = l;
            lst.Invalidate();
            CurrentIcon = l;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (CurrentIcon == null) return;
            var l = CreateIcon(CurrentIcon.Index, CurrentIcon.Name);
            Icons[CurrentIcon.Index] = l;
            lst.Invalidate();
            CurrentIcon = l;
        }

        private Point? firstPoint = null;
        private Point? lastPoint = null;

        private void pbicon_MouseDown(object sender, MouseEventArgs e)
        {
            if (CurrentIcon == null) return;
            if (e.Button == MouseButtons.Left)
            {
                firstPoint = new Point(e.Location.X + 1, e.Location.Y + 1);
                lastPoint = firstPoint;
            }
            else
            {
                firstPoint = null;
            }
            pbicon.Invalidate();
        }

        private void pbicon_MouseMove(object sender, MouseEventArgs e)
        {
            if (CurrentIcon == null) return;
            if (e.Button == MouseButtons.Left && firstPoint != null)
            {
                lastPoint = new Point(e.Location.X + 1, e.Location.Y + 1);
                pbicon.Invalidate();
            }
        }

        private void pbicon_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(pbicon.BackColor);
            e.Graphics.DrawImage(pbicon.Image, 0, 0);
            if (CurrentIcon == null) return;
            if (firstPoint == null || lastPoint == null) return;

            var rx1 = (firstPoint.Value.X / Zoom) * Zoom;
            var ry1 = (firstPoint.Value.Y / Zoom) * Zoom;
            var rx2 = (lastPoint.Value.X / Zoom) * Zoom;
            var ry2 = (lastPoint.Value.Y / Zoom) * Zoom;

            switch (DMode)
            {
                case DMode.Box:
                    if (rx2 < rx1)
                    {
                        (rx1, rx2) = (rx2, rx1);
                    }

                    if (ry2 < ry1)
                    {
                        (ry1, ry2) = (ry2, ry1);
                    }

                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(128, 255, 0, 0)), rx1, ry1, rx2 - rx1 + 1, ry2 - ry1 + 1);
                    break;

                case DMode.Line:
                    e.Graphics.DrawLine(new Pen(Color.FromArgb(128, 255, 0, 0)), rx1, ry1, rx2, ry2);
                    break;

                case DMode.Circle:
                    if (rx2 < rx1)
                    {
                        (rx1, rx2) = (rx2, rx1);
                    }

                    if (ry2 < ry1)
                    {
                        (ry1, ry2) = (ry2, ry1);
                    }
                    var c = new Point(rx1 + (rx2 - rx1) / 2, ry1 + (ry2 - ry1) / 2);
                    var r = Math.Max(rx2 - rx1, ry2 - ry1) / 2;
                    e.Graphics.DrawEllipse(new Pen(Color.FromArgb(128, 255, 0, 0)), c.X - r, c.Y - r, r * 2, r * 2);
                    break;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DMode = DMode.Box;
        }

        private void bmodeline_Click(object sender, EventArgs e)
        {
            DMode = DMode.Line;
        }

        private void bmodecircle_Click(object sender, EventArgs e)
        {
            DMode = DMode.Circle;
        }

        private List<Point>? MaskPoints = null;
        private void button3_Click_1(object sender, EventArgs e)
        {
            if (CurrentIcon == null) return;
            MaskPoints = new List<Point>();
            for (var x = 0; x < CurrentIcon.Icon.Width; x++)
            {
                for (var y = 0; y < CurrentIcon.Icon.Height; y++)
                {
                    var c = CurrentIcon.Icon.GetPixel(x, y);
                    if (c.A == 0 || c.R == 255) continue;
                    MaskPoints.Add(new Point(x, y));
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (CurrentIcon == null) return;
            if (MaskPoints == null) return;
            foreach (var p in MaskPoints)
            {
                var c = CurrentIcon.Icon.GetPixel(p.X, p.Y);
                if (c.A == 255 && c.R == 0) continue;
                CurrentIcon.Icon.SetPixel(p.X, p.Y, Color.Gray);
            }

            CurrentIcon.Dirty = true;
            CurrentIcon = CurrentIcon;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DMode = DMode.Fill;
        }

        private void fill(int rx2, int ry2, bool shift, bool ctrl)
        {
            if (CurrentIcon == null) return;
            var c = CurrentIcon.Icon.GetPixel(rx2, ry2);
            if (!IsWhite(c)) return;
            var q = new Queue<Point>();
            q.Enqueue(new Point(rx2, ry2));
            while (q.Count > 0)
            {
                var p = q.Dequeue();
                var cc = CurrentIcon.Icon.GetPixel(p.X, p.Y);
                if (!IsWhite(cc)) continue;
                CurrentIcon.Icon.SetPixel(p.X, p.Y, Color.Gray);
                if (p.X > 0) q.Enqueue(new Point(p.X - 1, p.Y));
                if (p.Y > 0) q.Enqueue(new Point(p.X, p.Y - 1));
                if (p.X < CurrentIcon.Icon.Width - 1) q.Enqueue(new Point(p.X + 1, p.Y));
                if (p.Y < CurrentIcon.Icon.Height - 1) q.Enqueue(new Point(p.X, p.Y + 1));
            }
            CurrentIcon.Dirty = true;
            CurrentIcon = CurrentIcon;
        }

        private static bool IsWhite(Color c)
        {
            return c.A == 0 || c.R == 255;
        }

        private static bool IsBlack(Color c)
        {
            return c.A == 255 && c.R == 0;
        }
        private static bool IsGray(Color c)
        {
            return c.A == 255 && c.R > 0 && c.R < 255;
        }

        private static bool IsWhiteOrGray(Color c) => IsGray(c) || IsWhite(c);

        private async void button5_Click_1(object sender, EventArgs e)
        {
            var listofresult = new ConcurrentBag<(int, string, Size, byte[])>();
            barra.Maximum = Icons.Length;
            barra.Value = 0;
            var progress = new Progress<int>();
            progress.ProgressChanged += (s, i) =>
            {
                if (i > barra.Value) barra.Value = i;
            };
            await Parallel.ForEachAsync(Icons.Select((icon, i) => (icon, i)), new ParallelOptions() { MaxDegreeOfParallelism = 1 }, async (tuple, cs) =>

            {
                var t = await LoadIcon(tuple.i, tuple.icon);
                listofresult.Add(t);
                ((IProgress<int>)progress).Report(tuple.i);
            });
            byte[] data;
            try
            {
                using var bmp = new MemoryStream();
                var bw = new BinaryWriter(bmp);
                var lista = listofresult.OrderBy(l => l.Item1);
                foreach (var (i, name, size, bytes) in lista)
                {
                    var finalbytes = bytes;
                    if (finalbytes.Length == 0)
                    {
                        Debug.Print($"Icono {name} VACIO");
                        finalbytes = new byte[0];
                    }
                    if (finalbytes.Length > 0)
                    {
                        bw.Write((Int32)finalbytes.Length);
                        bw.Write((ushort)size.Width);
                        bw.Write((ushort)size.Height);
                        bw.Write(finalbytes);
                    }
                    else
                    {
                        bw.Write((Int32)0);
                    }
                }
                bw.Flush();
                bmp.Position = 0;
                data = bmp.ToArray();
            }
            catch (Exception ex)
            {
                throw;
            }

            byte[] compressedData;
            using (var output = new MemoryStream())
            {
                using (var compressor = new DeflateStream(output, CompressionMode.Compress))
                {
                    compressor.Write(data, 0, data.Length);
                }
                compressedData = output.ToArray();
            }
            var w = new BinaryWriter(File.OpenWrite("c:\\tmp\\mdicons.bin"));
            w.Write(compressedData);
            w.Close();
            var cc = Icons.Select(i => i.Name).ToList();
            await File.WriteAllLinesAsync("c:\\tmp\\mdcodes.txt", cc);
        }

        private async Task<(int, string, Size, byte[])> LoadIcon(int index, PreIcon icon)
        {
            var name=icon.Name;
            var bmp = icon.Icon;
            var bytes2 =Rop.Bitmap2b.Converter.FromBmpTo2b(bmp);
            return (index, name, bmp.Size, bytes2);
        }
    }

    public class PreIcon
    {
        public string Name { get; set; }
        public int Index { get; set; }
        public Bitmap Icon { get; set; }
        public bool IsDuotone { get; set; }
        public SizeF Size { get; set; }
        public bool Dirty { get; set; } = false;
    };

    public enum DMode
    {
        Box,
        Line,
        Circle,
        Fill
    }
}