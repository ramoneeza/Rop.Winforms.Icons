namespace Rop.Winforms.DuotoneIcons.MaterialDesignBuild
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
            lst = new ListBox();
            pbicon = new PictureBox();
            materialDesignBank1 = new Icons.MaterialDesign.MaterialDesignBank();
            button1 = new Button();
            button2 = new Button();
            bmodebox = new Button();
            bmodeline = new Button();
            bmodecircle = new Button();
            button3 = new Button();
            button4 = new Button();
            bmmodefill = new Button();
            button5 = new Button();
            barra = new ProgressBar();
            ((System.ComponentModel.ISupportInitialize)pbicon).BeginInit();
            SuspendLayout();
            // 
            // lst
            // 
            lst.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            lst.DrawMode = DrawMode.OwnerDrawFixed;
            lst.FormattingEnabled = true;
            lst.IntegralHeight = false;
            lst.ItemHeight = 32;
            lst.Location = new Point(11, 62);
            lst.Margin = new Padding(2, 3, 2, 3);
            lst.Name = "lst";
            lst.Size = new Size(195, 437);
            lst.TabIndex = 2;
            lst.DrawItem += lst_DrawItem;
            lst.SelectedIndexChanged += lst_SelectedIndexChanged;
            // 
            // pbicon
            // 
            pbicon.Location = new Point(224, 138);
            pbicon.Name = "pbicon";
            pbicon.Size = new Size(288, 288);
            pbicon.SizeMode = PictureBoxSizeMode.Zoom;
            pbicon.TabIndex = 3;
            pbicon.TabStop = false;
            pbicon.Paint += pbicon_Paint;
            pbicon.MouseClick += pbicon_MouseClick;
            pbicon.MouseDown += pbicon_MouseDown;
            pbicon.MouseMove += pbicon_MouseMove;
            // 
            // button1
            // 
            button1.Location = new Point(224, 62);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 4;
            button1.Text = "Save";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(437, 62);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 5;
            button2.Text = "Reload";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // bmodebox
            // 
            bmodebox.Location = new Point(224, 91);
            bmodebox.Name = "bmodebox";
            bmodebox.Size = new Size(42, 25);
            bmodebox.TabIndex = 6;
            bmodebox.Text = "Box";
            bmodebox.UseVisualStyleBackColor = true;
            bmodebox.Click += button3_Click;
            // 
            // bmodeline
            // 
            bmodeline.Location = new Point(272, 91);
            bmodeline.Name = "bmodeline";
            bmodeline.Size = new Size(42, 25);
            bmodeline.TabIndex = 7;
            bmodeline.Text = "Line";
            bmodeline.UseVisualStyleBackColor = true;
            bmodeline.Click += bmodeline_Click;
            // 
            // bmodecircle
            // 
            bmodecircle.Location = new Point(320, 91);
            bmodecircle.Name = "bmodecircle";
            bmodecircle.Size = new Size(53, 25);
            bmodecircle.TabIndex = 8;
            bmodecircle.Text = "Circle";
            bmodecircle.UseVisualStyleBackColor = true;
            bmodecircle.Click += bmodecircle_Click;
            // 
            // button3
            // 
            button3.Location = new Point(459, 91);
            button3.Name = "button3";
            button3.Size = new Size(85, 25);
            button3.TabIndex = 9;
            button3.Text = "Copy Mask";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click_1;
            // 
            // button4
            // 
            button4.Location = new Point(550, 91);
            button4.Name = "button4";
            button4.Size = new Size(85, 25);
            button4.TabIndex = 10;
            button4.Text = "Paste Mask";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // bmmodefill
            // 
            bmmodefill.Location = new Point(379, 91);
            bmmodefill.Name = "bmmodefill";
            bmmodefill.Size = new Size(53, 25);
            bmmodefill.TabIndex = 11;
            bmmodefill.Text = "Fill";
            bmmodefill.UseVisualStyleBackColor = true;
            bmmodefill.Click += button5_Click;
            // 
            // button5
            // 
            button5.Location = new Point(47, 21);
            button5.Name = "button5";
            button5.Size = new Size(87, 25);
            button5.TabIndex = 12;
            button5.Text = "MAKE!";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click_1;
            // 
            // barra
            // 
            barra.Location = new Point(153, 19);
            barra.Name = "barra";
            barra.Size = new Size(626, 15);
            barra.TabIndex = 13;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 511);
            Controls.Add(barra);
            Controls.Add(button5);
            Controls.Add(bmmodefill);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(bmodecircle);
            Controls.Add(bmodeline);
            Controls.Add(bmodebox);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(pbicon);
            Controls.Add(lst);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pbicon).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ListBox lst;
        private PictureBox pbicon;
        public Icons.MaterialDesign.MaterialDesignBank materialDesignBank1;
        private Button button1;
        private Button button2;
        private Button bmodebox;
        private Button bmodeline;
        private Button bmodecircle;
        private Button button3;
        private Button button4;
        private Button bmmodefill;
        private Button button5;
        private ProgressBar barra;
    }
}