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

        List<DateConsult> _DateConsultonPatient;
        public List<DateConsult> DateConsultonPatient
        {
            get => _DateConsultonPatient;
            set => SetProperty(ref _DateConsultonPatient, value);
        }

        DateConsult _DateSelected;
        public DateConsult DateSelected
        {
            get => _DateSelected;
            set => SetProperty(ref _DateSelected, value);
        }

        public PatientDateViewModel() { }

        public PatientDateViewModel(DatePatient dp)
        {
            if (dp != null)
            {
                IDDate = dp.IdDate;
                IDPatient = dp.IdPatient;
            }
        }

        public async void GetListOrderProducts()
        {
            try
            {
                ApiResponse response = await new ApiService().GetListDataAsyncByID<DateConsult>("DatePatient", IDDate);
                if (response != null || response.Result != null)
                {
                    DateConsult = (ObservableCollection<DateConsult>)response.Result;
                    if (response.Result != null)
                    {
                        DateConsultonPatient = DateConsult.ToList();
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Citas del Paciente", "No cuenta con ninguna cita", "Ok");
                        await Application.Current.MainPage.Navigation.PopAsync();
                    }

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        private void DateSelectAction()
        {
            Application.Current.MainPage.Navigation.PushAsync(new DateDetailPage(DateSelected));
        }
    }
}
