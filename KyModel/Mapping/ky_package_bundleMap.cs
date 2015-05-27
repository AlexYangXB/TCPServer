using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using KyModel.Models;
namespace KyModel.Mapping
{
    public class ky_package_bundleMap : EntityTypeConfiguration<ky_package_bundle>
    {
        public ky_package_bundleMap()
        {
            // Primary Key
            this.HasKey(t => t.kId);

            // Properties
            // Table & Column Mappings
            this.ToTable("ky_package_bundle", "kydb");
            this.Property(t => t.kId).HasColumnName("kId");
            this.Property(t => t.kBundleId).HasColumnName("kBundleId");
            this.Property(t => t.kPackageId).HasColumnName("kPackageId");
        }
    }
}
