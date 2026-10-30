using ForzaTunerResults.DataModel;
using ForzaTunerResults.Service;
using ForzaTunerResults.ViewModel.Commands;
using ForzaTunerResults.Views.Dialog;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ForzaTunerResults.ViewModel
{
    public partial class TiresDataViewModel : AbstractViewModel
    {
        private static readonly ILogger<TiresDataViewModel> logger = App.CreateLogger<TiresDataViewModel>();
        private readonly TiresService tiresService;
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

        public TiresDataViewModel(TiresService tiresService)
        {
            this.tiresService = tiresService;
            AddTiresCommand = new DialogBasedCommand(async parent => await AddTires(parent));
        }

        public async Task LoadTires()
        {
            var tires = await tiresService.LoadTiresAsync();
            Tires = new ObservableCollection<Tires>(tires);

            logger.LogInformation("Loaded tires: {Tires}", Tires);
        }

        private async Task AddTires(UIElement parent)
        {
            var dialogContent = new AddTiresDialog();
            ContentDialog dialog = new()
            {
                XamlRoot = parent.XamlRoot,
                Title = "Add Tires",
                Content = dialogContent,
                PrimaryButtonText = "Add",
                CloseButtonText = "Cancel",
                DefaultButton = ContentDialogButton.Primary
            };
            dialog.DefaultButton = ContentDialogButton.Primary;

            var result = await dialog.ShowAsync();
            if (result != ContentDialogResult.Primary)
            {
                logger.LogInformation("Add Tires dialog was canceled.");
                return;
            }

            var name = dialogContent.ViewModel.Name;

            if (string.IsNullOrWhiteSpace(name))
            {
                logger.LogWarning("Cannot add Tires. Name is null or whitespace.");
                return;
            }

            logger.LogInformation("Adding Tires with Name: {Name}", name);
            var tires = new Tires(0, name);
            await tiresService.AddTireAsync(tires);
            await LoadTires();
            logger.LogInformation("Added tires: {Tires}", tires);
        }
    }
}

