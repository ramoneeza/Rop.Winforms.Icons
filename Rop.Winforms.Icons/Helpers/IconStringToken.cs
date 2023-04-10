namespace Rop.Winforms.Icons.Helpers;

public readonly struct IconStringToken
{
    public bool IsIcon { get; }
    public string Cad { get; }
    public IconStringToken(bool isIcon, string cad)
    {
        IsIcon = isIcon;
        Cad = cad;
    }
    public static IconStringToken FactoryIcon(string iconCode) => new IconStringToken(true, iconCode);
    public static IconStringToken FactoryText(string text) => new IconStringToken(false, text);

    public void Deconstruct(out bool isIcon, out string cad)
    {
        isIcon = IsIcon;
        cad = Cad;
    }
}