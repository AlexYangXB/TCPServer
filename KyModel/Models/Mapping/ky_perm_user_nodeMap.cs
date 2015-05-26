using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KyModel.Models.Mapping
{
    public class ky_perm_user_nodeMap : EntityTypeConfiguration<ky_perm_user_node>
    {
        public ky_perm_user_nodeMap()
        {
            // Primary Key
            this.HasKey(t => new { t.kUserId, t.kNodeId });

            // Properties
            this.Property(t => t.kUserId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.kNodeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ky_perm_user_node", "kydb");
            this.Property(t => t.kUserId).HasColumnName("kUserId");
            this.Property(t => t.kNodeId).HasColumnName("kNodeId");
        }
    }
}
