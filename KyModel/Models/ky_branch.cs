using System;
using System.Collections.Generic;
using SqlFu;
namespace KyModel.Models
{
    [Table("ky_branch", PrimaryKey = "kId")]
    public partial class ky_branch
    {
        public int kId { get; set; }
        public string kBranchName { get; set; }
        public string kBranchNumber { get; set; }
        public int kStatus { get; set; }
    }
}
