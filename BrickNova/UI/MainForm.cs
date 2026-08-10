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
            KeyPreview = true;

            _gameEngine = new GameEngine();

            Load += OnFormLoad;
            FormClosing += OnFormClosing;

            KeyDown += OnKeyDown;
            KeyUp += OnKeyUp;
        }

        private void OnFormLoad(object? sender, EventArgs e)
        {
            _gameEngine.Start();
        }

        private void OnFormClosing(object? sender, FormClosingEventArgs e)
        {
            _gameEngine.Stop();
        }

        private void OnKeyDown(object? sender, KeyEventArgs e)
        {
            _gameEngine.HandleKeyDown(e.KeyCode);
        }

        private void OnKeyUp(object? sender, KeyEventArgs e)
        {
            _gameEngine.HandleKeyUp(e.KeyCode);
        }
    }
}
