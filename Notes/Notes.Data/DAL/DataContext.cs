using Microsoft.EntityFrameworkCore;
using Notes.Models;
using Notes.Data.Extensions;
using System;

namespace Notes.Data
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
