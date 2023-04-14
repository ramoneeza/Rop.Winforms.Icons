using System.Drawing;
using System;
using System.Windows.Forms;

namespace Rop.Winforms.DuotoneIcons;
internal partial class dummy{}
public  class IconIndexLabel : IconLabel
{
    public string[] Items { get; set; } = Array.Empty<string>();
    public string[] ToolTips { get; set; } = Array.Empty<string>();
    public ToolTip ToolTip { get; set; }
    public string DefaultToolTip { get; set; } = "";
    public DuoToneColor[] ColorItems { get; set; } = Array.Empty<DuoToneColor>();
    private int _selectedIcon = -1;
    public event EventHandler SelectedIconChanged;
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
    public override DuoToneColor IconColor {
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
            Code = code;
            SelectedIconChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    
    public string[] TextItems
    {
        get => _textItems;
        set
        {
            _textItems = value; 
            Invalidate();
        }
    }

    public override string OnlyText
    {
        get
        {
            if (SelectedIcon == -1 || SelectedIcon >= TextItems.Length) return base.OnlyText;
            return TextItems[SelectedIcon];
        }
    }

private bool _useSuffix;
private string[] _textItems = Array.Empty<string>();

public bool UseSuffix
{
    get => _useSuffix;
    set
    {
        if (_useSuffix == value) return;
        base.PrefixCode = "";
        base.SuffixCode = "";
        _useSuffix = value;
        SelectedIcon = -1;
    }
}
public string Code
{
    get => (UseSuffix) ? base.SuffixCode : base.PrefixCode;
    private set
    {
        if (UseSuffix)
            base.SuffixCode = value;
        else
            base.PrefixCode = value;
    }
}

public override string PrefixCode
{
    get => base.PrefixCode;
    set
    {
        if (UseSuffix) base.PrefixCode = value;
    }
}

public override string SuffixCode
{
    get => base.SuffixCode;
    set
    {
        if (!UseSuffix) base.SuffixCode = value;
    }
}


public override string Text { get => base.Text; set { } }

public void SetBoolIcon(bool? value)
{
    SelectedIcon = (value is null) ? 2 : (value.Value ? 1 : 0);
}

public IconIndexLabel()
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