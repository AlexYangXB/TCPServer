using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using KyModel.Models;
namespace KyModel.Mapping
{
    public class ky_fakeregistMap : EntityTypeConfiguration<ky_fakeregist>
    {
        public ky_fakeregistMap()
        {
            // Primary Key
            this.HasKey(t => t.kId);

            // Properties
            this.Property(t => t.kCustomerName)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.kIdentityCertNumber)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.kReMarks)
                .HasMaxLength(100);

            this.Property(t => t.kPhoneNumber)
                .HasMaxLength(30);

            this.Property(t => t.kContactAddress)
                .HasMaxLength(100);

            this.Property(t => t.kSearchResult)
                .HasMaxLength(255);

            this.Property(t => t.kCopeResult)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("ky_fakeregist", "kydb");
            this.Property(t => t.kId).HasColumnName("kId");
            this.Property(t => t.kVerify).HasColumnName("kVerify");
            this.Property(t => t.kApplyTime).HasColumnName("kApplyTime");
            this.Property(t => t.kApplyType).HasColumnName("kApplyType");
            this.Property(t => t.kSerialNumber).HasColumnName("kSerialNumber");
            this.Property(t => t.kCustomerName).HasColumnName("kCustomerName");
            this.Property(t => t.kIdentityCertType).HasColumnName("kIdentityCertType");
            this.Property(t => t.kIdentityCertNumber).HasColumnName("kIdentityCertNumber");
            this.Property(t => t.kCertMaterialType).HasColumnName("kCertMaterialType");
            this.Property(t => t.kSearchType).HasColumnName("kSearchType");
            this.Property(t => t.kDrawTime).HasColumnName("kDrawTime");
            this.Property(t => t.kDrawPlace).HasColumnName("kDrawPlace");
            this.Property(t => t.kDrawType).HasColumnName("kDrawType");
            this.Property(t => t.kReMarks).HasColumnName("kReMarks");
            this.Property(t => t.kPhoneNumber).HasColumnName("kPhoneNumber");
            this.Property(t => t.kContactAddress).HasColumnName("kContactAddress");
            this.Property(t => t.kOperatorId).HasColumnName("kOperatorId");
            this.Property(t => t.kReCheckOperatorId).HasColumnName("kReCheckOperatorId");
            this.Property(t => t.kSearchResult).HasColumnName("kSearchResult");
            this.Property(t => t.kCopeResult).HasColumnName("kCopeResult");
        }
    }
}
