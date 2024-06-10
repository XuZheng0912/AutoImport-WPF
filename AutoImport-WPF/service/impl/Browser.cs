﻿using OpenQA.Selenium;

namespace AutoImport_WPF.service.impl;

public class Browser(IWebDriver webDriver) : IBrowser
{
    public void Click(string name)
    {
        webDriver.FindElement(By.Name(name))
            .Click();
    }

    public void SendKeys()
    {
        throw new NotImplementedException();
    }
}