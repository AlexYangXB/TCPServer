using System;
using SqlFu;

namespace KyModel.Models
{
    public partial  class ky_picture
    {

        public long kId { get; set; }
        public Nullable<DateTime> kInsertTime { get; set; }
        public string kImageType { get; set; }
        public byte[] kImageSNo { get; set; }

    }
}
