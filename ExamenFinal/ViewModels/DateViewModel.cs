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
    public class DateViewModel : BaseViewModel
    {
        static DateViewModel _instance;

        Command _AddDateCommand;
        public Command AddDateCommand => _AddDateCommand ?? (_AddDateCommand = new Command(AddDateAction));

        Command _DateSelectCommand;
        public Command DateSelectCommand => _DateSelectCommand ?? (_DateSelectCommand = new Command(DateSelectAction));

        int _ID;
        public int id
        {
            get => _ID;
            set => SetProperty(ref _ID, value);
        }

        public DateTime _DayDate;
        public DateTime DayDate

        {
            get => _DayDate;
            set => SetProperty(ref _DayDate, value);
        }

        public float _Cost;
        public float Cost

        {
            get => _Cost;
            set => SetProperty(ref _Cost, value);
        }

        DateConsult _DateSelected;
        public DateConsult DateSelected
        {
            get => _DateSelected;
            set => SetProperty(ref _DateSelected, value);
        }

        public Command LoadDatesCommand { get; set; }

        ObservableCollection<DateConsult> _DateConsult;
        public ObservableCollection<DateConsult> DateConsult
        {
            get => _DateConsult;
            set => SetProperty(ref _DateConsult, value);
        }

        public DateViewModel()
        {
            _instance = this;
            DateConsult = new ObservableCollection<DateConsult>();
            LoadDatesCommand = new Command(ExecuteLoadDatesCommand);

            ExecuteLoadDatesCommand();
        }

        public async void ExecuteLoadDatesCommand()
        {
            try
            {
                IsBusy = true;
                DateConsult.Clear();
                ApiResponse response = await new ApiService().GetDataAsync<DateConsult>("dates");
                if (response != null && response.Result != null)
                {
                    Debug.WriteLine("response.result: " + response.Result.ToString());
                    DateConsult = (ObservableCollection<DateConsult>)response.Result;
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

        public static DateViewModel GetInstance()
        {
            if (_instance == null) _instance = new DateViewModel();
            return _instance;
        }


        private void DateSelectAction()
        {
            Application.Current.MainPage.Navigation.PushAsync(new DateDetailPage(DateSelected));
        }

        private void AddDateAction()
        {
            Application.Current.MainPage.Navigation.PushAsync(new DateDetailPage());
        }
    }
}
