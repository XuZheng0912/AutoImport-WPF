using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AutoImport_WPF.service;

public class WebDriverBrowser : IBrowser
{
    private readonly IWebDriver _webDriver = new ChromeDriver();

    public void Click(string name)
    {
        _webDriver.FindElement(By.Name(name)).Click();
    }

    public void SendKeys()
    {
        throw new NotImplementedException();
    }
}