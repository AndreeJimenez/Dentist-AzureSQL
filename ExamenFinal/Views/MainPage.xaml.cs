using ExamenFinal.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExamenFinal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BindingContext = new MainPageViewModel();
        }
    }
}