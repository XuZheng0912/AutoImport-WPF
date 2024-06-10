using OpenQA.Selenium;

namespace AutoImport_WPF.service.impl;

public class Browser(IWebDriver webDriver) : IBrowser
{
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