using System;
namespace KyModel
{
    //ky_user
    public class ky_user
    {

        public int kId { get; set; }
        public string kUserName { get; set; }
        public string kUserNumber { get; set; }
        public int kPrivilege { get; set; }
        public int kStatus { get; set; }
        public string kPassWord { get; set; }
        public int kNodeId { get; set; }
        public string remember_token { get; set; }
        public DateTime updated_at { get; set; }

    }
}