using BrickNova.Audio;
using BrickNova.Collision;
using BrickNova.Database;
using BrickNova.Entities;
using BrickNova.Input;
using BrickNova.Levels;
using BrickNova.Models;

namespace BrickNova.Game;

public class GameEngine
{
    private readonly GameLoop _gameLoop;
    private readonly InputManager _inputManager;

    private readonly Ball _ball;
    private readonly Paddle _paddle;

    private readonly LevelManager _levelManager;

    private readonly CollisionManager _collisionManager;

    public IReadOnlyList<Brick> Bricks =>
        _levelManager.Bricks;

    public Paddle Paddle => _paddle;

    public Ball Ball => _ball;

    private GameState _gameState = GameState.Menu;

    public GameState CurrentState => _gameState;

    public event Action? RenderRequested;
    public event Action? ContinueFailed;
    public event Action? HighScoresRequested;
    public event Action? HelpRequested;
    public event Action? AboutRequested;
    public event Action? SettingsRequested;
    public event Action? ResetProgressRequested;
    public event Action? PlayerNameRequested;
    public event Action? ExitRequested;

    private InputState _currentInput = new();

    private const int InitialLives = 3;

    private int _lives = InitialLives;

    public int Lives => _lives;

    private int _score;

    public int Score => _score;

    public int CurrentLevel => _levelManager.CurrentLevel;

    private readonly AudioManager _audioManager;

    public AudioManager AudioManager => _audioManager;

    private readonly DatabaseManager _databaseManager;

    private readonly ScoreRepository _scoreRepository;

    private readonly GameProgressRepository _gameProgressRepository;

    private readonly SettingsRepository _settingsRepository;

    private bool _scoreSaved;
    
    public GameEngine()
    {
        _inputManager = new InputManager();
        _gameLoop = new GameLoop(this);

        _ball = new Ball();
        _paddle = new Paddle();

        _levelManager = new LevelManager();

        _collisionManager = new CollisionManager();

        _audioManager = new AudioManager();

        _databaseManager = new DatabaseManager();
        _databaseManager.Initialize();

        _scoreRepository = new ScoreRepository(
            _databaseManager
        );

        _gameProgressRepository = 
            new GameProgressRepository(
                _databaseManager
        );

        _settingsRepository = 
            new SettingsRepository(
                _databaseManager
        );

        _settingsRepository.Initialize();

        LoadAudioSettings();
    }

    public void Start()
    {
        _gameLoop.Start();
    }

    public void Stop()
    {
        _gameLoop.Stop();
    }

    public void HandleKeyDown(Keys key)
    {
        _inputManager.ProcessKeyDown(key);
    }

    public void HandleKeyUp(Keys key)
    {
        _inputManager.ProcessKeyUp(key);
    }

    public void UpdateCycle()
    {
        ProcessInput();
        Update();

        RenderRequested?.Invoke();
    }

    private void ProcessInput()
    {
        _currentInput = _inputManager.State;

        if (_gameState == GameState.Menu)
        {
            if (_currentInput.NewGame)
            {
                _currentInput.NewGame = false;

                NewGame();

                return;
            }

            if (_currentInput.Continue)
            {
                _currentInput.Continue = false;

                if (!ContinueGame())
                {
                    ContinueFailed?.Invoke();
                }

                return;
            }

            if (_currentInput.Help)
            {
                _currentInput.Help = false;

                HelpRequested?.Invoke();

                return;
            }

            if (_currentInput.HighScores)
            {
                _currentInput.HighScores = false;

                HighScoresRequested?.Invoke();

                return;
            }

            if (_currentInput.About)
            {
                _currentInput.About = false;

                AboutRequested?.Invoke();

                return;
            }

            if (_currentInput.Settings)
            {
                _currentInput.Settings = false;

                SettingsRequested?.Invoke();

                return;
            }

            if (_currentInput.ResetProgress)
            {
                _currentInput.ResetProgress = false;

                ResetProgressRequested?.Invoke();

                return;
            }

            if (_currentInput.Pause)
            {
                _currentInput.Pause = false;

                ExitRequested?.Invoke();

                return;
            }
        }

        if (_currentInput.Pause)
        {
            _currentInput.Pause = false;

            if (_gameState == GameState.Playing)
            {
                _gameState = GameState.Paused;

                _audioManager.PlayPause();
            }
            else if (_gameState == GameState.Paused)
            {
                _gameState = GameState.Playing;

                _audioManager.PlayResume();
            }
        }

        if (_gameState == GameState.Paused &&
            _currentInput.Start)
        {
            _currentInput.Start = false;

            _gameState = GameState.Playing;

            _audioManager.PlayResume();
        }

        if ((_gameState == GameState.Paused ||
             _gameState == GameState.GameOver ||
             _gameState == GameState.Victory) &&
            _currentInput.MainMenu)
        {
            _currentInput.MainMenu = false;

            _gameState = GameState.Menu;
        }

        if ((_gameState == GameState.Paused ||
            _gameState == GameState.Victory ||
            _gameState == GameState.GameOver) &&
            _currentInput.Restart)
        {
            _currentInput.Restart = false;

            RestartGame();
        }

        _currentInput.ClearCommands();
    }

