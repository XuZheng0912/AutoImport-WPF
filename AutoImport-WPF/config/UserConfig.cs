using System.Configuration;

namespace AutoImport_WPF.config;

public static class UserConfig
{
    private const string UsernameKey = "username";

    private const string PasswordKey = "password";


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
        return ConfigurationManager.AppSettings[setting];
    }

    private static void SaveSetting(string settingName, string settingValue)
    {
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