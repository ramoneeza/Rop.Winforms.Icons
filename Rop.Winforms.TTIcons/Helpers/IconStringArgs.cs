using System.Drawing;
using System.Drawing.Text;

namespace Rop.Winforms.DuotoneIcons.Helpers;

public class IconStringArgs : IconStringSizeArgs
{
    public Color ForeColor { get; set; }
    public DuoToneColor IconColor { get; set; }
    public TextRenderingHint TextRenderingHint { get; set; }
    public IconStringArgs() { }

    public IconStringArgs(IIconStringSizeArgs args)
    {
        Font = args.Font;
        Scale= args.Scale;
        IconBank = args.IconBank;
        OffsetIcon = args.OffsetIcon;
        OffsetText = args.OffsetText;
        if (args is IconStringArgs args2)
        {
            ForeColor = args2.ForeColor;
            IconColor = args2.IconColor;
            TextRenderingHint = args2.TextRenderingHint;
        }
    }
}