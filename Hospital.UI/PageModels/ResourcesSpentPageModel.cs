using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hospital.Core.Commands.ResourcesSpent;
using Hospital.Core.Models.Entities;
using Hospital.Core.Queries.ResourcesSpent;
using Hospital.UI.Helpers;
using Hospital.UI.Services;
using MediatR;
using System.Collections.ObjectModel;

namespace Hospital.UI.PageModels;

public partial class ResourcesSpentPageModel(ISender sender, INavigationService navigation) : ObservableObject
{

    [ObservableProperty]
    private ObservableCollection<ResourceSpent> _resourcesSpent = [];

    [ObservableProperty]
    private ResourceSpent? _selectedResourceSpent;

    [ObservableProperty]
    private string _resourceName = null!;

    [ObservableProperty]
    private string _comment = null!;

    [ObservableProperty]
    private uint _count;

    private Guid _contactId;

    [RelayCommand]
    private async Task Appearing()
    {
        using var cursor = new WaitCursor();
        _contactId = navigation.GetData<Guid>();
        await LoadData();
    }

    [RelayCommand]
    private void ResourceSpentSelected(ResourceSpent? resourceSpent)
    {
        if (resourceSpent is null)
            return;

        ResourceName = resourceSpent.Resource;
        Comment = resourceSpent.Comment;
        Count = resourceSpent.Count;
    }

    [RelayCommand]
    private async Task Add()
    {
        using var cursor = new WaitCursor();
        var result = await sender.Send(new AddResourceSpentRequest(
            _contactId, ResourceName, Comment, Count));

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

        if (SelectedResourceSpent == null)
            return;

        var result = await sender.Send(new EditResourceSpentRequest(
            _contactId, ResourceName, Comment, Count));

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
        if (SelectedResourceSpent == null)
            return;

        using var cursor = new WaitCursor();
        var result = await sender.Send(new DeleteResourceSpentRequest(
            _contactId, ResourceName));

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
        var result = await sender.Send(new GetResourcesSpentRequest(_contactId));
        ResourcesSpent = new ObservableCollection<ResourceSpent>(result.Value.OrderBy(x => x.Resource));
        if (ResourcesSpent.Any())
            SelectedResourceSpent = ResourcesSpent.First();
    }

}