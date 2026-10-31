using ForzaTunerResults.DataModel;
using ForzaTunerResults.Service;
using ForzaTunerResults.ViewModel.Commands;
using ForzaTunerResults.Views.Dialog;
using ForzaTunerResults.Views.ViewUtils;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ForzaTunerResults.ViewModel
{
    public partial class TiresDataViewModel : AbstractViewModel
    {
        private static readonly ILogger<TiresDataViewModel> logger = App.CreateLogger<TiresDataViewModel>();

        private readonly IDialogService _dialogService;
        private readonly TiresService _tiresService;
        private ObservableCollection<Tires> _tires = [];

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

        public ICommand AddTiresCommand { get; }

        public TiresDataViewModel(IDialogService dialogService, TiresService tiresService)
        {
            _dialogService = dialogService;
            _tiresService = tiresService;
            AddTiresCommand = new RelayCommand(async param => await AddTires(), () => true);
        }

        public async Task LoadTires()
        {
            var tires = await _tiresService.LoadTiresAsync();
            Tires = new ObservableCollection<Tires>(tires);

            logger.LogInformation("Loaded tires: {Tires}", Tires);
        }

        private async Task AddTires()
        {
            var dialogContent = new AddTiresDialog();
            var options = new ContentDialogOptions(
                Title: "Add Tires",
                PrimaryButtonText: "Add",
                CloseButtonText: "Cancel",
                Content: dialogContent
            );

            bool dialogApproved = await _dialogService.ShowContentDialog(options);

            if (!dialogApproved)
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
            await _tiresService.AddTireAsync(tires);
            await LoadTires();
            logger.LogInformation("Added tires: {Tires}", tires);
        }
    }
}

