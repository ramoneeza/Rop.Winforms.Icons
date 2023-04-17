using System;
using System.Drawing;
using System.Linq;

namespace Rop.Winforms.DuotoneIcons.MaterialDesign
{
    public class MaterialDesignIcons : BaseEmbeddedIcons
    {
        public static readonly string IconsResourceName = "mdicons.bin";
        public static readonly string CodesResourceName = "mdcodes.txt";
        public MaterialDesignIcons():base("MaterialDesign",IconsResourceName,CodesResourceName)
        {
        }

        protected override DuoToneIcon FactoryDuoToneIcon(string tt, string ch, int index, byte[] data, Size size)
        {
            var sw = size.Width / 4;
            var sm = sw / 2;
            var i=size.Height;
            var buf=new byte[sm];
            while (i>0)
            {
                var ptr = (i - 1) * sw;
                Array.Copy(data,ptr,buf,0,sm);
                if (buf.Any(b => b != 0)) break;
                i--;
            }
            if (i==0) i=size.Height;
            var bl= (float)i/size.Height;
            return new DuoToneIcon(tt, ch, index, data, size,bl);
        }        
    }
}