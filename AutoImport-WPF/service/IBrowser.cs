using OpenQA.Selenium;

namespace AutoImport_WPF.service;

public interface IBrowser
{
    void Get(string url);

    void Click(By by);

    void SendKeys(By by, string keys);
}