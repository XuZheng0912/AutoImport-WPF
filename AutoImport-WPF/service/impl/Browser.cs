using AutoImport_WPF.browser;
using AutoImport_WPF.log;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutoImport_WPF.service.impl;

public class Browser(IWebDriver webDriver) : IBrowser
{
    private static ILogger Logger => LogConfig.Logger;

    private readonly WebDriverWait _wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(1));

    public void ClickById(string id)
    {
        throw new NotImplementedException();
    }

    public void Clear(By by)
    {
        webDriver.FindElement(by).Clear();
    }

    public void Wait(By by)
    {
        _wait.Until(driver =>
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (Exception)
            {
                Logger.Debug($"查找{by}元素失败，正在重新查找");
                return false;
            }
        });
    }

    public void Get(string url)
    {
        webDriver.Manage().Window.Maximize();
        webDriver.Navigate().GoToUrl(url);
    }

    public void Click(By by)
    {
        webDriver.FindElement(by)
            .Click();
    }

    public void SendKeys(By by, string keys)
    {
        webDriver.FindElement(by).SendKeys(keys);
    }
}