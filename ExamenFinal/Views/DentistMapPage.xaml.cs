using ExamenFinal.Models;
using Xamarin.Forms.Maps;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ExamenFinal.Services;
using System;

namespace ExamenFinal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DentistMapPage : ContentPage
    {
        public DentistMapPage(Patient patient)
        {
            InitializeComponent();

            try
            {
                MapDentist.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    new Position(patient.Latitude, patient.Longitude),
                    Distance.FromMiles(.5)
                ));

                string imagePath = new ImageService().SaveImageFromBase64(patient.PictureBase64);
                patient.PictureBase64 = imagePath;
                MapDentist.patient = patient;

                Name.Text = patient.Name;
            }catch (Exception e)
            {
                throw e;
            }
            
        }
    }
}