using System;
using System.Collections.Generic;
using SqlFu;

namespace KyModel.Models
{
    [Table("ky_user", PrimaryKey = "kId")]
    public partial class ky_user
    {
        public int kId { get; set; }
        public string kUserName { get; set; }
        public string kUserNumber { get; set; }
        public int kPrivilege { get; set; }
        public int kStatus { get; set; }
        public string kPassWord { get; set; }
        public Nullable<int> kNodeId { get; set; }
        public string remember_token { get; set; }
        public Nullable<DateTime> updated_at { get; set; }
    }
}
