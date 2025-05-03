using Hospital.UI.Pages;
using Hospital.UI.Services;
using System.Windows;

namespace Hospital.UI.Windows;

public partial class MainWindow : Window
{
    public MainWindow(INavigationService navigation)
    {
        InitializeComponent();
        navigation.SetFrame(MainFrame);
        navigation.Navigate<AuthorizationPage>();
    }
}