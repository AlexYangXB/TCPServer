using System;
using System.Collections.Generic;

namespace KyModel.Models
{
    public partial class ky_import_machine
    {
        public int kId { get; set; }
        public int kMachineType { get; set; }
        public string kMachineNumber { get; set; }
        public string kMachineModel { get; set; }
        public int kNodeId { get; set; }
        public int kFactoryId { get; set; }
    }
}
