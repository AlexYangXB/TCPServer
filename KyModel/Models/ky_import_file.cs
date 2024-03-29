using System;
using System.Collections.Generic;
using SqlFu;

namespace KyModel.Models
{
    [Table("ky_import_file", PrimaryKey = "kId")]
    public partial class ky_import_file
    {
        public int kId { get; set; }
        public long kBatchId { get; set; }
        public string kFileName { get; set; }
        public System.DateTime kImportTime { get; set; }
        public string kType { get; set; }
        public int kNodeId { get; set; }
    }
}
