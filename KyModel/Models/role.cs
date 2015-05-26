using System;
using System.Collections.Generic;

namespace KyModel.Models
{
    public partial class role
    {
        public role()
        {
            this.permissions = new List<permission>();
            this.ky_user = new List<ky_user>();
        }

        public int kId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public System.DateTime created_at { get; set; }
        public System.DateTime updated_at { get; set; }
        public virtual ICollection<permission> permissions { get; set; }
        public virtual ICollection<ky_user> ky_user { get; set; }
    }
}
