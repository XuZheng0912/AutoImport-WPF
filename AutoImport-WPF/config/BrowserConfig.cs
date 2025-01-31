using System.IO;
using AutoImport_WPF.browser;
using AutoImport_WPF.service.impl;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace AutoImport_WPF.config;

public static class BrowserConfig
{
    public static string BrowserRegisterKey = @"SOFTWARE\Clients\StartMenuInternet";
    
    private static IBrowser? _browser;

    public static IBrowser Browser
    {
        get
        {
            var driverPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "geckodriver.exe");
            if (_browser == null)
            {
                FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(driverPath);
                IWebDriver driver = new FirefoxDriver(service);
                _browser = new Browser(driver);
                return _browser;
            }

            return _browser;
        }
    }
}