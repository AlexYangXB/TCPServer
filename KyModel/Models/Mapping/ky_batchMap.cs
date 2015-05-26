using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KyModel.Models.Mapping
{
    public class ky_batchMap : EntityTypeConfiguration<ky_batch>
    {
        public ky_batchMap()
        {
            // Primary Key
            this.HasKey(t => t.id);



            // Table & Column Mappings
            this.ToTable("ky_batch");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.ktype).HasColumnName("ktype");
            this.Property(t => t.kdate).HasColumnName("kdate");
            this.Property(t => t.knode).HasColumnName("knode");
            this.Property(t => t.kfactory).HasColumnName("kfactory");
            this.Property(t => t.kmachine).HasColumnName("kmachine");
            this.Property(t => t.ktotalvalue).HasColumnName("ktotalvalue");
            this.Property(t => t.ktotalnumber).HasColumnName("ktotalnumber");
            this.Property(t => t.kuser).HasColumnName("kuser");
            this.Property(t => t.kimgserver).HasColumnName("kimgserver");
            this.Property(t => t.hjson).HasColumnName("hjson");
        }
    }
}
