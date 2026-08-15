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

            case Keys.N:
                State.NewGame = true;
                break;

            case Keys.C:
                State.Continue = true;
                break;

            case Keys.H:
                State.HighScores = true;
                break;

            case Keys.F1:
                State.Help = true;
                break;

            case Keys.F2:
                State.About = true;
                break;

            case Keys.F3:
                State.Settings = true;
                break;

            case Keys.F4:
                State.ResetProgress = true;
                break;

            case Keys.M:
                State.MainMenu = true;
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

            case Keys.N:
                State.NewGame = false;
                break;

            case Keys.C:
                State.Continue = false;
                break;

            case Keys.H:
                State.HighScores = false;
                break;

            case Keys.F1:
                State.Help = false;
                break;

            case Keys.F2:
                State.About = false;
                break;

            case Keys.F3:
                State.Settings = false;
                break;

            case Keys.F4:
                State.ResetProgress = false;
                break;

            case Keys.M:
                State.MainMenu = false;
                break;
        }
    }
}
