using System.Drawing.Text;

namespace Rop.Winforms.DuotoneIcons;

public interface IHasIcons
{
    int IconScale { get; set; }
    IBankIcon BankIcon { get; set; }
    DuoToneColor IconColor { get; set; }
    TextRenderingHint TextRenderingHint { get; set; }
    string OnlyText { get; }
    string FullText { get; }
    string PrefixCode { get; set; }
    string SuffixCode { get; set; }
    bool UseIconColor { get; set; }
    int OffsetIcon { get; set; }
    int OffsetText { get; set; }
    DuoToneColor DisabledColor { get; set; }
    string Text { get; set; }
    void SetIcons(IEmbeddedIcons iconfont);
}