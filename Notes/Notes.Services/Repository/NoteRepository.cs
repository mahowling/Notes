using System;
using System.Collections.Generic;
using Notes.Entities;
using Notes.Entities.Models;
using System.Linq;
using System.Linq.Expressions;
using Notes.Services.Internal;

namespace Notes.Services
{
    /// <summary>
    /// Notes Respository
    /// </summary>
    public class NoteRepository : Disposable, INoteRepository
    {
        #region internal variables

        private readonly IDataContext context;

        #endregion

        #region Constructor

        public NoteRepository(IDataContext context)
        {
            this.context = context;
        }

        #endregion

        #region methods
        
        /// <summary>
        /// Delete a note
        /// </summary>
        public void Delete(Guid Id)
        {
            Delete(GetById(Id));
        }

        /// <summary>
        /// Delete a note
        /// </summary>
        public void Delete(Note entity)
        {
            context.Notes.Remove(entity);
            Save();
        }

        /// <summary>
        /// Return all Notes
        /// </summary>
        public IEnumerable<Note> GetAll()
        {
            return context.Notes.ToList();
        }

        /// <summary>
        /// Return all matching notes
        /// </summary>
        public IEnumerable<Note> GetAll(Expression<Func<Note, bool>> where)
        {
            return context.Notes.Where(where).ToList();
        }

        /// <summary>
        /// Find a specific note
        /// </summary>
        public Note GetById(Guid id)
        {
            return context.Notes.FirstOrDefault(i=>i.Id==id);
        }

        /// <summary>
        /// Add a new note
        /// </summary>
        public void Insert(Note entity)
        {
            context.Notes.Add(entity);
            Save();
        }
        /// <summary>
        /// Save changes
        /// </summary>
        public void Save()
        {
            context.SaveChanges();
        }

        /// <summary>
        /// Update an existing note
        /// </summary>
        public void Update(Note entity)
        {
            context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Save();
        }

        #endregion
    }
}
