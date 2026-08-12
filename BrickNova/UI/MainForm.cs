using BrickNova.Game;
using BrickNova.Rendering;

namespace BrickNova
{
    public partial class MainForm : Form
    {
        private readonly GameEngine _gameEngine;
        private readonly Renderer _renderer;

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
            KeyPreview = true;

            _gameEngine = new GameEngine();
            _renderer = new Renderer();

            _gameEngine.RenderRequested += OnRenderRequested;

            Load += OnFormLoad;
            FormClosing += OnFormClosing;

            KeyDown += OnKeyDown;
            KeyUp += OnKeyUp;

            Paint += OnPaint;
        }

        private void OnFormLoad(object? sender, EventArgs e)
        {
            _gameEngine.Start();
        }

        private void OnFormClosing(
            object? sender, 
            FormClosingEventArgs e)
        {
            _gameEngine.RenderRequested -= OnRenderRequested;

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

        private void OnPaint(object? sender, PaintEventArgs e)
        {
            _renderer.Render(
                e.Graphics,
                _gameEngine.Bricks,
                _gameEngine.Paddle,
                _gameEngine.Ball,
                _gameEngine.Score,
                _gameEngine.Lives,
                _gameEngine.CurrentState
            );
        }

        private void OnRenderRequested()
        {
            Invalidate();
        }
    }
}
