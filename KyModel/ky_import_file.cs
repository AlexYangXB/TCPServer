using System;
namespace KyModel
{
    //ky_import_file
    public class ky_import_file
    {

        public int kId { get; set; }
        public long kBatchId { get; set; }
        public string kFileName { get; set; }
        public DateTime kImportTime { get; set; }
        public string kType { get; set; }
        public int kNodeId { get; set; }

    }
}