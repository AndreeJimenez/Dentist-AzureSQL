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
    public partial class PatientDetailPage : ContentPage
    {
        public PatientDetailPage()
        {
            InitializeComponent();
            BindingContext = new PatientsDetailViewModel();
        }

        public PatientDetailPage(Patient patient)
        {
            InitializeComponent();
            BindingContext = new PatientsDetailViewModel(patient);
        }
    }
}