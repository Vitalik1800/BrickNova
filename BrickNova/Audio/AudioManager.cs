using NAudio.Wave;

namespace BrickNova.Audio;

public class AudioManager
{
    private readonly string _soundsPath;

    private float _masterVolume = 1.0f;

    public float MasterVolume
    {
        get => _masterVolume;
        set => _masterVolume = 
            Math.Clamp(value, 0.0f, 1.0f);
    }

    private bool _soundEnabled = true;

    public bool SoundEnabled
    {
        get => _soundEnabled;
        set => _soundEnabled = value;
    }

    public AudioManager()
    {
        _soundsPath = Path.Combine(
            AppContext.BaseDirectory,
            "Assets",
            "Sounds"
        );
    }

    public void PlaySound(string filename)
    {
        if (!SoundEnabled || MasterVolume <= 0.0f)
        {
            return;
        }

        string filePath = Path.Combine(
            _soundsPath,
            filename
        );

        if (!File.Exists(filePath))
        {
            return;
        }

        AudioFileReader audioFile =
            new AudioFileReader(filePath);

        audioFile.Volume = _masterVolume;

        WaveOutEvent outputDevice =
            new WaveOutEvent();

        outputDevice.Init(audioFile);

        outputDevice.PlaybackStopped += (_, _) =>
        {
            outputDevice.Dispose();
            audioFile.Dispose();
        };

        outputDevice.Play();
    }

    public void PlayPaddleHit()
    {
        PlaySound("paddle_hit.wav");
    }

    public void PlayBrickHit()
    {
        PlaySound("brick_hit.wav");
    }

    public void PlayBrickDestroyed()
    {
        PlaySound("brick_destroyed.wav");
    }

    public void PlayBallLost()
    {
        PlaySound("ball_lost.wav");
    }

    public void PlayGameOver()
    {
        PlaySound("game_over.wav");
    }

    public void PlayVictory()
    {
        PlaySound("victory.wav");
    }

    public void PlayMenuSelect()
    {
        PlaySound("menu_select.wav");
    }

    public void PlayMenuBack()
    {
        PlaySound("menu_back.wav");
    }

    public void PlayLevelUp()
    {
        PlaySound("level_up.wav");
    }

    public void PlayUIClick()
    {
        PlaySound("ui_click.wav");
    }

    public void PlayPause()
    {
        PlaySound("pause.wav");
    }

    public void PlayResume()
    {
        PlaySound("resume.wav");
    }
}
