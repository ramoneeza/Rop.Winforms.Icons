using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

namespace Rop.Winforms.Icons;
internal partial class dummy{}
public class SoloIconIndexLabel : SoloIconLabel
{
    public string[] Items { get; set; } = Array.Empty<string>();
    public Color[] ColorItems { get; set; } = Array.Empty<Color>();
    private int _selectedIcon = -1;
    public event EventHandler SelectedIconChanged;
    public override Color ForeColor { get=>IconColor; set { } }
    public Color DefaultColor { get; set; } = Color.Black;
    public virtual Color IconColor {
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