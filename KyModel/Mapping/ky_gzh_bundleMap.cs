using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using KyModel.Models;
namespace KyModel.Mapping
{
    public class ky_gzh_bundleMap : EntityTypeConfiguration<ky_gzh_bundle>
    {
        public ky_gzh_bundleMap()
        {
            // Primary Key
            this.HasKey(t => t.kId);

            // Properties
            this.Property(t => t.kBundleNumber)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.kFileName)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("ky_gzh_bundle", "kydb");
            this.Property(t => t.kId).HasColumnName("kId");
            this.Property(t => t.kBundleNumber).HasColumnName("kBundleNumber");
            this.Property(t => t.kBatchId).HasColumnName("kBatchId");
            this.Property(t => t.kFileName).HasColumnName("kFileName");
        }
    }
}
