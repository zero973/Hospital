using Hospital.UI.PageModels;
using System.Windows.Controls;

namespace Hospital.UI.Pages;

public partial class ResourcesSpentPage : Page
{
    public ResourcesSpentPage(ResourcesSpentPageModel pageModel)
    {
        InitializeComponent();
        DataContext = pageModel;
    }
}