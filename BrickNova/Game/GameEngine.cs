using BrickNova.Entities;
using BrickNova.Input;
using System.Diagnostics;

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

    private InputState _currentInput = new();

    private const int InitialLives = 3;

    private int _lives = InitialLives;

    public int Lives => _lives;

    private int _score;

    public int Score => _score;

    public int CurrentLevel => _levelManager.CurrentLevel;

    public GameEngine()
    {
        _inputManager = new InputManager();
        _gameLoop = new GameLoop(this);

        _ball = new Ball();
        _paddle = new Paddle();

        _levelManager = new LevelManager();

        _collisionManager = new CollisionManager();
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

        if (_gameState == GameState.Menu && _currentInput.Start)
        {
            _gameState = GameState.Playing;
        }

        if (_gameState == GameState.Playing && _currentInput.Pause)
        {
            _gameState = GameState.Paused;
        }

        if (_gameState == GameState.Paused && _currentInput.Start)
        {
            _gameState = GameState.Playing;
        }

        if ((_gameState == GameState.Victory ||
            _gameState == GameState.GameOver) &&
            _currentInput.Restart)
        {
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

        if (collisionResult.DestroyedBrick != null)
        {
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

        if (_lives <= 0)
        {
            SetGameOver();
            return;
        }

        _ball.Reset();
        _paddle.Reset();
    }

    private void AdvanceToNextLevel()
    {
        if (_levelManager.IsFinalLevel)
        {
            SetVictory();
            return;
        }

        int nextLevel = _levelManager.CurrentLevel + 1;

        _levelManager.LoadLevel(nextLevel);

        _ball.Reset();
        _paddle.Reset();
    }

    private void SetVictory()
    {
        if (_gameState == GameState.Playing)
        {
            _gameState = GameState.Victory;
        }
    }

    private void SetGameOver()
    {
        if (_gameState == GameState.Playing)
        {
            _gameState = GameState.GameOver;
        }
    }

    private void RestartGame()
    {
        _lives = InitialLives;
        _score = 0;

        _ball.Reset();
        _paddle.Reset();

        _levelManager.Reset();

        _gameState = GameState.Playing;
    }

}
