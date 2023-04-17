using System.Drawing;

namespace Rop.Winforms.DuotoneIcons.Helpers;

public interface IIconStringSizeArgs
{
    Font Font { get; set; }
    int Scale { get; set; }
    IEmbeddedIcons IconBank { get; set; }
    int OffsetIcon { get; set; }
    int OffsetText { get; set; }
    bool UseAscent { get; set; }
}