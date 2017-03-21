using Notes.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notes.Wpf.Data
{
    interface IDataCalls
    {
        Task<bool> DeleteNoteAsync(INote note);

        Task<IEnumerable<INote>> GetAllNotesAsync();
        Task<INote> CreateNoteAsync(INote note);

        Task<bool> UpdateNoteAsync(INote note);
    }
}
