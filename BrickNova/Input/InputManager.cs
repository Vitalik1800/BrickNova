namespace BrickNova.Input;

public class InputManager
{
    public InputState State { get; } = new();

    public void ProcessKeyDown(Keys key)
    {
        switch (key)
        {
            case Keys.Left:
            case Keys.A:
                State.Left = true;
                break;

            case Keys.Right:
            case Keys.D:
                State.Right = true;
                break;

            case Keys.Escape:
                State.Pause = true;
                break;

            case Keys.Space:
                State.Start = true;
                break;

            case Keys.R:
                State.Restart = true;
                break;
        }
    }

    public void ProcessKeyUp(Keys key)
    {
        switch (key)
        {
            case Keys.Left:
            case Keys.A:
                State.Left = false;
                break;

            case Keys.Right:
            case Keys.D:
                State.Right = false;
                break;

            case Keys.Escape:
                State.Pause = false;
                break;

            case Keys.Space:
                State.Start = false;
                break;

            case Keys.R:
                State.Restart = false;
                break;
        }
    }

    public void ProcessInput()
    {

    }
}
