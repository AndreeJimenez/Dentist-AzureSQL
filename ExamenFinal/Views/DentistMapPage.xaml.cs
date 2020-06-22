using ExamenFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms.Maps;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ExamenFinal.Services;

namespace ExamenFinal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DentistMapPage : ContentPage
    {
        public DentistMapPage(Patient patient)
        {
            InitializeComponent();

            MapDentist.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    new Position(patient.Latitude, patient.Longitude),
                    Distance.FromMiles(.5)
            ));

            string imagePath = new ImageService().SaveImageFromBase64(patient.PictureBase64);
            patient.PictureBase64 = imagePath;
            MapDentist.patient = patient;

            Name.Text = patient.Name;
        }
    }
}