using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KyModel.Models;
using KyModel;
namespace KyBll
{
    public class FSNFormat
    {
        /// <summary>
        /// 机型、设备类别、编号的转换
        /// </summary>
        /// <param name="machine"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 从FSN文件获取机具信息，若在ky_machine和ky_import_machine未能找到对应机具编号，则在ky_import_machine插入机具信息
        /// </summary>
        /// <param name="file"></param>
        /// <param name="NodeId"></param>
        /// <returns></returns>
        public static ky_machine FindMachineByFsn(string file, int NodeId,int FacotryId=0)
        {
            //获取FSN文件内的机具编号
            string machineModel = "";
            string[] str = KyDataLayer2.GetMachineNumberFromFSN(file, out machineModel).Split("/".ToCharArray());
            string machineMac = "";
            string factory = "";
            if (str.Length == 3)
            {
                factory = str[1];
                machineMac = str[2];
            }
            //获取厂家ID
            if (FacotryId == 0)
            {
                FacotryId = KyDataOperation.GetFactoryId(factory);
                if (FacotryId == 0)//厂家编号不存在
                    FacotryId = KyDataOperation.InsertFactory("", factory);
                  
            }

            int machineId = 0;
            int machineId2 = 0;
            machineId = KyDataOperation.GetMachineIdByMachineNumber(machineMac);
            if (machineId == 0)//未在机具列表中找到该机具编号
            {
                //获取数据库内的上传文件的机具列表
                machineId2 = KyDataOperation.GetMachineIdFromImportMachine(machineMac);
                if (machineId2 == 0)//未在上传文件的机具列表中找到该机具编号
                {
                    ky_import_machine import_machine = new ky_import_machine
                    {
                        kMachineNumber = machineMac,
                        kMachineModel=machineModel,
                        kNodeId = NodeId,
                        kFactoryId = FacotryId
                    };
                    int id = KyDataOperation.InsertMachineToImportMachine(import_machine);
                    if (id > 0)
                        machineId2 = id;
                }
            }
            ky_machine machine = new ky_machine();
            machine.kMachineNumber = machineMac;
            machine.kFactoryId = FacotryId;
            machine.kId = machineId;
            machine.importMachineId = machineId2;
            return machine;

        }
      
    }
}
