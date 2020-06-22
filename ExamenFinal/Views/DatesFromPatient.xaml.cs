using ExamenFinal.Models;
using ExamenFinal.ViewModels;
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
    public partial class DatesFromPatient : ContentPage
    {
        public DatesFromPatient()
        {
            InitializeComponent();

            BindingContext = new PatientDateViewModel();
        }

        public DatesFromPatient(Patient PatienteSelected)
        {
            InitializeComponent();

            BindingContext = new PatientDateViewModel(PatienteSelected);
        }
    }
}