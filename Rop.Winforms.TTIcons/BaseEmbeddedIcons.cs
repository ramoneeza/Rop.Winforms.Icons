using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Rop.Winforms.DuotoneIcons.Helpers;

namespace Rop.Winforms.DuotoneIcons;

/// <summary>
/// Base class for embedded icons
/// </summary>
public abstract class BaseEmbeddedIcons : IEmbeddedIcons
{
    private readonly DuoToneIcon[] _icons;

    /// <summary>
    /// For each name, the code and the character
    /// </summary>
    private readonly ConcurrentDictionary<string, DuoToneIcon> CharSet = new();

    private readonly ConcurrentDictionary<int, Bitmap> _bitmaps = new();

    /// <summary>
    /// Name of the icon Font
    /// </summary>
    public string FontName { get; }

    public string[] Codes { get; }

    public int BaseSize { get; }

    /// <summary>
    /// FontFamily
    /// </summary>
    /// <summary>
    /// Number of codes
    /// </summary>
    public int Count => _icons.Length;

    public DuoToneIcon? GetIcon(string nameorcode)
    {
        return CharSet.TryGetValue(nameorcode ?? "", out var t) ? t : null;
    }

    public DuoToneIcon? GetIcon(int index)
    {
        return (index < 0 || index >= Count) ? null : _icons[index];
    }

    /// <summary>
    ///  Index of codename in the list
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public int SearchIndex(string name)
    {
        var inicio = 0;
        var final = Count - 1;
        while (inicio <= final)
        {
            var medio = (inicio + final) / 2;
            var c = StringComparer.OrdinalIgnoreCase.Compare(_icons[medio].Name, name);
            if (c == 0) return medio;
            if (c < 0) inicio = medio + 1;
            else final = medio - 1;
        }
        return -1;
    }
    
    /// <summary>
    /// Get char from code
    /// </summary>
    public string GetChar(string code) => GetIcon(code)?.TheChar ?? "";

    /// <summary>
    /// Get name from index
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public string GetName(int index) => GetIcon(index)?.Name ?? "";

    /// <summary>
    /// Get Char from index
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public string GetChar(int index) => GetIcon(index)?.TheChar ?? "";

    public Bitmap GetBaseBitmap(string nameorcode)
    {
        return GetBaseBitmap(GetIcon(nameorcode));
    }

    public Bitmap GetBaseBitmap(int index)
    {
        return GetBaseBitmap(GetIcon(index));
    }
    public Bitmap GetBaseBitmap(DuoToneIcon icon)
    {
        if (icon == null) return null;
        var index = icon.Index;
        if (_bitmaps.TryGetValue(index, out var b)) return b;
        b = IntGetBitmap(icon);
        _bitmaps[index] = b;
        return b;
    }

    private Bitmap IntGetBitmap(DuoToneIcon icon)
    {
        var size=icon.Size;
        var bmp = Rop.Bitmap2b.Converter.From2bToBmp(icon.Data, icon.Size.Width, icon.Size.Height);
        //bmp.Save($"c:\\tmp\\{icon.Name}.test.png",ImageFormat.Png);
        return bmp;
    }

    public Bitmap GetBitmap(DuoToneIcon icon, DuoToneColor color)
    {
        var b = GetBaseBitmap(icon);
        if (b == null) return null;
        var nb = (Bitmap)b.Clone();
        var p = nb.Palette;
        p.Entries[1] = color.Color1;
        p.Entries[2] = color.Color2;
        nb.Palette = p;
        return nb;
    }
    public Bitmap GetBitmap(int index, DuoToneColor color)
    {
        return GetBitmap(GetIcon(index),color);
    }

    public Bitmap GetBitmap(string nameorcode, DuoToneColor color)
    {
        return GetBitmap(GetIcon(nameorcode),color);
    }

    public SizeF MeasureIcon(string code, float height)
    {
        var icon = GetIcon(code);
        if (icon == null) return SizeF.Empty;
        var w= height * icon.WidthUnit;
        return new SizeF(w, height);
    }

