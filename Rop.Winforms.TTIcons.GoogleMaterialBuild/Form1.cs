using System.Collections.Concurrent;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO.Compression;
using System.Net;
using System.Runtime.InteropServices;
using System.Text.Json;
using SkiaSharp;
using Svg.Skia;

namespace Rop.Winforms.DuotoneIcons.GoogleMaterialBuild;

public partial class Form1 : Form
{
    public const string jsonpath = "https://raw.githubusercontent.com/material-icons/material-icons/master/data.json";
    public const string pngpath="https://material-icons.github.io/material-icons-png/png/black/";
    public const string svgpath="https://material-icons.github.io/material-icons/svg/";
    public const string tmppath="c:\\tmp\\gm\\";
    public DataIcon[] IconData { get; private set; }
    public string[] Codes { get; set; }

    public Form1()
    {
        InitializeComponent();
    }

    private async void button1_Click(object sender, EventArgs e)
    {
        // Load the json file from jsonpath
        var json = await new HttpClient().GetStringAsync(jsonpath);
        // Deserialize the json file
        var data = System.Text.Json.JsonSerializer.Deserialize<Data>(json, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        var icons = data.Icons.OrderBy(i => i.Name,StringComparer.OrdinalIgnoreCase).ToArray();
        IconData=icons;
        var cc = IconData.Select(i => i.Name).ToList();
        txtcodes.Text = string.Join(Environment.NewLine, cc);
        await File.WriteAllLinesAsync("c:\\tmp\\codes.txt", cc);
    }

    //CARGA GOOGLE MATERIAL ICONS
    private async void button2_Click(object sender, EventArgs e)
    {
        var listofresult = new ConcurrentBag<(int, string,Size, byte[])>();
        barra.Maximum = IconData.Length;
        barra.Value = 0;
        var progress = new Progress<int>();
        progress.ProgressChanged += (s, i) =>
        {
            if (i>barra.Value) barra.Value = i;
        };
        await Parallel.ForEachAsync(IconData.Select((icon,i)=>(icon,i)),new ParallelOptions(){MaxDegreeOfParallelism = 1} ,async (tuple,cs)=> 

        {
            var t = await LoadIcon(tuple.i,tuple.icon);
            listofresult.Add(t);
            ((IProgress<int>)progress).Report(tuple.i);
        });
        byte[] data;
        try
        {
            using var bmp = new MemoryStream();
            var bw = new BinaryWriter(bmp);
            var lista = listofresult.OrderBy(l => l.Item1);
            foreach(var (i, name,size, bytes) in lista)
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
        var w = new BinaryWriter(File.OpenWrite("c:\\tmp\\icons.bin"));
        w.Write(compressedData);
        w.Close();
    }

    private async Task<(int, string,Size, byte[])> LoadIcon(int i, DataIcon icon)
    {
        var name = icon.Name;
        if (File.Exists($"c:\\tmp\\gm\\{name}.png"))
        {
            return LoadIconFromCache(i,name);
        }
        return await LoadIconFromGoogle(i, icon, name);
    }

    private async Task<(int, string, Size, byte[])> LoadIconFromGoogle(int i, DataIcon icon, string name)
    {
        try
        {
            var url = SvgUrlForIcon(icon);
            var pngsize = await GetPngBytesFromSvg(name,url);
            if (pngsize!=Size.Empty)
            {
                Debug.Print($"Ok SVG {url}");
            }

            return LoadIconFromCache(i, name);
        }
        catch (Exception ex)
        {
            return (i, name, Size.Empty, Array.Empty<byte>());
        }
        
    }

    private (int, string, Size, byte[]) LoadIconFromCache(int index, string name)
    {
        using var bmp = new Bitmap($"{tmppath}{name}.png");
        var bytes2 =Rop.Bitmap2b.Converter.FromBmpTo2b(bmp);
        //var check= Rop.Bitmap2b.Converter.From2bToBmp(bytes2, bmp.Size.Width,bmp.Size.Height);
        //check.Save($"{tmppath}{name}.check.png");
        //for (var f = 0; f < bmp.Size.Height; f++)
        //{
        //    for (var g = 0; g < bmp.Size.Width; g++)
        //    {
        //        var p1= bmp.GetPixel(g, f);
        //        var p2= check.GetPixel(g, f);
        //        if (p1.A == 255) p1 = Color.White;
        //        if (p1 != Color.White && p1 != Color.Black) p1 = Color.Gray;
        //        if (p1.R != p2.R)
        //        {
        //            Debug.Print($"Error en {name} {g},{f}");
        //        }
        //    }
        //}
        return (index, name, bmp.Size, bytes2);
    }
    private byte[] PngBytesTo2ppp(byte[] bytes)
    {
        using var ms = new MemoryStream(bytes);
        using var bmp = new Bitmap(ms);
        var bytes2 =Rop.Bitmap2b.Converter.FromBmpTo2b(bmp);
        return bytes;
    }

    
    private async Task<Size>  GetPngBytesFromSvg(string name,string url)
    {
        Stream svgText;
        try
        {
            svgText = await new HttpClient().GetStreamAsync(url);
        }
        catch (Exception ex)
        {
            return Size.Empty;
        }
        // Crear un objeto SKSvg a partir del objeto SKXmlStream
        var generalscale = 4;
        using var svg = new SKSvg();
        svg.Load(svgText);
        // Crear una imagen de mapa de bits a partir del objeto SKSvg
        var size = new Size((int)svg.Picture.CullRect.Width*generalscale, (int)svg.Picture.CullRect.Height*generalscale);
        var bitmap = new SKBitmap(size.Width,size.Height);
        using (var canvas = new SKCanvas(bitmap))
        {
            using var paint = new SKPaint() { IsAntialias = false };
            canvas.Clear(SKColors.White);
            canvas.Scale(generalscale);
            canvas.DrawPicture(svg.Picture, paint);
        }
        // Guardar la imagen de mapa de bits como archivo PNG
        using (var imageStream = new FileStream($"{tmppath}{name}.png",FileMode.Create))
        {
            bitmap.Encode(imageStream, SKEncodedImageFormat.Png, 100);
        }
        return size;
    }

    private string SvgUrlForIcon(DataIcon icon)
    {
        var name = icon.Name;
        if (icon.Unsupported_Families.Contains("twotone"))
        {
            if (icon.Unsupported_Families.Contains("outline"))
                return $"{svgpath}{name}/baseline.svg";
            else
                return $"{svgpath}{name}/outline.svg";
        }
        else
            return $"{svgpath}{name}/twotone.svg";
    }
}