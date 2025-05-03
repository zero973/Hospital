using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hospital.Core.Commands.Patients;
using Hospital.Core.Models.Entities;
using Hospital.Core.Queries.Patients;
using Hospital.UI.Extensions;
using Hospital.UI.Helpers;
using Hospital.UI.Services;
using MediatR;
using System.Collections.ObjectModel;

namespace Hospital.UI.PageModels;

public partial class PatientsPageModel(ISender sender, INavigationService navigation) : ObservableObject
{

    [ObservableProperty]
    private ObservableCollection<Patient> _patients = [];

    [ObservableProperty]
    private Patient? _selectedPatient;

    [ObservableProperty]
    private string _fio = null!;

    [ObservableProperty]
    private DateTime _birthday;

    [ObservableProperty]
    private string _snils = null!;

    [RelayCommand]
    private async Task Appearing()
    {
        using var cursor = new WaitCursor();
        await LoadData();
    }

    [RelayCommand]
    private void PatientSelected(Patient? patient)
    {
        if (patient is null)
            return;

        Fio = patient.FIO;
        Birthday = patient.Birthday.AsDateTime();
        Snils = patient.Snils;
    }

    [RelayCommand]
    private async Task Add()
    {
        using var cursor = new WaitCursor();
        var result = await sender.Send(new AddPatientRequest(Fio, Birthday.AsDateOnly(), Snils));

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

        if (SelectedPatient == null)
            return;

        var result = await sender.Send(
            new EditPatientRequest(SelectedPatient.Id, Fio, Birthday.AsDateOnly(), Snils));

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
        if (SelectedPatient == null)
            return;

        using var cursor = new WaitCursor();
        var result = await sender.Send(new DeletePatientRequest(SelectedPatient.Id));

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
        var patientsResult = await sender.Send(new GetPatientsRequest());
        Patients = new ObservableCollection<Patient>(patientsResult.Value.OrderBy(x => x.FIO));
        if (Patients.Any())
            SelectedPatient = Patients.First();
    }

}