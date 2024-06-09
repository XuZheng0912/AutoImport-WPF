using OpenQA.Selenium;

namespace AutoImport_WPF.service;

public interface IBrowser
{
    void Click(string name);

    void SendKeys();
}