using System.Drawing;

namespace Rop.Winforms.Icons.Helpers;

public class IconStringSizeArgs : IIconStringSizeArgs
{
    public Font Font { get; set; }
    public Font Iconfont { get; set; }
    public IEmbeddedIcons IconBank { get; set; }
    public int OffsetIcon { get; set; }
    public int OffsetText { get; set; }
}