using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WPFWeatherApp.Model;

namespace WPFWeatherApp.ViewModel.Helpers
{
    internal class AccuWeatherHelper
    {
        public const string BASE_URL = "http://dataservice.accuweather.com";
        public const string AUTOCOMPLETE_ENDPOINT = "/locations/v1/cities/autocomplete?apikey={0}&q={1}";
        public const string API_KEY = "";
        public const string CURRENT_CONDITION_ENDPOINT = "/currentconditions/v1/{0}?apikey={1}";

        public static async Task<List<City>> GetCities(string query)
        {
            List<City> cities = new List<City>();

            using(HttpClient client = new HttpClient())
            {
                string url = $"{BASE_URL}{string.Format(AUTOCOMPLETE_ENDPOINT, API_KEY, query)}";

                HttpResponseMessage response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();

                cities = JsonConvert.DeserializeObject<List<City>>(json);
            }

            return cities;
        }

        public static async Task<CurrentConditions> GetCurrentConditions(string cityKey)
        {
            CurrentConditions currentConditions = new CurrentConditions();

            using(HttpClient client = new HttpClient())
            {
                string url = $"{BASE_URL}{string.Format(CURRENT_CONDITION_ENDPOINT, cityKey, API_KEY)}";

                HttpResponseMessage response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();

                currentConditions = JsonConvert.DeserializeObject<List<CurrentConditions>>(json).FirstOrDefault();
            }

            return currentConditions;
        }
    }
}
