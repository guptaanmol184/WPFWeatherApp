using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFWeatherApp.Model;
using WPFWeatherApp.ViewModel.Commands;
using WPFWeatherApp.ViewModel.Helpers;

namespace WPFWeatherApp.ViewModel
{
    public class WeatherVM : INotifyPropertyChanged
    {
        private string query;

        public string Query
        {
            get { return query; }
            set
            {
                query = value;
                OnPropertyChanged(nameof(Query));
            }
        }

        public ObservableCollection<City> Cities { get; set; }

        private CurrentConditions currentConditions;

        public CurrentConditions CurrentConditions
        {
            get { return currentConditions; }
            set
            {
                currentConditions = value;
                OnPropertyChanged(nameof(CurrentConditions));
            }
        }

        private City selectedCity;

        public City SelectedCity
        {
            get { return selectedCity; }
            set { selectedCity = value; }
        }

        public SearchCommand SearchCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public WeatherVM()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                SelectedCity = new City()
                {
                    LocalizedName = "India"
                };
                CurrentConditions = new CurrentConditions()
                {
                    WeatherText = "Partly Cloudy",
                    Temperature = new Temperature()
                    {
                        Metric = new Units()
                        {
                            Value = 21
                        }
                    }
                };
            }

            SearchCommand = new SearchCommand(this);
            Cities = new ObservableCollection<City>();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void MakeQuery()
        {
            var cities = await AccuWeatherHelper.GetCities(Query);

            Cities.Clear();
            cities.ForEach(city => Cities.Add(city));
        }
    }
}
