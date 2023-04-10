namespace SelectBankIcon
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            cbBank = new ComboBox();
            lst = new ListBox();
            codelabel = new Label();
            charlabel = new Label();
            button1 = new Button();
            label3 = new Label();
            label4 = new Label();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            label1 = new Label();
            namelabel = new Label();
            biglabel = new Rop.Winforms.Icons.IconLabel();
            tabla = new PictureBox();
            panel1 = new Panel();
            edbuscar = new TextBox();
            btnbuscar = new Button();
            edicontext = new TextBox();
            edoffseticon = new NumericUpDown();
            label2 = new Label();
            label5 = new Label();
            edoffsettext = new NumericUpDown();
            btnicon = new Rop.Winforms.Icons.IconButton();
            ((System.ComponentModel.ISupportInitialize)tabla).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)edoffseticon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)edoffsettext).BeginInit();
            SuspendLayout();
            // 
            // cbBank
            // 
            cbBank.DropDownStyle = ComboBoxStyle.DropDownList;
            cbBank.FormattingEnabled = true;
            cbBank.Items.AddRange(new object[] { "FluentUI", "FontAwesone", "GoogleMaterial", "Ionicons", "MaterialDesign", "NotoEmoji" });
            cbBank.Location = new Point(487, 12);
            cbBank.Margin = new Padding(2, 3, 2, 3);
            cbBank.Name = "cbBank";
            cbBank.Size = new Size(195, 23);
            cbBank.TabIndex = 0;
            cbBank.SelectedIndexChanged += cbBank_SelectedIndexChanged;
            // 
            // lst
            // 
            lst.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            lst.DrawMode = DrawMode.OwnerDrawFixed;
            lst.FormattingEnabled = true;
            lst.IntegralHeight = false;
            lst.ItemHeight = 32;
            lst.Location = new Point(487, 43);
            lst.Margin = new Padding(2, 3, 2, 3);
            lst.Name = "lst";
            lst.Size = new Size(195, 420);
            lst.TabIndex = 1;
            lst.DrawItem += lst_DrawItem;
            lst.SelectedIndexChanged += lst_SelectedIndexChanged;
            // 
            // codelabel
            // 
            codelabel.AutoSize = true;
            codelabel.Location = new Point(700, 86);
            codelabel.Margin = new Padding(2, 0, 2, 0);
            codelabel.Name = "codelabel";
            codelabel.Size = new Size(68, 15);
            codelabel.TabIndex = 3;
            codelabel.Text = "\\U000Fxxxx";
            // 
            // charlabel
            // 
            charlabel.AutoSize = true;
            charlabel.Location = new Point(700, 151);
            charlabel.Margin = new Padding(2, 0, 2, 0);
            charlabel.Name = "charlabel";
            charlabel.Size = new Size(68, 15);
            charlabel.TabIndex = 4;
            charlabel.Text = "\\U000Fxxxx";
            // 
            // button1
            // 
            button1.Location = new Point(804, 77);
            button1.Margin = new Padding(2, 3, 2, 3);
            button1.Name = "button1";
            button1.Size = new Size(69, 25);
            button1.TabIndex = 5;
            button1.Text = "Copy";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(700, 62);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(35, 15);
            label3.TabIndex = 6;
            label3.Text = "Code";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(700, 126);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(32, 15);
            label4.TabIndex = 7;
            label4.Text = "Char";
            // 
            // button2
            // 
            button2.Location = new Point(804, 147);
            button2.Margin = new Padding(2, 3, 2, 3);
            button2.Name = "button2";
            button2.Size = new Size(69, 25);
            button2.TabIndex = 8;
            button2.Text = "Copy";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button3.Location = new Point(843, 444);
            button3.Margin = new Padding(2, 3, 2, 3);
            button3.Name = "button3";
            button3.Size = new Size(69, 25);
            button3.TabIndex = 9;
            button3.Text = "Exit";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(804, 219);
            button4.Margin = new Padding(2, 3, 2, 3);
            button4.Name = "button4";
            button4.Size = new Size(69, 25);
            button4.TabIndex = 12;
            button4.Text = "Copy";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(700, 198);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 11;
            label1.Text = "Name";
            // 
            // namelabel
            // 
            namelabel.AutoSize = true;
            namelabel.Location = new Point(700, 223);
            namelabel.Margin = new Padding(2, 0, 2, 0);
            namelabel.Name = "namelabel";
            namelabel.Size = new Size(31, 15);
            namelabel.TabIndex = 10;
            namelabel.Text = "xxxx";
            // 
            // biglabel
            // 
            biglabel.AutoSize = true;
            biglabel.BackColor = Color.White;
            biglabel.BankIcon = null;
            biglabel.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point);
            biglabel.IconColor = Color.Empty;
            biglabel.IconScale = 125;
            biglabel.Location = new Point(707, 267);
            biglabel.Name = "biglabel";
            biglabel.OffsetIcon = 0;
            biglabel.OffsetText = 0;
            biglabel.PrefixCode = null;
            biglabel.Size = new Size(166, 44);
            biglabel.SuffixCode = null;
            biglabel.TabIndex = 13;
            biglabel.Text = "iconLabel1";
            biglabel.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            biglabel.UseIconColor = false;
            // 
            // tabla
            // 
            tabla.BackColor = Color.White;
            tabla.Location = new Point(3, 3);
            tabla.Name = "tabla";
            tabla.Size = new Size(447, 420);
            tabla.TabIndex = 14;
            tabla.TabStop = false;
            tabla.Click += tabla_Click;
            tabla.MouseEnter += tabla_MouseEnter;
            tabla.MouseLeave += tabla_MouseLeave;
            tabla.MouseMove += tabla_MouseMove;
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.Controls.Add(tabla);
            panel1.Location = new Point(12, 43);
            panel1.Name = "panel1";
            panel1.Size = new Size(470, 406);
            panel1.TabIndex = 15;
            // 
            // edbuscar
            // 
            edbuscar.Location = new Point(700, 12);
            edbuscar.Name = "edbuscar";
            edbuscar.Size = new Size(100, 23);
            edbuscar.TabIndex = 16;
            // 
            // btnbuscar
            // 
            btnbuscar.Location = new Point(804, 12);
            btnbuscar.Name = "btnbuscar";
            btnbuscar.Size = new Size(69, 23);
            btnbuscar.TabIndex = 17;
            btnbuscar.Text = "Buscar";
            btnbuscar.UseVisualStyleBackColor = true;
            btnbuscar.Click += btnbuscar_Click;
            // 
            // edicontext
            // 
            edicontext.Location = new Point(759, 358);
            edicontext.Name = "edicontext";
            edicontext.Size = new Size(100, 23);
            edicontext.TabIndex = 18;
            edicontext.Text = "icon";
            edicontext.TextChanged += edicontext_TextChanged;
            // 
            // edoffseticon
            // 
            edoffseticon.Location = new Point(793, 387);
            edoffseticon.Maximum = new decimal(new int[] { 16, 0, 0, 0 });
            edoffseticon.Minimum = new decimal(new int[] { 16, 0, 0, int.MinValue });
            edoffseticon.Name = "edoffseticon";
            edoffseticon.Size = new Size(66, 23);
            edoffseticon.TabIndex = 19;
            edoffseticon.ValueChanged += edoffseticon_ValueChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(700, 389);
            label2.Name = "label2";
            label2.Size = new Size(62, 15);
            label2.TabIndex = 20;
            label2.Text = "OffsetIcon";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(700, 417);
            label5.Name = "label5";
            label5.Size = new Size(60, 15);
            label5.TabIndex = 22;
            label5.Text = "OffsetText";
            // 
            // edoffsettext
            // 
            edoffsettext.Location = new Point(793, 415);
            edoffsettext.Maximum = new decimal(new int[] { 16, 0, 0, 0 });
            edoffsettext.Minimum = new decimal(new int[] { 16, 0, 0, int.MinValue });
            edoffsettext.Name = "edoffsettext";
            edoffsettext.Size = new Size(66, 23);
            edoffsettext.TabIndex = 21;
            edoffsettext.ValueChanged += edoffsettext_ValueChanged;
            // 
            // btnicon
            // 
            btnicon.BankIcon = null;
            btnicon.IconColor = Color.Empty;
            btnicon.IconScale = 125;
            btnicon.Location = new Point(714, 322);
            btnicon.Name = "btnicon";
            btnicon.OffsetIcon = 0;
            btnicon.OffsetText = 0;
            btnicon.PrefixCode = null;
            btnicon.Size = new Size(158, 31);
            btnicon.SuffixCode = null;
            btnicon.TabIndex = 23;
            btnicon.Text = " iconButton";
            btnicon.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            
            btnicon.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(940, 480);
            Controls.Add(btnicon);
            Controls.Add(label5);
            Controls.Add(edoffsettext);
            Controls.Add(label2);
            Controls.Add(edoffseticon);
            Controls.Add(edicontext);
            Controls.Add(btnbuscar);
            Controls.Add(edbuscar);
            Controls.Add(panel1);
            Controls.Add(biglabel);
            Controls.Add(button4);
            Controls.Add(label1);
            Controls.Add(namelabel);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(button1);
            Controls.Add(charlabel);
            Controls.Add(codelabel);
            Controls.Add(lst);
            Controls.Add(cbBank);
            Margin = new Padding(2, 3, 2, 3);
            Name = "Form1";
            Text = "Select Bank Icon";
            ((System.ComponentModel.ISupportInitialize)tabla).EndInit();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)edoffseticon).EndInit();
            ((System.ComponentModel.ISupportInitialize)edoffsettext).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cbBank;
        private ListBox lst;
        private Rop.Winforms.Icons.IconLabel biglabel;
        private Label codelabel;
        private Label charlabel;
        private Button button1;
        private Label label3;
        private Label label4;
        private Button button2;
        private Button button3;
        private Button button4;
        private Label label1;
        private Label namelabel;
        private PictureBox tabla;
        private Panel panel1;
        private TextBox edbuscar;
        private Button btnbuscar;
        private TextBox edicontext;
        private NumericUpDown edoffseticon;
        private Label label2;
        private Label label5;
        private NumericUpDown edoffsettext;
        private Rop.Winforms.Icons.IconButton btnicon;
    }
}