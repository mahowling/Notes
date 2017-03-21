using Notes.Models;
using System;

namespace Notes.Wpf.UI.ViewModels
{
    public class NoteEventArgs : EventArgs
    {
        public INote Note { get; private set; }
        public NoteView View { get; private set; }

        public NoteEventArgs(INote note, NoteView view)
        {
            Note = note;
            View = view;
        }
    }
}
