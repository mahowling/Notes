using Notes.Models;
using Notes.Wpf.UI.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Input;
using Notes.Wpf.UI.Internal.Generic;
using System.Windows.Data;

namespace Notes.Wpf.UI.ViewModels
{

    /// <summary>
    /// Class handling the Presentation of the Notes
    /// </summary>
    class NotesPresenter : Notifier
    {

        #region Internal Variables

        //private ICollection _notes;
        private bool _isLoading = false;
        private ObservableCollection<NoteView> _notes;
        private Exception _loadError;
        private RelayCommand _refreshCommand;
        private RelayCommand<Window> _closeCommand;
        private RelayCommand _addNoteCommand;

        #endregion

        #region Constructor
        public NotesPresenter()
        {
            Load();
        }

        #endregion

        #region Methods 

        /// <summary>
        /// Add a new note.
        /// </summary>
        /// <param name="parameter"></param>
        private void AddNote(object parameter)
        {
            if (!CanAddNote(parameter)) return;
            {
                var newNote = CreateNoteView(null);
                _notes.Add(newNote);
                Notes.MoveCurrentTo(newNote);
            }


        }

        /// <summary>
        /// Command for Adding a new note
        /// </summary>
        public RelayCommand AddNoteCommand { get => _addNoteCommand ?? (_addNoteCommand = new RelayCommand(AddNote, CanAddNote)); }


        /// <summary>
        /// Returns whether a new note can be added
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private bool CanAddNote(object parameter)
        {
            return _notes != null && !IsLoading;
        }

        /// <summary>
        /// Returns TRUE if the specified View can be closed
        /// </summary>
        /// <param name="parameter">View to close</param>
        /// <remarks>Used to allow the View to close</remarks>
        private bool CanCloseView(Window parameter)
        {
            return parameter != null;
        }

        /// <summary>
        /// Returns whether the Notes can be refreshed
        /// </summary>
        private bool CanRefresh(object parameter)
        {
            return !IsLoading;      //Providing we're not already in the process of refreshing
        }


        /// <summary>
        /// Create a View for the Note
        /// </summary>
        /// <remarks>Also adds a handler for the NoteDeleted Event</remarks>
        private NoteView CreateNoteView(INote note)
        {
            var view = new NoteView(note);
            view.NoteDeleted += (o, e) =>
            {
                if (e.View != null)
                {
                    //Remove the Note
                    _notes.Remove(e.View);
                    OnPropertyChanged("Notes");
                }
            };

            return view;
        }

        /// <summary>
        /// Close the View
        /// </summary>
        private void CloseView(Window parameter)
        {
            if (!CanCloseView(parameter)) return;
            parameter?.Close();
        }

        /// <summary>
        /// Command for Closing a view
        /// </summary>
        /// <remarks>A valid view must be passed as the Command Parameter</remarks>
        public RelayCommand<Window> CloseCommand { get => _closeCommand ?? (_closeCommand = new RelayCommand<Window>(CloseView, CanCloseView)); }

        /// <summary>
        /// Returns whether the Notes are loading
        /// </summary>
        public bool IsLoading { get { return _isLoading; } protected set { SetField(ref _isLoading, value, "IsLoading"); } }

        /// <summary>
        /// Load the Notes
        /// </summary>
        public async void Load()
        {
            //Clear any previous Errors
            LoadError = null;
            try
            {
                IsLoading = true;       //Mark the notes as loading
                //Perform Load
                var data = App.Services.GetService<Data.IDataCalls>();
                if (data == null)
                {
                    throw new Exception("No data service found.");
                }
                var notes = await data.GetAllNotesAsync();
                _notes = new ObservableCollection<NoteView>(notes.Select(n => CreateNoteView(n)).ToList());
                OnPropertyChanged("Notes");
            }
            catch (Exception ex)
            {
                //Display an error
                LoadError = ex;
            }
            finally
            {
                // Mark the notes as no longer loading
                IsLoading = false;
                CommandManager.InvalidateRequerySuggested();
            }
        }

        /// <summary>
        /// Holds any error that occurred during the loading process
        /// </summary>
        public Exception LoadError { get { return _loadError; } protected set { SetField(ref _loadError, value, "LoadError"); } }

        /// <summary>
        /// Collection of Notes
        /// </summary>
        public ICollectionView Notes
        {
            get { return CollectionViewSource.GetDefaultView(_notes); }
        }

        /// <summary>
        /// Command for Reloading the Notes
        /// </summary>
        public RelayCommand RefreshCommand { get => _refreshCommand ?? (_refreshCommand = new RelayCommand(Refresh, CanRefresh)); }


        /// <summary>
        /// Reload the Notes
        /// </summary>
        /// <param name="parameter"></param>
        private void Refresh(object parameter)
        {
            if (!CanRefresh(parameter)) return;
            Load();
        }

        #endregion  
    }
}
