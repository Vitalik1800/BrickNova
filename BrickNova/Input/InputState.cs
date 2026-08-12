namespace BrickNova.Input;

public class InputState
{
    public bool Left {  get; set; }

    public bool Right { get; set; }

    public bool Pause { get; set; } 

    public bool Start { get; set; }
    public bool Restart { get; set; }

    public void ClearCommands()
    {
        Pause = false;
        Start = false;
        Restart = false;
    }
}
