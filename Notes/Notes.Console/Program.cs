using Notes.Entities.Models;
using Notes.Entities;
using Notes.Services;

namespace Notes.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new DataContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                using (var repository = new NoteRepository(db))
                {
                    for (var i = 0; i < 20; i++)
                    {
                        var note = new Note() { Title = $"Test Note {i}", Content = $"A Test Note.  Index {i}" };
                        repository.Insert(note);
                    }
                    repository.Save();
                }
            }
        }
    }
}