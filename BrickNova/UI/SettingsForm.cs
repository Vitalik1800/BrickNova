using BrickNova.Audio;

namespace BrickNova.UI;

public partial class SettingsForm : Form
{
    private readonly AudioManager _audioManager;
    private readonly Action _saveSettings;

    private TrackBar _volumeTrackBar = null!;
    private Label _volumeValueLabel = null!;
    private CheckBox _soundCheckBox = null!;

    

    public SettingsForm(
        AudioManager audioManager,
        Action saveSettings)
    {
        InitializeComponent();

        _audioManager = audioManager;
        _saveSettings = saveSettings;

        Text = "Settings";
        ClientSize = new Size(500, 300);
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
            Text = "Settings",
            Dock = DockStyle.Top,
            Height = 55,
            Font = new Font(
                "Arial",
                24,
                FontStyle.Bold
            ),
            TextAlign = ContentAlignment.MiddleCenter
        };

        Label volumeLabel = new Label
        {
            Text = "Master Volume",
            AutoSize = true,
            Font = new Font(
                "Arial",
                12,
                FontStyle.Regular
            ),
            Location = new Point(30, 90)
        };

        _volumeValueLabel = new Label
        {
            AutoSize = true,
            Font = new Font(
                "Arial",
                10,
                FontStyle.Bold
            ),
            Location = new Point(430, 90)
        };

        _volumeTrackBar = new TrackBar
        {
            Name = "volumeTrackBar",
            Minimum = 0,
            Maximum = 100,
            TickFrequency = 10,
            Location = new Point(30, 120),
            Size = new Size(440, 45),

            Value = (int)(
                _audioManager.MasterVolume * 100
            )
        };

        UpdateVolumeLabel();

        _volumeTrackBar.ValueChanged += OnVolumeChanged;

        _soundCheckBox = new CheckBox
        {
            Name = "soundCheckBox",
            Text = "Sound Enabled",

            Checked = _audioManager.SoundEnabled,

            Font = new Font(
                "Arial",
                11,
                FontStyle.Regular
            ),

            Location = new Point(30, 180)
        };

        _soundCheckBox.CheckedChanged += OnSoundEnabledChanged;

        Button backButton = new Button
        {
            Name = "backButton",
            Text = "Back",
            Size = new Size(100, 35),
            Location = new Point(370,220)
        };

        backButton.Click += OnBackButtonClick;

        Controls.Add(titleLabel);
        Controls.Add(volumeLabel);
        Controls.Add(_volumeValueLabel);
        Controls.Add(_volumeTrackBar);
        Controls.Add(_soundCheckBox);
        Controls.Add(backButton);
    }

    private void OnVolumeChanged(
        object? sender,
        EventArgs e)
    {
        _audioManager.MasterVolume =
            _volumeTrackBar.Value / 100f;

        UpdateVolumeLabel();
    }

    private void UpdateVolumeLabel()
    {
        int volume =
            (int)(_audioManager.MasterVolume * 100);

        _volumeValueLabel.Text =
            $"{volume}%";
    }

    private void OnSoundEnabledChanged(
        object? sender,
        EventArgs e)
    {
        _audioManager.SoundEnabled = 
            _soundCheckBox.Checked;
    }

    private void OnBackButtonClick(
        object? sender,
        EventArgs e)
    {
        _audioManager.PlayMenuBack();

        _saveSettings();

        Close();
    }

}
