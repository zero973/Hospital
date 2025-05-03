using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hospital.Core.Queries.Users;
using Hospital.UI.Helpers;
using Hospital.UI.Pages;
using Hospital.UI.Services;
using MediatR;

namespace Hospital.UI.PageModels;

public partial class AuthorizationPageModel(ISender sender, INavigationService navigation) : ObservableObject
{

    [ObservableProperty]
    private string _login = null!;

    [ObservableProperty]
    private string _password = null!;

    [RelayCommand]
    private async Task Auth()
    {
        var result = await sender.Send(new LoginRequest(Login, Password));

        if (result.IsSuccess)
        {
            navigation.Navigate<ContactsPage>();
            return;
        }

        MessageBoxHelper.ErrorMessage(result.Errors);
    }

}