using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Notes.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Data
{
    public interface IDataContext
    {
        DbSet<Note> Notes { get; set; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        void SaveChanges();
    }
}
