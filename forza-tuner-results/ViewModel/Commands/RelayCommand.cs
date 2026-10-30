using Microsoft.UI.Xaml;
using System;
using System.Windows.Input;

namespace ForzaTunerResults.ViewModel.Commands
{
    public sealed partial class DialogBasedCommand : ICommand
    {
        private readonly Action<UIElement> _execute;
        public event EventHandler? CanExecuteChanged;

        public DialogBasedCommand(Action<UIElement> execute)
        {
            ArgumentNullException.ThrowIfNull(execute, nameof(execute));
            _execute = execute;
        }

        public bool CanExecute(object? parameter) => parameter is not null && parameter is UIElement;

        public void Execute(object? parameter)
        {
            ArgumentNullException.ThrowIfNull(parameter, nameof(parameter));
            if (parameter is not UIElement)
            {
                throw new ArgumentException("Parameter must be of type UIElement.", nameof(parameter));
            }
            _execute((UIElement)parameter);
        }

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
