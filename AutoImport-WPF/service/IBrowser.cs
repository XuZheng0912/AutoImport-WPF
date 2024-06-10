using OpenQA.Selenium;

namespace AutoImport_WPF.service;

public interface IBrowser
{
    void Get(string url);

    void Click(string name);

    void SendKeys();
}