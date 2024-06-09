using AutoImport_WPF.config;

namespace AutoImport_WPF.log;

public static class LogConfig
{
    public static ILogger Logger { get; set; } = new ConsoleLogger();

    public static LogLevel LogLevel()
    {
        return config.LogLevel.Debug;
    }
}