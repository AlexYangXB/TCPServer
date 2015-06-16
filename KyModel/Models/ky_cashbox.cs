using System;
using System.Collections.Generic;
using SqlFu;
namespace KyModel.Models
{
    [Table("ky_cashbox", PrimaryKey = "kId")]
    public partial class ky_cashbox
    {
        public int kId { get; set; }
        public string kCashBoxNumber { get; set; }
        public int kNodeId { get; set; }
    }
}
