using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KyModel.Models.Mapping
{
    public class ky_signMap : EntityTypeConfiguration<ky_sign>
    {
        public ky_signMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            

            // Table & Column Mappings
            this.ToTable("ky_sign");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.knumber).HasColumnName("knumber");
            this.Property(t => t.kdate).HasColumnName("kdate");
            this.Property(t => t.ksign).HasColumnName("ksign");
            this.Property(t => t.kbatchid).HasColumnName("kbatchid");
            this.Property(t => t.kvalue).HasColumnName("kvalue");
            this.Property(t => t.kversion).HasColumnName("kversion");
            this.Property(t => t.kcurrency).HasColumnName("kcurrency");
            this.Property(t => t.kstatus).HasColumnName("kstatus");
            this.Property(t => t.hjson).HasColumnName("hjson");
        }
    }
}