    public float DrawTTIcon(Graphics gr,string code,DuoToneColor iconcolor, float x, float y, float height)
    {
        var icon = GetIcon(code);
        if (icon == null) return 0f;
        var bmp = GetBitmap(icon, iconcolor);
        if (bmp == null) return 0f;
        var w= height * icon.WidthUnit;
        var a = gr.InterpolationMode;
        gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
        gr.DrawImage(bmp, x, y, w,height);
        gr.InterpolationMode = a;
        return w;
    }
    public void DrawTTIconFit(Graphics gr,string code,DuoToneColor iconcolor, float x, float y, float size)
    {
        var icon = GetIcon(code);
        if (icon == null) return;
        var bmp = GetBitmap(icon, iconcolor);
        if (bmp == null) return;
        float h;
        float w;
        if (icon.WidthUnit > 1)
        {
            h= size / icon.WidthUnit;
            w= size;
            y= y + (size - h) / 2;
        }
        else
        {
            w= size * icon.WidthUnit;
            h= size;
            x= x + (size - w) / 2;
        }
        
        var a = gr.InterpolationMode;
        gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
        gr.DrawImage(bmp, x, y, w,h);
        gr.InterpolationMode = a;
    }

    public RectangleF MeasureIcon(Graphics gr, string code, Font font, float scale)
    {
        var icon = GetIcon(code);
        if (icon == null) return RectangleF.Empty;
        var b=gr.MeasureBaseIcon(font,scale);
        return new RectangleF(b.Location,new SizeF(b.Height*icon.WidthUnit,b.Height));
    }


    protected BaseEmbeddedIcons(string fontname, string iconresourcename, string codesresourcename)
    {
        FontName = fontname;
        Codes = GetResourceStr(this.GetType(), codesresourcename);

        var compressed = GetResource(this.GetType(), iconresourcename);

        byte[] uncompressedData;
        using (var input = new MemoryStream(compressed))
        {
            using (var decompressor = new DeflateStream(input, CompressionMode.Decompress))
            {
                using (var output = new MemoryStream())
                {
                    decompressor.CopyTo(output);
                    uncompressedData = output.ToArray();
                }
            }
        }
        var i = 0;
        _icons = new DuoToneIcon[Codes.Length];
        using var iconmstream = new MemoryStream(uncompressedData);
        using var iconstream = new BinaryReader(iconmstream);
        BaseSize = 0;
        while(i<Codes.Length)
        {
            var tt=Codes[i];
            var bytesize=iconstream.ReadInt32();
            if (bytesize == 0)
            {
                i++;
                continue;
            }
            var w =iconstream.ReadUInt16();
            var h=iconstream.ReadUInt16();
            uint cu32 = (uint)(0x000F0000 + i);
            var ch = FromUTf32(cu32);
            var span = iconstream.ReadBytes(bytesize);
            var icon = new DuoToneIcon(tt, ch, i, span,new Size(w,h));
            _icons[i] = icon;
            CharSet[tt] = icon;
            CharSet[ch] = icon;
            if (BaseSize<h) BaseSize = h;
            i++;
        }
        // Locals fun
        string FromUTf32(uint u)
        {
            byte[] bytes=BitConverter.GetBytes(u);
            char[] chars = Encoding.UTF32.GetChars(bytes);
            return new string(chars);
        }
    }

    private static byte[] GetResource(Type t, string name)
    {
        var assembly = t.Assembly;
        string resourceName = assembly.GetManifestResourceNames().FirstOrDefault(str => str.EndsWith(name, StringComparison.OrdinalIgnoreCase));
        if (resourceName == null) return null;
        using Stream stream = assembly.GetManifestResourceStream(resourceName);
        if (stream is null) return null;
        using MemoryStream mstream = new MemoryStream();
        stream.CopyTo(mstream);
        return mstream.ToArray();
    }

    private static string[] GetResourceStr(Type t, string name)
    {
        var assembly = t.Assembly;
        string resourceName = assembly.GetManifestResourceNames().FirstOrDefault(str => str.EndsWith(name, StringComparison.OrdinalIgnoreCase));
        if (resourceName == null) return null;
        using Stream stream = assembly.GetManifestResourceStream(resourceName);
        if (stream is null) return null;
        using MemoryStream mstream = new MemoryStream();
        using var reader = new StreamReader(stream);
        var content = reader.ReadToEnd();
        return content.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
    }
}