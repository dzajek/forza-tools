using ForzaTunerResults.DataModel.DTOs;
using ForzaTunerResults.ViewModel.Dialog;
using ForzaTunerResults.ViewModel.Utils;
using ForzaTunerResults.Views.Dialog;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;

namespace ForzaTunerResults.Views.Utils
{
    internal class DialogService : IDialogService
    {
        private static readonly ILogger<DialogService> logger = App.CreateLogger<DialogService>();

        private Func<XamlRoot>? _xamlRootProvider = null;

        public Func<XamlRoot> XamlRootProvider
        {
            get
            {
                ArgumentNullException.ThrowIfNull(_xamlRootProvider, "XamlRootProvider");
                return _xamlRootProvider;
            }
            set
            {
                _xamlRootProvider = value;
            }
        }

        public async Task<AddCarClassDialogViewModel?> ShowAddCarClassDialog()
        {
            var dialogContent = new AddCarClassDialog();

            var options = new ContentDialogOptions<AddCarClassDialog, AddCarClassDialogViewModel>(
                Title: "Add Car Class",
                PrimaryButtonText: "Add",
                CloseButtonText: "Cancel",
                Content: dialogContent
            );

            return await ShowContentDialog(options);
        }

        public async Task<AddCarDialogViewModel?> ShowAddCarDialog()
        {
            var dialogContent = new AddCarDialog();

            var options = new ContentDialogOptions<AddCarDialog, AddCarDialogViewModel>(
                Title: "Add Car",
                PrimaryButtonText: "Add",
                CloseButtonText: "Cancel",
                Content: dialogContent
            );

            return await ShowContentDialog(options);
        }

        public async Task<AddTrackDialogViewModel?> ShowAddTrackDialog()
        {
            var dialogContent = new AddTrackDialog();

            var options = new ContentDialogOptions<AddTrackDialog, AddTrackDialogViewModel>(
                Title: "Add Track",
                PrimaryButtonText: "Add",
                CloseButtonText: "Cancel",
                Content: dialogContent
            );

            return await ShowContentDialog(options);
        }

        public async Task<AddTuneDialogViewModel?> ShowAddTuneDialog(Car car)
        {
            var dialogContent = new AddTuneDialog();
            dialogContent.ViewModel.Car = car;

            var options = new ContentDialogOptions<AddTuneDialog, AddTuneDialogViewModel>(
                Title: "Add Tune",
                PrimaryButtonText: "Add",
                CloseButtonText: "Cancel",
                Content: dialogContent
            );

            return await ShowContentDialog(options);
        }

        public async Task<SingleInputDialogViewModel?> ShowSingleInputDialog(string title, string label)
        {
            var dialogContent = new SingleInputDialog();
            dialogContent.ViewModel.Label = label;

            var options = new ContentDialogOptions<SingleInputDialog, SingleInputDialogViewModel>(
                Title: title,
                PrimaryButtonText: "OK",
                CloseButtonText: "Cancel",
                Content: dialogContent
            );

            return await ShowContentDialog(options);
        }

        private async Task<M?> ShowContentDialog<D, M>(
            ContentDialogOptions<D, M> options
        ) where D : IDialogWithModel<M> where M : class
        {
            var dialog = new ContentDialog()
            {
                XamlRoot = XamlRootProvider(),
                Title = options.Title,
                PrimaryButtonText = options.PrimaryButtonText,
                CloseButtonText = options.CloseButtonText,
                Content = options.Content,
                DefaultButton = ContentDialogButton.Primary
            };
            dialog.Resources["ContentDialogMaxWidth"] = 1080;

            var result = await dialog.ShowAsync();

            if (result != ContentDialogResult.Primary)
            {
                logger.LogDebug("Dialog '{DialogTitle}' was canceled.", options.Title);
                return null;
            }

            return options.Content.ViewModel;
        }
    }
}
