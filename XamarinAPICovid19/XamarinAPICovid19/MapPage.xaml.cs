using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace XamarinAPICovid19
{
    
    public partial class MapPage : ContentPage
    {
        Position position;
        Xamarin.Forms.Maps.Map map;
        Entry country;
        Button findLocationBTN;

        // Data used for the map (Variables to get and store data form the API class)
        CovidAPI CAPI;
        int deaths = 0;
        int confirmed = 0;
        int recovered = 0;

        StackLayout mainStackLayout;
        public MapPage()
        {
            InitializeComponent();
            CAPI = new CovidAPI();
            map = new Xamarin.Forms.Maps.Map();
            map.HasZoomEnabled = false;
            map.HasScrollEnabled = false;
            map.Pins.Clear();

            mainStackLayout = new StackLayout();

            country = new Entry
            {
                Placeholder = "Enter in the Country",
                PlaceholderColor = Color.Olive
            };
            findLocationBTN = new Button
            {
                Text = "Check Covid-19 in this Country"
            };
            findLocationBTN.Clicked += getLoction;

            mainStackLayout.Children.Add(map);
            mainStackLayout.Children.Add(country);
            mainStackLayout.Children.Add(findLocationBTN);

            Content = mainStackLayout;
            
           // Thread.Sleep(2000);
           // _ = FindTheLocation("", "", "New Zealand");

        }

        public async void getLoction(object sender, EventArgs e)
        {
            await FindTheLocation("", "", country.Text);
        }

        public async Task FindTheLocation(string street, string city, string country)
        {
            try
            {
                //Website: Mallibone
                //Title: Using addresses, maps and geocoordinates in your Xamarin Forms apps 
                //URL: https://mallibone.com/post/xamarin-maps-addresses-geocoords

                var locations = await Geocoding.GetLocationsAsync($"{street},{city},{country}");

                var location = locations?.FirstOrDefault();

                if (location == null)
                {
                    await DisplayAlert("Invaled Country", "Can't find Country: try using capital letters like New Zealand or duoble check spelling", "OK");
                    return;
                }

                position = new Position(location.Latitude, location.Longitude);
                var placeholderDeaths = Deaths(country);
                var placeholderRecovered = Recovered(country);
                var placeholderConfirmed = Confirmed(country);

                await Task.WhenAll(placeholderDeaths, placeholderRecovered, placeholderConfirmed);

                deaths = placeholderDeaths.Result;
                recovered = placeholderRecovered.Result;
                confirmed = placeholderConfirmed.Result;

                CreatePins(country);
                MoveTo();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Error: " + ex.Message + " " + "Solution factory reset the virtual phone", "OK");
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
        }

        public void CreatePins(string country)
        {
            Pin pin = new Pin()
            {
                Label = "Deaths: " + deaths + " " + "Recovered: " + recovered + " " + "Confirmed: " + confirmed,
                Address = country,
                Type = PinType.Place,
                Position = new Position(position.Latitude, position.Longitude)
            };
            map.Pins.Add(pin);
        }

        public void MoveTo()
        {
            if (map.VisibleRegion != null)
            {
                Console.WriteLine("***************************************");
                Console.WriteLine("Hello");
                Console.WriteLine("***************************************");
                map.MoveToRegion(new MapSpan(position, position.Latitude, position.Longitude));
            }
        }

        public async Task<int> Deaths(string country)
        {
            var a = await CAPI.ReturnDeaths(country);
            return a;
        }

        public async Task<int> Recovered(string country)
        {
            var a = await CAPI.ReturnRecovered(country);
            return a;
        }

        public async Task<int> Confirmed(string country)
        {
            var a = await CAPI.ReturnConfirmed(country);
            return a;
        }
    }
}
