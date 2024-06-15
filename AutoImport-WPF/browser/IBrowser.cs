using OpenQA.Selenium;

namespace AutoImport_WPF.browser;

public interface IBrowser
{
    void Get(string url);

    void Click(By by);

    void ClickById(string id);

    void SendKeys(By by, string keys);

    void Wait(By by);

    void Clear(By by);

    void Close();
}