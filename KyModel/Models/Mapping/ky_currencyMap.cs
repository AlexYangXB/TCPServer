using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KyModel.Models.Mapping
{
    public class ky_currencyMap : EntityTypeConfiguration<ky_currency>
    {
        public ky_currencyMap()
        {
            // Primary Key
            this.HasKey(t => t.kId);

            // Properties
            this.Property(t => t.kCurrencyCode)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.kCurrencyName)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("ky_currency", "kydb");
            this.Property(t => t.kId).HasColumnName("kId");
            this.Property(t => t.kCurrencyCode).HasColumnName("kCurrencyCode");
            this.Property(t => t.kCurrencyName).HasColumnName("kCurrencyName");
        }
    }
}
