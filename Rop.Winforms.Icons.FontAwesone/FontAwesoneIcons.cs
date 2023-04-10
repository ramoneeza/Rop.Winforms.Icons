using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;


namespace Rop.Winforms.Icons.FontAwesone;

    public partial class FontAwesoneIcons : BaseEmbeddedIcons
    {
        public static readonly string ResourceName = "fa-regular-400.ttf";
        
        public FontAwesoneIcons(PrivateFontCollection fontCollection):base(ResourceName,fontCollection)
        {
        }
    }
public class FontAwesoneBank : Component,IBankIcon
{
    public IEmbeddedIcons Bank { get; }

    public FontAwesoneBank()
    {
        Bank = IconRepository.GetEmbeddedIcons<FontAwesoneIcons>();
    }
}




