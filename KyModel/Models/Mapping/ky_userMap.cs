using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KyModel.Models.Mapping
{
    public class ky_userMap : EntityTypeConfiguration<ky_user>
    {
        public ky_userMap()
        {
            // Primary Key
            this.HasKey(t => t.kId);

            // Properties
            this.Property(t => t.kUserName)
                .HasMaxLength(255);

            this.Property(t => t.kUserNumber)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.kPassWord)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.remember_token)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("ky_user", "kydb");
            this.Property(t => t.kId).HasColumnName("kId");
            this.Property(t => t.kUserName).HasColumnName("kUserName");
            this.Property(t => t.kUserNumber).HasColumnName("kUserNumber");
            this.Property(t => t.kPrivilege).HasColumnName("kPrivilege");
            this.Property(t => t.kStatus).HasColumnName("kStatus");
            this.Property(t => t.kPassWord).HasColumnName("kPassWord");
            this.Property(t => t.kNodeId).HasColumnName("kNodeId");
            this.Property(t => t.remember_token).HasColumnName("remember_token");
            this.Property(t => t.updated_at).HasColumnName("updated_at");
        }
    }
}
