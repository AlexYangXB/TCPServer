using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KyModel.Models;
namespace KyBll
{
    public class FSNFormat
    {
        public static ky_machine ConvertToFsnMachine(ky_machine machine)
        {
            int MachineType = machine.kMachineType;
            switch (MachineType)
            {
                //1 清分机具  =>1 大型清分机 2 中型清分机 3 小型清分机
                //3 点钞机 => 4 一口半点钞机 5 A类点钞机 
                //2 存取款一体机 =>6 存取款一体机 
                //4 取款机 => 7 ATM取款机  
                //5 兑换机具 => 9 兑换机具 
                //0 未定义 =>10 其他机具 8 循环柜员机
                case 1:
                case 2:
                case 3: machine.kMachineType = 1; break;
                case 4:
                case 5: machine.kMachineType = 3; break;
                case 6: machine.kMachineType = 2; break;
                case 7: machine.kMachineType = 4; break;
                case 9: machine.kMachineType = 5; break;
                default: machine.kMachineType = 0; break;
            }
            string MachineNumber = machine.kMachineNumber;
            string MachineModel = machine.kMachineModel;
            int index=MachineNumber.IndexOf(MachineModel);
            if (index!= -1)
            {
                machine.kMachineNumber = MachineNumber.Substring(index + MachineModel.Length);
            }
            return machine;
        }
    }
}
