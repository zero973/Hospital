using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hospital.Core.Commands.Contacts;
using Hospital.Core.Models.DTO;
using Hospital.Core.Models.Entities;
using Hospital.Core.Queries.Contacts;
using Hospital.Core.Queries.Diseases;
using Hospital.Core.Queries.Patients;
using Hospital.UI.Extensions;
using Hospital.UI.Helpers;
using Hospital.UI.Services;
using MediatR;
using System.Collections.ObjectModel;
using System.Windows;
using Hospital.Core.Interfaces;

namespace Hospital.UI.PageModels;

public partial class ContactsPageModel(ISender sender, INavigationService navigation, 
    IUserStore store) : ObservableObject
{

    [ObservableProperty]
    private ObservableCollection<ContactDto> _contacts = [];

    [ObservableProperty]
    private ObservableCollection<Patient> _patients = [];

    [ObservableProperty]
    private ObservableCollection<Disease> _diseases = [];

    [ObservableProperty]
    private ContactDto? _selectedContact;

    [ObservableProperty]
    private Patient? _selectedPatient;

    [ObservableProperty]
    private Disease? _selectedDisease;

    [ObservableProperty]
    private DateTime _selectedDate = DateTime.Today;

    [ObservableProperty]
    private Visibility _isShowButtons = Visibility.Hidden;

    [RelayCommand]
    private async Task Appearing()
    {
        using var cursor = new WaitCursor();
        await LoadData();
    }

    [RelayCommand]
    private void ContactSelected(ContactDto? contact)
    {
        if (contact is null)
            return;

        using var cursor = new WaitCursor();

        SelectedPatient = contact.Patient;
        SelectedDisease = contact.Disease;
        SelectedDate = contact.Date.ToDateTime(TimeOnly.MinValue);
    }

    [RelayCommand]
    private void EditResources(Guid contactId)
    {
        navigation.Navigate<Pages.ResourcesSpentPage>(contactId);
    }

    [RelayCommand]
    private async Task AddContact()
    {
        if (SelectedPatient == null || SelectedDisease == null)
            return;

        using var cursor = new WaitCursor();
        var result = await sender.Send(new AddContactRequest(
            SelectedPatient.Id, SelectedDisease.Id, SelectedDate.AsDateOnly(), new List<ResourceSpent>()));

        if (!result.IsSuccess)
        {
            MessageBoxHelper.ErrorMessage(result.Errors);
            return;
        }

        await LoadData();
    }

    [RelayCommand]
    private async Task EditContact()
    {
        if (SelectedPatient == null || SelectedDisease == null || SelectedContact == null)
            return;

        using var cursor = new WaitCursor();
        var result = await sender.Send(
            new EditContactRequest(SelectedContact.Id, SelectedPatient.Id, 
                SelectedDisease.Id, SelectedDate.AsDateOnly()));

        if (!result.IsSuccess)
        {
            MessageBoxHelper.ErrorMessage(result.Errors);
            return;
        }

        await LoadData();
    }

    [RelayCommand]
    private async Task DeleteContact()
    {
        if (SelectedContact == null)
            return;

        using var cursor = new WaitCursor();
        var result = await sender.Send(new DeleteContactRequest(SelectedContact.Id));

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

    [RelayCommand]
    private void GoToPatients() 
        => navigation.Navigate<Pages.PatientsPage>();

    [RelayCommand]
    private void GoToDiseases()
        => navigation.Navigate<Pages.DiseasesPage>();

    [RelayCommand]
    private void GoToUsers()
        => navigation.Navigate<Pages.UsersPage>();

    private async Task LoadData()
    {
        var patientsResult = await sender.Send(new GetPatientsRequest());
        Patients = new ObservableCollection<Patient>(patientsResult.Value.OrderBy(x => x.FIO));
        if (Patients.Any())
            SelectedPatient = Patients.First();

        var diseasesResult = await sender.Send(new GetDiseasesRequest());
        Diseases = new ObservableCollection<Disease>(diseasesResult.Value.OrderBy(x => x.Name));
        if (Diseases.Any())
            SelectedDisease = Diseases.First();

        var contactsResult = await sender.Send(new GetContactsRequest());
        Contacts = new ObservableCollection<ContactDto>(contactsResult.Value.OrderByDescending(x => x.Date));
        if (Contacts.Any())
            SelectedDisease = Diseases.First();

        IsShowButtons = store.IsAdmin ? Visibility.Visible : Visibility.Hidden;
    }

}