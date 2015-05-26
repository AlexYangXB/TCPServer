using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KyModel.Models.Mapping
{
    public class ky_atmMap : EntityTypeConfiguration<ky_atm>
    {
        public ky_atmMap()
        {
            // Primary Key
            this.HasKey(t => t.kId);

            // Properties
            this.Property(t => t.kATMNumber)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.kATMAddress)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("ky_atm", "kydb");
            this.Property(t => t.kId).HasColumnName("kId");
            this.Property(t => t.kATMNumber).HasColumnName("kATMNumber");
            this.Property(t => t.kATMAddress).HasColumnName("kATMAddress");
            this.Property(t => t.kNodeId).HasColumnName("kNodeId");
            this.Property(t => t.kStatus).HasColumnName("kStatus");
        }
    }
}
