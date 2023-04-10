using System.ComponentModel;
using System.Drawing.Text;

namespace Rop.Winforms.Icons.FluentUI;

    public partial class FluentUIIcons : BaseEmbeddedIcons
    {
        public static readonly string ResourceName = "FluentSystemIcons-Regular.ttf";
        
        public FluentUIIcons(PrivateFontCollection fontCollection):base(ResourceName,fontCollection)
        {
        }
    }
public class FluentUIBank : Component,IBankIcon
{
    public IEmbeddedIcons Bank { get; }

    public FluentUIBank()
    {
        Bank = IconRepository.GetEmbeddedIcons<FluentUIIcons>();
    }
}




