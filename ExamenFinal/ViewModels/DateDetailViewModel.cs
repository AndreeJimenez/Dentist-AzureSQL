using ExamenFinal.Models;
using ExamenFinal.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ExamenFinal.ViewModels
{
    class DateDetailViewModel : BaseViewModel {

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

        Command _saveCommand;
        public Command SaveCommand => _saveCommand ?? (_saveCommand = new Command(SaveAction));

        Command _deleteCommand;
        public Command DeleteCommand => _deleteCommand ?? (_deleteCommand = new Command(DeleteAction));

        public DateDetailViewModel() {}

        public DateDetailViewModel(DateConsult date)
        {
            if (date != null)
            {
                DayDate = date.DayDate;
                Cost = date.Cost;
            }
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
            PatientsViewModel.GetInstance().ExecuteLoadPatientsCommand();
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
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }  
}
