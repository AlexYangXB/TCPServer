using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KyModel.Models.Mapping
{
    public class ky_branchMap : EntityTypeConfiguration<ky_branch>
    {
        public ky_branchMap()
        {
            // Primary Key
            this.HasKey(t => t.kId);

            // Properties
            this.Property(t => t.kBranchName)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.kBranchNumber)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("ky_branch", "kydb");
            this.Property(t => t.kId).HasColumnName("kId");
            this.Property(t => t.kBranchName).HasColumnName("kBranchName");
            this.Property(t => t.kBranchNumber).HasColumnName("kBranchNumber");
            this.Property(t => t.kStatus).HasColumnName("kStatus");
        }
    }
}
