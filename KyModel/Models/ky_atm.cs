using System;
using System.Collections.Generic;

namespace KyModel.Models
{
    public partial class ky_atm
    {
        public int kId { get; set; }
        public string kATMNumber { get; set; }
        public string kATMAddress { get; set; }
        public int kNodeId { get; set; }
        public Nullable<long> kStatus { get; set; }
    }
}
