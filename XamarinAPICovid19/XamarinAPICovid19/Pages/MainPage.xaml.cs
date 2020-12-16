using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinAPICovid19
{
    public partial class MainPage : ContentPage
    {
        CovidAPI CAPI;
        public MainPage()
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
                Text = "Welcome to Covid-19 Data!",
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

            // CountryList button as btnCList
            Button btnCList = new Button
            {
                Text = "Country List",
                FontSize = 25,
                TextColor = Color.Purple
            };
            btnCList.Clicked += BtnCList_Clicked;

            // Map button as btnMap
            Button btnMap = new Button
            {
                Text = "Map",
                FontSize = 25,
                TextColor = Color.DarkCyan
            };
            btnMap.Clicked += BtnMap_Clicked;

            // Exit button as btnExit
            Button btnExit = new Button
            {
                Text = "Exit",
                FontSize = 25,
                TextColor = Color.Red
            };
            btnExit.Clicked += BtnExit_Clicked;

            // SubPage button as btnSub
            //Button btnSub = new Button
            //{
            //    Text = "Sub Page",
            //    FontSize = 25,
            //    TextColor = Color.Red
            //};
            //btnSub.Clicked += BtnSub_Clicked;

            // Layout setting
            Content = new StackLayout
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Children =
                {
                    BoxLine,
                    message,
                    BoxLine2,
                    btnCList,
                    btnMap,
                    btnExit,
                    //btnSub,
                }
            };
        }
        private void BtnCList_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CountryListPage());
        }

        private void BtnMap_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MapPage());
        }

        private void BtnExit_Clicked(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        //private void BtnSub_Clicked(object sender, EventArgs e)
        //{
        //    Navigation.PushAsync(new SubPage());
        //}
    }
}
