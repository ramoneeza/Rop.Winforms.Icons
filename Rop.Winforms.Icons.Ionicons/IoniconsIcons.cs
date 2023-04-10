using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;
namespace Rop.Winforms.Icons.Ionicons;

    public partial class IoniconsIcons : BaseEmbeddedIcons
    {
        public static readonly string ResourceName = "ionicons.ttf";
        public IoniconsIcons(PrivateFontCollection fontCollection):base(ResourceName,fontCollection)
        {
        }
    }
public class IoniconsBank : Component,IBankIcon
{
    public IEmbeddedIcons Bank { get; }

    public IoniconsBank()
    {
        Bank = IconRepository.GetEmbeddedIcons<IoniconsIcons>();
    }
}


