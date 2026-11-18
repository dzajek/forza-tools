using Microsoft.Extensions.Logging;
using System.ComponentModel;

namespace ForzaTunerResults.ViewModel
{
    public abstract class AbstractViewModel : INotifyPropertyChanged
    {
        private static readonly ILogger<AbstractViewModel> logger = App.CreateLogger<AbstractViewModel>();

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            logger.LogTrace("PropertyChanged event raised for property: {propertyName}", propertyName);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
