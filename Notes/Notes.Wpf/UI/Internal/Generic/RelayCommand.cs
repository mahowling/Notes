using System;
using System.Windows.Input;

namespace Notes.Wpf.UI.Internal.Generic
{

    /// <summary>
    /// Generic Class for handling Commands in MVVM format
    /// </summary>
    public class RelayCommand<T> : ICommand
    {

        private readonly Func<T, bool> _canExecute = null;
        private readonly Action<T> _execute = null;

        public RelayCommand(Action<T> execute) : this(execute, null)
        {
        }

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute((T)parameter);
        }

#pragma warning disable 67
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
#pragma warning restore 67
    }
}



