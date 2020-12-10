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
        Position position; // store the position of the Country
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
            map.HasZoomEnabled = false; //disable zoom on the map
            map.HasScrollEnabled = false;//disable Scroll on the map
            map.Pins.Clear(); // clear all the pins form the map

            mainStackLayout = new StackLayout(); // the main layout

            country = new Entry
            {
                Placeholder = "Enter in the Country",
                PlaceholderColor = Color.Olive
            };
            findLocationBTN = new Button
            {
                Text = "Check Covid-19 in this Country"
            };
            findLocationBTN.Clicked += getLoction; // get the country location

            // add all the elements to the main layout
            mainStackLayout.Children.Add(map);
            mainStackLayout.Children.Add(country);
            mainStackLayout.Children.Add(findLocationBTN);

            Content = mainStackLayout; // add main layout to the page
            
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

                // get location from the address 
                var locations = await Geocoding.GetLocationsAsync($"{street},{city},{country}");

                var location = locations?.FirstOrDefault(); // get the first result in the list

                if (location == null) // check if location has been found / location is not empty
                {
                    await DisplayAlert("Invaled Country", "Can't find Country: try using capital letters like New Zealand or duoble check spelling", "OK");
                    return;
                }

                position = new Position(location.Latitude, location.Longitude);

                // start the task of geting the data from the Covid19 API
                var placeholderDeaths = Deaths(country);
                var placeholderRecovered = Recovered(country);
                var placeholderConfirmed = Confirmed(country);

                // wait untell all of them have completed their task
                await Task.WhenAll(placeholderDeaths, placeholderRecovered, placeholderConfirmed);

                // get the result of the task and store it in a global variable
                deaths = placeholderDeaths.Result;
                recovered = placeholderRecovered.Result;
                confirmed = placeholderConfirmed.Result;

                //create the pins on the map 
                CreatePins(country);
                MoveTo(); // move the Google API Map to the position of the country
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Error: " + ex.Message + " " + "Solution factory reset the virtual phone", "OK");
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
        }

        //Create Pin on Google API Map
        public void CreatePins(string country)
        {
            Pin pin = new Pin() // fill the pin with information
            {
                Label = "Deaths: " + deaths + " " + "Recovered: " + recovered + " " + "Confirmed: " + confirmed,
                Address = country,
                Type = PinType.Place,
                Position = new Position(position.Latitude, position.Longitude) // tell pin where on the map to be placed
            };
            map.Pins.Add(pin); // add pin to the map
        }

        //Move the Google API Map to a position on the map
        public void MoveTo()
        {
            if (map.VisibleRegion != null)
            {
                map.MoveToRegion(new MapSpan(position, position.Latitude, position.Longitude));
            }
        }

        /*
            Get data form the Covid19 API:
            
            Deaths, Recovered and Confirmed
         
         */
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
