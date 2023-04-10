using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.Linq;

namespace Rop.Winforms.Icons.GoogleMaterial;

    public partial class GoogleMaterialIcons : BaseEmbeddedIcons
    {
        public static readonly string ResourceName = "MaterialIcons-Regular.ttf";
        public GoogleMaterialIcons(PrivateFontCollection pfc):base(ResourceName,pfc)
        {
        }
    }
    public class GoogleMaterialBank : Component,IBankIcon
    {
        public IEmbeddedIcons Bank { get; }

        public GoogleMaterialBank()
        {
            Bank = IconRepository.GetEmbeddedIcons<GoogleMaterialIcons>();
        }
    }

