using System;
using System.Windows.Input;

namespace Notes.Wpf.UI.Internal
{

    /// <summary>
    /// Class for handling Commands in MVVM format
    /// </summary>
    public class RelayCommand : Generic.RelayCommand<object>
    {

        public RelayCommand(Action<object> execute) : this(execute, null)
        {
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute) : base(execute, canExecute)
        {            
        }
    }
}

