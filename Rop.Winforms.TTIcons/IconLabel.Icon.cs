using System;
using System.Drawing;
using System.Drawing.Text;
using Rop.Winforms.DuotoneIcons.Helpers;

namespace Rop.Winforms.DuotoneIcons;

internal partial class dummy { }
public partial class IconLabel:IHasIcons
{
    private IBankIcon _bankIcon;
    private IEmbeddedIcons _icons;
    private int _iconScale = 125;
    private TextRenderingHint _textRenderingHint = TextRenderingHint.AntiAliasGridFit;
    private string _prefixCode;
    private string _suffixCode;
    private bool _useIconColor = false;
    private int _offsetIcon = 0;
    private int _offsetText = 0;
    private DuoToneColor _iconColor = DuoToneColor.Empty;
    private DuoToneColor _disabledColor = DuoToneColor.Disabled;

    public int IconScale
    {
        get => _iconScale;
        set { _iconScale = value; AjFonts(); }
    }

    public IBankIcon BankIcon
    {
        get => _bankIcon;
        set
        {
            _bankIcon = value;
            SetIcons(_bankIcon?.Bank);
            Invalidate();
        }
    }

    private void AjFonts()
    {
        base.OnFontChanged(EventArgs.Empty);
    }

    public void SetIcons(IEmbeddedIcons iconfont)
    {
        if (iconfont == null) return;
        _icons = iconfont;
        AjFonts();
    }

    public Font IconFont { get; private set; }


    public virtual DuoToneColor IconColor
    {
        get => _iconColor;
        set
        {
            _iconColor = value;
            Invalidate();
        }
    }

    public TextRenderingHint TextRenderingHint
    {
        get => _textRenderingHint;
        set
        {
            _textRenderingHint = value;
            Invalidate();
        }
    }

    public virtual string OnlyText => Text;


    public virtual string FullText => $"{_icons?.GetChar(PrefixCode) ?? ""}{OnlyText}{_icons?.GetChar(SuffixCode) ?? ""}";

    public virtual string PrefixCode
    {
        get => _prefixCode;
        set
        {
            if (_prefixCode == value) return;
            var avalue = _prefixCode;
            _prefixCode = value;
            if (avalue == "" || value == "")
                AjFonts();
            else
                Invalidate();
        }
    }

    public virtual string SuffixCode
    {
        get => _suffixCode;
        set
        {
            if (_suffixCode == value) return;
            var avalue = _suffixCode;
            _suffixCode = value;
            if (avalue == "" || value == "")
                AjFonts();
            else
                Invalidate();
        }
    }

    public bool UseIconColor
    {
        get => _useIconColor;
        set
        {
            _useIconColor = value;
            Invalidate();
        }
    }

    public int OffsetIcon
    {
        get => _offsetIcon;
        set
        {
            _offsetIcon = value;
           AjFonts();
        }
    }

    public int OffsetText
    {
        get => _offsetText;
        set
        {
            _offsetText = value;
            AjFonts();
        }
    }

    private bool _useAscent = false;

    public bool UseAscent
    {
        get => _useAscent;
        set
        {
            _useAscent = value;
            AjFonts();
        }
    }

    protected override void OnFontChanged(EventArgs e)
    {
        AjFonts();
    }

    public DuoToneColor DisabledColor
    {
        get => _disabledColor;
        set
        {
            _disabledColor = value; 
            Invalidate();
        }
    }

    protected virtual IconStringArgs GetIconStringArgs()
    {
        var forecolor = UseIconColor ? IconColor.Color1 : ForeColor;
        var iconcolor = IconColor;
        if (!Enabled && DisabledColor != DuoToneColor.Empty)
        {
            forecolor = DisabledColor.Color1;
            iconcolor = DisabledColor;
        }


        return new IconStringArgs
        {
            ForeColor =forecolor,
            TextRenderingHint = TextRenderingHint,
            IconColor = iconcolor,
            OffsetIcon = OffsetIcon,
            OffsetText = OffsetText,
            IconBank = _icons,
            Scale= IconScale,
            Font=Font,
            UseAscent=UseAscent,
        };
    }

   

    
}