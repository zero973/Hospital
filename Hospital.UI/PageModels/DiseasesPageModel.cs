using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hospital.Core.Commands.Diseases;
using Hospital.Core.Enums;
using Hospital.Core.Models.Entities;
using Hospital.Core.Queries.Diseases;
using Hospital.UI.Extensions;
using Hospital.UI.Helpers;
using Hospital.UI.Services;
using MediatR;
using System.Collections.ObjectModel;

namespace Hospital.UI.PageModels;

public partial class DiseasesPageModel(ISender sender, INavigationService navigation) : ObservableObject
{

    [ObservableProperty]
    private ObservableCollection<Disease> _diseases = [];

    [ObservableProperty]
    private ObservableCollection<string> _periodTypes = [];

    [ObservableProperty]
    private Disease? _selectedDisease;

    [ObservableProperty]
    private int _selectedPeriodTypeIndex;

    [ObservableProperty]
    private string _name = null!;

    [RelayCommand]
    private async Task Appearing()
    {
        using var cursor = new WaitCursor();
        await LoadData();
    }

    [RelayCommand]
    private void DiseaseSelected(Disease? disease)
    {
        if (disease is null)
            return;

        Name = disease.Name;
        SelectedPeriodTypeIndex = (int)disease.PeriodType;
    }

    [RelayCommand]
    private async Task Add()
    {
        using var cursor = new WaitCursor();
        var result = await sender.Send(
            new AddDiseaseRequest(Name, (PeriodTypes)SelectedPeriodTypeIndex));

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

        if (SelectedDisease == null)
            return;

        var result = await sender.Send(
            new EditDiseaseRequest(SelectedDisease.Id, Name, (PeriodTypes)SelectedPeriodTypeIndex));

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
        if (SelectedDisease == null)
            return;

        using var cursor = new WaitCursor();
        var result = await sender.Send(new DeleteDiseaseRequest(SelectedDisease.Id));

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
        var diseasesResult = await sender.Send(new GetDiseasesRequest());
        Diseases = new ObservableCollection<Disease>(diseasesResult.Value.OrderBy(x => x.Name));
        if (Diseases.Any())
            SelectedDisease = Diseases.First();

        PeriodTypes = new ObservableCollection<string>(
            Enum.GetValues<PeriodTypes>().Select(x => x.AsString()));
        SelectedPeriodTypeIndex = 1;
        SelectedPeriodTypeIndex = 0;
    }

}