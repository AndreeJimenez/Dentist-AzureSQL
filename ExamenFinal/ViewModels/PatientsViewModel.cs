using ExamenFinal.ViewModels;
using ExamenFinal.Models;
using ExamenFinal.Services;
using ExamenFinal.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace ExamenFinal.ViewModels
{
    class PatientsViewModel:BaseViewModel
    {
        static PatientsViewModel _instance;

        Command _addCommand;
        public Command AddCommand => _addCommand ?? (_addCommand = new Command(AddAction));

        Command _selectCommand;
        public Command SelectCommand => _selectCommand ?? (_selectCommand = new Command(SelectAction));

        Patient patientSelected;
        public Patient PatientSelected
        {
            get => patientSelected;
            set => SetProperty(ref patientSelected, value);
        }

        ObservableCollection<Patient> patients;
        public ObservableCollection<Patient> Patients
        {
            get => patients;
            set => SetProperty(ref patients, value);
        }

        public Command LoadPatientsCommand { get; set; }

        public PatientsViewModel()
        {
            _instance = this;
            Title = "Patients";
            Patients = new ObservableCollection<Patient>();
            LoadPatientsCommand = new Command(ExecuteLoadPatientsCommand);

            ExecuteLoadPatientsCommand();
        }

        public static PatientsViewModel GetInstance()
        {
            if (_instance == null) _instance = new PatientsViewModel();
            return _instance;
        }

        private void AddAction()
        {
            Application.Current.MainPage.Navigation.PushAsync(new PatientDetailPage());
        }

        private void SelectAction()
        {
            Application.Current.MainPage.Navigation.PushAsync(new PatientDetailPage(PatientSelected));
        }

        public async void ExecuteLoadPatientsCommand()
        {
            try
            {
                IsBusy = true;
                Patients.Clear();
                ApiResponse response = await new ApiService().GetDataAsync<Patient>("patient"); //DataStore.GetItemsAsync(true);
                if (response != null && response.Result != null)
                {
                    Debug.WriteLine("response.result: " + response.Result.ToString());
                    Patients = (ObservableCollection<Patient>)response.Result;
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
    }
}
