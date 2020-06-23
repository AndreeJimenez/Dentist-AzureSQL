using ExamenFinal.Models;
using ExamenFinal.Services;
using ExamenFinal.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace ExamenFinal.ViewModels
{
    public class DateDetailViewModel : BaseViewModel 
    {
        Command _saveCommand;
        public Command SaveCommand => _saveCommand ?? (_saveCommand = new Command(SaveAction));

        Command _AddDateCommand;
        public Command AddDateCommand => _AddDateCommand ?? (_AddDateCommand = new Command(CreateDateAction));

        Command _deleteCommand;
        public Command DeleteCommand => _deleteCommand ?? (_deleteCommand = new Command(DeleteAction));

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

        DateConsult _DateToAdd;
        public DateConsult DateToAdd
        {
            get => _DateToAdd;
            set => SetProperty(ref _DateToAdd, value);
        }

        public DateDetailViewModel() {
            DayDate = DateTime.Today;
        }

        public DateDetailViewModel(DateConsult date)
        {
            if (date != null)
            {
                id = date.IdDate;
                DayDate = date.DayDate;
                Cost = date.Cost;
            }
            DateToAdd = date;
        }

        private async void SaveAction()
        {
            IsBusy = true;
            if (id == 0)
            {
                ApiResponse response = await new ApiService().PostDataAsync("dates", new DateConsult
                {
                    DayDate = this.DayDate,
                    Cost = this.Cost
                });
                if (response == null)
                {
                    await Application.Current.MainPage.DisplayAlert("ExamenFinal", "Error creating date", "Ok");
                    return;
                }
                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("ExamenFinal", response.Message, "Ok");
                    return;
                }
                PatientsViewModel.GetInstance().ExecuteLoadPatientsCommand();
                await Application.Current.MainPage.DisplayAlert("ExamenFinal", response.Message, "Ok");
            }
            else
            {
                ApiResponse response = await new ApiService().PutDataAsync("dates", this.id, new DateConsult
                {
                    DayDate = this.DayDate,
                    Cost = this.Cost

                });
                if (response == null)
                {
                    await Application.Current.MainPage.DisplayAlert("ExamenFinal", "Error updating date", "Ok");
                    return;
                }
                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("ExamenFinal", response.Message, "Ok");
                    return;
                }
                await Application.Current.MainPage.DisplayAlert("ExamenFinal", response.Message, "Ok");
            }
            IsBusy = false;
            DateViewModel.GetInstance().ExecuteLoadDatesCommand();
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private async void DeleteAction()
        {
            IsBusy = true;

            ApiResponse response = await new ApiService().DeleteDataAsync("dates", id);
            if (response == null)
            {
                await Application.Current.MainPage.DisplayAlert("ExamenFinal", "Error removing date", "Ok");
                return;
            }
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Examen Final", response.Message, "OK");
                return;
            }
            PatientsViewModel.GetInstance().ExecuteLoadPatientsCommand();
            await Application.Current.MainPage.DisplayAlert("ExamenFinal", response.Message, "Ok");

            IsBusy = false;
            DateViewModel.GetInstance().ExecuteLoadDatesCommand();
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private async void CreateDateAction()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new PatientItem(DateToAdd));
        }
    }  
}