    private void Update()
    {
        if (_gameState != GameState.Playing)
        {
            return;
        }

        if (_currentInput.Left)
        {
            _paddle.MoveLeft();
        }

        if (_currentInput.Right)
        {
            _paddle.MoveRight();
        }

        _ball.Move();

        CollisionResult collisionResult = _collisionManager.Update(
            _ball,
            _paddle,
            _levelManager.Bricks
        );

        if (collisionResult.PaddleHit)
        {
            _audioManager.PlayPaddleHit();
        }

        if (collisionResult.DestroyedBrick != null)
        {
            _audioManager.PlayBrickDestroyed();

            AddScore(collisionResult.DestroyedBrick.Points);

            if (_levelManager.IsLevelCompleted())
            {
                AdvanceToNextLevel();
                return;
            }
        }

        if (collisionResult.BallLost)
        {
            HandleBallLost();
        }
    }

    private void AddScore(int points)
    {
        _score += points;
    }

    private void HandleBallLost()
    {
        _lives--;

        _audioManager.PlayBallLost();

        if (_lives <= 0)
        {
            SetGameOver();
            return;
        }

        _ball.Reset();
        _paddle.Reset();

        SaveProgress();
    }

    private void AdvanceToNextLevel()
    {
        if (_levelManager.IsFinalLevel)
        {
            SetVictory();
            return;
        }

        _audioManager.PlayLevelUp();

        int nextLevel = _levelManager.CurrentLevel + 1;

        _levelManager.LoadLevel(nextLevel);

        _ball.Reset();
        _paddle.Reset();

        SaveProgress();
    }

    private void SaveScore(string playerName)
    {
        if (_scoreSaved)
        {
            return;
        }

        ScoreRecord record = new ScoreRecord
        {
            PlayerName = playerName,
            Score = _score,
            Level = CurrentLevel,
            CreatedAt = DateTime.Now
        };

        _scoreRepository.SaveScore(record);

        _scoreSaved = true;
    }

    public void SavePlayerScore(string playerName)
    {
        if (string.IsNullOrWhiteSpace(playerName))
        {
            return;
        }

        SaveScore(playerName);
    }

    public List<ScoreRecord> GetHighScores()
    {
        return _scoreRepository.GetHighScores();
    }

    private void SaveProgress()
    {
        GameProgress progress = new GameProgress
        {
            Id = 1,
            CurrentLevel = CurrentLevel,
            Score = _score,
            Lives = _lives,
            UpdatedAt = DateTime.Now
        };

        _gameProgressRepository.SaveProgress(progress);
    }

    private bool ContinueGame()
    {
        GameProgress? progress = 
            _gameProgressRepository.LoadProgress();

        if (progress == null)
        {
            return false;
        }

        if (progress.CurrentLevel < 1 ||
            progress.CurrentLevel > _levelManager.TotalLevels)
        {
            return false;
        }

        if (progress.Lives < 1 ||
            progress.Lives > InitialLives)
        {
            return false;
        }

        _score = progress.Score;
        _lives = progress.Lives;

        _levelManager.LoadLevel(
            progress.CurrentLevel
        );

        _ball.Reset();
        _paddle.Reset();

        _gameState = GameState.Playing;

        return true;
    }

    public void NewGame()
    {
        _score = 0;
        _lives = InitialLives;

        _levelManager.LoadLevel(1);

        _ball.Reset();
        _paddle.Reset();

        _gameProgressRepository.DeleteProgress();

        _gameState = GameState.Playing;
    }

    public void ResetProgress()
    {
        _gameProgressRepository.DeleteProgress();
    }

    private void SetVictory()
    {
        if (_gameState != GameState.Playing)
        {
            return;
        }

        _gameState = GameState.Victory;

        _audioManager.PlayVictory();

        _gameProgressRepository.DeleteProgress();

        PlayerNameRequested?.Invoke();
    }

    private void SetGameOver()
    {
        if (_gameState != GameState.Playing)
        {
            return;
        }

        _gameState = GameState.GameOver;

        _audioManager.PlayGameOver();

        _gameProgressRepository.DeleteProgress();

        PlayerNameRequested?.Invoke();

    }

    private void RestartGame()
    {
        _lives = InitialLives;

        _scoreSaved = false;

        _levelManager.LoadLevel(CurrentLevel);

        _ball.Reset();
        _paddle.Reset();

        _gameState = GameState.Playing;
    }

    private void LoadAudioSettings()
    {
        GameSettings settings =
            _settingsRepository.LoadSettings();

        _audioManager.MasterVolume = 
            settings.MasterVolume;

        _audioManager.SoundEnabled = 
            settings.SoundEnabled;
    }

    public void SaveAudioSettings()
    {
        GameSettings settings = new GameSettings
        {
            Id = 1,

            MasterVolume = 
                _audioManager.MasterVolume,

            SoundEnabled = 
                _audioManager.SoundEnabled,

            UpdatedAt = DateTime.Now
        };

        _settingsRepository.SaveSettings(settings);
    }

}
