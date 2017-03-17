using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Notes.Entities.Extensions
{
    internal static class ModelBuilderExtensions
    {

        /// <summary>
        /// Ensures the Tables created do not get pluralised.
        /// </summary>
        public static void RemovePluralisingTableNameConvention(this ModelBuilder modelBuilder)
        {
            foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.Relational().TableName = entity.DisplayName();
            }
        }
    }
}
