using ExamenFinal.ViewModels;
using ExamenFinal.Models;
using ExamenFinal.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Essentials;
using Plugin.Media;
using ExamenFinal.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ExamenFinal.ViewModels
{
    class PatientsDetailViewModel:BaseViewModel
    {
        Command _saveCommand;
        public Command SaveCommand => _saveCommand ?? (_saveCommand = new Command(SaveAction));

        Command _deleteCommand;
        public Command DeleteCommand => _deleteCommand ?? (_deleteCommand = new Command(DeleteAction));
        
        Command _mapCommand;
        public Command MapCommand => _mapCommand ?? (_mapCommand = new Command(MapAction));

        Command _GetLocationCommand;
        public Command GetLocationCommand => _GetLocationCommand ?? (_GetLocationCommand = new Command(GetLocationAction));

        Command _TakePictureCommand;
        public Command TakePictureCommand => _TakePictureCommand ?? (_TakePictureCommand = new Command(TakePictureAction));

        Command _SelectPictureCommand;
        public Command SelectPictureCommand => _SelectPictureCommand ?? (_SelectPictureCommand = new Command(SelectPictureAction));

        Command _CreateDateCommand;
        public Command CreateDateCommand => _CreateDateCommand ?? (_CreateDateCommand = new Command(CreateDateAction));

        Command _GetDatesCommand;
        public Command GetDatesCommand => _GetDatesCommand ?? (_GetDatesCommand = new Command(GetDatesAction));

        
        int _ID;
        public int id
        {
            get => _ID;
            set => SetProperty(ref _ID, value);
        }

        string _Name;
        public string Name
        {
            get => _Name;
            set => SetProperty(ref _Name, value);
        }

        string _Picture;
        public string PictureBase64
        {
            get => _Picture;
            set => SetProperty(ref _Picture, value);
        }

        string _Process;
        public string Process
        {
            get => _Process;
            set => SetProperty(ref _Process, value);
        }

        public double _Longitude;
        public double Longitude
        {
            get => _Longitude;
            set => SetProperty(ref _Longitude, value);
        }

        double _Latitude;
        public double Latitude
        {
            get => _Latitude;
            set => SetProperty(ref _Latitude, value);
        }

        public Patient _Patient;
        public Patient Patient

        {
            get => _Patient;
            set => SetProperty(ref _Patient, value);
        }

        ImageSource _PictureSource;
        public ImageSource PictureSource
        {
            get => _PictureSource;
            set => SetProperty(ref _PictureSource, value);
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


        public PatientsDetailViewModel() {}

        public PatientsDetailViewModel(Patient patient)
        {
            if (patient != null)
            {
                id = patient.IdPatient;
                Name = patient.Name;
                Process = patient.Process;
                PictureBase64 = patient.PictureBase64;
                Latitude = patient.Latitude;
                Longitude = patient.Longitude;
            }
        }


        private async void SaveAction()
        {
            IsBusy = true;
            if (id == 0)
            {
                ApiResponse response = await new ApiService().PostDataAsync("patient", new Patient
                {
                    Name = this.Name,
                    Process = this.Process,
                    PictureBase64 = this.PictureBase64,
                   
                        Latitude = this.Latitude,
                        Longitude = this.Longitude
                    
                });
                if (response == null)
                {
                    await Application.Current.MainPage.DisplayAlert("ExamenFinal", "Error creating driver", "Ok");
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
                ApiResponse response = await new ApiService().PutDataAsync("patient", this.id, new Patient
                {
                    Name = this.Name,
                    Process = this.Process,
                    PictureBase64 = this.PictureBase64,
                    
                        Latitude = this.Latitude,
                        Longitude = this.Longitude
                    
                });
                if (response == null)
                {
                    await Application.Current.MainPage.DisplayAlert("ExamenFinal", "Error updating driver", "Ok");
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

            ApiResponse response = await new ApiService().DeleteDataAsync("patient", id);
            if (response == null)
            {
                await Application.Current.MainPage.DisplayAlert("ExamenFinal", "Error removing driver", "Ok");
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

        private void MapAction()
        {
            Application.Current.MainPage.Navigation.PushAsync(new DentistMapPage(new Patient
            {
                Name = Name,
                PictureBase64 = PictureBase64,
                Latitude = Latitude,
                Longitude = Longitude
            }));
        }

        private async void GetLocationAction()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    Latitude = location.Latitude;
                    Longitude = location.Longitude;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async void TakePictureAction()
        {
            if (Device.RuntimePlatform == Device.UWP)
            {
                await CrossMedia.Current.Initialize();
            }

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg"
            });

            if (file == null)
                return;

            this.PictureBase64 = await new ImageService().ConvertImageFileToBase64(file.Path);
            await Application.Current.MainPage.DisplayAlert("File Location", file.Path, "OK");

            PictureSource = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream(); //del archivo que ya obtuvo el plugin 
                return stream;
            });
        }

        private async void SelectPictureAction()
        {
            if (Device.RuntimePlatform == Device.UWP)
            {
                await CrossMedia.Current.Initialize();
            }

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Not supported", "OK");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
            });

            if (file == null)
                return;

            this.PictureBase64 = await new ImageService().ConvertImageFileToBase64(file.Path);

            PictureSource = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream(); //del archivo que ya obtuvo el plugin 
                return stream;
            });
        }

        private async void CreateDateAction()
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

        private void GetDatesAction()
        {
            Application.Current.MainPage.Navigation.PushAsync(new DatesPage());
        }
    }
}
