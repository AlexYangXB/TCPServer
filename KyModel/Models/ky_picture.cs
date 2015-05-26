using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KyModel.Models
{
    public class ky_picture
    {

        public long kId { get; set; }
        public DateTime kInsertTime { get; set; }
        public string kImageType { get; set; }
        public byte[] kImageSNo { get; set; }

    }
}
