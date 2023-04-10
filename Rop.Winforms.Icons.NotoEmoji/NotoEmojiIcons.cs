using System.ComponentModel;
using System.Drawing.Text;

namespace Rop.Winforms.Icons.NotoEmoji;

    public partial class NotoEmojiIcons : BaseEmbeddedIcons
    {
        public static readonly string ResourceName = "NotoEmoji-Medium.ttf";
        public NotoEmojiIcons(PrivateFontCollection fontCollection):base(ResourceName,fontCollection)
        {
        }
    }
public class NotoEmojiBank : Component,IBankIcon
{
    public IEmbeddedIcons Bank { get; }

    public NotoEmojiBank()
    {
        Bank = IconRepository.GetEmbeddedIcons<NotoEmojiIcons>();
    }
}


