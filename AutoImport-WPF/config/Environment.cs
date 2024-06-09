using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace AutoImport_WPF.config;

public class Environment
{
    public static void Init()
    {
        try
        {
            InitWebDriver();
            TestTargetWebSite();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private static void InitWebDriver()
    {
        new DriverManager().SetUpDriver(new ChromeConfig());
    }

    private static void TestTargetWebSite()
    {
        
    }
}