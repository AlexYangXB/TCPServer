using System;
using System.Collections.Generic;
using SqlFu;
namespace KyModel.Models
{
    [Table("ky_atm", PrimaryKey = "kId")]
    public partial class ky_atm
    {
        public int kId { get; set; }
        public string kATMNumber { get; set; }
        public string kATMAddress { get; set; }
        public int kNodeId { get; set; }
        public Nullable<long> kStatus { get; set; }
    }
}
