using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;

namespace Hospital.UI.Services.Impl;

public class NavigationService(IServiceProvider provider) : INavigationService
{

    private Frame _frame = null!;

    private Stack<object> _paramsStack = new Stack<object>();

    public void SetFrame(Frame frame) 
        => _frame = frame;

    public void Navigate<TPage>() where TPage : Page 
        => _frame.Navigate(provider.GetRequiredService<TPage>());

    public void Navigate<TPage>(object prm) where TPage : Page
    {
        _paramsStack.Push(prm);
        _frame.Navigate(provider.GetRequiredService<TPage>());
    }

    public T GetData<T>()
        => (T)_paramsStack.Pop();

    public void GoBack() 
        => _frame.GoBack();

    public bool CanGoBack()
        => _frame.CanGoBack;

}