using System.ComponentModel;
using System.Drawing;

namespace Rop.Winforms.DuotoneIcons.MaterialDesign;
 internal partial class dummy{}
public class SwitchIconMd : IconBoolLabel
{
    [Browsable(false)]
    public override string IconOff { get; set; }
    [Browsable(false)]
    public override string IconOn { get; set; }

    public SwitchIconMd()
    {
        SetIcons(IconRepository.GetEmbeddedIcons<MaterialDesignIcons>());
        IconOff = "ToggleSwitchOffOutline";
        IconOn = "ToggleSwitch";
        IconColorOff = DuoToneColor.OneTone(Color.Black);
        IconColorOn = DuoToneColor.OneTone(Color.Black);
    }
    public Color SwitchColor
    {
        get => IconColorOn.Color1;
        set => IconColorOn = IconColorOn.WithColor1(value);
    }
}
public class LockIconMd : IconBoolLabel
{
    [Browsable(false)]
    public override string IconOff { get; set; }
    [Browsable(false)]
    public override string IconOn { get; set; }

    public LockIconMd()
    {
        SetIcons(IconRepository.GetEmbeddedIcons<MaterialDesignIcons>());
        IconOff = "LockOpen";
        IconOn = "Lock";
        IconColorOff = DuoToneColor.OneTone(Color.Black);
        IconColorOn = DuoToneColor.OneTone(Color.Black);
    }
    public Color LockColor
    {
        get => IconColorOn.Color1;
        set => IconColorOn = IconColorOn.WithColor1(value);
    }
}