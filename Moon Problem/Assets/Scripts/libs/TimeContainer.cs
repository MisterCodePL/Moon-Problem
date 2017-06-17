using System;

public class TimeContainer
{
    public float AllSeconds { get; private set; }
    public int Minutes { get; private set; }
    public int Seconds { get; private set; }

    public TimeContainer(float allSeconds)
    {
        SetTime(allSeconds);
    }

    public void SetTime(float allSeconds)
    {
        AllSeconds = allSeconds;
        Minutes = (int)(AllSeconds / 60);
        Seconds = (int)(AllSeconds - Minutes * 60);
    }

    public void AddSeconds(float seconds)
    {
        SetTime(AllSeconds + seconds);
    }
}
