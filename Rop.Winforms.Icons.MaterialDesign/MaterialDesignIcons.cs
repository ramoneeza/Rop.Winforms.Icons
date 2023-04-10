using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.Linq;

namespace Rop.Winforms.Icons.MaterialDesign
{
    public partial class MaterialDesignIcons : BaseEmbeddedIcons
    {
        public static readonly string ResourceName = "materialdesignicons.ttf";
        public MaterialDesignIcons(PrivateFontCollection pfc):base(ResourceName,pfc)
        {
        }
    }
    public class MaterialDesignBank : Component,IBankIcon
    {
        public IEmbeddedIcons Bank { get; }

        public MaterialDesignBank()
        {
            Bank = IconRepository.GetEmbeddedIcons<MaterialDesignIcons>();
        }
    }
}
