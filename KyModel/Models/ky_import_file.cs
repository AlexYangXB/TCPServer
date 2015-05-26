using System;
using System.Collections.Generic;

namespace KyModel.Models
{
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
