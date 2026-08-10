using BrickNova.Entities;
using BrickNova.Input;
using System.Diagnostics;

namespace BrickNova.Game;

public class GameEngine
{
    private readonly GameLoop _gameLoop;
    private readonly InputManager _inputManager;
    private readonly Paddle _paddle;
    private GameState _gameState;

    public GameEngine()
    {
        _inputManager = new InputManager();
        _gameLoop = new GameLoop(this);
        _paddle = new Paddle();
        _gameState = GameState.Menu;
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

        if (input.Pause)
        {
            _gameState = GameState.Paused;
        }

        if (input.Start)
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

    }

    private void Render()
    {

    }

}
