[System.Serializable]
public class Runnable
{
    public delegate void Action();

    private Action action;

    public Runnable(Action action)
    {
        this.action = action;
    }

    public void Run() 
    {
        action();
    }
}