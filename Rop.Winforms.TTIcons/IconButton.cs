using System.Drawing;
using System.Windows.Forms;
using Rop.Winforms.DuotoneIcons.Helpers;

namespace Rop.Winforms.DuotoneIcons
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
            var measure = e.Graphics.MeasureIconString(text, args,_icons);

            var offset = AlignOffset(measure.Bounds);

            if (!Enabled)
            {
                args.IconColor = new DuoToneColor(Color.DarkGray,Color.Transparent);
                args.ForeColor = Color.DarkGray;
            }
            e.Graphics.DrawStringWithIcons(offset.X, offset.Y, measure, args,_icons);
        }
        public IconButton()
        {
            base.Text = "";
            AjFonts();
        }
    }
}
