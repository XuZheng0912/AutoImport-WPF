using AutoImport_WPF.config;

namespace AutoImport_WPF.log.logger;

public class CommonLogger(Action<string> log) : ILogger
{
    public static bool EnableDebug()
    {
        return EnableLog(LogLevel.Debug);
    }

    public static bool EnableInfo()
    {
        return EnableLog(LogLevel.Info);
    }

    public static bool EnableWarn()
    {
        return EnableLog(LogLevel.Warn);
    }

    private static bool EnableLog(LogLevel logLevel)
    {
        return LogConfig.LogLevel >= logLevel;
    }

    public void Debug(string content)
    {
        if (EnableDebug())
        {
            log(LogContent("debug", content));
        }
    }

    public void Info(string content)
    {
        if (EnableInfo())
        {
            log(LogContent("info", content));
        }
    }

    public void Warn(string content)
    {
        if (EnableWarn())
        {
            log(LogContent("warn", content));
        }
    }

    public void Error(string content)
    {
        log(LogContent("error", content));
    }

    protected virtual string LogContent(string mode, string content)
    {
        return $"[{DateTime.Now}]-[{mode}]-{content}";
    }
}