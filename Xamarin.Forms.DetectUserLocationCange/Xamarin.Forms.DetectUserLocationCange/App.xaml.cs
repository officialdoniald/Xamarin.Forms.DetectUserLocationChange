using System;
using Xamarin.Forms;
using Xamarin.Forms.DetectUserLocationCange.Services;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Xamarin.Forms.DetectUserLocationCange
{
    public partial class App : Application
    {
        public static ILocationUpdateService LocationUpdateService;

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();

            LocationUpdateService.LocationChanged += LocationUpdateService_LocationChanged; ;
        }

        private void LocationUpdateService_LocationChanged(object sender, ILocationEventArgs e)
        {
            //Here you can get the user's location from "e" -> new Location(e.Latitude, e.Longitude);
            //new Location is from Xamarin.Essentials Location object.
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
