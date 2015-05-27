using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using KyModel.Models;
namespace KyModel.Mapping
{
    public class ky_cashboxMap : EntityTypeConfiguration<ky_cashbox>
    {
        public ky_cashboxMap()
        {
            // Primary Key
            this.HasKey(t => t.kId);

            // Properties
            this.Property(t => t.kCashBoxNumber)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("ky_cashbox", "kydb");
            this.Property(t => t.kId).HasColumnName("kId");
            this.Property(t => t.kCashBoxNumber).HasColumnName("kCashBoxNumber");
            this.Property(t => t.kNodeId).HasColumnName("kNodeId");
        }
    }
}
