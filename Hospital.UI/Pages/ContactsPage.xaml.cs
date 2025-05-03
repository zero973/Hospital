using Hospital.UI.PageModels;
using System.Windows.Controls;

namespace Hospital.UI.Pages;

public partial class ContactsPage : Page
{
    public ContactsPage(ContactsPageModel pageModel)
    {
        InitializeComponent();
        DataContext = pageModel;
    }
}