namespace LostAnimals.Services.Settings;

public class LogSettings
{
    public string Level { get; private set; }
    public bool WriteToConsole { get; private set; }
    public bool WriteToFile { get; private set; }
    public string FileRollingInterval { get; private set; }
    public string FileRollingSize { get; private set; }

}
public enum LogLevel
{
    Verbose,
    Debug,
    Information,
    Warning,
    Error,
    Fatal
}

public enum LogRollingInterval
{
    Infinite,
    Year,
    Month,
    Day,
    Hour,
    Minute
}
