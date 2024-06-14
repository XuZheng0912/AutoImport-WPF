using AutoImport_WPF.service;

namespace AutoImport_WPF.browser;

public class BrowserActionExecutor(IBrowser browser)
{
    private readonly List<BrowserAction> _actions = [];

    public void Execute()
    {
        _actions.ForEach(action => action(browser));
    }

    public void AddAction(BrowserAction browserAction)
    {
        _actions.Add(browserAction);
    }
}