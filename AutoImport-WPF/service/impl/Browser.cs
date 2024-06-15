using AutoImport_WPF.browser;
using AutoImport_WPF.log;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace AutoImport_WPF.service.impl;

public class Browser(IWebDriver webDriver) : IBrowser
{
    private static ILogger Logger => LogConfig.Logger;

    private readonly WebDriverWait _wait = new(webDriver, TimeSpan.FromSeconds(0.5));

    public void Close()
    {
        webDriver.Close();
    }

    public void ClickByXpath(string xpath)
    {
        Click(By.XPath(xpath));
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
        Click(By.Id(id));
    }

    private IWebElement WaitFindElement(By by)
    {
        WaitElementLoad(by);
        return webDriver.FindElement(by);
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
        WaitFindElement(by).Click();
    }

    public void ClickByText(string htmlText)
    {
        var webElements = webDriver.FindElements(By.XPath($"//button[text()='{htmlText}']"));
        foreach (var webElement in webElements)
        {
            try
            {
                webElement.Click();
                return;
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }

    public void SendKeys(By by, string keys)
    {
        WaitFindElement(by).SendKeys(keys);
    }

    public void SendKeysByName(string name, string keys)
    {
        SendKeys(By.Name(name), keys);
    }

    public void Clear(By by)
    {
        WaitFindElement(by).Clear();
    }

    public void ClearByName(string name)
    {
        Clear(By.Name(name));
    }

    public void DoubleClick(By by)
    {
        var actions = new Actions(webDriver);
        actions.DoubleClick(WaitFindElement(by)).Perform();
    }

    public void DoubleClickByXpath(string xpath)
    {
        DoubleClick(By.XPath(xpath));
    }

    private void WaitElementLoad(By by)
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
                return false;
            }
        });
    }
}