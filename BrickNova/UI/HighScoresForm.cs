using BrickNova.Audio;
using BrickNova.Models;

namespace BrickNova.UI;

public partial class HighScoresForm : Form
{
    private readonly List<ScoreRecord> _scores;

    private readonly DataGridView _scoresGrid;
    private readonly Button _backButton;

    private readonly AudioManager _audioManager;

    public HighScoresForm(List<ScoreRecord> scores, AudioManager audioManager)
    {
        InitializeComponent();

        _audioManager = audioManager;

        _scores = scores;

        Label titleLabel = new Label
        {
            Text = "HIGH SCORES",
            Dock = DockStyle.Top,
            Height = 55,
            Font = new Font(
                "Arial",
                24,
                FontStyle.Bold
            ),
            TextAlign = ContentAlignment.MiddleCenter
        };

        _scoresGrid = new DataGridView
        {
            Name = "scoresGrid",
            Location = new Point(20, 100),
            Size = new Size(560, 280),

            ReadOnly = true,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            AllowUserToResizeRows = false,

            SelectionMode = 
                DataGridViewSelectionMode.FullRowSelect,

            MultiSelect = false,

            AutoSizeColumnsMode = 
                DataGridViewAutoSizeColumnsMode.Fill,

            RowHeadersVisible = false
        };

        _scoresGrid.Columns.Add(
            "Rank",
            "Rank"
        );

        _scoresGrid.Columns.Add(
            "PlayerName",
            "Player"
        );

        _scoresGrid.Columns.Add(
            "Score",
            "Score"
        );

        _scoresGrid.Columns.Add(
            "Level",
            "Level"
        );

        _scoresGrid.Columns.Add(
            "CreatedAt",
            "Date"
        );

        _scoresGrid.Columns["CreatedAt"].Width = 130;

        _backButton = new Button
        {
            Name = "backButton",
            Text = "Back",
            Size = new Size(100, 35),
            Location = new Point(480, 400),
        };

        _backButton.Click += OnBackButtonClick;

        Controls.Add(titleLabel);
        Controls.Add(_scoresGrid);
        Controls.Add(_backButton);

        LoadScores();
    }

    private void LoadScores()
    {
        _scoresGrid.Rows.Clear();

        for (int i = 0; i < _scores.Count; i++)
        {
            ScoreRecord score = _scores[i];

            _scoresGrid.Rows.Add(
                i + 1,
                score.PlayerName,
                score.Score,
                score.Level,
                score.CreatedAt.ToString(
                    "dd.MM.yyyy HH:mm"
                )
            );
        }
    }

    private void OnBackButtonClick(
        object? sender,
        EventArgs e)
    {
        _audioManager.PlayMenuBack();
        Close();
    }
}
