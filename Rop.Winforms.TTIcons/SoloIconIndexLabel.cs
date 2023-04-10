using System;
using System.Drawing;

namespace Rop.Winforms.DuotoneIcons;
internal partial class dummy{}
public class SoloIconIndexLabel : SoloIconLabel
{
    public string[] Items { get; set; } = Array.Empty<string>();
    public DuoToneColor[] ColorItems { get; set; } = Array.Empty<DuoToneColor>();
    private int _selectedIcon = -1;
    public event EventHandler SelectedIconChanged;
    public override Color ForeColor { get=>IconColor.Color1; set { } }
    public DuoToneColor DefaultColor { get; set; } = DuoToneColor.Default;
    public virtual DuoToneColor IconColor {
        get
        {
            if (SelectedIcon == -1 || SelectedIcon>=ColorItems.Length) return DefaultColor;
            return ColorItems[SelectedIcon];
        }
        set { }
    }
    public int SelectedIcon
    {
        get => _selectedIcon;
        set
        {
            if (_selectedIcon == value) return;
            _selectedIcon = value;
            var code = "";
            if (_selectedIcon >= 0 && _selectedIcon < Items.Length)
            {
                code = Items[value];
            }
            base.Code = code;
            SelectedIconChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    public override string Text { get => base.Text; set { } }
    public override string Code
    {
        get => base.Code;
        set { }
    }
    public void SetBoolIcon(bool? value)
    {
        SelectedIcon = (value is null) ? 2 : (value.Value ? 1 : 0);
    }
}