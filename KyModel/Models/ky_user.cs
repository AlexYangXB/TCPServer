using System;
using System.Collections.Generic;

namespace KyModel.Models
{
    public partial class ky_user
    {
        public ky_user()
        {
            this.roles = new List<role>();
        }

        public int kId { get; set; }
        public string kUserName { get; set; }
        public string kUserNumber { get; set; }
        public int kPrivilege { get; set; }
        public int kStatus { get; set; }
        public string kPassWord { get; set; }
        public Nullable<int> kNodeId { get; set; }
        public string remember_token { get; set; }
        public Nullable<DateTime> updated_at { get; set; }
        public virtual ICollection<role> roles { get; set; }
    }
}
