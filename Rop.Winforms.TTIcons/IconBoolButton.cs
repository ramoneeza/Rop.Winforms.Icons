using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Rop.Winforms.DuotoneIcons;

internal partial class dummy
{ }

public partial class IconBoolButton : IconButton
{
    private string _iconOff = "";

    public virtual string IconOff
    {
        get => _iconOff;
        set
        {
            _iconOff = value;
            Invalidate();
        }
    }

    private string _iconOn = "";

    public virtual string IconOn
    {
        get => _iconOn;
        set
        {
            _iconOn = value;
            Invalidate();
        }
    }

    private string _iconDisabled="";

    public virtual string IconDisabled
    {
        get => _iconDisabled;
        set
        {
            _iconDisabled = value;
            Invalidate();
        }
    }

    public string ToolTipText { get; set; } = "";
    public ToolTip ToolTip { get; set; }
    private DuoToneColor _iconColorOff = DuoToneColor.DefaultOneTone;
    public virtual DuoToneColor IconColorOff
    {
        get => _iconColorOff;
        set
        {
            _iconColorOff = value;
            Invalidate();
        }
    }
    private DuoToneColor _iconColorOn = DuoToneColor.DefaultOneTone;

    public virtual DuoToneColor IconColorOn
    {
        get => _iconColorOn;
        set
        {
            _iconColorOn = value;
            Invalidate();
        }
    }
    private bool _value;
    public bool Value
    {
        get => _value;
        set
        {
            _value = value;
            Invalidate();
        }
    }
    public bool Disabled => !Enabled;
    public bool ShowToolTip { get; set; }

    [Browsable(false)]
    public override DuoToneColor IconColor
    {
        get
        {
            if (Disabled) return DisabledColor;
            return Value ? IconColorOn : IconColorOff;
        }
        set { }
    }
    private string _textOn;
    public string TextOn
    {
        get => _textOn;
        set
        {
            _textOn = value;
            Invalidate();
        }
    }
    private string _textOff;
    public string TextOff
    {
        get => _textOff;
        set
        {
            _textOff = value;
            Invalidate();
        }
    }
    public override string OnlyText
    {
        get
        {
            return Value ? TextOn : TextOff;
        }
    }
    private bool _useSuffix;
    public bool UseSuffix
    {
        get => _useSuffix;
        set
        {
            if (_useSuffix == value) return;
            _useSuffix = value;
            Invalidate();
        }
    }
    protected virtual string Code => (Disabled && !string.IsNullOrEmpty(IconDisabled))?IconDisabled:(Value ? IconOn : IconOff);

    public override string PrefixCode
    {
        get => (UseSuffix) ? "" : Code;
        set
        {
        }
    }

    public override string SuffixCode
    {
        get => (UseSuffix) ? Code : "";
        set
        {
        }
    }

    [Browsable(false)]
    public override string Text
    { get => base.Text; set { } }

    

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
        ToolTip.SetToolTip(this, s);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        DoShowTooltip(null);
    }

    
}