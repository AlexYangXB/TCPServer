using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KyModel.Models.Mapping
{
    public class ky_atmtypeMap : EntityTypeConfiguration<ky_atmtype>
    {
        public ky_atmtypeMap()
        {
            // Primary Key
            this.HasKey(t => t.kId);

            // Properties
            this.Property(t => t.kAtmType)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("ky_atmtype", "kydb");
            this.Property(t => t.kId).HasColumnName("kId");
            this.Property(t => t.kAtmType).HasColumnName("kAtmType");
        }
    }
}
