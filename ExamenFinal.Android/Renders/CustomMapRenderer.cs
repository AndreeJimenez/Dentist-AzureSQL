using System;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Graphics;
using Android.Widget;
using ExamenFinal.Droid.Renders;
using ExamenFinal.Models;
using ExamenFinal.Renders;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace ExamenFinal.Droid.Renders
{
    public class CustomMapRenderer : MapRenderer, GoogleMap.IInfoWindowAdapter
    {
        Patient Patient;

        public CustomMapRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                Patient = ((CustomMap)e.NewElement).patient;
            }
        }

        protected override void OnMapReady(GoogleMap map)
        {
            base.OnMapReady(map);

            NativeMap.SetInfoWindowAdapter(this);
        }

        protected override MarkerOptions CreateMarker(Pin pin)
        {
            //return base.CreateMarker(pin);
            var marker = new MarkerOptions();
            marker.SetPosition(new LatLng(Patient.Latitude, Patient.Longitude));
            marker.SetTitle(Patient.Name);
            return marker;
        }

        public Android.Views.View GetInfoContents(Marker marker)
        {
            var inflater = Android.App.Application.Context.GetSystemService(Context.LayoutInflaterService) as Android.Views.LayoutInflater;
            if (inflater != null)
            {
                Android.Views.View view;
                view = inflater.Inflate(Resource.Layout.MarkerWindow, null);
                var infoImage = view.FindViewById<ImageView>(Resource.Id.MarkerWindowImage);
                var infoName = view.FindViewById<TextView>(Resource.Id.MarkerWindowName);

                if (infoImage != null) infoImage.SetImageBitmap(BitmapFactory.DecodeFile(Patient.PictureBase64));
                if (infoName != null) infoName.Text = Patient.Name;

                return view;
            }
            return null;
        }

        public Android.Views.View GetInfoWindow(Marker marker)
        {
            throw new NotImplementedException();
        }
    }
}