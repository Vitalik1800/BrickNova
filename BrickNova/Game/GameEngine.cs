using BrickNova.Entities;
using BrickNova.Input;

namespace BrickNova.Game;

public class GameEngine
{
    private readonly GameLoop _gameLoop;
    private readonly InputManager _inputManager;
    private readonly Paddle _paddle;
    private GameState _gameState = GameState.Menu;
    public GameState CurrentState => _gameState;

    public GameEngine()
    {
        _inputManager = new InputManager();
        _gameLoop = new GameLoop(this);
        _paddle = new Paddle();
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
        Render();
    }

    private void ProcessInput()
    {
        InputState input = _inputManager.State;

        if (_gameState == GameState.Menu && input.Start)
        {
            _gameState = GameState.Playing;
        }

        if (_gameState == GameState.Playing && input.Pause)
        {
            _gameState = GameState.Paused;
        }

        if (_gameState == GameState.Paused && input.Start)
        {
            _gameState = GameState.Playing;
        }

        if (_gameState == GameState.Playing)
        {
            if (input.Left)
            {
                _paddle.MoveLeft();
            } 

            if (input.Right)
            {
                _paddle.MoveRight();
            }
        }

        input.ClearCommands();
    }

    private void Update()
    {
        if (_gameState != GameState.Playing)
        {
            return;
        }
    }

    private void Render()
    {

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


}
