using Hospital.UI.PageModels;
using System.Windows.Controls;

namespace Hospital.UI.Pages;

public partial class DiseasesPage : Page
{
    public DiseasesPage(DiseasesPageModel pageModel)
    {
        InitializeComponent();
        DataContext = pageModel;
    }
}