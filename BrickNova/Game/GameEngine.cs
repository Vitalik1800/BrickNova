using BrickNova.Input;

namespace BrickNova.Game;

public class GameEngine
{
    private readonly GameLoop _gameLoop;
    private readonly InputManager _inputManager;

    public GameEngine()
    {
        _inputManager = new InputManager();
        _gameLoop = new GameLoop(this);
    }

    public void Start()
    {
        _gameLoop.Start();
    }

    public void Stop()
    {
        _gameLoop.Stop();
    }

    public void UpdateCycle()
    {
        ProcessInput();
        Update();
        Render();
    }

    private void ProcessInput()
    {
        _inputManager.ProcessInput();
    }

    private void Update()
    {

    }

    private void Render()
    {

    }

}
