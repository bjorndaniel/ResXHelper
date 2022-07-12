//Code copied from https://github.com/madskristensen/AddAnyFile
internal static class Logger
{
    private static string _name;
    private static IVsOutputWindowPane _pane;
    private static IVsOutputWindow _output;

    public static void Initialize(IServiceProvider provider, string name)
    {
        ThreadHelper.ThrowIfNotOnUIThread();
        _output = (IVsOutputWindow)provider.GetService(typeof(SVsOutputWindow));
        Assumes.Present(_output);
        _name = name;
    }

    public static void Log(object message)
    {
        ThreadHelper.ThrowIfNotOnUIThread();
        try
        {
            if (EnsurePane())
            {
                _ = _pane.OutputStringThreadSafe($"{DateTime.Now}:{message}{Environment.NewLine}");
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Write(ex);
        }
    }

    private static bool EnsurePane()
    {
        ThreadHelper.ThrowIfNotOnUIThread();
        if (_pane == null)
        {
            Guid guid = Guid.NewGuid();
            _output.CreatePane(ref guid, _name, 1, 1);
            _output.GetPane(ref guid, out _pane);
        }
        return _pane != null;
    }
}