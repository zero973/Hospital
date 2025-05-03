using System.Windows.Controls;

namespace Hospital.UI.Services;

public interface INavigationService
{

    void SetFrame(Frame frame);

    void Navigate<TPage>() where TPage : Page;

    void Navigate<TPage>(object prm) where TPage : Page;

    T GetData<T>();

    void GoBack();

    bool CanGoBack();

}