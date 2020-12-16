using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinAPICovid19;

namespace APIwithXamarin3
{
    public partial class SubPage : ContentPage
    {
        CovidAPI CAPI;

        // Setting labels and entry.
        Label data = new Label
        {
            FontSize = 15,
            TextColor = Color.Blue
        };

        Entry country = new Entry
        {
            FontSize = 20,
            HorizontalOptions = LayoutOptions.FillAndExpand,
            VerticalOptions = LayoutOptions.Center,
        };

        Label deaths = new Label
        {
            FontSize = 20,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
        };

        Label recovered = new Label
        {
            FontSize = 20,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
        };

        Label confirmed = new Label
        {
            FontSize = 20,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
        };

        public SubPage()
        {
            InitializeComponent();

            CAPI = new CovidAPI();

            // Color line.
            BoxView BoxLine = new BoxView
            {
                Color = Color.DarkGreen,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            // Welcome message
            Label message = new Label
            {
                Text = "Welcome to Covid-19 Data!  Please put country name below!",
                TextColor = Color.Blue,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 29
            };

            // Color line.
            BoxView BoxLine2 = new BoxView
            {
                Color = Color.DarkGreen,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            // When country entry changed, set blank to all labels.
            country.TextChanged += Country_TextChanged;

            // Data button as btnData
            Button btnData = new Button
            {
                Text = "Response from Data Base",
                FontSize = 25,
                TextColor = Color.DarkCyan
            };
            btnData.Clicked += BtnData_Clicked;

            // Deaths button as btnDeaths
            Button btnDeaths = new Button
            {
                Text = "Deaths",
                FontSize = 25,
                TextColor = Color.Red
            };
            btnDeaths.Clicked += BtnDeaths_Clicked;

            // Recovered button as btnRecovered
            Button btnRecovered = new Button
            {
                Text = "Recovered",
                FontSize = 25,
                TextColor = Color.Blue
            };
            btnRecovered.Clicked += BtnRecovered_Clicked;

            // Confirmed button as btnConfirmed
            Button btnConfirmed = new Button
            {
                Text = "Confirmed",
                FontSize = 25,
                TextColor = Color.Green
            };
            btnConfirmed.Clicked += BtnConfirmed_Clicked;

            // Exit button as btnExit
            Button btnExit = new Button
            {
                Text = "Exit",
                FontSize = 25,
                TextColor = Color.Black
            };
            btnExit.Clicked += BtnExit_Clicked;

            // Layout setting
            Content = new StackLayout
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Children =
                {
                    BoxLine,
                    message,
                    country,
                    btnData,
                    data,
                    new StackLayout
                    {
                        HorizontalOptions = LayoutOptions.Center,
                        Children =
                        {
                            btnDeaths,
                            deaths,
                            btnRecovered,
                            recovered,
                            btnConfirmed,
                            confirmed,
                            btnExit,
                        }
                    },
                    BoxLine2,
                }
            };
            //ScrollView scrollView = new ScrollView();
            //Content = scrollView;
        }

        // When new country name input, all datas are back to blank.
        private void Country_TextChanged(object sender, TextChangedEventArgs e)
        {
            data.Text = "";
            deaths.Text = "";
            recovered.Text = "";
            confirmed.Text = "";
        }

        // Getting data and showing message.
        private async void BtnData_Clicked(object sender, EventArgs e)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://covid-19-coronavirus-statistics.p.rapidapi.com/v1/total?country=" + country.Text),
                Headers =
            {
                { "x-rapidapi-key", "7ab2f6eae9msh7c5a0dc76fd6e4dp120a5fjsn68768a5398bd" },
                { "x-rapidapi-host", "covid-19-coronavirus-statistics.p.rapidapi.com" },
            },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var MyData = JsonConvert.DeserializeObject<Root>(body);
                if (MyData.message == "OK")
                {
                    data.Text = "Data is ready. Please press the buttons below!";
                }
                else
                    data.Text = "Warning: The conytry name is not correct. Please make sure for uppercases and retry.";
                data.TextColor = Color.Red;
            }
        }

        private async void BtnDeaths_Clicked(object sender, EventArgs e)
        {
            int output = await CAPI.ReturnDeaths(country.Text);
            deaths.Text = output.ToString();
            //((Button)sender).IsEnabled = false;
        }

        private async void BtnRecovered_Clicked(object sender, EventArgs e)
        {
            int output = await CAPI.ReturnRecovered(country.Text);
            recovered.Text = output.ToString();
            //((Button)sender).IsEnabled = false;
        }

        private async void BtnConfirmed_Clicked(object sender, EventArgs e)
        {
            int output = await CAPI.ReturnConfirmed(country.Text);
            confirmed.Text = output.ToString();
            //((Button)sender).IsEnabled = false;
        }

        private void BtnExit_Clicked(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
}
