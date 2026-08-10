namespace BrickNova
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            Text = "BrickNova";
            ClientSize = new Size(800, 600);
            StartPosition = FormStartPosition.CenterScreen;

            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = true;
            DoubleBuffered = true;
        }
    }
}
