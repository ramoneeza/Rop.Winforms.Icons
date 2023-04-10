using System;
using System.Drawing;
using System.Windows.Forms;
using Rop.Winforms.DuotoneIcons.Helpers;

namespace Rop.Winforms.DuotoneIcons
{
    internal partial class dummy{}
    public partial class IconLabel:Label
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(this.BackColor),e.ClipRectangle);
           
            var args = GetIconStringArgs();
            var text = FullText.SplitIconString();
            var measure = e.Graphics.MeasureIconString(text,args,_icons);
            
            var offset = this.AlignOffset(TextAlign, measure.Bounds);
            e.Graphics.DrawStringWithIcons(offset.X,offset.Y,measure,args,_icons);
        }
        
        public override Size GetPreferredSize(Size proposedSize)
        {
            if (!AutoSize) return base.GetPreferredSize(proposedSize);
            using var g = this.CreateGraphics();

            var args = GetIconStringArgs();
            var ft = FullText;
            if (ft == "") ft = " ";
            var text = ft.SplitIconString();
            var measure = g.MeasureIconString(text,args,_icons);
            var sf=measure.BoundsZero().Size;
            var w=1+sf.Width;
            var h = 1+sf.Height;
            w += this.Padding.Horizontal+1f;
            h += this.Padding.Vertical+1f;

            var m=Math.Max(args.OffsetIcon,args.OffsetText);
            if (m > 0) h += m;
            
            return new Size((int) w, (int) h);
        }
        
        public IconLabel()
        {
            AjFonts();
        }
    }
}
