using System.Drawing.Text;
using Rop.Winforms.Icons;
using Rop.Winforms.Icons.FluentUI;
using Rop.Winforms.Icons.FontAwesone;
using Rop.Winforms.Icons.GoogleMaterial;
using Rop.Winforms.Icons.Ionicons;
using Rop.Winforms.Icons.MaterialDesign;
using Rop.Winforms.Icons.NotoEmoji;

namespace SelectBankIcon
{
    public partial class Form1 : Form
    {
        public IBankIcon? CurrentBank { get; private set; }
        public Form1()
        {
            InitializeComponent();
        }

        private int _dwgcolumnas;
        private int _dwgfilas;
        private int _dwgsize;
        private int _tablawidth = 420;
        private int _lastover = -1;
        private Bitmap _dwg;
        private async void cbBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                biglabel.Text = edicontext.Text;
                lst.BeginUpdate();
                lst.Items.Clear();
                ClearLabels();
                var i = cbBank.SelectedIndex;
                var bname = (i < 0) ? "" : cbBank.Items[i] as string;
                CurrentBank = (bname?.ToLower()) switch
                {
                    "fontawesone" => CurrentBank = new FontAwesoneBank(),
                    "googlematerial" => CurrentBank = new GoogleMaterialBank(),
                    "ionicons" => CurrentBank = new IoniconsBank(),
                    "materialdesign" => CurrentBank = new MaterialDesignBank(),
                    "notoemoji" => CurrentBank = new NotoEmojiBank(),
                    "fluentui" => CurrentBank = new FluentUIBank(),
                    _ => null
                };
                _dwg = null;
                if (CurrentBank != null)
                {

                    var numcodes = CurrentBank.Bank.Codes.Count;
                    _dwgcolumnas = 16;
                    _dwgfilas = (int)Math.Ceiling((numcodes * 1.0) / _dwgcolumnas);
                    _dwgsize = 2 + (_tablawidth / _dwgcolumnas);
                    _dwg = new Bitmap(_dwgsize * _dwgcolumnas, _dwgsize * _dwgfilas);
                    var x = 0;
                    var y = 0;
                    var font = CurrentBank.Bank.GetFont(_dwgsize - 2, FontStyle.Regular, GraphicsUnit.Pixel);
                    var br = new SolidBrush(Color.Black);
                    using var gr = Graphics.FromImage(_dwg);


                    foreach (var codename in CurrentBank.Bank.Codes)
                    {
                        lst.Items.Add(codename);
                        gr.TextRenderingHint = TextRenderingHint.AntiAlias;
                        gr.DrawString(CurrentBank.Bank.GetChar(codename), font, br, 1 + x * _dwgsize, 1 + y * _dwgsize,
                            StringFormat.GenericTypographic);
                        x++;
                        if (x >= _dwgcolumnas)
                        {
                            x = 0;
                            y++;
                        }

                        await Task.Yield();
                    }

                    biglabel.BankIcon = CurrentBank;
                    btnicon.BankIcon = CurrentBank;
                    tabla.Size = _dwg.Size;
                    tabla.BackgroundImage = _dwg;
                }
            }
            finally
            {
                lst.EndUpdate();
            }
        }
        private void ClearLabels()
        {
            SetIcon("");
        }
        private void SetIcon(string name)
        {
            if (name == "" || CurrentBank is null)
            {
                biglabel.PrefixCode = "";
                btnicon.PrefixCode = "";
                codelabel.Text = "";
                charlabel.Text = "";
                return;
            }
            var code = CurrentBank.Bank.GetCode(name);
            var ch = CurrentBank.Bank.GetChar(name);
            biglabel.PrefixCode = name;
            btnicon.PrefixCode= name;
            codelabel.Text = ToUniHex(code);
            charlabel.Text = ToUniHex(ch);
            codelabel.Tag = code;
            charlabel.Tag = ch;
            namelabel.Text = name;
        }

        private string ToUniHex(string code)
        {
            var chs = code.ToCharArray();
            var hxs = chs.Select(c => $"\\U{(int)c:X4}");
            return string.Join("", hxs);
        }

        private void lst_SelectedIndexChanged(object sender, EventArgs e)
        {
            var i = lst.SelectedIndex;
            if (i < 0)
            {
                ClearLabels();
                return;
            }

            var codename = lst.Items[i] as string;
            SetIcon(codename ?? "");
        }

        private void lst_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            var i = e.Index;
            if (i < 0) return;
            var codename = lst.Items[i] as string;
            if (codename is null) return;
            var fc = new SolidBrush(e.ForeColor);
            var ficon = CurrentBank?.Bank.GetFont(18);
            if (ficon is not null && CurrentBank is not null)
            {
                var ch = CurrentBank.Bank.GetChar(codename);
                e.Graphics.DrawString(ch, ficon, fc, 0, e.Bounds.Y);
            }
            e.Graphics.DrawString(codename, e.Font ?? lst.Font, fc, 40, e.Bounds.Y + 4);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(codelabel.Tag as string ?? "");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(charlabel.Tag as string ?? "");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(namelabel.Text ?? "");
        }


        private Bitmap _dwgover;
        private void tabla_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dwgover is null) return;
            using var gr = Graphics.FromImage(_dwgover);
            var p = e.Location;
            var plast = new Point(p.X / _dwgsize, p.Y / _dwgsize);
            _lastover = plast.X + plast.Y * _dwgcolumnas;
            var x = _dwgsize * plast.X;
            var y = _dwgsize * plast.Y;
            var c = new SolidBrush(Color.FromArgb(128, Color.Yellow));
            gr.Clear(Color.Transparent);
            gr.FillRectangle(c, x, y, _dwgsize, _dwgsize);
            gr.DrawImage(_dwg, x, y, new Rectangle(x, y, _dwgsize, _dwgsize), GraphicsUnit.Pixel);
            tabla.Image = _dwgover;
        }

        private void tabla_MouseEnter(object sender, EventArgs e)
        {
            if (_dwg is null)
            {
                _dwgover = null;
                _lastover = -1;
                return;
            }
            _dwgover = new Bitmap(_dwg.Width, _dwg.Height);
        }

        private void tabla_MouseLeave(object sender, EventArgs e)
        {
            tabla.Image = null;
            _dwgover = null;
            _lastover = -1;
        }

        private void tabla_Click(object sender, EventArgs e)
        {
            if (_lastover == -1) return;
            lst.SelectedIndex = _lastover;
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            var txt = edbuscar.Text;
            if (string.IsNullOrEmpty(txt)) return;
            var i = lst.SelectedIndex + 1;
            var n = lst.Items.Count;
            for (var j = i; j < n; j++)
            {
                var codename = lst.Items[j] as string;
                if (codename is null) continue;
                if (codename.Contains(txt))
                {
                    lst.SelectedIndex = j;
                    return;
                }
            }
        }

        private void edoffseticon_ValueChanged(object sender, EventArgs e)
        {
            biglabel.OffsetIcon = (int)edoffseticon.Value;
        }

        private void edoffsettext_ValueChanged(object sender, EventArgs e)
        {
            biglabel.OffsetText = (int)edoffsettext.Value;
        }

        private void edicontext_TextChanged(object sender, EventArgs e)
        {
            biglabel.Text = edicontext.Text;
        }
    }
}