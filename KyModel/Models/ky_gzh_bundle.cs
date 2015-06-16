using System;
using System.Collections.Generic;
using SqlFu;

namespace KyModel.Models
{
    [Table("ky_gzh_bundle", PrimaryKey = "kId")]
    public partial class ky_gzh_bundle
    {
        public int kId { get; set; }
        public string kBundleNumber { get; set; }
        public long kBatchId { get; set; }
        public string kFileName { get; set; }
    }
}
