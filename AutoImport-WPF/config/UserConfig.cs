using System.Configuration;
using AutoImport_WPF.log;

namespace AutoImport_WPF.config;

public static class UserConfig
{
    private const string UsernameKey = "username";

    private const string PasswordKey = "password";

    private static ILogger Logger => LogConfig.Logger;

    public static void SaveUsername(string username)
    {
        SaveSetting(UsernameKey, username);
    }

    public static void SavePassword(string password)
    {
        SaveSetting(PasswordKey, password);
    }

    public static string? GetUsername()
    {
        return GetSetting(UsernameKey);
    }

    public static string? GetPassword()
    {
        return GetSetting(PasswordKey);
    }

    private static string? GetSetting(string setting)
    {
        Logger.Debug($"获取appSetting:{setting}");
        return ConfigurationManager.AppSettings[setting];
    }

    private static void SaveSetting(string settingName, string settingValue)
    {
        Logger.Debug($"保存appSetting:{settingName}-{settingValue}");
        var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        if (ConfigurationManager.AppSettings[settingName] != null)
        {
            config.AppSettings.Settings.Remove(settingName);
        }

        config.AppSettings.Settings.Add(settingName, settingValue);
        config.Save(ConfigurationSaveMode.Modified);
        ConfigurationManager.RefreshSection("appSettings");
    }
}