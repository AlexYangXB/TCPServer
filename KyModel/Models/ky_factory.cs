using System;
using System.Collections.Generic;
using SqlFu;

namespace KyModel.Models
{
    [Table("ky_factory", PrimaryKey = "kId")]
    public partial class ky_factory
    {
        public int kId { get; set; }
        public string kFactoryName { get; set; }
        public string kFactoryCode { get; set; }
    }
}
