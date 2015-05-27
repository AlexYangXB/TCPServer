using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using KyModel.Models;
namespace KyModel.Mapping
{
    public class roleMap : EntityTypeConfiguration<role>
    {
        public roleMap()
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
            this.ToTable("roles", "kydb");
            this.Property(t => t.kId).HasColumnName("kId");
            this.Property(t => t.name).HasColumnName("name");
            this.Property(t => t.description).HasColumnName("description");
            this.Property(t => t.created_at).HasColumnName("created_at");
            this.Property(t => t.updated_at).HasColumnName("updated_at");

            // Relationships
            this.HasMany(t => t.ky_user)
                .WithMany(t => t.roles)
                .Map(m =>
                    {
                        m.ToTable("role_user", "kydb");
                        m.MapLeftKey("role_id");
                        m.MapRightKey("user_id");
                    });


        }
    }
}
