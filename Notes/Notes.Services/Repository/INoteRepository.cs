using Notes.Models;
    using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Services
{
    /// <summary>
    /// Notes Respository
    /// </summary>
    public interface INoteRepository  : IDisposable, Internal.IRepositoryBase<Note>
    {
    }
}
