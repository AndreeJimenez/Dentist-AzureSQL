using ExamenFinal.Models;
using ExamenFinal.Services;
using ExamenFinal.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace ExamenFinal.ViewModels
{
    public class PatientDateViewModel : BaseViewModel
    {

        Command _ExecuteLoadDatesPatCommand;
        public Command ExecuteLoadDatesPatCommand => _ExecuteLoadDatesPatCommand ?? (_ExecuteLoadDatesPatCommand = new Command(LoadDatesPatCommand));

        Command _DateSelectCommand;
        public Command DateSelectCommand => _DateSelectCommand ?? (_DateSelectCommand = new Command(DateSelectAction));

        int _IDPatient;
        public int IDPatient
        {
            get => _IDPatient;
            set => SetProperty(ref _IDPatient, value);
        }

        int _IDDate;
        public int IDDate
        {
            get => _IDDate;
            set => SetProperty(ref _IDDate, value);
        }

        ObservableCollection<DateConsult> _DateConsult;
        public ObservableCollection<DateConsult> DateConsult
        {
            get => _DateConsult;
            set => SetProperty(ref _DateConsult, value);
        }

        DateConsult _DateSelected;
        public DateConsult DateSelected
        {
            get => _DateSelected;
            set => SetProperty(ref _DateSelected, value);
        }
        
        public PatientDateViewModel() 
        {
            Title = "Dates from Patient";
            DateConsult = new ObservableCollection<DateConsult>();
            LoadDatesPatCommand();
        }
        public PatientDateViewModel(Patient patient)
        {
            IDPatient = patient.IdPatient;
            LoadDatesPatCommand();
        }

        public async void LoadDatesPatCommand()
        {
            try
            {
                IsBusy = true;
                ApiResponse response = await new ApiService().GetListDataAsyncByID<DateConsult>("DatePatient", IDPatient);
                if (response != null && response.Result != null)
                {
                    Debug.WriteLine("response.result: " + response.Result.ToString());
                    DateConsult = (ObservableCollection<DateConsult>)response.Result;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Citas del Paciente", "No cuenta con ninguna cita", "Ok");
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        private void DateSelectAction()
        {
            Application.Current.MainPage.Navigation.PushAsync(new DateDetailPage(DateSelected));
        }
    }
}
