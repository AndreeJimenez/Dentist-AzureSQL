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
    public partial class PatientItem : ContentPage
    {
        PatientsViewModel vista;
        public PatientItem()
        {
           
            InitializeComponent();
            BindingContext = vista = new PatientsViewModel(); 
        
        }
        public PatientItem(DateConsult dateToAdd)
        {

            InitializeComponent();
            BindingContext = new PatientsViewModel(dateToAdd);

        }
    }
}