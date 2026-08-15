namespace BrickNova.Input;

public class InputState
{
    public bool Left {  get; set; }

    public bool Right { get; set; }

    public bool Pause { get; set; } 

    public bool Start { get; set; }
    public bool Restart { get; set; }
    public bool NewGame { get; set; }
    public bool Continue { get; set; }
    public bool HighScores { get; set; }
    public bool Help { get; set; }
    public bool About { get; set; }
    public bool Settings { get; set; }
    public bool ResetProgress { get; set; }
    public bool MainMenu { get; set; }

    public void ClearCommands()
    {
        Pause = false;
        Start = false;
        Restart = false;
        NewGame = false;
        Continue = false;
        HighScores = false;
        Help = false;
        About = false;
        Settings = false;
        ResetProgress = false;
        MainMenu = false;
    }
}
