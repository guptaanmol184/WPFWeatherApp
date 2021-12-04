using System;
using System.Windows.Input;

namespace WPFWeatherApp.ViewModel.Commands
{
    public class SearchCommand : ICommand
    {
        public WeatherVM WeatherVM { get; set; }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value;  }
            remove { CommandManager.RequerySuggested -= value; }

        }

        public bool CanExecute(object parameter)
        {
            string query = parameter as string;

            return !string.IsNullOrWhiteSpace(query);
        }

        public SearchCommand(WeatherVM weatherVM)
        {
            WeatherVM = weatherVM;
        }

        public void Execute(object parameter)
        {
            WeatherVM.MakeQuery();
        }
    }
}
