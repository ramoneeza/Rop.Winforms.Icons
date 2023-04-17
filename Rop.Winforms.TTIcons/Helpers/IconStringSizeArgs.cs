using System.Drawing;

namespace Rop.Winforms.DuotoneIcons.Helpers;

public class IconStringSizeArgs : IIconStringSizeArgs
{
    public Font Font { get; set; }
    public int Scale { get; set; }
    public IEmbeddedIcons IconBank { get; set; }
    public int OffsetIcon { get; set; }
    public int OffsetText { get; set; }
    public bool UseAscent { get; set; }
}