using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinAPICovid19;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinAPICovid19
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CountryListPage : ContentPage
    {
        CovidAPI CAPI;

        public CountryListPage()
        {
            InitializeComponent();

            CAPI = new CovidAPI();

            //List of Countries
            String[] CountryArr = new string[20];
            CountryArr[0] = "Australia";
            CountryArr[1] = "Brazil";
            CountryArr[2] = "Canada";
            CountryArr[3] = "China";
            CountryArr[4] = "France";
            CountryArr[5] = "Germany";
            CountryArr[6] = "Greenland";
            CountryArr[7] = "India";
            CountryArr[8] = "Italy";
            CountryArr[9] = "Japan";
            CountryArr[10] = "Mexico";
            CountryArr[11] = "New Zealand";
            CountryArr[12] = "Norway";
            CountryArr[13] = "Philippines";
            CountryArr[14] = "Russia";
            CountryArr[15] = "South Africa";
            CountryArr[16] = "Spain";
            CountryArr[17] = "Sweden";
            CountryArr[18] = "UK";
            CountryArr[19] = "USA";

            Label ThisLabel;
            Image ThisImage;

            //Making Grid
            Grid grid = new Grid();

            int CountryCount = 0;

            //For (Country) Loop
            for (int r = 0; r < (CountryArr.Length * 3); r++)
            {
                string Country = CountryArr[CountryCount];

                //Row 1
                //Country
                BoxView CountryBox = new BoxView
                {
                    Color = Color.Gray
                };
                Grid.SetRow(CountryBox, r);
                Grid.SetColumnSpan(CountryBox, 6);

                Label CountryLbl = new Label
                {
                    Text = Country,
                    FontAttributes = FontAttributes.Bold,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                };
                Grid.SetRow(CountryLbl, r);
                Grid.SetColumnSpan(CountryLbl, 6);

                //Adding Country to Grid
                grid.Children.Add(CountryBox);
                grid.Children.Add(CountryLbl);
                //Country ENDS
                //Row 1 ENDS

                r++;

                //Row 2
                //Death Label
                BoxView DeathBox = new BoxView
                {
                    Color = Color.LightGray
                };
                Grid.SetRow(DeathBox, r);
                Grid.SetColumn(DeathBox, 0);
                Grid.SetColumnSpan(DeathBox, 2);

                Label DeathBoxLbl = new Label
                {
                    Text = "'Deaths':",
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                };
                Grid.SetRow(DeathBoxLbl, r);
                Grid.SetColumn(DeathBoxLbl, 0);
                Grid.SetColumnSpan(DeathBoxLbl, 2);

                //Adding Death to Grid
                grid.Children.Add(DeathBox);
                grid.Children.Add(DeathBoxLbl);
                //Death Label ENDS

                //Confirmed Label
                BoxView ConfirmBox = new BoxView
                {
                    Color = Color.LightGray
                };
                Grid.SetRow(ConfirmBox, r);
                Grid.SetColumn(ConfirmBox, 2);
                Grid.SetColumnSpan(ConfirmBox, 2);

                Label ConfirmLbl = new Label
                {
                    Text = "'Confirmed' Cases:",
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                };
                Grid.SetRow(ConfirmLbl, r);
                Grid.SetColumn(ConfirmLbl, 2);
                Grid.SetColumnSpan(ConfirmLbl, 2);

                //Adding Confirmed to Grid
                grid.Children.Add(ConfirmBox);
                grid.Children.Add(ConfirmLbl);
                //Confirmed Label ENDS

                //Recovered Label
                BoxView RecoverBox = new BoxView
                {
                    Color = Color.LightGray
                };
                Grid.SetRow(RecoverBox, r);
                Grid.SetColumn(RecoverBox, 4);
                Grid.SetColumnSpan(RecoverBox, 2);

                Label RecoverLbl = new Label
                {
                    Text = "'Recovered':",
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                };
                Grid.SetRow(RecoverLbl, r);
                Grid.SetColumn(RecoverLbl, 4);
                Grid.SetColumnSpan(RecoverLbl, 2);

                //Adding Recovered to Grid
                grid.Children.Add(RecoverBox);
                grid.Children.Add(RecoverLbl);
                //Recovered Label ENDS
                //Row 2 ENDS

                r++;

                //Row 3
                //The Data Columns
                for (int c = 0; c < 6; c++)
                {
                    grid.Children.Add(new BoxView
                    {
                        Margin = 0,
                        HeightRequest = 25
                    }, 1, 1);

                    //Between Each Column Data
                    switch (c)
                    {
                        //Death Num
                        case 0:
                            grid.Children.Add(ThisLabel = new Label()
                            {
                                HeightRequest = 25,
                                HorizontalOptions = LayoutOptions.Center,
                                VerticalOptions = LayoutOptions.Center
                            }, c, r);
                            GetDeathNum(Country, ThisLabel);
                            break;

                        //Death Image
                        case 1:
                            grid.Children.Add(ThisImage = new Image()
                            {
                                HeightRequest = 25,
                                HorizontalOptions = LayoutOptions.Center,
                                VerticalOptions = LayoutOptions.Center
                            }, c, r);
                            GiveArrow(3, ThisImage, false);
                            break;

                        //Confirmed Num
                        case 2:
                            grid.Children.Add(ThisLabel = new Label()
                            {
                                HeightRequest = 25,
                                HorizontalOptions = LayoutOptions.Center,
                                VerticalOptions = LayoutOptions.Center
                            }, c, r);
                            GetConfirmNum(Country, ThisLabel);
                            break;

                        //Confirmed Image
                        case 3:
                            grid.Children.Add(ThisImage = new Image()
                            {
                                HeightRequest = 25,
                                HorizontalOptions = LayoutOptions.Center,
                                VerticalOptions = LayoutOptions.Center
                            }, c, r);
                            GiveArrow(175, ThisImage, false);
                            break;

                        //Recovered Num
                        case 4:
                            grid.Children.Add(ThisLabel = new Label()
                            {
                                HeightRequest = 25,
                                HorizontalOptions = LayoutOptions.Center,
                                VerticalOptions = LayoutOptions.Center
                            }, c, r);
                            GetRecoverNum(Country, ThisLabel);
                            break;

                        //Recovered Image
                        case 5:
                            grid.Children.Add(ThisImage = new Image()
                            {
                                HeightRequest = 25,
                                HorizontalOptions = LayoutOptions.Center,
                                VerticalOptions = LayoutOptions.Center
                            }, c, r);
                            GiveArrow(7, ThisImage, true);
                            break;
                    } //Switch For Columns ENDS
                } //The Data Columns ENDS
                  //Row 3 ENDS

                CountryCount++;


            } //For (Country) Loop ENDS

            //Content = grid;

            //Adds Scroll
            ScrollView scrollview = new ScrollView { Content = grid };
            Content = scrollview;
        } //Loading CountryLists() Page ENDS


        public async void GetDeathNum(string Country, Label DeathLbl)
        {
            var deathNum = await CAPI.ReturnDeaths(Country);
            DeathLbl.Text = Convert.ToString(deathNum);
        }

        public async void GetConfirmNum(string Country, Label ConfirmLbl)
        {
            var confirmNum = await CAPI.ReturnConfirmed(Country);
            ConfirmLbl.Text = Convert.ToString(confirmNum);
        }

        public async void GetRecoverNum(string Country, Label RecoverLbl)
        {
            var recoverNum = await CAPI.ReturnRecovered(Country);
            RecoverLbl.Text = Convert.ToString(recoverNum);
        }

        //Giving Suitable Image Based on Difference
        public void GiveArrow(int Diff, Image ThisImg, bool MoreIsGood)
        {

            string UpColor = "";
            string DownColor = "";

            //Determines if Up or Down is Good
            switch (MoreIsGood)
            {
                case true: UpColor = "G"; DownColor = "R"; break;
                case false: UpColor = "R"; DownColor = "G"; break;
            }

            //Giving Suitable Image Based on Difference
            if (Diff >= 10)
            {
                ThisImg.Source = UpColor + "BigUp.png";
            }
            else if (Diff > 0 && Diff < 10)
            {
                ThisImg.Source = UpColor + "Up.png";
            }
            else if (Diff == 0)
            {
                ThisImg.Source = "Equal.png";
            }
            else if (Diff < 0 && Diff > -10)
            {
                ThisImg.Source = DownColor + "Down.png";
            }
            else
            {
                ThisImg.Source = DownColor + "BigDown.png";
            }
        } //Giving Suitable Image Based on Difference ENDS
    }
}
