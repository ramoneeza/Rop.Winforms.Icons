using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rop.Winforms.DuotoneIcons.MaterialDesign
{
    internal partial class dummy{}

    public class OrderIconMd : OrderIcon
    {
        public OrderIconMd()
        {
            SetIcons(IconRepository.GetEmbeddedIcons<MaterialDesignIcons>());
            Items = new[] {
                "ChevronDown",
                "ChevronUp",
                "ChevronDown"};
            ColorItems = new[]
            {
                DuoToneColor.OneTone(Color.Black),
                DuoToneColor.OneTone(Color.Black),
                DuoToneColor.OneTone(Color.Silver),
            };
            base.SelectedIcon = 2;
        }
    }
}
