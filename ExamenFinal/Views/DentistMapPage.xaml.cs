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
        public DentistMapPage(Patient patientSelected)
        {
            InitializeComponent();

            MapDentist.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    new Position(patientSelected.Latitude, patientSelected.Longitude),
                    Distance.FromMiles(.5)
            ));

            string imagePath = new ImageService().SaveImageFromBase64(patientSelected.PictureBase64);
            patientSelected.PictureBase64 = imagePath;
            MapDentist.Patient = patientSelected;

            MapDentist.Pins.Add(
                new Pin
                {
                    Type = PinType.Place,
                    Label = patientSelected.Name,
                    Position = new Position(patientSelected.Latitude, patientSelected.Longitude)
                }
            );

            Name.Text = patientSelected.Name;
        }
    }
}