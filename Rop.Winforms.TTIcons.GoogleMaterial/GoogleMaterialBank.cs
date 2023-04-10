using System.ComponentModel;
using Rop.Winforms.DuotoneIcons;

namespace Rop.Winforms.TTIcons.GoogleMaterial;

internal partial class dummy
{
}

public class GoogleMaterialBank : Component, IBankIcon
{
    public IEmbeddedIcons Bank { get; }

    public GoogleMaterialBank()
    {
        Bank = IconRepository.GetEmbeddedIcons<GoogleMaterialIcons>();
    }
}