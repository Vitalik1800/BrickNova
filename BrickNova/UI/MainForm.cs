using BrickNova.Game;
using BrickNova.Models;
using BrickNova.Rendering;
using BrickNova.UI;

namespace BrickNova;

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
        _gameEngine.ContinueFailed += OnContinueFailed;
        _gameEngine.HighScoresRequested += ShowHighScores;
        _gameEngine.HelpRequested += ShowHelp;
        _gameEngine.AboutRequested += ShowAbout;
        _gameEngine.SettingsRequested += OnSettingsRequested;
        _gameEngine.ResetProgressRequested += OnResetProgressRequested;
        _gameEngine.PlayerNameRequested += OnPlayerNameRequested;
        _gameEngine.ExitRequested += OnExitRequested;

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
        _gameEngine.ContinueFailed -= OnContinueFailed;
        _gameEngine.HighScoresRequested -= ShowHighScores;
        _gameEngine.HelpRequested -= ShowHelp;
        _gameEngine.AboutRequested -= ShowAbout;
        _gameEngine.SettingsRequested -= OnSettingsRequested;
        _gameEngine.ResetProgressRequested -= OnResetProgressRequested;
        _gameEngine.PlayerNameRequested -= OnPlayerNameRequested;
        _gameEngine.ExitRequested -= OnExitRequested;

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
            _gameEngine.CurrentLevel,
            _gameEngine.CurrentState
        );
    }

    private void OnContinueFailed()
    {
        MessageBox.Show(
            "No saved progress found.",
            "BrickNova",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information
        );
    }

    private void ShowHighScores()
    {
        List<ScoreRecord> scores =
            _gameEngine.GetHighScores();

        using HighScoresForm form =
            new HighScoresForm(scores, _gameEngine.AudioManager);

        form.ShowDialog(this);
    }

    private void ShowHelp()
    {
        using HelpForm form =
            new HelpForm(_gameEngine.AudioManager);

        form.ShowDialog(this);
    }

    private void ShowAbout()
    {
        using AboutForm form = 
            new AboutForm(_gameEngine.AudioManager);

        form.ShowDialog(this);
    }

    private void OnSettingsRequested()
    {
        using SettingsForm form = 
            new SettingsForm(
                _gameEngine.AudioManager,
                _gameEngine.SaveAudioSettings
            );

        form.ShowDialog(this);
    }

    private void OnPlayerNameRequested()
    {
        using PlayerNameForm form = 
            new PlayerNameForm();

        if (form.ShowDialog(this) == DialogResult.OK)
        {
            _gameEngine.SavePlayerScore(
                form.PlayerName
            );
        }
        else
        {
            _gameEngine.SavePlayerScore(
                "Player"
            );
        }
    }

    private void OnExitRequested()
    {
        DialogResult result = MessageBox.Show(
            "Exit BrickNova?",
            "BrickNova",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        );

        if (result != DialogResult.Yes)
        {
            return;
        }

        Close();
    }

    private void OnRenderRequested()
    {
        Invalidate();
    }

    private void OnResetProgressRequested()
    {
        DialogResult result = MessageBox.Show(
            "Delete saved progress?",
            "BrickNova",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning
        );

        if (result != DialogResult.Yes)
        {
            return;
        }

        _gameEngine.ResetProgress();

        MessageBox.Show(
            "Progress deleted.",
            "BrickNova",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information
        );
    }
}
