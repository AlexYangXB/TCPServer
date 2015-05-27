using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using KyModel.Models;
namespace KyModel.Mapping
{
    public class migrationMap : EntityTypeConfiguration<migration>
    {
        public migrationMap()
        {
            // Primary Key
            this.HasKey(t => new { t.migration1, t.batch });

            // Properties
            this.Property(t => t.migration1)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.batch)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("migrations", "kydb");
            this.Property(t => t.migration1).HasColumnName("migration");
            this.Property(t => t.batch).HasColumnName("batch");
        }
    }
}
