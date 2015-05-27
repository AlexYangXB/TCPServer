using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using KyModel.Models;
namespace KyModel.Mapping
{
    public class ky_cashstatusMap : EntityTypeConfiguration<ky_cashstatus>
    {
        public ky_cashstatusMap()
        {
            // Primary Key
            this.HasKey(t => t.kId);

            // Properties
            this.Property(t => t.kCashStatus)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("ky_cashstatus", "kydb");
            this.Property(t => t.kId).HasColumnName("kId");
            this.Property(t => t.kCashStatus).HasColumnName("kCashStatus");
        }
    }
}
