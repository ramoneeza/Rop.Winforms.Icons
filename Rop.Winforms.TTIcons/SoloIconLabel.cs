using Rop.Winforms.DuotoneIcons.Helpers;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Rop.Winforms.DuotoneIcons;

internal partial class dummy
{ }

public class SoloIconLabel : Label
{
    private IBankIcon _bankIcon;
    private IEmbeddedIcons _icons;
    private string _code;

    public IBankIcon BankIcon
    {
        get => _bankIcon;
        set
        {
            _bankIcon = value;
            SetIcons(_bankIcon?.Bank);
            if (string.IsNullOrEmpty(_code)) Code = _icons?.GetName(0);
        }
    }

    public void SetIcons(IEmbeddedIcons iconfont)
    {
        _icons = iconfont;
        base.OnFontChanged(EventArgs.Empty);
    }

    public virtual string Code
    {
        get => _code;
        set
        {
            _code = value;
            base.OnFontChanged(EventArgs.Empty);
        }
    }

    private DuoToneColor _color = DuoToneColor.Default;

    public virtual DuoToneColor Color
    {
        get => _color;
        set
        {
            _color = value;
            Invalidate();
        }
    }

    private DuoToneColor _disabledColor = DuoToneColor.Disabled;

    public DuoToneColor DisabledColor
    {
        get => _disabledColor;
        set
        {
            _disabledColor = value; 
            Invalidate();
        }
    }

    private bool _disabled;

    public bool Disabled
    {
        get => _disabled;
        set { _disabled = value; Invalidate();}
    }

    public SoloIconLabel() : base()
    {
    }

    protected PointF AlignOffset(RectangleF textbounds)
    {
        var controlbounds = new RectangleF(Padding.Left, Padding.Top, Width - Padding.Horizontal, Height - Padding.Vertical);

        var r = textbounds.AlignOffset(TextAlign, controlbounds);

        return r;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        e.Graphics.FillRectangle(new SolidBrush(this.BackColor), e.ClipRectangle);
        var text = Code;
        var measure = e.Graphics.MeasureBaseIcon(Font);
        var offset = AlignOffset(new RectangleF(PointF.Empty, measure));
        var color = Disabled ? DisabledColor : Color;
        _icons?.DrawTTIcon(e.Graphics, Code, color, offset.X, offset.Y, measure.Height);
    }

    public override Size GetPreferredSize(Size proposedSize)
    {
        if (!AutoSize) return base.GetPreferredSize(proposedSize);
        using var g = CreateGraphics();
        var sz = _bankIcon?.Bank.MeasureIcon(g, Code, Font, 1,false).Size ?? SizeF.Empty;
        if (sz == SizeF.Empty) sz = new SizeF(16, 16);
        sz = new SizeF(1 + sz.Width + Padding.Horizontal, 1 + sz.Height + Padding.Vertical);
        return new Size((int)sz.Width, (int)sz.Height);
    }
}