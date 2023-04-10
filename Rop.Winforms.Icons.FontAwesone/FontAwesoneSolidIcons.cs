using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;

namespace Rop.Winforms.Icons.FontAwesone;

public partial class FontAwesoneSolidIcons : BaseEmbeddedIcons
{
    public static readonly string ResourceName = "fa-solid-900.ttf";
    public FontAwesoneSolidIcons(PrivateFontCollection fontCollection):base(ResourceName,fontCollection)
    {
    }
}
public class FontAwesoneSolidBank : Component,IBankIcon
{
    public IEmbeddedIcons Bank { get; }

    public FontAwesoneSolidBank()
    {
        Bank = IconRepository.GetEmbeddedIcons<FontAwesoneSolidIcons>();
    }
}