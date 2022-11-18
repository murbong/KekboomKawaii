using System;
using System.Windows.Input;

namespace KekboomKawaii.ViewModels
{
    public class RelayCommand : ICommand
    {
        // Property
        private readonly Action<object> command;
        private readonly Func<bool> canExecute;

        // Event
        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<object> _commandAction, Func<bool> _canExecute = null)
        {
            command = _commandAction;
            canExecute = _canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute();
        }

        /// <summary>
        /// Implement changed logic if needed
        /// </summary>
        public void Execute(object parameter)
        {
            command?.Invoke(parameter);
        }
    }
}
