using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Rop.Winforms.Icons.Panel
{
    internal partial class dummy{}
    public class IconPanel:Control
    {
        private ScrollBar _scrollBar;
        private int _columns = 4;
        private IBankIcon? _bank;
        private IEmbeddedIcons? _icons => _bank?.Bank;
        private int _overIcon = -1;
        private int _selectedIcon = -1;
        protected override Size DefaultSize => new Size(150, 300);
        public event EventHandler<string> SelectedIconChanged;
        public IconPanel()
        {
            
            _scrollBar = new VScrollBar();
            _scrollBar.Dock = DockStyle.Right;
            _scrollBar.Scroll += (sender, e) => { Invalidate(); };
            Controls.Add(_scrollBar);
            DoubleBuffered = true;
        }

        public IBankIcon BankIcon { get=>_bank;
        
            set
            {
                if (_bank==value) return;
                _bank = value;
                _measureIcons();
            }
        }

        public Bitmap? BmpIcons { get; private set; }

        public int YOffset
        {
            get => _scrollBar.Value;
            set => _scrollBar.Value = value;
        }

        public string SelectedIcon
        {
            get => _icons?.GetName(_selectedIcon) ?? "";
            set
            {
                var i =(string.IsNullOrEmpty(value)||_icons is null)?-1:_icons.GetIndex(value);
                if (_selectedIcon != i)
                {
                    _selectedIcon = i;
                    Invalidate();
                    SelectedIconChanged?.Invoke(this,SelectedIcon);
                }
            }
        }

        public int Columns
        {
            get => _columns;
            set
            {
                _columns = value;
                if (_bank is not null)
                {
                    _measureIcons();
                    Invalidate();
                }
            }
        }
        public Font IconFont { get; private set; }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (BmpIcons is null) return;
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            e.Graphics.DrawImageUnscaled(BmpIcons, _offsetPoint(Point.Empty));
            if (_selectedIcon >= 0)
            {
                var p = GetBox(_selectedIcon);
                _paintIcon(e.Graphics, p, Color.Cyan);
            }
            if (_overIcon >= 0)
            {
                var p = GetBox(_overIcon);
                _paintIcon(e.Graphics, p, Color.Yellow);
            }
        }

        private Point _offsetPoint(Point p)
        {
            return new Point(p.X, p.Y - YOffset);
        }

        private Rectangle _offsetRect(Rectangle r)
        {
            return new Rectangle(_offsetPoint(r.Location), r.Size);
        }


        private void _paintIcon(Graphics eGraphics, Rectangle box, Color color)
        {
            if (BmpIcons is null) return;
            var or = _offsetRect(box);
            var br = new SolidBrush(color);
            eGraphics.FillRectangle(br, or);
            eGraphics.DrawImage(BmpIcons,or, box, GraphicsUnit.Pixel);
        }

        private void _measureIcons()
        {
            var n = _bank?.Bank.Count ?? 0;
            Rows = (n == 0) ? 0 : 1 + ((n - 1) / Columns);
            var width = Width - _scrollBar.Width;
            BoxSize = width / Columns;
            var scale = 96f / CreateGraphics().DpiX;
            float sz = BoxSize - 2 * IconPadding;
            IconFont = _icons.GetFont(sz*scale, FontStyle.Regular, GraphicsUnit.Pixel);
            _createBmpIcons();
            _ajScroll();
        }
        private void _ajScroll()
        {
            var m = (1 + Rows) * BoxSize - Height;
            _scrollBar.Maximum = (m > 0) ? m : 0;
        }
        public int IconPadding { get; set; } = 1;
        public int BoxSize { get; set; }
        public int Rows { get; private set; }

        private void _createBmpIcons()
        {
            if (Rows <= 0)
            {
                BmpIcons = null;
                return;
            }
            BmpIcons = new Bitmap(BoxSize * Columns, BoxSize * Rows);
            using (var g = Graphics.FromImage(BmpIcons))
            {
                g.TextRenderingHint = TextRenderingHint.AntiAlias;
                g.Clear(Color.Transparent);
                var br = new SolidBrush(Color.Black);
                for (var f = 0; f < _icons.Count; f++)
                {
                    var p = GetBox(f);
                    g.DrawString(_icons.GetChar(f), IconFont, br, p.X+IconPadding,p.Y+IconPadding);
                }
            }
        }
        public Rectangle GetBox(int index)
        {
            if (index < 0) return Rectangle.Empty;
            var y = index / Columns;
            var x = index % Columns;
            return new Rectangle(x * BoxSize, y * BoxSize, BoxSize, BoxSize);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (_overIcon != -1)
            {
                _overIcon = -1;
                Invalidate();
            }
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (_overIcon != -1)
            {
                _overIcon = -1;
                Invalidate();
            }
            base.OnMouseLeave(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            var p =new Point(e.Location.X,e.Location.Y+YOffset);
            var i = GetIcon(p);
            if (_overIcon != i)
            {
                _overIcon = i;
                Invalidate();
            }
            base.OnMouseMove(e);
        }
        public int GetIcon(Point point)
        {
            var c = point.X / BoxSize;
            var r = point.Y / BoxSize;
            if (c < 0 || c >= Columns) return -1;
            if (r < 0 || r >= Rows) return -1;
            return c + r * Columns;
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            _selectedIcon = _overIcon;
            Invalidate();
            base.OnMouseClick(e);
            SelectedIconChanged?.Invoke(this,SelectedIcon);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            _ajScroll();
            Invalidate();
        }
    }
}
