using System;
using CoreLocation;
using Xamarin.Forms.DetectUserLocationCange.iOS.Services;
using Xamarin.Forms.DetectUserLocationCange.Services;

[assembly: Xamarin.Forms.Dependency(typeof(LocationUpdateService))]
namespace Xamarin.Forms.DetectUserLocationCange.iOS.Services
{
    public class LocationEventArgs : EventArgs, ILocationEventArgs
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class LocationUpdateService : ILocationUpdateService
    {
        CLLocationManager locationManager;

        public event EventHandler<ILocationEventArgs> LocationChanged;

        event EventHandler<ILocationEventArgs> ILocationUpdateService.LocationChanged
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

        public void GetUserLocation()
        {
            locationManager = new CLLocationManager
            {
                DesiredAccuracy = CLLocation.AccuracyBest,
                DistanceFilter = CLLocationDistance.FilterNone
            };

            locationManager.LocationsUpdated +=
                (object sender, CLLocationsUpdatedEventArgs e) =>
                {
                    var locations = e.Locations;
                    var strLocation =locations[locations.Length - 1].Coordinate.Latitude.ToString();

                    strLocation = strLocation + "," + locations[locations.Length - 1].Coordinate.Longitude.ToString();

                    LocationEventArgs args = new LocationEventArgs();
                    args.Latitude = locations[locations.Length - 1].Coordinate.Latitude;
                    args.Longitude = locations[locations.Length - 1].Coordinate.Longitude;

                    LocationChanged(this, args);
                };

            locationManager.AuthorizationChanged += (object sender, CLAuthorizationChangedEventArgs e) =>
            {
                if (e.Status ==
                    CLAuthorizationStatus.AuthorizedWhenInUse)
                {
                    locationManager.StartUpdatingLocation();
                }
            };

            locationManager.RequestWhenInUseAuthorization();
        }

        ~LocationUpdateService()
        {
            locationManager.StopUpdatingLocation();
        }
    }
}