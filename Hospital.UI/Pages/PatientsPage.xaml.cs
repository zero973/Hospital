using Hospital.UI.PageModels;
using System.Windows.Controls;

namespace Hospital.UI.Pages;

public partial class PatientsPage : Page
{
    public PatientsPage(PatientsPageModel pageModel)
    {
        InitializeComponent();
        DataContext = pageModel;
    }
}