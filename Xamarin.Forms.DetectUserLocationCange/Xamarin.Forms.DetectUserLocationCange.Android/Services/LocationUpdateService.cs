using System;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Xamarin.Forms;
using Xamarin.Forms.DetectUserLocationCange.Droid.Services;
using Xamarin.Forms.DetectUserLocationCange.Services;

[assembly: Dependency(typeof(LocationUpdateService))]
namespace Xamarin.Forms.DetectUserLocationCange.Droid.Services
{
    public class LocationEventArgs : EventArgs, ILocationEventArgs
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class LocationUpdateService : Java.Lang.Object, ILocationUpdateService, ILocationListener
    {
        LocationManager locationManager;

        public void GetUserLocation()
        {
#pragma warning disable CS0618 // Type or member is obsolete
            locationManager = (LocationManager)Forms.Context.GetSystemService(Context.LocationService);
#pragma warning restore CS0618 // Type or member is obsolete
            locationManager.RequestLocationUpdates(
                provider: LocationManager.NetworkProvider,
                minTime: 0,//millisec
                minDistance: 0,//metres
                listener: this);
        }

        ~LocationUpdateService()
        {
            locationManager.RemoveUpdates(this);
        }

        public void OnLocationChanged(Location location)
        {
            if (location != null)
            {
                LocationEventArgs args = new LocationEventArgs
                {
                    Latitude = location.Latitude,
                    Longitude = location.Longitude
                };
                LocationChanged(this, args);
            };
        }

        public event EventHandler<ILocationEventArgs> LocationChanged;
        
        event EventHandler<ILocationEventArgs>
            ILocationUpdateService.LocationChanged
        {
            add
            {
                LocationChanged += value;
            }
            remove
            {
                LocationChanged -= value;
            }
        }

        public void OnProviderDisabled(string provider) { }

        public void OnProviderEnabled(string provider) { }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras) { }
    }
}