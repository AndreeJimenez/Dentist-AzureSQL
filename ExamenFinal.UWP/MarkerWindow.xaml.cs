using ExamenFinal.Models;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace ExamenFinal.UWP
{
    public sealed partial class MarkerWindow : UserControl
    {
        public MarkerWindow(Patient patient)
        {
            this.InitializeComponent();
            try
            {
                MarkerWindowImage.Source = new BitmapImage(new Uri(patient.PictureBase64));
                MarkerWindowTitle.Text = patient.Name;
            } catch
            {
                MarkerWindowImage.Source = null;
                MarkerWindowTitle.Text = "";
            }
        }
    }
}
