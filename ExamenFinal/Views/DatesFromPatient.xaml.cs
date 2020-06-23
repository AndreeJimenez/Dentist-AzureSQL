using ExamenFinal.Models;
using ExamenFinal.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExamenFinal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DatesFromPatient : ContentPage
    {
        public DatesFromPatient(Patient PatienteSelected)
        {
            InitializeComponent();

            BindingContext = new PatientDateViewModel(PatienteSelected);
        }
    }
}