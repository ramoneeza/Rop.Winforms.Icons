using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rop.Winforms.Icons.Helpers;

namespace Rop.Winforms.Icons
{
    internal partial class dummy{}
    public partial class IconLabel:Label
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(this.BackColor),e.ClipRectangle);
           
            var args = GetIconStringArgs();
            var text = FullText.SplitIconString();
            var measure = e.Graphics.MeasureIconString(text,args);

            var offset = AlignOffset(measure.Bounds);
            e.Graphics.DrawStringWithIcons(offset.X,offset.Y,measure,args);
        }
        
        public override Size GetPreferredSize(Size proposedSize)
        {
            if (!AutoSize) return base.GetPreferredSize(proposedSize);
            using var g = this.CreateGraphics();
            var sf = g.MeasureIconString(FullText+"_", Font, IconFont,_icons);
            var w=1+sf.Width;
            var h = 1+sf.Height;
            w += this.Padding.Horizontal+1f;
            h += this.Padding.Vertical+1f;
            return new Size((int) w, (int) h);
        }
        
        public IconLabel()
        {
            AjFonts();
        }
    }
}
