using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KyModel.Models.Mapping
{
    public class permissionMap : EntityTypeConfiguration<permission>
    {
        public permissionMap()
        {
            // Primary Key
            this.HasKey(t => t.kId);

            // Properties
            this.Property(t => t.name)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.description)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("permissions", "kydb");
            this.Property(t => t.kId).HasColumnName("kId");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.description).HasColumnName("description");
            this.Property(t => t.created_at).HasColumnName("created_at");
            this.Property(t => t.updated_at).HasColumnName("updated_at");

            // Relationships
            this.HasMany(t => t.roles)
                .WithMany(t => t.permissions)
                .Map(m =>
                    {
                        m.ToTable("permission_role", "kydb");
                        m.MapLeftKey("permission_id");
                        m.MapRightKey("role_id");
                    });


        }
    }
}
