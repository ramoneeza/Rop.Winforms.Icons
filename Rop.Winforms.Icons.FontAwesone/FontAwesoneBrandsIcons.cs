using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;

namespace Rop.Winforms.Icons.FontAwesone;

public partial class FontAwesoneBrandsIcons : BaseEmbeddedIcons
{
    public static readonly string ResourceName = "fa-brands-400.ttf";
    public FontAwesoneBrandsIcons(PrivateFontCollection fontCollection):base(ResourceName,fontCollection)
    {
    }
}
public class FontAwesoneBrandsBank : Component,IBankIcon
{
    public IEmbeddedIcons Bank { get; }

    public FontAwesoneBrandsBank()
    {
        Bank = IconRepository.GetEmbeddedIcons<FontAwesoneBrandsIcons>();
    }
}
