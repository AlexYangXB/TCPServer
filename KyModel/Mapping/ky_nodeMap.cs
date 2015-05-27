using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using KyModel.Models;
namespace KyModel.Mapping
{
    public class ky_nodeMap : EntityTypeConfiguration<ky_node>
    {
        public ky_nodeMap()
        {
            // Primary Key
            this.HasKey(t => t.kId);

            // Properties
            this.Property(t => t.kNodeName)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.kNodeNumber)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.kBindIpAddress)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("ky_node", "kydb");
            this.Property(t => t.kId).HasColumnName("kId");
            this.Property(t => t.kNodeName).HasColumnName("kNodeName");
            this.Property(t => t.kNodeNumber).HasColumnName("kNodeNumber");
            this.Property(t => t.kBindIpAddress).HasColumnName("kBindIpAddress");
            this.Property(t => t.kBranchId).HasColumnName("kBranchId");
            this.Property(t => t.kStatus).HasColumnName("kStatus");
        }
    }
}
