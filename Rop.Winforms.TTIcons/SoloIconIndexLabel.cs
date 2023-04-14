using System;
using System.Drawing;
using System.Windows.Forms;

namespace Rop.Winforms.DuotoneIcons;
internal partial class dummy{}
public class SoloIconIndexLabel : SoloIconLabel
{
    public string[] Items { get; set; } = Array.Empty<string>();
    public string[] ToolTips { get; set; } = Array.Empty<string>();
    public ToolTip ToolTip { get; set; }
    public string DefaultToolTip { get; set; } = "";
    public DuoToneColor[] ColorItems { get; set; } = Array.Empty<DuoToneColor>();
    private int _selectedIcon = -1;
    public event EventHandler SelectedIconChanged;
    public override Color ForeColor { get=>IconColor.Color1; set { } }
    public DuoToneColor DefaultColor { get; set; } = DuoToneColor.DefaultOneTone;
    public DuoToneColor DisabledColor { get; set; }=DuoToneColor.Disabled;
    public bool Disabled { get; set; }
    public bool ShowToolTip { get; set; }
    public string ToolTipText
    {
        get
        {
            if (!ShowToolTip) return "";
            if (SelectedIcon == -1 || SelectedIcon >= ToolTips.Length) return DefaultToolTip;
            return ToolTips[SelectedIcon];
        }
    }
    public virtual DuoToneColor IconColor {
        get
        {
            if (SelectedIcon == -1 || SelectedIcon>=ColorItems.Length) return DefaultColor;
            if (Disabled) return DisabledColor;
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

    public SoloIconIndexLabel()
    {

    }

    protected override void OnMouseEnter(EventArgs e)
    {
        base.OnMouseEnter(e);
        var s = ToolTipText;
        if (string.IsNullOrEmpty(s)) s = null;
        DoShowTooltip(s);
    }

    protected void DoShowTooltip(string s)
    {
        if (ToolTip == null) return;
        if (string.IsNullOrEmpty(s)) s = null;
        ToolTip.SetToolTip(this,s);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        DoShowTooltip(null);
    }
}