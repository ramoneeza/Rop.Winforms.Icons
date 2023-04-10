using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rop.Winforms.Icons;

internal partial class dummy { }
public partial class IconIndexLabel:IIconIndex
{
    private string[] _items = Array.Empty<string>();
    private Color[] _colorItems = Array.Empty<Color>();
    private int _selectedIcon = -1;
    public event EventHandler SelectedIconChanged;
    public string[] Items
    {
        get => _items;
        set
        {
            _items = value; 
            Invalidate();
        }
    }
    public Color[] ColorItems
    {
        get => _colorItems;
        set
        {
            _colorItems = value; 
            Invalidate();
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

    public override Color IconColor
    {
        get
        {
            if (SelectedIcon == -1 || SelectedIcon >= ColorItems.Length) return ForeColor;
            return ColorItems[SelectedIcon];
        }
        set { }
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

    public void SetBoolIcon(bool? value)
    {
        SelectedIcon = (value is null) ? 2 : (value.Value ? 1 : 0);
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
}

public interface IIconIndex
{
    event EventHandler SelectedIconChanged;
    string[] Items { get; set; }
    Color[] ColorItems { get; set; }
    bool UseSuffix { get; set; }
    int SelectedIcon { get; set; }
    string[] TextItems { get; set; }
    void SetBoolIcon(bool? value);
}