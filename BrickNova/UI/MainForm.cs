using BrickNova.Game;

namespace BrickNova
{
    public partial class MainForm : Form
    {
        private readonly GameEngine _gameEngine;

        public MainForm()
        {
            InitializeComponent();

            Text = "BrickNova";
            ClientSize = new Size(800, 600);
            StartPosition = FormStartPosition.CenterScreen;

            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = true;

            BackColor = Color.Black;
            ForeColor = Color.White;

            DoubleBuffered = true;

            _gameEngine = new GameEngine();

            Load += OnFormLoad;
            FormClosing += OnFormClosing;
        }

        private void OnFormLoad(object? sender, EventArgs e)
        {
            _gameEngine.Start();
        }

        private void OnFormClosing(object? sender, FormClosingEventArgs e)
        {
            _gameEngine.Stop();
        }
    }
}
