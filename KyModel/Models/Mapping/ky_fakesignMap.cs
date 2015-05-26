using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KyModel.Models.Mapping
{
    public class ky_fakesignMap : EntityTypeConfiguration<ky_fakesign>
    {
        public ky_fakesignMap()
        {
            // Primary Key
            this.HasKey(t => t.kId);

            // Properties
            this.Property(t => t.kSign)
                .IsRequired()
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("ky_fakesign", "kydb");
            this.Property(t => t.kId).HasColumnName("kId");
            this.Property(t => t.kRegistId).HasColumnName("kRegistId");
            this.Property(t => t.kSign).HasColumnName("kSign");
            this.Property(t => t.kNumber).HasColumnName("kNumber");
            this.Property(t => t.kRetrieval).HasColumnName("kRetrieval");
        }
    }
}
