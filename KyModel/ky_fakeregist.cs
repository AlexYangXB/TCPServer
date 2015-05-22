using System;
namespace KyModel
{
    //ky_fakeregist
    public class ky_fakeregist
    {

        public int kId { get; set; }
        public int kVerify { get; set; }
        public DateTime kApplyTime { get; set; }
        public int kApplyType { get; set; }
        public long kSerialNumber { get; set; }
        public string kCustomerName { get; set; }
        public int kIdentityCertType { get; set; }
        public string kIdentityCertNumber { get; set; }
        public int kCertMaterialType { get; set; }
        public int kSearchType { get; set; }
        public DateTime kDrawTime { get; set; }
        public int kDrawPlace { get; set; }
        public int kDrawType { get; set; }
        public string kReMarks { get; set; }
        public string kPhoneNumber { get; set; }
        public string kContactAddress { get; set; }
        public int kOperatorId { get; set; }
        public int kReCheckOperatorId { get; set; }
        public string kSearchResult { get; set; }
        public string kCopeResult { get; set; }

    }
}