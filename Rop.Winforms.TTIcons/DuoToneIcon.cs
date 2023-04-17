using System.Drawing;

namespace Rop.Winforms.DuotoneIcons;

public class DuoToneIcon
{
    public string Name { get; }
    public string TheChar { get; }
    public int Index { get; }
    public byte[] Data { get; }
    public Size Size { get; }
    public float WidthUnit { get; }
    public float AscentUnit { get; }
    public DuoToneIcon(string name, string theChar, int index,byte[] data, Size size, float ascentUnit=1)
    {
        Name = name;
        TheChar = theChar;
        Index = index;
        Data = data;
        Size = size;
        WidthUnit = (float)size.Width / (float)size.Height;
        AscentUnit = ascentUnit;
    }
}