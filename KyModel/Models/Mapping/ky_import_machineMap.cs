using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace KyModel.Models.Mapping
{
    public class ky_import_machineMap : EntityTypeConfiguration<ky_import_machine>
    {
        public ky_import_machineMap()
        {
            // Primary Key
            this.HasKey(t => t.kId);

            // Properties
            this.Property(t => t.kMachineNumber)
                .IsRequired()
                .HasMaxLength(255);

            this.Property(t => t.kMachineModel)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("ky_import_machine", "kydb");
            this.Property(t => t.kId).HasColumnName("kId");
            this.Property(t => t.kMachineType).HasColumnName("kMachineType");
            this.Property(t => t.kMachineNumber).HasColumnName("kMachineNumber");
            this.Property(t => t.kMachineModel).HasColumnName("kMachineModel");
            this.Property(t => t.kNodeId).HasColumnName("kNodeId");
            this.Property(t => t.kFactoryId).HasColumnName("kFactoryId");
        }
    }
}
