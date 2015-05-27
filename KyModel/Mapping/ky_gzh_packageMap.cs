using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using KyModel.Models;
namespace KyModel.Mapping
{
    public class ky_gzh_packageMap : EntityTypeConfiguration<ky_gzh_package>
    {
        public ky_gzh_packageMap()
        {
            // Primary Key
            this.HasKey(t => t.kId);

            // Properties
            this.Property(t => t.kCashCenter)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.kVersion)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.kPackageNumber)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.kCurrency)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.kFileName)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("ky_gzh_package", "kydb");
            this.Property(t => t.kId).HasColumnName("kId");
            this.Property(t => t.kDate).HasColumnName("kDate");
            this.Property(t => t.kBranchId).HasColumnName("kBranchId");
            this.Property(t => t.kNodeId).HasColumnName("kNodeId");
            this.Property(t => t.kUserId).HasColumnName("kUserId");
            this.Property(t => t.kType).HasColumnName("kType");
            this.Property(t => t.kFSNNumber).HasColumnName("kFSNNumber");
            this.Property(t => t.kTotalValue).HasColumnName("kTotalValue");
            this.Property(t => t.kTotalNumber).HasColumnName("kTotalNumber");
            this.Property(t => t.kCashCenter).HasColumnName("kCashCenter");
            this.Property(t => t.kVersion).HasColumnName("kVersion");
            this.Property(t => t.kPackageNumber).HasColumnName("kPackageNumber");
            this.Property(t => t.kCurrency).HasColumnName("kCurrency");
            this.Property(t => t.kFileName).HasColumnName("kFileName");
        }
    }
}
