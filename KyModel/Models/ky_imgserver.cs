using System;
using System.Collections.Generic;

namespace KyModel.Models
{
    public partial class ky_imgserver
    {
        public int kId { get; set; }
        public string kIpAddress { get; set; }
        public int kPort { get; set; }
        public Nullable<int> kNodeId { get; set; }
        public Nullable<int> kStatus { get; set; }
    }
}
