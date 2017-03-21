using Notes.Wpf.UI.Internal;
using Notes.Models;
using Notes.Wpf.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using System.Threading;
using System.Diagnostics;

namespace Notes.Wpf.UI.ViewModels
{
    /// <summary>
    /// ViewModel for a Note
    /// </summary>
    public class NoteView : Notifier
    {
        public event EventHandler<NoteEventArgs> NoteDeleted;
        public event EventHandler<NoteEventArgs> NoteSaved;

        #region Internal Variables

        private INote _note;
        private RelayCommand _deleteCommand;
        private RelayCommand _saveCommand;
        private string _title = "Untitled";
        private string _content;
        private bool _isBusy = false;
        private Timer _autoSaveTimer;        

        #endregion

        #region Constructor

        public NoteView(INote note)
        {
            RefreshNote(note);
        }

        #endregion

        #region Methods

        private bool CanDelete(object parameter)
        {
            return (!IsBusy);

        }

        private bool CanSave(object parameter)
        {
            if (IsBusy) return false;
            return HasChanged();
        }

        public string Content
        {
            get { return _content; }
            set
            {
                if (SetField(ref _content, value, "Content"))
                {
                    if (HasChanged())
                    {
                        //Start a timer to auto save the changes in x minutes.
                        if (_autoSaveTimer!=null)
                        {
                            //cancel any previous timer
                            _autoSaveTimer.Dispose();
                            _autoSaveTimer = null;
                        }
                        var time = new TimeSpan(0, 0, 30);
                        _autoSaveTimer = new Timer(AutoSave, null, new TimeSpan(0, 0, 30), TimeSpan.Zero);
                        Debug.WriteLine($"Auto save in {time.TotalSeconds} seconds.");
                    }
                }
            }
        }

        public DateTime? Created
        {
            get
            {
                if (_note == null)
                    return null;

                return _note.CreatedUtc.ToLocalTime();
            }
        }

        private void AutoSave(object state)
        {
            Debug.WriteLine("Attempt Auto Save");
            if (HasChanged())
                Save(null);
        }


        private void Delete(object parameter)
        {
            if (!CanDelete(parameter)) return;
            try
            {
                if (MessageBox.Show("Delete this note?", "Delete?", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes) return;
                IsBusy = true;
                PerformDelete();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }

        }

        public RelayCommand DeleteCommand { get => _deleteCommand ?? (_deleteCommand = new RelayCommand(Delete, CanDelete)); }

        private bool HasChanged()
        {
            if (_note == null || _note.Id == null)
            {
                return !(string.IsNullOrWhiteSpace(Content) && string.IsNullOrWhiteSpace(Title));
            }

            if (Content != _note.Content) return true;
            if (Title != _note.Title) return true;
            return false;
        }

        public bool IsBusy { get { return _isBusy; } protected set { SetField(ref _isBusy, value, "IsBusy"); } }

        private void RefreshNote(INote note)
        {
            _note = note;
            if (_note != null)
            {
                Title = note.Title;
                Content = note.Content;                
            }
            OnPropertyChanged("Created");
        }

        public string Title
        {
            get { return _title; }
            set
            {
                SetField(ref _title, value, "Title");
            }
        }

        private void OnNoteDeleted(NoteEventArgs e)
        {
            NoteDeleted?.Invoke(this, e);
        }

        private void OnNoteSaved(NoteEventArgs e)
        {
            NoteSaved?.Invoke(this, e);
        }

        private void Save(object parameter)
        {
            if (!CanSave(parameter)) return;
            try
            {
                IsBusy = true;
                if (_note == null)
                    CreateNote();
                else
                    UpdateNote();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void PerformDelete()
        {
            if (_note != null && _note.Id != null)
            {
                IDataCalls data = App.Services.GetService<IDataCalls>();
                if (data == null)
                    throw new Exception("No data service found.");

                if (await data.DeleteNoteAsync(_note))
                {
                    //Raise Note Deleted event
                    OnNoteDeleted(new NoteEventArgs(_note, this));
                }
            }
        }

        private async void CreateNote()
        {
            _note = new Note()
            {
                Title = _title,
                Content = _content,
            };

            //Get the Data Service
            IDataCalls data = App.Services.GetService<IDataCalls>();
            if (data == null)
                throw new Exception("No data service found.");

            var result = await data.CreateNoteAsync(_note);
            RefreshNote(result);
            //Raise Note Saved event
            OnNoteSaved(new NoteEventArgs(_note, this));
        }

        private async void UpdateNote()
        {
            _note.Title = _title;
            _note.Content = _content;

            //Get the Data Service
            IDataCalls data = App.Services.GetService<IDataCalls>();
            if (data == null)
                throw new Exception("No data service found.");

            await data.UpdateNoteAsync(_note);
            //Raise Note Saved event
            OnNoteSaved(new NoteEventArgs(_note, this));
        }

        public RelayCommand SaveCommand { get => _saveCommand ?? (_saveCommand = new RelayCommand(Save, CanSave)); }

        #endregion

    }
}
