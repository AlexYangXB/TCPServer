using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KyModel.Models.Mapping
{
    public class ky_factoryMap : EntityTypeConfiguration<ky_factory>
    {
        public ky_factoryMap()
        {
            // Primary Key
            this.HasKey(t => t.kId);

            // Properties
            this.Property(t => t.kFactoryName)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.kFactoryCode)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("ky_factory", "kydb");
            this.Property(t => t.kId).HasColumnName("kId");
            this.Property(t => t.kFactoryName).HasColumnName("kFactoryName");
            this.Property(t => t.kFactoryCode).HasColumnName("kFactoryCode");
        }
    }
}
