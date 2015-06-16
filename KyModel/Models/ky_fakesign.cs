using System;
using System.Collections.Generic;
using SqlFu;

namespace KyModel.Models
{
    [Table("ky_fakesign", PrimaryKey = "kId")]
    public partial class ky_fakesign
    {
        public int kId { get; set; }
        public int kRegistId { get; set; }
        public string kSign { get; set; }
        public Nullable<int> kNumber { get; set; }
        public Nullable<int> kRetrieval { get; set; }
    }
}
