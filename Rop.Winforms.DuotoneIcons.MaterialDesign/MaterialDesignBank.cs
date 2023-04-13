using System.ComponentModel;

namespace Rop.Winforms.DuotoneIcons.MaterialDesign;

internal partial class dummy
{
}

public class MaterialDesignBank : Component, IBankIcon
{
    public IEmbeddedIcons Bank { get; }

    public MaterialDesignBank()
    {
        Bank = IconRepository.GetEmbeddedIcons<MaterialDesignIcons>();
    }
}