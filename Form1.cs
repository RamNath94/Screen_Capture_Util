namespace CapturePress
{
    public partial class Form1 : Form
    {
        private CapturePgm Pgm = new CapturePgm();

        public Form1()
        {
            InitializeComponent();
            Pgm.CleanUp();
            Pgm.CreateDoc();
            Pgm.SaveFolder();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Pgm.CaptureScreenshot();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Pgm.CloseDoc();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Pgm.CreateDoc();

        }
    }
}