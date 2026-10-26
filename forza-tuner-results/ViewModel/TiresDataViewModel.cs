using ForzaTunerResults.DataModel;
using ForzaTunerResults.Internal;
using ForzaTunerResults.Service;
using ForzaTunerResults.ViewModel.Commands;
using ForzaTunerResults.Views.Dialog;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ForzaTunerResults.ViewModel
{
    public partial class TiresDataViewModel : INotifyPropertyChanged
    {
        private static readonly ILogger<TiresDataViewModel> logger = App.CreateLogger<TiresDataViewModel>();
        private readonly TiresService tiresService;
        private readonly DialogService dialogService;

        private ObservableCollection<Tires> _tires = [];

        public ICommand AddTiresCommand { get; }

        public ObservableCollection<Tires> Tires
        {
            get => _tires;
            set
            {
                if (_tires != value)
                {
                    _tires = value;
                    OnPropertyChanged(nameof(Tires));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public TiresDataViewModel(TiresService tiresService, DialogService dialogService)
        {
            this.tiresService = tiresService;
            this.dialogService = dialogService;
            AddTiresCommand = new RelayCommand(async () => await AddTires(), () => true);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task LoadTires()
        {
            var tires = await tiresService.LoadTiresAsync();
            Tires = new ObservableCollection<Tires>(tires);

            logger.LogInformation("Loaded tires: {Tires}", Tires);
        }

        private async Task AddTires()
        {
            var dialogContent = new AddTiresDialog();
            ContentDialog dialog = dialogService.BuildContentDialog("Add Tires", dialogContent);
            dialog.PrimaryButtonText = "Add";
            dialog.CloseButtonText = "Cancel";
            dialog.DefaultButton = ContentDialogButton.Primary;

            var result = await dialog.ShowAsync();
            if (result != ContentDialogResult.Primary)
            {
                logger.LogInformation("Add Tires dialog was canceled.");
                return;
            }

            logger.LogInformation("Adding new Tires");
            var name = dialogContent.ViewModel.Name;
            if (!string.IsNullOrEmpty(name))
            {
                logger.LogInformation("Adding Tires with Name: {Name}", name);
                var tires = new Tires(0, name);
                await tiresService.AddTireAsync(tires);
                await LoadTires();
                logger.LogInformation("Added tires: {Tires}", tires);
            }
            else
            {
                logger.LogWarning("Cannot add Tires. Name: {Name}", name);
            }
        }
    }
}

