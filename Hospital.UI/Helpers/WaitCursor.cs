using System.Windows.Input;

namespace Hospital.UI.Helpers;

internal class WaitCursor : IDisposable
{

    public WaitCursor()
    {
        Mouse.OverrideCursor = Cursors.Wait;
    }

    public void Dispose()
    {
        Mouse.OverrideCursor = null;
    }

}