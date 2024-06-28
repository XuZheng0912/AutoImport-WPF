using System.Windows.Documents;
using OpenQA.Selenium;

namespace AutoImport_WPF.browser;

public interface IBrowser
{
    void Get(string url);

    void DoubleClickFirstByXpath(string xpath);
    
    void ScrollTo(string name);
    
    string GetInputValueByName(string name);

    void SelectByName(string name, string value);

    bool IsOptionSelected(string name, string value);

    bool IsOptionSelected(string name);

    void Click(By by);

    void ClickById(string id);

    void ClickByXpath(string xpath);

    void ClickByText(string htmlText);

    void ClickByPossibleXpathList(List<string> possibleXpathList);

    void DoubleClick(By by);

    void DoubleClickByXpath(string xpath);

    void DoubleClickByPossibleXpath(List<string> xpathList);

    void SendKeys(By by, string keys);

    void SendKeysByName(string name, string keys);

    void Wait(By by);

    void Wait(List<By> possibleBy);

    void Clear(By by);

    void ClearByName(string name);

    void Close();
}