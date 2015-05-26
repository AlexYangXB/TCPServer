using System;
using System.Collections.Generic;

namespace KyModel.Models
{
    public partial class permission
    {
        public permission()
        {
            this.roles = new List<role>();
        }

        public int kId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public System.DateTime created_at { get; set; }
        public System.DateTime updated_at { get; set; }
        public virtual ICollection<role> roles { get; set; }
    }
}
