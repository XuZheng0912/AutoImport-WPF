using OpenQA.Selenium;

namespace AutoImport_WPF.browser;

public interface IBrowser
{
    void Get(string url);

    void Click(By by);

    void ClickById(string id);

    void ClickByXpath(string xpath);

    void ClickByPossibleXpathList(List<string> possibleXpathList);

    void DoubleClick(By by);

    void DoubleClickByXpath(string xpath);

    void SendKeys(By by, string keys);

    void SendKeysByName(string name, string keys);

    void Wait(By by);

    void Wait(List<By> possibleBy);

    void Clear(By by);

    void ClearByName(string name);

    void Close();
}