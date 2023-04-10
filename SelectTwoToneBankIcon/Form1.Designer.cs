namespace SelectTwoToneBankIcon
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
            label5 = new Label();
            edoffsettext = new NumericUpDown();
            label2 = new Label();
            edoffseticon = new NumericUpDown();
            edicontext = new TextBox();
            btnbuscar = new Button();
            edbuscar = new TextBox();
            panel1 = new Panel();
            tabla = new PictureBox();
            button4 = new Button();
            label1 = new Label();
            namelabel = new Label();
            button3 = new Button();
            button2 = new Button();
            label4 = new Label();
            charlabel = new Label();
            lst = new ListBox();
            cbBank = new ComboBox();
            biglabel = new Rop.Winforms.DuotoneIcons.IconLabel();
            soloIcon = new Rop.Winforms.DuotoneIcons.SoloIconLabel();
            colorDialog1 = new ColorDialog();
            label3 = new Label();
            label6 = new Label();
            label7 = new Label();
            lcb = new Label();
            lc1 = new Label();
            lc2 = new Label();
            btnIcon = new Rop.Winforms.DuotoneIcons.IconButton();
            ((System.ComponentModel.ISupportInitialize)edoffsettext).BeginInit();
            ((System.ComponentModel.ISupportInitialize)edoffseticon).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tabla).BeginInit();
            SuspendLayout();
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(735, 420);
            label5.Name = "label5";
            label5.Size = new Size(60, 15);
            label5.TabIndex = 42;
            label5.Text = "OffsetText";
            // 
            // edoffsettext
            // 
            edoffsettext.Location = new Point(828, 418);
            edoffsettext.Maximum = new decimal(new int[] { 16, 0, 0, 0 });
            edoffsettext.Minimum = new decimal(new int[] { 16, 0, 0, int.MinValue });
            edoffsettext.Name = "edoffsettext";
            edoffsettext.Size = new Size(66, 23);
            edoffsettext.TabIndex = 41;
            edoffsettext.ValueChanged += edoffsettext_ValueChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(735, 392);
            label2.Name = "label2";
            label2.Size = new Size(62, 15);
            label2.TabIndex = 40;
            label2.Text = "OffsetIcon";
            // 
            // edoffseticon
            // 
            edoffseticon.Location = new Point(828, 390);
            edoffseticon.Maximum = new decimal(new int[] { 16, 0, 0, 0 });
            edoffseticon.Minimum = new decimal(new int[] { 16, 0, 0, int.MinValue });
            edoffseticon.Name = "edoffseticon";
            edoffseticon.Size = new Size(66, 23);
            edoffseticon.TabIndex = 39;
            edoffseticon.ValueChanged += edoffseticon_ValueChanged;
            // 
            // edicontext
            // 
            edicontext.Location = new Point(794, 361);
            edicontext.Name = "edicontext";
            edicontext.Size = new Size(100, 23);
            edicontext.TabIndex = 38;
            edicontext.Text = "icon";
            edicontext.TextChanged += edicontext_TextChanged;
            // 
            // btnbuscar
            // 
            btnbuscar.Location = new Point(839, 73);
            btnbuscar.Name = "btnbuscar";
            btnbuscar.Size = new Size(69, 23);
            btnbuscar.TabIndex = 37;
            btnbuscar.Text = "Buscar";
            btnbuscar.UseVisualStyleBackColor = true;
            btnbuscar.Click += btnbuscar_Click;
            // 
            // edbuscar
            // 
            edbuscar.Location = new Point(735, 73);
            edbuscar.Name = "edbuscar";
            edbuscar.Size = new Size(100, 23);
            edbuscar.TabIndex = 36;
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.Controls.Add(tabla);
            panel1.Location = new Point(47, 104);
            panel1.Name = "panel1";
            panel1.Size = new Size(470, 406);
            panel1.TabIndex = 35;
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
            // button4
            // 
            button4.Location = new Point(839, 200);
            button4.Margin = new Padding(2, 3, 2, 3);
            button4.Name = "button4";
            button4.Size = new Size(69, 25);
            button4.TabIndex = 34;
            button4.Text = "Copy";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(735, 179);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 33;
            label1.Text = "Name";
            // 
            // namelabel
            // 
            namelabel.AutoSize = true;
            namelabel.Location = new Point(735, 204);
            namelabel.Margin = new Padding(2, 0, 2, 0);
            namelabel.Name = "namelabel";
            namelabel.Size = new Size(31, 15);
            namelabel.TabIndex = 32;
            namelabel.Text = "xxxx";
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button3.Location = new Point(894, 596);
            button3.Margin = new Padding(2, 3, 2, 3);
            button3.Name = "button3";
            button3.Size = new Size(69, 25);
            button3.TabIndex = 31;
            button3.Text = "Exit";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.Location = new Point(839, 128);
            button2.Margin = new Padding(2, 3, 2, 3);
            button2.Name = "button2";
            button2.Size = new Size(69, 25);
            button2.TabIndex = 30;
            button2.Text = "Copy";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(735, 107);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(32, 15);
            label4.TabIndex = 29;
            label4.Text = "Char";
            // 
            // charlabel
            // 
            charlabel.AutoSize = true;
            charlabel.Location = new Point(735, 132);
            charlabel.Margin = new Padding(2, 0, 2, 0);
            charlabel.Name = "charlabel";
            charlabel.Size = new Size(68, 15);
            charlabel.TabIndex = 26;
            charlabel.Text = "\\U000Fxxxx";
            // 
            // lst
            // 
            lst.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            lst.DrawMode = DrawMode.OwnerDrawFixed;
            lst.FormattingEnabled = true;
            lst.IntegralHeight = false;
            lst.ItemHeight = 32;
            lst.Location = new Point(522, 104);
            lst.Margin = new Padding(2, 3, 2, 3);
            lst.Name = "lst";
            lst.Size = new Size(195, 463);
            lst.TabIndex = 24;
            lst.DrawItem += lst_DrawItem;
            lst.SelectedIndexChanged += lst_SelectedIndexChanged;
            // 
            // cbBank
            // 
            cbBank.DropDownStyle = ComboBoxStyle.DropDownList;
            cbBank.FormattingEnabled = true;
            cbBank.Items.AddRange(new object[] { "GoogleMaterial", "FontAwesone" });
            cbBank.Location = new Point(522, 73);
            cbBank.Margin = new Padding(2, 3, 2, 3);
            cbBank.Name = "cbBank";
            cbBank.Size = new Size(195, 23);
            cbBank.TabIndex = 23;
            cbBank.SelectedIndexChanged += cbBank_SelectedIndexChanged;
            // 
            // biglabel
            // 
            biglabel.AutoSize = true;
            biglabel.BackColor = Color.FromArgb(255, 224, 192);
            biglabel.BankIcon = null;
            biglabel.IconScale = 150;
            biglabel.Location = new Point(748, 324);
            biglabel.Name = "biglabel";
            biglabel.OffsetIcon = 0;
            biglabel.OffsetText = 0;
            biglabel.PrefixCode = "apple";
            biglabel.Size = new Size(26, 23);
            biglabel.SuffixCode = null;
            biglabel.TabIndex = 43;
            biglabel.Text = "Hola";
            biglabel.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            biglabel.UseIconColor = false;
            // 
            // soloIcon
            // 
            soloIcon.AutoSize = true;
            soloIcon.BankIcon = null;
            soloIcon.Code = null;
            soloIcon.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point);
            soloIcon.Location = new Point(748, 263);
            soloIcon.Name = "soloIcon";
            soloIcon.Size = new Size(35, 35);
            soloIcon.TabIndex = 44;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(735, 465);
            label3.Name = "label3";
            label3.Size = new Size(64, 15);
            label3.TabIndex = 45;
            label3.Text = "Color Back";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(754, 495);
            label6.Name = "label6";
            label6.Size = new Size(45, 15);
            label6.TabIndex = 46;
            label6.Text = "Color 1";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(752, 522);
            label7.Name = "label7";
            label7.Size = new Size(45, 15);
            label7.TabIndex = 47;
            label7.Text = "Color 2";
            // 
            // lcb
            // 
            lcb.BorderStyle = BorderStyle.FixedSingle;
            lcb.Location = new Point(828, 464);
            lcb.Name = "lcb";
            lcb.Size = new Size(45, 23);
            lcb.TabIndex = 48;
            lcb.Click += lcb_Click;
            // 
            // lc1
            // 
            lc1.BorderStyle = BorderStyle.FixedSingle;
            lc1.Location = new Point(828, 488);
            lc1.Name = "lc1";
            lc1.Size = new Size(45, 23);
            lc1.TabIndex = 49;
            lc1.Click += lc1_Click;
            // 
            // lc2
            // 
            lc2.BorderStyle = BorderStyle.FixedSingle;
            lc2.Location = new Point(828, 514);
            lc2.Name = "lc2";
            lc2.Size = new Size(45, 23);
            lc2.TabIndex = 50;
            lc2.Click += lc2_Click;
            // 
            // btnIcon
            // 
            btnIcon.BankIcon = null;
            btnIcon.IconScale = 125;
            btnIcon.Location = new Point(828, 246);
            btnIcon.Name = "btnIcon";
            btnIcon.OffsetIcon = 0;
            btnIcon.OffsetText = 0;
            btnIcon.PrefixCode = null;
            btnIcon.Size = new Size(114, 34);
            btnIcon.SuffixCode = null;
            btnIcon.TabIndex = 51;
            btnIcon.Text = "iconButton1";
            btnIcon.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            btnIcon.UseIconColor = false;
            btnIcon.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(994, 646);
            Controls.Add(btnIcon);
            Controls.Add(lc2);
            Controls.Add(lc1);
            Controls.Add(lcb);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label3);
            Controls.Add(soloIcon);
            Controls.Add(biglabel);
            Controls.Add(label5);
            Controls.Add(edoffsettext);
            Controls.Add(label2);
            Controls.Add(edoffseticon);
            Controls.Add(edicontext);
            Controls.Add(btnbuscar);
            Controls.Add(edbuscar);
            Controls.Add(panel1);
            Controls.Add(button4);
            Controls.Add(label1);
            Controls.Add(namelabel);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(label4);
            Controls.Add(charlabel);
            Controls.Add(lst);
            Controls.Add(cbBank);
            Name = "Form1";
            Text = "Select Bank Icon Two Tones";
            ((System.ComponentModel.ISupportInitialize)edoffsettext).EndInit();
            ((System.ComponentModel.ISupportInitialize)edoffseticon).EndInit();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)tabla).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label5;
        private NumericUpDown edoffsettext;
        private Label label2;
        private NumericUpDown edoffseticon;
        private TextBox edicontext;
        private Button btnbuscar;
        private TextBox edbuscar;
        private Panel panel1;
        private PictureBox tabla;
        private Button button4;
        private Label label1;
        private Label namelabel;
        private Button button3;
        private Button button2;
        private Label label4;
        private Label charlabel;
        private ListBox lst;
        private ComboBox cbBank;
        private Rop.Winforms.DuotoneIcons.IconLabel biglabel;
        private Rop.Winforms.DuotoneIcons.SoloIconLabel soloIcon;
        private ColorDialog colorDialog1;
        private Label label3;
        private Label label6;
        private Label label7;
        private Label lcb;
        private Label lc1;
        private Label lc2;
        private Rop.Winforms.DuotoneIcons.IconButton btnIcon;
    }
}