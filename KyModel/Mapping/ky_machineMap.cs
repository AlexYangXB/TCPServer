using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using KyModel.Models;
namespace KyModel.Mapping
{
    public class ky_machineMap : EntityTypeConfiguration<ky_machine>
    {
        public ky_machineMap()
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

            this.Property(t => t.kIpAddress)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("ky_machine", "kydb");
            this.Property(t => t.kId).HasColumnName("kId");
            this.Property(t => t.kMachineType).HasColumnName("kMachineType");
            this.Property(t => t.kMachineNumber).HasColumnName("kMachineNumber");
            this.Property(t => t.kMachineModel).HasColumnName("kMachineModel");
            this.Property(t => t.kIpAddress).HasColumnName("kIpAddress");
            this.Property(t => t.kStatus).HasColumnName("kStatus");
            this.Property(t => t.kNodeId).HasColumnName("kNodeId");
            this.Property(t => t.kFactoryId).HasColumnName("kFactoryId");
            this.Property(t => t.kUpdateTime).HasColumnName("kUpdateTime");
            this.Ignore(p => p.startBusinessCtl);
            this.Ignore(p => p.fileName);
            this.Ignore(p => p.dateTime);
            this.Ignore(p => p.business);
            this.Ignore(p => p.bundleCount);
            this.Ignore(p => p.userId);
            this.Ignore(p => p.imgServerId);
            this.Ignore(p => p.importMachineId);
            this.Ignore(p => p.bussinessNumber);
            this.Ignore(p => p.atmId);
            this.Ignore(p => p.cashBoxId);
            this.Ignore(p => p.isClearCenter);
            this.Ignore(p => p.packageNumber);
        }
    }
}
