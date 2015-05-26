using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KyModel.Models.Mapping
{
    public class ky_pictureMap : EntityTypeConfiguration<ky_picture>
    {
        public ky_pictureMap()
        {
            // Primary Key
            this.HasKey(t => t.kId);



            // Table & Column Mappings
            this.ToTable("ky_picture");
            this.Property(t => t.kId).HasColumnName("kId");
            this.Property(t => t.kInsertTime).HasColumnName("kInsertTime");
            this.Property(t => t.kImageType).HasColumnName("kImageType");
            this.Property(t => t.kImageSNo).HasColumnName("kImageSNo");
        }
    }
}
