using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using KyModel.Models;
namespace KyModel.Mapping
{
    public class ky_imgserverMap : EntityTypeConfiguration<ky_imgserver>
    {
        public ky_imgserverMap()
        {
            // Primary Key
            this.HasKey(t => t.kId);

            // Properties
            this.Property(t => t.kIpAddress)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("ky_imgserver", "kydb");
            this.Property(t => t.kId).HasColumnName("kId");
            this.Property(t => t.kIpAddress).HasColumnName("kIpAddress");
            this.Property(t => t.kPort).HasColumnName("kPort");
            this.Property(t => t.kNodeId).HasColumnName("kNodeId");
            this.Property(t => t.kStatus).HasColumnName("kStatus");
        }
    }
}
