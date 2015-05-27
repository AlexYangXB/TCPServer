using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using KyModel.Models;
namespace KyModel.Mapping
{
    public class ky_versionMap : EntityTypeConfiguration<ky_version>
    {
        public ky_versionMap()
        {
            // Primary Key
            this.HasKey(t => t.kId);

            // Properties
            this.Property(t => t.kVersion)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("ky_version", "kydb");
            this.Property(t => t.kId).HasColumnName("kId");
            this.Property(t => t.kVersion).HasColumnName("kVersion");
        }
    }
}
