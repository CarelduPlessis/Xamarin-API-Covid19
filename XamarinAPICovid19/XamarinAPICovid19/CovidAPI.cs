using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace XamarinAPICovid19
{
    public class CovidAPI
    {
        // Deaths data setting
        public async Task<int> ReturnDeaths(string country)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://covid-19-coronavirus-statistics.p.rapidapi.com/v1/total?country=" + country),
                Headers =
        {
            { "x-rapidapi-key", "b3633bbe54mshc96fc5720819e6dp122b57jsncd3f6b2c086f" },
            { "x-rapidapi-host", "covid-19-coronavirus-statistics.p.rapidapi.com" },
        },
            };
            using (var response = await client.SendAsync(request))
            {
                //Console.WriteLine("**********************************");
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                //richTextBox1.Text = body;
                var MyData = JsonConvert.DeserializeObject<Root>(body);
                return MyData.data.deaths;
            }
        }

        // Recovered data setting
        public async Task<int> ReturnRecovered(string country)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://covid-19-coronavirus-statistics.p.rapidapi.com/v1/total?country=" + country),
                Headers =
        {
            { "x-rapidapi-key", "b3633bbe54mshc96fc5720819e6dp122b57jsncd3f6b2c086f" },
            { "x-rapidapi-host", "covid-19-coronavirus-statistics.p.rapidapi.com" },
        },
            };
            using (var response = await client.SendAsync(request))
            {
                //Console.WriteLine("**********************************");
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                //richTextBox1.Text = body;
                var MyData = JsonConvert.DeserializeObject<Root>(body);
                return MyData.data.recovered;
            }
        }

        // Confirmed data setting
        public async Task<int> ReturnConfirmed(string country)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://covid-19-coronavirus-statistics.p.rapidapi.com/v1/total?country=" + country),
                Headers =
        {
            { "x-rapidapi-key", "b3633bbe54mshc96fc5720819e6dp122b57jsncd3f6b2c086f" },
            { "x-rapidapi-host", "covid-19-coronavirus-statistics.p.rapidapi.com" },
        },
            };
            using (var response = await client.SendAsync(request))
            {
                //Console.WriteLine("**********************************");
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                //richTextBox1.Text = body;
                var MyData = JsonConvert.DeserializeObject<Root>(body);
                return MyData.data.confirmed;
            }
        }
    }
}
