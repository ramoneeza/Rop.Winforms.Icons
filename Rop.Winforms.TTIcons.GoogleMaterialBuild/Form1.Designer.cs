namespace Rop.Winforms.DuotoneIcons.GoogleMaterialBuild
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
            button1 = new Button();
            panel1 = new Panel();
            tabla = new PictureBox();
            panel2 = new Panel();
            barra = new ProgressBar();
            tabControl1 = new TabControl();
            tabicons = new TabPage();
            tabPage2 = new TabPage();
            txtcodes = new TextBox();
            button2 = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tabla).BeginInit();
            panel2.SuspendLayout();
            tabControl1.SuspendLayout();
            tabicons.SuspendLayout();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(12, 12);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "Load JSON";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.Controls.Add(tabla);
            panel1.Location = new Point(32, 47);
            panel1.Name = "panel1";
            panel1.Size = new Size(792, 417);
            panel1.TabIndex = 16;
            // 
            // tabla
            // 
            tabla.BackColor = Color.White;
            tabla.Location = new Point(3, 3);
            tabla.Name = "tabla";
            tabla.Size = new Size(768, 420);
            tabla.TabIndex = 14;
            tabla.TabStop = false;
            // 
            // panel2
            // 
            panel2.Controls.Add(barra);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 482);
            panel2.Name = "panel2";
            panel2.Size = new Size(1035, 34);
            panel2.TabIndex = 17;
            // 
            // barra
            // 
            barra.Location = new Point(13, 9);
            barra.Name = "barra";
            barra.Size = new Size(846, 15);
            barra.TabIndex = 0;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabicons);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(13, 45);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(988, 419);
            tabControl1.TabIndex = 18;
            // 
            // tabicons
            // 
            tabicons.Controls.Add(panel1);
            tabicons.Location = new Point(4, 24);
            tabicons.Name = "tabicons";
            tabicons.Padding = new Padding(3);
            tabicons.Size = new Size(980, 391);
            tabicons.TabIndex = 0;
            tabicons.Text = "Icons";
            tabicons.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(txtcodes);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(980, 391);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Codes";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtcodes
            // 
            txtcodes.Location = new Point(6, 6);
            txtcodes.Multiline = true;
            txtcodes.Name = "txtcodes";
            txtcodes.ScrollBars = ScrollBars.Vertical;
            txtcodes.Size = new Size(506, 379);
            txtcodes.TabIndex = 0;
            // 
            // button2
            // 
            button2.Location = new Point(93, 12);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 19;
            button2.Text = "LoadIcons";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1035, 516);
            Controls.Add(button2);
            Controls.Add(tabControl1);
            Controls.Add(panel2);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)tabla).EndInit();
            panel2.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tabicons.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Panel panel1;
        private PictureBox tabla;
        private Panel panel2;
        private ProgressBar barra;
        private TabControl tabControl1;
        private TabPage tabicons;
        private TabPage tabPage2;
        private TextBox txtcodes;
        private Button button2;
    }
}