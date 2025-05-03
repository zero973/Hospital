using Hospital.UI.PageModels;
using System.Windows.Controls;

namespace Hospital.UI.Pages;

public partial class UsersPage : Page
{
    public UsersPage(UsersPageModel pageModel)
    {
        InitializeComponent();
        DataContext = pageModel;
    }
}