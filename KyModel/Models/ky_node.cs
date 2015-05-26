using System;
using System.Collections.Generic;

namespace KyModel.Models
{
    public partial class ky_node
    {
        public int kId { get; set; }
        public string kNodeName { get; set; }
        public string kNodeNumber { get; set; }
        public string kBindIpAddress { get; set; }
        public int kBranchId { get; set; }
        public int kStatus { get; set; }
    }
}
