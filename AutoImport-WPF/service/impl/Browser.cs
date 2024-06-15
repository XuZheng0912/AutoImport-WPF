using AutoImport_WPF.browser;
using AutoImport_WPF.log;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutoImport_WPF.service.impl;

public class Browser(IWebDriver webDriver) : IBrowser
{
    private static ILogger Logger => LogConfig.Logger;

    private readonly WebDriverWait _wait = new(webDriver, TimeSpan.FromSeconds(1));

    public void Close()
    {
        webDriver.Quit();
    }

    public void ClickByXpath(string xpath)
    {
        WaitThenClick(By.XPath(xpath));
    }

    public void ClickByPossibleXpathList(List<string> possibleXpathList)
    {
        foreach (var idLimitXpath in possibleXpathList)
        {
            try
            {
                ClickByXpath(idLimitXpath);
                break;
            }
            catch (Exception)
            {
                Logger.Debug("正在查找具有多个可能定位符的元素");
            }
        }
    }

    public void ClickById(string id)
    {
        WaitThenClick(By.Id(id));
    }

    private void WaitThenClick(By by)
    {
        WaitFindElement(by).Click();
    }

    private IWebElement WaitFindElement(By by)
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
                Logger.Debug($"正在查找{by}元素");
                return false;
            }
        });
        return webDriver.FindElement(by);
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

    public void Wait(List<By> possibleBy)
    {
        _wait.Until(driver =>
        {
            try
            {
                foreach (var by in possibleBy)
                {
                    driver.FindElement(by);
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                Logger.Debug($"查找{possibleBy[0]}元素失败，正在重新查找");
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