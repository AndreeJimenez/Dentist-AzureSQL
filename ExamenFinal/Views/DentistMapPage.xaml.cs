using ExamenFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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

            string imagePath = new ImageService().SaveImageFromBase64(patientSelected.ImageBase64, patientSelected.ID);
            patientSelected.ImageBase64 = imagePath;
            MapDentist.Pet = patientSelected;

            MapDentist.Pins.Add(
                new Pin
                {
                    Type = PinType.Place,
                    Label = patientSelected.Name,
                    Position = new Position(patientSelected.Latitude, patientSelected.Longitude)
                }
            );

            Name.Text = patientSelected.Name;
            PictureBase64.Text = patientSelected.PictureBase64;
        }
    }
}