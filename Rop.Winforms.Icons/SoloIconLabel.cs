using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace Rop.Winforms.Icons;
internal partial class dummy{}



public class SoloIconLabel : Label
{
    private IBankIcon _bankIcon;
    private IEmbeddedIcons _icons;
    public IBankIcon BankIcon
    {
        get => _bankIcon;
        set
        {
            _bankIcon = value;
            SetIcons(_bankIcon?.Bank);
            if (string.IsNullOrEmpty(_code)) Code = _icons?.GetName(0);
            Invalidate();
        }
    }
    private Font _font;
    private string _code;

    public override Font Font
    {
        get => (_bankIcon is null) ? base.Font : _font;
        set
        {
            if (_bankIcon is null || value.FontFamily.Name == _icons.FontFamily.Name)
            {
                _font = value;
            }
            else
            {
                _font = new Font(_icons.FontFamily, value.Size, value.Style);
            }
            base.Font = _font;
        }
    }
    public void SetIcons(IEmbeddedIcons iconfont)
    {
        _icons = iconfont;
        Font = base.Font;
    }
    public virtual string Code
    {
        get => _code;
        set
        {
            _code = value;
            base.Text = _icons?.GetChar(_code);
        }
    }

    public float FontSize
    {
        get => Font.Size;
        set
        {
            if (FontSize==value) return;
            Font = new Font(Font.FontFamily, value,Font.Style);
        }
    }
    public override string Text
    {
        get => base.Text;
        set => base.Text= _icons?.GetChar(_code);
    }

    public SoloIconLabel():base()
    {
            
    }
}