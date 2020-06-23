using ExamenFinal.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ExamenFinal.ViewModels
{
    public class MainPageViewModel
    {
        Command _PatientsCommand;
        public Command PatientsCommand => _PatientsCommand ?? (_PatientsCommand = new Command(PatientsAction));

        Command _DatesCommand;
        public Command DatesCommand => _DatesCommand ?? (_DatesCommand = new Command(DatesAction));

        Command _TeamCommand;
        public Command TeamCommand => _TeamCommand ?? (_TeamCommand = new Command(TeamAction));
        private void DatesAction()
        {
            Application.Current.MainPage.Navigation.PushAsync(new DatesPage());
        }

        private void PatientsAction()
        {
            Application.Current.MainPage.Navigation.PushAsync(new PatientItem());
        }
        private void TeamAction()
        {
            Application.Current.MainPage.Navigation.PushAsync(new TeamInfo());
        }
    }
}
