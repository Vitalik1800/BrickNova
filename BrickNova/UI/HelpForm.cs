using BrickNova.Audio;

namespace BrickNova.UI;

public partial class HelpForm : Form
{
    private readonly AudioManager _audioManager;

    public HelpForm(AudioManager audioManager)
    {
        InitializeComponent();

        _audioManager = audioManager;

        Text = "Help";
        ClientSize = new Size(600, 500);
        StartPosition = FormStartPosition.CenterParent;

        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        MinimizeBox = false;

        CreateControls();
    }

    private void CreateControls()
    {
        Label titleLabel = new Label
        {
            Text = "BrickNova - Help",
            Dock = DockStyle.Top,
            Height = 55,
            Font = new Font(
                "Arial",
                24,
                FontStyle.Bold
            ),
            TextAlign = ContentAlignment.MiddleCenter
        };

        Label controlsTitle = new Label
        {
            Text = "GAME CONTROLS",
            AutoSize = true,
            Font = new Font(
                "Arial",
                16,
                FontStyle.Bold
            ),
            Location = new Point(30, 80)
        };

        Label leftLabel = new Label
        {
            Text = "← / A",
            AutoSize = true,
            Font = new Font(
                "Arial",
                12,
                FontStyle.Bold
            ),
            Location = new Point(40, 125)
        };

        Label leftDescription = new Label
        {
            Text = "Move Paddle Left",
            AutoSize = true,
            Font = new Font(
                "Arial",
                12,
                FontStyle.Regular
            ),
            Location = new Point(150, 125)
        };

        Label rightLabel = new Label
        {
            Text = "→ / D",
            AutoSize = true,
            Font = new Font(
                "Arial",
                12,
                FontStyle.Bold
            ),
            Location = new Point(40, 160)
        };

        Label rightDescription = new Label
        {
            Text = "Move Paddle Right",
            AutoSize = true,
            Font = new Font(
                "Arial",
                12,
                FontStyle.Regular
            ),
            Location = new Point(150, 160)
        };

        Label spaceLabel = new Label
        {
            Text = "Space",
            AutoSize = true,
            Font = new Font(
                "Arial",
                12,
                FontStyle.Bold
            ),
            Location = new Point(40, 195)
        };

        Label spaceDescription = new Label
        {
            Text = "Start / Resume Game",
            AutoSize = true,
            Font = new Font(
                "Arial",
                12,
                FontStyle.Regular
            ),
            Location = new Point(150, 195)
        };

        Label escapeLabel = new Label
        {
            Text = "Esc",
            AutoSize = true,
            Font = new Font(
                "Arial",
                12,
                FontStyle.Bold
            ),
            Location = new Point(40, 230)
        };

        Label escapeDescription = new Label
        {
            Text = "Pause Game",
            AutoSize = true,
            Font = new Font(
                "Arial",
                12,
                FontStyle.Regular
            ),
            Location = new Point(150, 230)
        };

        Label restartLabel = new Label
        {
            Text = "R",
            AutoSize = true,
            Font = new Font(
                "Arial",
                12,
                FontStyle.Bold
            ),
            Location = new Point(40, 265)
        };

        Label restartDescription = new Label
        {
            Text = "Restart Game",
            AutoSize = true,
            Font = new Font(
                "Arial",
                12,
                FontStyle.Regular
            ),
            Location = new Point(150, 265)
        };

        Button backButton = new Button
        {
            Text = "Back",
            Size = new Size(100, 35),
            Location = new Point(480, 440)
        };

        backButton.Click += (_, _) =>
        {
            _audioManager.PlayMenuBack();
            Close();
        };

        Controls.Add(titleLabel);
        Controls.Add(controlsTitle);

        Controls.Add(leftLabel);
        Controls.Add(leftDescription);

        Controls.Add(rightLabel);
        Controls.Add(rightDescription);

        Controls.Add(spaceLabel);
        Controls.Add(spaceDescription);

        Controls.Add(escapeLabel);
        Controls.Add(escapeDescription);

        Controls.Add(restartLabel);
        Controls.Add(restartDescription);

        Controls.Add(backButton);
    }
}
