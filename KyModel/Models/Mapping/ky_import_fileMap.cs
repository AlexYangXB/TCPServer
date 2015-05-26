using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KyModel.Models.Mapping
{
    public class ky_import_fileMap : EntityTypeConfiguration<ky_import_file>
    {
        public ky_import_fileMap()
        {
            // Primary Key
            this.HasKey(t => t.kId);

            // Properties
            this.Property(t => t.kFileName)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.kType)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("ky_import_file", "kydb");
            this.Property(t => t.kId).HasColumnName("kId");
            this.Property(t => t.kBatchId).HasColumnName("kBatchId");
            this.Property(t => t.kFileName).HasColumnName("kFileName");
            this.Property(t => t.kImportTime).HasColumnName("kImportTime");
            this.Property(t => t.kType).HasColumnName("kType");
            this.Property(t => t.kNodeId).HasColumnName("kNodeId");
        }
    }
}
