using ExamenFinal.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ExamenFinal.ViewModels
{
    class MainPageViewModel
    {
        Command _PatientsCommand;
        public Command PatientsCommand => _PatientsCommand ?? (_PatientsCommand = new Command(PatientsAction));

        Command _DatesCommand;
        public Command DatesCommand => _DatesCommand ?? (_DatesCommand = new Command(DatesAction));

        private void DatesAction()
        {
            Application.Current.MainPage.Navigation.PushAsync(new DatesPage());
        }

        private void PatientsAction()
        {
            Application.Current.MainPage.Navigation.PushAsync(new PatientItem());
        }
    }
}
