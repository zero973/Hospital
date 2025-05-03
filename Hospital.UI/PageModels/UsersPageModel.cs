using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hospital.Core.Commands.Users;
using Hospital.Core.Enums;
using Hospital.Core.Models.Entities;
using Hospital.Core.Queries.Users;
using Hospital.UI.Extensions;
using Hospital.UI.Helpers;
using Hospital.UI.Services;
using MediatR;
using System.Collections.ObjectModel;

namespace Hospital.UI.PageModels;

public partial class UsersPageModel(ISender sender, INavigationService navigation) : ObservableObject
{

    [ObservableProperty]
    private ObservableCollection<User> _users = [];

    [ObservableProperty]
    private ObservableCollection<string> _userRoleNames = [];

    [ObservableProperty]
    private User? _selectedUser;

    [ObservableProperty]
    private int _selectedRoleIndex;

    [ObservableProperty]
    private string _login = null!;

    [ObservableProperty]
    private string _password = null!;

    [RelayCommand]
    private async Task Appearing()
    {
        using var cursor = new WaitCursor();
        await LoadData();
    }

    [RelayCommand]
    private void UserSelected(User? user)
    {
        if (user is null)
            return;

        Login = user.Login;
        Password = "";
        SelectedRoleIndex = (int)user.Role;
    }

    [RelayCommand]
    private async Task Add()
    {
        using var cursor = new WaitCursor();
        var result = await sender.Send(
            new AddUserRequest(Login, Password, (UserRoles)SelectedRoleIndex));

        if (!result.IsSuccess)
        {
            MessageBoxHelper.ErrorMessage(result.Errors);
            return;
        }

        await LoadData();
    }

    [RelayCommand]
    private async Task Edit()
    {
        using var cursor = new WaitCursor();

        if (SelectedUser == null)
            return;

        var result = await sender.Send(
            new EditUserRequest(SelectedUser.Id, Login, Password, (UserRoles)SelectedRoleIndex));

        if (!result.IsSuccess)
        {
            MessageBoxHelper.ErrorMessage(result.Errors);
            return;
        }

        await LoadData();
    }

    [RelayCommand]
    private async Task Delete()
    {
        if (SelectedUser == null)
            return;

        using var cursor = new WaitCursor();
        var result = await sender.Send(new DeleteUserRequest(SelectedUser.Id));

        if (!result.IsSuccess)
        {
            MessageBoxHelper.ErrorMessage(result.Errors);
            return;
        }

        await LoadData();
    }

    [RelayCommand]
    private void GoBack()
        => navigation.GoBack();

    private async Task LoadData()
    {
        var usersResult = await sender.Send(new GetUsersRequest());
        Users = new ObservableCollection<User>(usersResult.Value.OrderBy(x => x.Login));
        if (Users.Any())
            SelectedUser = Users.First();

        UserRoleNames = new ObservableCollection<string>(
            Enum.GetValues<UserRoles>().Select(x => x.AsString()));
        SelectedRoleIndex = 1;
        SelectedRoleIndex = 0;
    }

}