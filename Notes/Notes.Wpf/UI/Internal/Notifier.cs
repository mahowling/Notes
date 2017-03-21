using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace Notes.Wpf.UI.Internal
{
    /// <summary>
    /// Class for handling simple Property Change Notification
    /// </summary>
    public class Notifier : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raise the Property Changed
        /// </summary>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Changes a Properties value and raises the PropertyChanged event
        /// </summary>
        protected virtual bool SetField<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            Debug.WriteLine($"{propertyName} changed.");
            return true;
        }

    }
}
