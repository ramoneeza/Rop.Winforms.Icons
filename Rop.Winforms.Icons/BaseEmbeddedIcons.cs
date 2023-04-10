using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace Rop.Winforms.Icons;


internal class IconCodes
{
    public string Code { get; }
    public string TheChar { get; }
    public int Index { get; }
    public IconCodes(string code, string theChar, int index)
    {
        Code = code;
        TheChar = theChar;
        Index = index;
    }
}

/// <summary>
/// Base class for embedded icons
/// </summary>
public abstract class BaseEmbeddedIcons : IEmbeddedIcons
{
    /// <summary>
    /// For each name, the code and the character
    /// </summary>
    private readonly ConcurrentDictionary<string,IconCodes> CharSet = new();

    /// <summary>
    /// Name of the icon Font
    /// </summary>
    public string FontName=>FontFamily.Name;

    /// <summary>
    /// FontFamily
    /// </summary>
    public FontFamily FontFamily { get; private set; }

    /// <summary>
    /// All Codes
    /// </summary>
    public IReadOnlyList<string> Codes { get; }

    /// <summary>
    /// Number of codes
    /// </summary>
    public int Count => Codes.Count;

    private IconCodes GetIconCodes(string indexornameorcode)
    {
        return CharSet.TryGetValue(indexornameorcode ?? "", out var t) ? t :null;
    }

    /// <summary>
    ///  Index of codename in the list
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public int GetIndex(string name)
    {
        return GetIconCodes(name)?.Index ?? -1;
    }

    /// <summary>
    /// Get code from name
    /// </summary>
    public string GetCode(string name) =>GetIconCodes(name)?.Code??"";

    /// <summary>
    /// Get char from code
    /// </summary>
    public string GetChar(string code) =>GetIconCodes(code)?.TheChar??"";

    /// <summary>
    /// Get name from index
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public string GetName(int index) =>(index<0||index>=Count)?"":Codes[index];

    /// <summary>
    /// Get code from index
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public string GetCode(int index) => GetCode(GetName(index));

    /// <summary>
    /// Get Char from index
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public string GetChar(int index) => GetChar(GetName(index));
    /// <summary>
    /// Get Font from Icons
    /// </summary>
    /// <param name="size"></param>
    /// <returns></returns>
    public Font GetFont(float size) => new Font(FontFamily, size);
    /// <summary>
    /// Get Font from Icons
    /// </summary>
    /// <param name="size"></param>
    /// <param name="style"></param>
    /// <returns></returns>
    public Font GetFont(float size, FontStyle style) => new Font(FontFamily, size, style);
    /// <summary>
    /// Get Font from Icons
    /// </summary>
    public Font GetFont(float size, FontStyle style, GraphicsUnit unit) => new Font(FontFamily, size, style, unit);

    protected BaseEmbeddedIcons(string resourcename,PrivateFontCollection pfc)
    {
        var t = this.GetType();
        FontFamily = LoadFont(t, resourcename,pfc);
        var fields = t.GetFields(BindingFlags.Public|BindingFlags.Static).Where(f=>f.FieldType==typeof(string)).Select(f=>(f.Name,(f.GetValue(null) as string)??"")).Where(f=>f.Item2.Length<=2).ToDictionary(f=>f.Name,f=>f.Item2);
        var lstcodes = new List<string>();
        var precodes= new List<(string,string,string)>();
        foreach (var kv in fields)
        {
            var k = kv.Key;
            if (k.StartsWith("Char_")) continue;
            var vcode = kv.Value;
            if (!fields.TryGetValue($"Char_{k}", out var vchar)) continue;
            precodes.Add((k,vcode,vchar));
        }
        precodes = precodes.OrderBy(p => p.Item1).ToList();
        foreach (var tt in precodes)
        {
            var k = tt.Item1;
            var vcode = tt.Item2;
            var vchar=tt.Item3;
            var index=lstcodes.Count;
            var cc= new IconCodes(vcode, vchar, index);
            CharSet[k] = cc;
            CharSet[vcode] = cc;
            CharSet[$"#{index}"] = cc;
            lstcodes.Add(k);
        }
        Codes=lstcodes;
    }
    private static byte[] GetResource(Type t,string name)
    {
        var assembly = t.Assembly;
        string resourceName = assembly.GetManifestResourceNames().FirstOrDefault(str => str.EndsWith(name,StringComparison.OrdinalIgnoreCase));
        if (resourceName == null) return null;
        using Stream stream = assembly.GetManifestResourceStream(resourceName);
        if (stream is null) return null;
        using MemoryStream mstream = new MemoryStream();
        stream.CopyTo(mstream);
        return mstream.ToArray();
    }
    private static FontFamily LoadFont(Type t, string ttfname,PrivateFontCollection pfc)
    {
        var memoryfont = GetResource(t, ttfname);
        var length = memoryfont.Length;
        var data = Marshal.AllocCoTaskMem(length);
        Marshal.Copy(memoryfont, 0, data,length);
        // Bug on pfc
        var hashfam = pfc.Families.ToHashSet();
        pfc.AddMemoryFont(data,length);
        var fn = pfc.Families.FirstOrDefault(f=>!hashfam.Contains(f));
        return fn;
    }
}