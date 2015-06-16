using System;
using System.Collections.Generic;
using SqlFu;

namespace KyModel.Models
{
    [Table("ky_package_bundle", PrimaryKey = "kId")]
    public partial class ky_package_bundle
    {
        public int kId { get; set; }
        public int kBundleId { get; set; }
        public int kPackageId { get; set; }
    }
}
