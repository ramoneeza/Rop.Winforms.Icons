using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace Rop.Winforms.Icons;

public interface IEmbeddedIcons
{
    string FontName { get; }
    FontFamily FontFamily { get; }
    string GetCode(string name);
    string GetChar(string code);
    IReadOnlyList<string> Codes { get; }
    int Count { get; }
    int GetIndex(string name);
    string GetName(int index);
    string GetCode(int index);
    string GetChar(int index);

    /// <summary>
    /// Get Font from Icons
    /// </summary>
    /// <param name="size"></param>
    /// <returns></returns>
    Font GetFont(float size);

    /// <summary>
    /// Get Font from Icons
    /// </summary>
    /// <param name="size"></param>
    /// <param name="style"></param>
    /// <returns></returns>
    Font GetFont(float size, FontStyle style);

    /// <summary>
    /// Get Font from Icons
    Font GetFont(float size, FontStyle style, GraphicsUnit unit);
}

public interface IBankIcon
{
    IEmbeddedIcons Bank { get; }
}