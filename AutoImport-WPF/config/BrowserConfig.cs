using AutoImport_WPF.browser;
using AutoImport_WPF.service.impl;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace AutoImport_WPF.config;

public static class BrowserConfig
{
    public static string BrowserRegisterKey = @"SOFTWARE\Clients\StartMenuInternet";

    private static readonly string FireFoxDriverPath = "C:\\Users\\Administrator\\Desktop\\geckodriver.exe";

    private static IBrowser? _browser;

    public static IBrowser Browser
    {
        get
        {
            if (_browser == null)
            {
                FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(FireFoxDriverPath);
                IWebDriver driver = new FirefoxDriver(service);
                _browser = new Browser(driver);
                return _browser;
            }

            return _browser;
        }
    }
}