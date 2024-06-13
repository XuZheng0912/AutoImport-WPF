using AutoImport_WPF.config;
using AutoImport_WPF.log.logger;

namespace AutoImport_WPF.log;

public static class LogConfig
{
    public static ILogger Logger { get; set; } = new ConsoleLogger();

    public static LogLevel LogLevel()
    {
        return config.LogLevel.Debug;
    }
}