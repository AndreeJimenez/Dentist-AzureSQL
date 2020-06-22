using ExamenFinal.Models;
using ExamenFinal.Renders;
using ExamenFinal.UWP.Renders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls.Maps;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.UWP;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRender))]
namespace ExamenFinal.UWP.Renders
{
    class CustomMapRender:MapRenderer
    {
        MapControl nativeMap;
        MarkerWindow markerWindow;
        bool markerWindowShow = false;
        Patient patient;

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                nativeMap.MapElementClick -= OnMapElementClick;
                nativeMap.Children.Clear();
                markerWindow = null;
                nativeMap = null;
            }

            if (e.NewElement != null)
            {
                this.patient = (e.NewElement as CustomMap).patient;
                var formsMap = (CustomMap)e.NewElement;
                nativeMap = Control as MapControl;
                nativeMap.MapElementClick += OnMapElementClick;

                var snPosition = new BasicGeoposition
                {
                    Latitude = patient.Latitude,
                    Longitude = patient.Longitude
                };
                var snPoint = new Geopoint(snPosition);

                var mapIcon = new MapIcon();
                mapIcon.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms:appx:///ping.png"));
                mapIcon.CollisionBehaviorDesired = MapElementCollisionBehavior.RemainVisible;
                mapIcon.Location = snPoint;
                mapIcon.NormalizedAnchorPoint = new Windows.Foundation.Point(0.5, 1.0);

                nativeMap.MapElements.Add(mapIcon);
            }
        }

        private void OnMapElementClick(MapControl sender, MapElementClickEventArgs args)
        {
            var mapIcon = args.MapElements.FirstOrDefault(x => x is MapIcon) as MapIcon;
            if (mapIcon != null)
            {
                if (!markerWindowShow)
                {
                    if (markerWindow == null) markerWindow = new MarkerWindow(patient);

                    var snPosition = new BasicGeoposition
                    {
                        Latitude = patient.Latitude,
                        Longitude = patient.Longitude
                    };
                    var snPoint = new Geopoint(snPosition);

                    nativeMap.Children.Add(markerWindow);
                    MapControl.SetLocation(markerWindow, snPoint);
                    MapControl.SetNormalizedAnchorPoint(markerWindow, new Windows.Foundation.Point(0.5, 1.0));

                    markerWindowShow = true;
                }
                else
                {
                    nativeMap.Children.Remove(markerWindow);
                    markerWindowShow = false;
                }
            }
        }
    }
}
