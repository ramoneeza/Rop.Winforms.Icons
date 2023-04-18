using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Rop.Winforms.DuotoneIcons;
internal partial class dummy{}
public class SoloIconBoolLabel : SoloIconLabel
{
    public ToolTip ToolTip { get; set; }
    public string ToolTipText { get; set; } = "";
    private DuoToneColor _iconColorOff = DuoToneColor.Disabled;

    public DuoToneColor IconColorOff
    {
        get => _iconColorOff;
        set
        {
            _iconColorOff = value;
            Invalidate();
        }
    }

    private DuoToneColor _iconColorOn = DuoToneColor.DefaultOneTone;

    public DuoToneColor IconColorOn
    {
        get => _iconColorOn;
        set
        {
            _iconColorOn = value;
            Invalidate();
        }
    }

    

    private string _iconOff;

    private void _ajcode()
    {
        base.Code = Value ? IconOn : IconOff;
        Invalidate();
    }
    public string IconOff
    {
        get => _iconOff;
        set
        {
            _iconOff = value;
            _ajcode();
        }
    }

    private string _iconOn;

    public string IconOn
    {
        get => _iconOn;
        set
        {
            _iconOn = value; 
            _ajcode();
        }
    }

    public bool ShowToolTip { get; set; }
    public bool Value { get; set; }
    [Browsable(false)]
    public override string Text { get => base.Text; set { } }
    public override string Code
    {
        get => base.Code;
        set { }
    }

    public override DuoToneColor Color { get=>Value?IconColorOn:IconColorOff;
        set { }
    }

    public SoloIconBoolLabel()
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