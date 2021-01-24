public sealed class Context
{
    public bool IsRun { get; set; }
    public int Score { get; set; }

    public Context()
    {
        IsRun = false;
        Score = 0;
    }
}
