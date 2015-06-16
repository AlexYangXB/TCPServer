using System;
using System.Collections.Generic;
using SqlFu;

namespace KyModel.Models
{
    [Table("ky_gzh_package", PrimaryKey = "kId")]
    public partial class ky_gzh_package
    {
        public int kId { get; set; }
        public System.DateTime kDate { get; set; }
        public int kBranchId { get; set; }
        public int kNodeId { get; set; }
        public int kUserId { get; set; }
        public int kType { get; set; }
        public int kFSNNumber { get; set; }
        public int kTotalValue { get; set; }
        public int kTotalNumber { get; set; }
        public string kCashCenter { get; set; }
        public string kVersion { get; set; }
        public string kPackageNumber { get; set; }
        public string kCurrency { get; set; }
        public string kFileName { get; set; }
    }
}
