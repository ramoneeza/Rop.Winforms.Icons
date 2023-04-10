using System.Drawing;

namespace Rop.Winforms.Icons.Helpers;

public interface IIconStringSizeArgs
{
    Font Font { get; set; }
    Font Iconfont { get; set; }
    IEmbeddedIcons IconBank { get; set; }
    int OffsetIcon { get; set; }
    int OffsetText { get; set; }
}