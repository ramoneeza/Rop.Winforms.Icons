using System.ComponentModel;

namespace Rop.Winforms.DuotoneIcons.FontAwesone;

internal partial class dummy
{
}

public class FontAwesoneBank : Component, IBankIcon
{
    public IEmbeddedIcons Bank { get; }

    public FontAwesoneBank()
    {
        Bank = IconRepository.GetEmbeddedIcons<FontAwesoneIcons>();
    }
}