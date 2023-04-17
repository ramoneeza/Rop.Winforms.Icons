using System.Drawing;

namespace Rop.Winforms.DuotoneIcons.MaterialDesign;
 internal partial class dummy{}
public class SwitchIconMd : IconIndexLabel
{
   
    public SwitchIconMd()
    {
        SetIcons(IconRepository.GetEmbeddedIcons<MaterialDesignIcons>());
        Items = new[]
        {
            "ToggleSwitchOffOutline",
            "ToggleSwitch"
        };
        ColorItems=new[]
        {
            DuoToneColor.OneTone(Color.Black),
            DuoToneColor.OneTone(Color.Black),
        };
    }

    public Color SwitchColor
    {
        get => ColorItems[1].Color1;
        set
        {
            ColorItems[1] = DuoToneColor.OneTone(value);
            Invalidate();
        }
    }
    public new DuoToneColor[] ColorItems
    {
        get => base.ColorItems;
        set { }
    }
}