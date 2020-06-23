using ExamenFinal.Models;
using ExamenFinal.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExamenFinal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DateDetailPage : ContentPage
    {
        public DateDetailPage()
        {
            InitializeComponent();

            BindingContext = new DateDetailViewModel();
        }

        public DateDetailPage(DateConsult dateConsult)
        {
            InitializeComponent();

            BindingContext = new DateDetailViewModel(dateConsult);
        }
    }
}