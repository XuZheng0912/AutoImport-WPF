using OpenQA.Selenium;

namespace AutoImport_WPF.service;

public interface IBrowser
{
    void Get(string url);

    void Click(By by);

    void SendKeys(By by, string keys);

    void Wait(By by);

    void Clear(By by);
}