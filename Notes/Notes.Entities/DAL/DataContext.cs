using Microsoft.EntityFrameworkCore;
using Notes.Entities.Models;
using Notes.Entities.Extensions;
using System;

namespace Notes.Entities
{
    public class DataContext : DbContext, IDataContext
    {
        public virtual DbSet<Note> Notes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=Notes;Trusted_Connection=True;");            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.RemovePluralisingTableNameConvention();
            base.OnModelCreating(modelBuilder);

        }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }
    }
}
