using System.Drawing;
using System.Drawing.Text;

namespace Rop.Winforms.Icons;

public interface IHasIcons
{
    int IconScale { get; set; }
    FontFamily IconFontFamily { get; }
    IBankIcon BankIcon { get; set; }
    Font IconFont { get; }
    Color IconColor { get; set; }
    TextRenderingHint TextRenderingHint { get; set; }
    string OnlyText { get; }
    string FullText { get; }
    string PrefixCode { get; set; }
    string SuffixCode { get; set; }
    bool UseIconColor { get; set; }
    int OffsetIcon { get; set; }
    int OffsetText { get; set; }
    Color DisabledColor { get; set; }
    string Text { get; set; }
    void SetIcons(IEmbeddedIcons iconfont);
}