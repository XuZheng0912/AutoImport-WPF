using AutoImport_WPF.browser;
using AutoImport_WPF.service.impl;

namespace AutoImport_WPF.config;

public static class BrowserConfig
{
    public static string BrowserRegisterKey = @"SOFTWARE\Clients\StartMenuInternet";

    public static IBrowser Browser { get; } = new ChromeBrowser();
}