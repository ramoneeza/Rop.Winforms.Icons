using System.Drawing;

namespace Rop.Winforms.DuotoneIcons;

public interface IEmbeddedIcons
{
    /// <summary>
    /// Name of the icon Font
    /// </summary>
    string FontName { get; }

    string[] Codes { get; }

    int BaseSize { get; }

    /// <summary>
    /// FontFamily
    /// </summary>
    /// <summary>
    /// Number of codes
    /// </summary>
    int Count { get; }

    DuoToneIcon GetIcon(string nameorcode);
    DuoToneIcon GetIcon(int index);
    
    /// <summary>
    /// Get char from code
    /// </summary>
    string GetChar(string code);

    /// <summary>
    /// Get name from index
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    string GetName(int index);

    /// <summary>
    /// Get Char from index
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    string GetChar(int index);

    Bitmap GetBaseBitmap(string nameorcode);
    Bitmap GetBaseBitmap(int index);
    Bitmap GetBitmap(int index, DuoToneColor color);
    Bitmap GetBitmap(string nameorcode, DuoToneColor color);

    float DrawTTIcon(Graphics gr,string code,DuoToneColor iconcolor, float x, float y, float height);
    void DrawTTIconFit(Graphics gr,string code,DuoToneColor iconcolor, float x, float y, float size);
    SizeF MeasureIcon(string code, float height);

    RectangleF MeasureIcon(Graphics gr, string code, Font font, float scale);
}
public interface IBankIcon
{
    IEmbeddedIcons Bank { get; }
}