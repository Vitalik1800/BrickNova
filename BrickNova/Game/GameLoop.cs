using System;
using System.Windows.Forms;

namespace BrickNova.Game;

public class GameLoop
{
    private readonly System.Windows.Forms.Timer _timer;
    private readonly GameEngine _gameEngine;

    private bool _isRunning;

    public GameLoop(GameEngine gameEngine)
    {
        _gameEngine = gameEngine;

        _timer = new System.Windows.Forms.Timer
        {
            Interval = 16
        };

        _timer.Tick += OnTimerTick;
    }

    public void Start()
    {
        if (_isRunning)
        {
            return;
        }

        _isRunning = true;
        _timer.Start();
    }

    public void Stop()
    {
        if (!_isRunning)
        {
            return;
        }

        _isRunning = false;
        _timer.Stop();
    }

    public void Tick()
    {
        _gameEngine.UpdateCycle();
    }

    private void OnTimerTick(object? sender, EventArgs e)
    {
        Tick();
    }

}
