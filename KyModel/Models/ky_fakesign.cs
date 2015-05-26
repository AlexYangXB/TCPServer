using System;
using System.Collections.Generic;

namespace KyModel.Models
{
    public partial class ky_fakesign
    {
        public int kId { get; set; }
        public int kRegistId { get; set; }
        public string kSign { get; set; }
        public Nullable<int> kNumber { get; set; }
        public Nullable<int> kRetrieval { get; set; }
    }
}
