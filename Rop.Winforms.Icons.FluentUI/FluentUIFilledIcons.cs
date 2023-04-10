using System.ComponentModel;
using System.Drawing.Text;

namespace Rop.Winforms.Icons.FluentUI;

    public partial class FluentUIFilledIcons : BaseEmbeddedIcons
    {
        public static readonly string ResourceName = "FluentSystemIcons-Filled.ttf";
        
        public FluentUIFilledIcons(PrivateFontCollection fontCollection):base(ResourceName,fontCollection)
        {
        }
    }
public class FluentUIFilledBank : Component,IBankIcon
{
    public IEmbeddedIcons Bank { get; }

    public FluentUIFilledBank()
    {
        Bank = IconRepository.GetEmbeddedIcons<FluentUIFilledIcons>();
    }
}




