using System;
using System.Collections.Generic;
using SqlFu;
namespace KyModel.Models
{
     [Table("ky_currency", PrimaryKey = "kId")]
    public partial class ky_currency
    {
        public int kId { get; set; }
        public string kCurrencyCode { get; set; }
        public string kCurrencyName { get; set; }
    }
}
