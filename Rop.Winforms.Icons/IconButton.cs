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
    public partial class IconButton:Button
    {
        private bool _painting = false;
        public override string Text
        {
            get => (!_painting)?base.Text:"";
            set => base.Text = value;
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            _painting = true;
            base.OnPaint(e);
            _painting = false;
            
            var args = GetIconStringArgs();
            var text = FullText.SplitIconString();
            var measure = e.Graphics.MeasureIconString(text, args);

            var offset = AlignOffset(measure.Bounds);

            if (!Enabled)
            {
                args.IconColor = Color.DarkGray;
                args.ForeColor = Color.DarkGray;
            }
            e.Graphics.DrawStringWithIcons(offset.X, offset.Y, measure, args);
        }
        public IconButton()
        {
            base.Text = "";
            AjFonts();
        }
    }
}
