using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFWeatherApp.Model;

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

        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
