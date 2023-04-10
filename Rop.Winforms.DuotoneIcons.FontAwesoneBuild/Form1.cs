using SkiaSharp;
using Svg.Skia;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO.Compression;
using System.Runtime.InteropServices;

namespace Rop.Winforms.DuotoneIcons.FontAwesoneBuild
{
    public partial class Form1 : Form
    {
        private const string _directorypath = "https://www.hansomang.ca/_chboard/js/font-awesome/svgs/duotone/";
        public string[] IconData { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            // Load the json file from jsonpath
            var path = await new HttpClient().GetStringAsync(_directorypath);
            var lines = ExtractLines(path);
            IconData = lines;
            txtcodes.Text = string.Join(Environment.NewLine, lines);
            await File.WriteAllLinesAsync("c:\\tmp\\facodes.txt", lines);
        }

        private const string _start = "<tr><td valign=\"top\">&nbsp;</td><td><a href=\"";

        private string[] ExtractLines(string path)
        {
            var rawlines = path.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var res = new List<string>();
            foreach (var rawline in rawlines)
            {
                if (!rawline.StartsWith(_start)) continue;
                var charline = new string(rawline.Substring(_start.Length).TakeWhile(c => c != '"').ToArray());
                if (charline.EndsWith(".svg")) res.Add(charline);
            }
            return res.OrderBy(r => r, StringComparer.OrdinalIgnoreCase).ToArray();
        }
        // CARGA FONT AWESONE ICONS
        private async void button2_Click(object sender, EventArgs e)
        {
            var listofresult = new ConcurrentBag<(int, string,Size, byte[])>();
            barra.Maximum = IconData.Length;
            barra.Value = 0;
            var progress = new Progress<int>();
            progress.ProgressChanged += (s, i) =>
            {
                if (i > barra.Value) barra.Value = i;
            };
            await Parallel.ForEachAsync(IconData.Select((icon, i) => (icon, i)), new ParallelOptions() { MaxDegreeOfParallelism = 1 }, async (tuple, cs) =>
            {
                var t = await LoadIcon(tuple.i, tuple.icon);
                listofresult.Add(t);
                ((IProgress<int>)progress).Report(tuple.i);
            });
            byte[] data;
            try
            {
                using var bmp = new MemoryStream();
                var bw= new BinaryWriter(bmp);
                var lista = listofresult.OrderBy(l => l.Item1);
                foreach (var (i, name,size, bytes) in lista)
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
            var w = new BinaryWriter(File.OpenWrite("c:\\tmp\\faicons.bin"));
            w.Write(compressedData);
            w.Close();
        }

        private async Task<(int, string,Size, byte[])> LoadIcon(int index, string icon)
        {
            var name = icon.Replace(".svg", "");
            if (!File.Exists($"c:\\tmp\\fa\\{name}.png"))
            {
                string url = _directorypath + icon;
                var hay = await GetPng(url,name);
                if (hay)
                {
                    Debug.Print($"Ok PNG {url}");
                }
                else
                {
                    return (index, name,Size.Empty, new byte[0]);
                }
            }
            return LoadIconFromCache(index, name);
        }

        private (int, string, Size, byte[]) LoadIconFromCache(int index, string name)
        {
            using var bmp = new Bitmap($"c:\\tmp\\fa\\{name}.png");
            var bytes2 = Rop.Bitmap2b.Converter.FromBmpTo2b(bmp);
            return (index, name, bmp.Size, bytes2);
        }
        private async Task<bool> GetPng(string url,string name)
        {
            Stream svgText;
            try
            {
                svgText = await new HttpClient().GetStreamAsync(url);
            }
            catch (Exception ex)
            {
                return false;
            }
            // Crear un objeto SKSvg a partir del objeto SKXmlStream
            using var svg = new SKSvg();
            svg.Load(svgText);
            // Crear una imagen de mapa de bits a partir del objeto SKSvg
            var size = new Size((int)svg.Picture.CullRect.Width, (int)svg.Picture.CullRect.Height);
            var bitmap = new SKBitmap(size.Width,size.Height);
            using (var canvas = new SKCanvas(bitmap))
            {
                using var paint = new SKPaint() { IsAntialias = false };
                canvas.Clear(SKColors.White);
                canvas.Scale(1);
                canvas.DrawPicture(svg.Picture, paint);
            }
            // Guardar la imagen de mapa de bits como archivo PNG
            using (var imageStream = new FileStream($"c:\\tmp\\fa\\{name}.png",FileMode.Create))
            {
                bitmap.Encode(imageStream, SKEncodedImageFormat.Png, 100);
            }
            return true;
        }
    }
}