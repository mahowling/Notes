using System;
using System.Windows.Input;

namespace Notes.Wpf.UI.Internal
{

    /// <summary>
    /// Class to represent a "NOTHING" command.
    /// </summary>
    /// <remarks>This is most useful when used as Fallback Value.</remarks>
    public class NullCommand : ICommand
    {
#pragma warning disable 67
        public event EventHandler CanExecuteChanged;
#pragma warning restore 67

        public bool CanExecute(object parameter)
        {
            return false;
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException("NullCommand cannot be executed");
        }
    }
}
