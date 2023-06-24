using System.Diagnostics;

public class RateLimiter
{
    private Stopwatch stopwatch;
    private float msSinceLastEvent;
    private float minMsBetweenEvents;

    public RateLimiter(float _minMsBetweenEvents)
    {
        stopwatch = new Stopwatch();
        stopwatch.Start();
        msSinceLastEvent = 0f;
        minMsBetweenEvents = _minMsBetweenEvents;
    }

    public bool ShouldAllowEvent()
    {
        msSinceLastEvent += stopwatch.ElapsedMilliseconds;
        stopwatch.Reset();
        stopwatch.Start();
        if (msSinceLastEvent < minMsBetweenEvents)
        {
            return false;
        }
        msSinceLastEvent = 0f;
        return true;
    }
}
