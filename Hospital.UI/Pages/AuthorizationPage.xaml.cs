using Hospital.UI.PageModels;
using System.Windows.Controls;

namespace Hospital.UI.Pages;

public partial class AuthorizationPage : Page
{
    public AuthorizationPage(AuthorizationPageModel pageModel)
    {
        InitializeComponent();
        DataContext = pageModel;
    }
}