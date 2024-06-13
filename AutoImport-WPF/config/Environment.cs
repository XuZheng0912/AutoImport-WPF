using System.Net.Http;
using AutoImport_WPF.log;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace AutoImport_WPF.config;

public static class Environment
{
    private static ILogger Logger => LogConfig.Logger;

    public static bool Init()
    {
        return InitWebDriver() && TestNetwork();
    }

    private static bool TestNetwork()
    {
        Logger.Debug("正在初始化网络环境配置");
        using var client = new HttpClient();
        try
        {
            var response = client.GetAsync(WebConfig.TargetUrl);
            var resultIsSuccessStatusCode = response.Result.IsSuccessStatusCode;
            Logger.Debug($"访问目标网站状态响应码：{resultIsSuccessStatusCode.ToString()}");
            Logger.Info("网络初始化成功");
            return resultIsSuccessStatusCode;
        }
        catch (Exception e)
        {
            Logger.Info("网络初始化失败，请检查网络设置或网站是否正常");
            Logger.Error(e.ToString());
            return false;
        }
    }


    private static bool InitWebDriver()
    {
        Logger.Debug("正在初始化Chrome Driver");
        try
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            Logger.Debug("Chrome Driver初始化成功");
            return true;
        }
        catch (Exception e)
        {
            Logger.Info("Chrome Driver初始化失败");
            Logger.Error(e.ToString());
            return false;
        }
    }
}