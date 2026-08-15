using BrickNova.Audio;

namespace BrickNova.UI;

public partial class AboutForm : Form
{
    private const string AppVersion = "1.0";

    private readonly AudioManager _audioManager;

    public AboutForm(AudioManager audioManager)
    {
        InitializeComponent();

        _audioManager = audioManager;

        Text = "About BrickNova";
        ClientSize = new Size(850, 600);
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
            Text = "BrickNova",
            Dock = DockStyle.Top,
            Height = 70,
            Font = new Font(
                "Arial",
                28,
                FontStyle.Bold
            ),
            TextAlign = ContentAlignment.MiddleCenter
        };

        Label versionLabel = new Label
        {
            Text = $"Version {AppVersion}",
            AutoSize = true,
            Font = new Font(
                "Arial",
                12,
                FontStyle.Regular
            ),
            Location = new Point(20, 90)
        };

        Label descriptionLabel = new Label
        {
            Text =
                "BrickNova is a classic brick-breaking game " +
                "inspired by the Arkanoid genre.\r\n\r\n" +
                "Control the paddle, bounce the ball and destroy " +
                "all bricks on each level.\r\n\r\n" +
                "Earn points, preserve your lives and progress " +
                "through increasingly challenging levels.\r\n\r\n" +
                "Your best results are saved in the High Scores.",

            AutoSize = true,
            Font = new Font(
                "Arial",
                12,
                FontStyle.Regular
            ),
            Location = new Point(20, 130)
        };

        Label featuresTitle = new Label
        {
            Text = "FEATURES",
            AutoSize = true,
            Font = new Font(
                "Arial",
                16,
                FontStyle.Bold
            ),
            Location = new Point(20, 245)
        };

        Label featuresLabel = new Label
        {
            Text =
                "• Multiple game levels\r\n" +
                "• Score and lives system\r\n" +
                "• Save / Continue progress\r\n" +
                "• High Scores\r\n" +
                "• Game Settings",

            AutoSize = true,
            Font = new Font(
                "Arial",
                12,
                FontStyle.Regular
            ),
            Location = new Point(30, 300)
        };

        Label technologyTitle = new Label
        {
            Text = "TECHNOLOGY",
            AutoSize = true,
            Font = new Font(
                "Arial",
                16,
                FontStyle.Bold
            ),
            Location = new Point(300, 245)
        };

        Label technologyLabel = new Label
        {
            Text =
                "C#\r\n" +
                "  Core game logic and application code\r\n\r\n" +

                "Windows Forms\r\n" +
                "  User interface and game window\r\n\r\n" +

                "SQLite\r\n" +
                "  Local storage for scores and game progress\r\n\r\n" +

                ".NET\r\n" +
                "  Runtime and application framework",
            
            AutoSize = true,
            Font = new Font(
                "Arial",
                11,
                FontStyle.Regular
            ),
            Location = new Point(310, 300)
        };

        Label copyrightLabel = new Label
        {
            Text = "© 2026 BrickNova",
            AutoSize = true,
            Font = new Font(
                "Arial",
                10,
                FontStyle.Regular
            ),
            Location = new Point(20, 380)
        };

        Button backButton = new Button
        {
            Text = "Back",
            Size = new Size(100, 35),
            Location = new Point(725, 550)
        };

        backButton.Click += (_, _) =>
        {
            _audioManager.PlayMenuBack();
            Close();
        };

        Controls.Add(titleLabel);
        Controls.Add(versionLabel);
        Controls.Add(descriptionLabel);
        Controls.Add(featuresTitle);
        Controls.Add(featuresLabel);
        Controls.Add(technologyTitle);
        Controls.Add(technologyLabel);
        Controls.Add(copyrightLabel);
        Controls.Add(backButton);
    }
}