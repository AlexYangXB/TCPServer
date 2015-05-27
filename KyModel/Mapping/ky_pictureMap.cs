using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using KyModel.Models;
namespace KyModel.Mapping
{
    public class ky_pictureMap : EntityTypeConfiguration<ky_picture>
    {
        public ky_pictureMap()
        {
            // Primary Key
            this.HasKey(t => new { t.kId, t.kInsertTime });



            // Table & Column Mappings
            this.ToTable("ky_picture");
            this.Property(t => t.kId).HasColumnName("kId");
            this.Property(t => t.kInsertTime).HasColumnName("kInsertTime");
            this.Property(t => t.kImageType).HasColumnName("kImageType");
            this.Property(t => t.kImageSNo).HasColumnName("kImageSNo");
        }
    }
}
