namespace TestDuotone
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            iconIndexLabel1.UseAscent = true;
            propertyGrid1.SelectedObject = iconIndexLabel1;
        }
    }
}