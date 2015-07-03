using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using KyBll.DBUtility;
using KyModel;
using KyModel.Models;
using SqlFu;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
namespace KyBll
{
    public class KyDataOperation
    {
        public static Queue<ky_picture> pictureQueue = new Queue<ky_picture>();
        public static Queue<string> sqlQueue = new Queue<string>();
        #region 保存冠字号码数据到Sphinx

        /// <summary>
        /// 往sphinx中插入批次信息
        /// </summary>
        /// <param name="batch"></param>
        /// <returns></returns>
        public static bool InsertSignBatch(ky_batch batch)
        {
            try
            {
                string strSql = string.Format(
                    "INSERT INTO ky_batch(id,ktype,kdate,knode,kfactory,kmachine,ktotalnumber,ktotalvalue,kuser,kimgserver,hjson) values({0},'{1}',{2},{3},{4},({5}),{6},{7},{8},{9},'{10}')",
                    batch.id, batch.ktype, batch.kdate, batch.knode, batch.kfactory, batch.kmachine, batch.ktotalnumber, batch.ktotalvalue,
                    batch.kuser, batch.kimgserver, batch.hjson);
                object synObj = new object();
                lock (synObj)
                {
                    sqlQueue.Enqueue(strSql);
                }
                return true;
            }
            catch (Exception e)
            {
                KyBll.MyLog.DataBaseException("保存批次异常", e);
                return false;
            }
        }


        /// <summary>
        /// 往sphinx中插入冠字号码数据，往图片数据库中插入图片数据
        /// </summary>
        /// <param name="batchId">批次号</param>
        /// <param name="startIndex">批内起始序号 </param>
        /// <param name="signs">冠字号码数据</param>
        /// <returns></returns>
        public static bool InsertSign(Int64 batchId, int startIndex, List<KYDataLayer1.SignTypeL2> signs)
        {
            try
            {
                //线程访问控制
                object synObj = new object();
                lock (synObj)
                {
                    if (signs.Count > 0)
                    {
                        DateTime now = DateTime.Now;
                        int count = 0;
                        string strSql = "INSERT INTO ky_sign(id,kdate,ksign,kbatchid,kvalue,kversion,kcurrency,kstatus,knumber,hjson) values";
                        foreach (var sign in signs)
                        {
                            if (count != 0)
                            {
                                strSql += ",";
                            }
                            int time = DateTimeAndTimeStamp.ConvertDateTimeInt(sign.Date);
                            Int64 id = KyDataLayer2.GuidToLongID();

                            if (MySetting.GetProgramValue("UploadPicture"))
                            {
                                ky_picture picture = new ky_picture { kId = id, kImageSNo = sign.imageData, kInsertTime = now, kImageType = sign.ImageType };

                                pictureQueue.Enqueue(picture);
                            }
                            JObject jo = new JObject();
                            if(sign.ErrorCode!="0-0-0")
                                jo["kerrcode"]=sign.ErrorCode;

                            strSql += string.Format("({0},{1},'{2}',{3},{4},{5},{6},{7},{8},'{9}')", id, time,
                                                       sign.Sign, batchId, sign.Value, sign.Version,
                                                       sign.Currency, sign.True, startIndex + count, JsonConvert.SerializeObject(jo));
                            count++;
                        }

                        sqlQueue.Enqueue(strSql);

                    }
                    return true;
                }
            }
            catch (Exception e)
            {
                MyLog.DataBaseException("保存冠字号码异常", e);
                return false;
            }

        }
        public static bool InsertPictures(List<ky_picture> pictures)
        {
            try
            {
                using (var conn = DbHelperMySQL.OpenImageConnection())
                {
                    SqlFuDao.OnCommand = cmd => cmd.CommandTimeout = 120;
                    DateTime start = DateTime.Now;
                    conn.InsertAll<ky_picture>(pictures);
                    TimeSpan span = DateTime.Now - start;
                    KyBll.MyLog.TestLog(pictures.Count + " 张图像上传用时" + span.TotalMilliseconds + "ms.");
                }
                return true;
            }
            catch (Exception e)
            {
                MyLog.DataBaseException("保存图像异常", e);
                return false;
            }
        }
        public static bool InsertWithSql(List<string> strSqls)
        {
            try
            {
                DateTime start = DateTime.Now;
                using (var conn = DbHelperMySQL.OpenSphinxConnection())
                {
                    foreach (var strSql in strSqls)
                    {
                        conn.Execute(strSql);
                    }

                }
                TimeSpan span = DateTime.Now - start;
                KyBll.MyLog.TestLog(strSqls.Count + " 条Sql执行用时 " + span.TotalMilliseconds + "ms.");
                return true;
            }
            catch (Exception e)
            {
                MyLog.DataBaseException("执行Sql异常", e);
                return false;
            }
        }
        /// <summary>
        /// 根据时间范围获取批次总数
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static int GetBatchCount(int startTime, int endTime)
        {
            int totalCount = 0;
            try
            {
                string strSql =
                    string.Format("select count(*) as count from ky_agent_batch where kdate>{0} and kdate<{1}", startTime, endTime);
                using (var conn = DbHelperMySQL.OpenSphinxConnection())
                {
                    totalCount = conn.GetValue<int>(strSql);

                }
            }
            catch (Exception e)
            {
                MyLog.DataBaseException("获取批次总数异常", e);
            }
            return totalCount;
        }


        /// <summary>
        /// 根据时间范围获取批次
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static List<ky_agent_batch> GetBatches(int startTime, int endTime, int count)
        {
            List<ky_agent_batch> batches = new List<ky_agent_batch>();
            if (count > 0)
            {
                try
                {
                    string strSql =
                        string.Format("select * from ky_agent_batch where kdate>{0} and kdate<{1} limit 0,{2} option max_matches={2}", startTime, endTime, count);
                    using (var conn = DbHelperMySQL.OpenSphinxConnection())
                    {
                        batches = conn.Query<ky_agent_batch>(strSql).ToList();
                    }
                }
                catch (Exception e)
                {
                    MyLog.DataBaseException("获取批次异常", e);
                }
            }
            return batches;
        }


        /// <summary>
        /// 根据批次id获取冠字号码信息 最大取10万条冠字号码数据（一个业务可能超过10万？）
        /// </summary>
        /// <returns></returns>
        public static List<ky_agent_sign> GetSignByBatchId(long batchid)
        {
            List<ky_agent_sign> signs = null;
            try
            {
                string strSql =
                    string.Format("select * from ky_agent_sign where kbatchid={0} limit 0,100000 option max_matches=100000", batchid);
                using (var conn = DbHelperMySQL.OpenSphinxConnection())
                {
                    signs = conn.Query<ky_agent_sign>(strSql).ToList();

                }
            }
            catch (Exception e)
            {
                MyLog.DataBaseException("获取冠字号码信息异常", e);
            }
            return signs;
        }

        #endregion


        #region device数据库操作


        /// <summary>
        /// 根据绑定的IP地址 获取ky_node 表
        /// </summary>
        /// <param name="bindIpAddress"></param>
        /// <returns></returns>
        public static List<ky_node> GetNodeWithBindIp(string bindIpAddress)
        {
            List<ky_node> nodes;
            using (var conn = DbHelperMySQL.OpenDeviceConnection())
            {
                nodes = conn.Query<ky_node>(cc => cc.kBindIpAddress == bindIpAddress).ToList();
            }
            return nodes;
        }
        /// <summary>
        /// 根据ID获取ky_node 表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<ky_node> GetNodeWithIds(List<int> id)
        {
            List<ky_node> nodes;
            using (var conn = DbHelperMySQL.OpenDeviceConnection())
            {
                nodes = conn.Query<ky_node>(q => id.Contains(q.kId)).ToList();
            }
            return nodes;
        }


        /// <summary>
        /// 更新ky_node表中的kBindIpAddress项
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="bindIpAdress"></param>
        /// <returns></returns>
        public static bool UpdateNodeTable(List<int> ids, string bindIpAdress)
        {
            try
            {
                using (var conn = DbHelperMySQL.OpenDeviceConnection())
                {
                    var selectNodes = conn.Query<ky_node>(q => ids.Contains(q.kId)).ToList();
                    foreach (ky_node selectNode in selectNodes)
                    {
                        selectNode.kBindIpAddress = bindIpAdress;
                        conn.Update<ky_node>(selectNode);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                MyLog.DataBaseException("更新绑定IP地址异常", e);
                return false;
            }
        }

        /// <summary>
        /// 通过网点编号获取网点ID
        /// </summary>
        /// <param name="nodeNumber"></param>
        /// <returns></returns>
        public static int GetNodeIdByNodeNumber(string nodeNumber)
        {
            ky_node node;
            using (var conn = DbHelperMySQL.OpenDeviceConnection())
            {
                node = conn.Get<ky_node>(q => q.kNodeNumber == nodeNumber);
            }
            if (node != null)
                return node.kId;
            else
                return 0;
        }
        /// <summary>
        /// 通过网点id获取网点
        /// </summary>
        /// <param name="nodeNumber"></param>
        /// <returns></returns>
        public static ky_node GetNodeByNodeId(int id)
        {
            ky_node node;
            using (var conn = DbHelperMySQL.OpenDeviceConnection())
            {
                node = conn.Get<ky_node>(q => q.kId == id);
            }
            if (node != null)
                return node;
            else
                return null;
        }

        /// <summary>
        /// 通过银行编号查询银行Id
        /// </summary>
        /// <param name="branchNumber"></param>
        /// <returns></returns>
        public static int GetBranchId(string branchNumber)
        {
            ky_branch branch;
            using (var conn = DbHelperMySQL.OpenDeviceConnection())
            {
                branch = conn.Get<ky_branch>(q => q.kBranchNumber == branchNumber);
            }
            if (branch != null)
                return branch.kId;
            else
                return 0;
        }
        /// <summary>
        /// 根据网点Id获取银行Id
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public static int GetBranchIdWithNodeId(int nodeId)
        {
            ky_node node;
            using (var conn = DbHelperMySQL.OpenDeviceConnection())
            {
                node = conn.Get<ky_node>(q => q.kId == nodeId);
            }
            if (node != null)
                return node.kBranchId;
            else
                return 0;
        }
        /// <summary>
        /// 根据网点Id获取银行 
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public static ky_branch GetBranchWithNodeId(int nodeId)
        {
            ky_branch branch;
            using (var conn = DbHelperMySQL.OpenDeviceConnection())
            {
                ky_node node = conn.Get<ky_node>(q => q.kId == nodeId);
                branch = conn.Get<ky_branch>(q => q.kId == node.kBranchId);
            }
            if (branch != null)
                return branch;
            else
                return null;
        }

        /// <summary>
        /// 通过厂家编号获取厂家ID
        /// </summary>
        /// <param name="factoryNumber"></param>
        /// <returns></returns>
        public static int GetFactoryId(string factoryNumber)
        {
            ky_factory factory;
            using (var conn = DbHelperMySQL.OpenDeviceConnection())
            {
                factory = conn.Get<ky_factory>(q => q.kFactoryCode == factoryNumber);

            }
            if (factory != null)
                return factory.kId;
            else
                return 0;
        }

        /// <summary>
        /// 添加厂家信息
        /// </summary>
        /// <param name="factoryName"></param>
        /// <param name="factoryNumber"></param>
        /// <returns></returns>
        public static int InsertFactory(string factoryName, string factoryNumber)
        {
            ky_factory factory = new ky_factory { kFactoryName = factoryName, kFactoryCode = factoryNumber };
            try
            {
                using (var conn = DbHelperMySQL.OpenDeviceConnection())
                {
                    conn.Insert(factory);
                }
                return factory.kId;
            }
            catch (Exception e)
            {
                MyLog.DataBaseException("添加厂家信息异常", e);
                return 0;
            }
        }

        /// <summary>
        /// 根据网点编号获取机具
        /// </summary>
        /// <param name="nodeIds"></param>
        /// <returns></returns>
        public static List<ky_machine> GetMachineWithNodeIds(int[] nodeIds)
        {
            try
            {
                List<ky_machine> machines;
                using (var conn = DbHelperMySQL.OpenDeviceConnection())
                {
                    machines = conn.Query<ky_machine>(q => nodeIds.Contains(q.kNodeId)).ToList();
                }
                return machines;
            }
            catch (Exception e)
            {
                MyLog.ConnectionException("获取机具异常", e);
                return null;
            }

        }

        /// <summary>
        /// 通过机具编号获取机具ID
        /// </summary>
        /// <param name="machineNumber"></param>
        /// <returns></returns>
        public static int GetMachineIdByMachineNumber(string machineNumber)
        {
            ky_machine machine;
            using (var conn = DbHelperMySQL.OpenDeviceConnection())
            {
                machine = conn.Get<ky_machine>(q => q.kMachineNumber == machineNumber);
            }
            if (machine != null)
                return machine.kId;
            else
                return 0;
        }
        /// <summary>
        /// 通过机具Id获取机具
        /// </summary>
        /// <param name="machineNumber"></param>
        /// <returns></returns>
        public static ky_machine GetMachineByMachineId(int id)
        {
            ky_machine machine;
            using (var conn = DbHelperMySQL.OpenDeviceConnection())
            {
                machine = conn.Get<ky_machine>(q => q.kId == id);
            }
            if (machine != null)
                return machine;
            else
                return null;
        }
        /// <summary>
        /// 通过用户编号获取用户ID
        /// </summary>
        /// <param name="userNumber"></param>
        /// <returns></returns>
        public static int GetUserId(string userNumber)
        {
            ky_user user;
            using (var conn = DbHelperMySQL.OpenDeviceConnection())
            {
                user = conn.Get<ky_user>(q => q.kUserNumber == userNumber);
            }
            if (user != null)
                return user.kId;
            else
                return 0;
        }

        /// <summary>
        /// 通过图像服务器IP获取图像服务器ID
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static int GetPictureServerId(string ip)
        {
            ky_imgserver imgserver;
            using (var conn = DbHelperMySQL.OpenDeviceConnection())
            {
                imgserver = conn.Get<ky_imgserver>(q => q.kIpAddress == ip);
            }
            if (imgserver != null)
                return imgserver.kId;
            else
                return 0;
        }


        /// <summary>
        /// 根据用户编号查询用户，返回用户资料；
        /// </summary>
        /// <param name="UserNumber"></param>
        /// <returns></returns>
        public static ky_user GetUser(string UserNumber)
        {
            ky_user user;
            using (var conn = DbHelperMySQL.OpenDeviceConnection())
            {
                user = conn.Get<ky_user>(q => q.kUserNumber == UserNumber);
            }
            return user;
        }

        /// <summary>
        /// 通过IP获取机具信息
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static ky_machine GetMachineWithIp(string ip)
        {
            ky_machine machine;
            using (var conn = DbHelperMySQL.OpenDeviceConnection())
            {
                machine = conn.Get<ky_machine>(q => q.kIpAddress == ip);
            }
            return machine;
        }

        /// <summary>
        /// 通过网点ID查询该网点下所有机器数据。
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public static List<ky_machine> GetMachineWithNodeId(int nodeId)
        {
            List<ky_machine> machines;
            using (var conn = DbHelperMySQL.OpenDeviceConnection())
            {
                machines = conn.Query<ky_machine>(q => q.kNodeId == nodeId).ToList();
            }
            return machines;
        }

        /// <summary>
        /// 更新机器的机具编号和机型
        /// </summary>
        /// <param name="id"></param>
        /// <param name="machineNumber"></param>
        /// <param name="machineModel"> </param>
        /// <returns></returns>
        public static bool UpdateMachine(int id, string machineNumber, string machineModel)
        {
            try
            {
                using (var conn = DbHelperMySQL.OpenDeviceConnection())
                {
                    conn.Update<ky_machine>(new { kMachineNumber = machineNumber, kMachineModel = machineModel }, p => p.kId == id);
                }
                return true;
            }
            catch (Exception e)
            {
                MyLog.DataBaseException("更新机器机具编号和机型异常", e);
                return false;
            }
        }
        /// <summary>
        /// 更新机器的上传时间
        /// </summary>
        /// <param name="id"></param>
        /// <param name="machineNumber"></param>
        /// <param name="machineModel"> </param>
        /// <returns></returns>
        public static bool UpdateMachineTime(int id, DateTime time)
        {
            try
            {
                using (var conn = DbHelperMySQL.OpenDeviceConnection())
                {
                    conn.Update<ky_machine>(new { kUpdateTime = time }, p => p.kId == id);
                }
                return true;
            }
            catch (Exception e)
            {
                MyLog.DataBaseException("更新机器上传时间异常", e);
                return false;
            }
        }

        /// <summary>
        /// 获取机器的 ID，IP，最后上传时间、状态。用于设备监控
        /// </summary>
        /// <returns></returns>
        public static List<ky_machine> GetMachineStatus(List<int> nodeIds)
        {
            List<ky_machine> machines;
            using (var conn = DbHelperMySQL.OpenDeviceConnection())
            {
                machines = conn.Query<ky_machine>(q => nodeIds.Contains(q.kNodeId)).ToList();

            }
            return machines;
        }

        /// <summary>
        /// 根据atm编号，返回atmId
        /// </summary>
        /// <param name="UserNumber"></param>
        /// <returns></returns>
        public static int GetATMId(string ATMNumber, int NodeId)
        {
            int atmId = 0;
            using (var conn = DbHelperMySQL.OpenDeviceConnection())
            {
                var atm = conn.Get<ky_atm>(q => q.kATMNumber == ATMNumber && q.kNodeId == NodeId);
                if (atm != null)
                    atmId = atm.kId;
            }
            return atmId;
        }
        /// <summary>
        /// 通过网点Id获取该网点下的所有ATM数据
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public static List<ky_atm> GetAtmWithNodeId(List<int> nodeIds)
        {
            List<ky_atm> atms;
            using (var conn = DbHelperMySQL.OpenDeviceConnection())
            {
                atms = conn.Query<ky_atm>(q => nodeIds.Contains(q.kNodeId)).ToList();
            }
            return atms;
        }
        /// <summary>
        /// 添加atm信息 返回id
        /// </summary>
        /// <param name="factoryName"></param>
        /// <param name="factoryNumber"></param>
        /// <returns></returns>
        public static int InsertATM(ky_atm ATM)
        {
            try
            {
                using (var conn = DbHelperMySQL.OpenDeviceConnection())
                {
                    conn.Insert(ATM);
                }
                return ATM.kId;
            }
            catch (Exception e)
            {
                MyLog.DataBaseException("添加ATM信息异常", e);
                return 0;
            }
        }
        /// <summary>
        /// 根据cashbox编号，返回cashboxId
        /// </summary>
        /// <param name="UserNumber"></param>
        /// <returns></returns>
        public static int GetCashBoxId(string CashBoxNumber, int NodeId)
        {
            int cashboxId = 0;
            using (var conn = DbHelperMySQL.OpenDeviceConnection())
            {
                var cashbox = conn.Get<ky_cashbox>(q => q.kCashBoxNumber == CashBoxNumber && q.kNodeId == NodeId);
                if (cashbox != null)
                    cashboxId = cashbox.kId;
            }
            return cashboxId;
        }

        /// <summary>
        /// 通过网点Id获取该网点下的所有CashBox数据
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public static List<ky_cashbox> GetCashBoxWithNodeId(List<int> nodeIds)
        {
            List<ky_cashbox> cashboxs;
            using (var conn = DbHelperMySQL.OpenDeviceConnection())
            {
                cashboxs = conn.Query<ky_cashbox>(q => nodeIds.Contains(q.kNodeId)).ToList();
            }
            return cashboxs;
        }
        /// <summary>
        /// 添加cashbox信息
        /// </summary>
        /// <param name="factoryName"></param>
        /// <param name="factoryNumber"></param>
        /// <returns></returns>
        public static int InsertCashBox(ky_cashbox CashBox)
        {
            try
            {
                using (var conn = DbHelperMySQL.OpenDeviceConnection())
                {
                    conn.Insert(CashBox);
                }
                return CashBox.kId;
            }
            catch (Exception e)
            {
                MyLog.DataBaseException("添加ATM信息异常", e);
                return 0;
            }
        }
        /// <summary>
        /// 获取全部钞箱信息
        /// </summary>
        /// <returns></returns>
        public static List<ky_cashbox> GetAllCashBox()
        {
            List<ky_cashbox> cashboxs;
            using (var conn = DbHelperMySQL.OpenDeviceConnection())
            {
                cashboxs = conn.Query<ky_cashbox>().ToList();
            }
            return cashboxs;
        }

        /// <summary>
        /// 获取全部网点信息
        /// </summary>
        /// <returns></returns>
        public static List<ky_node> GetAllNode()
        {
            List<ky_node> nodes;
            using (var conn = DbHelperMySQL.OpenDeviceConnection())
            {
                nodes = conn.Query<ky_node>().ToList();
            }
            return nodes;
        }

        /// <summary>
        /// 获取全部厂家信息
        /// </summary>
        /// <returns></returns>
        public static List<ky_factory> GetAllFactory()
        {
            List<ky_factory> factorys;
            using (var conn = DbHelperMySQL.OpenDeviceConnection())
            {
                factorys = conn.Query<ky_factory>().ToList();
            }
            return factorys;
        }

        /// <summary>
        /// 获取表ky_import_machine内的全部信息
        /// </summary>
        /// <returns></returns>
        public static List<ky_import_machine> GetAllImportMachine()
        {
            List<ky_import_machine> importmachines;
            using (var conn = DbHelperMySQL.OpenDeviceConnection())
            {
                importmachines = conn.Query<ky_import_machine>().ToList();
            }
            return importmachines;
        }
        /// <summary>
        /// 根据id返回Import_machine
        /// </summary>
        /// <returns></returns>
        public static ky_import_machine GetmportMachineByImportMachineId(int id)
        {
            ky_import_machine importmachine;
            using (var conn = DbHelperMySQL.OpenDeviceConnection())
            {
                importmachine = conn.Get<ky_import_machine>(q => q.kId == id);
            }
            if (importmachine != null)
                return importmachine;
            else
                return null;
        }
        /// <summary>
        /// 根据机具编号获取(导入文件的)机具id
        /// </summary>
        /// <returns></returns>
        public static int GetMachineIdFromImportMachine(string machineNumber)
        {
            ky_import_machine machine;
            using (var conn = DbHelperMySQL.OpenDeviceConnection())
            {
                machine = conn.Get<ky_import_machine>(p => p.kMachineNumber == machineNumber);
            }
            if (machine != null)
                return machine.kId;
            else
                return 0;
        }

        /// <summary>
        /// 往表ky_import_machine中插入数据，并返回id
        /// </summary>
        public static int InsertMachineToImportMachine(ky_import_machine import_machine)
        {
            int id = 0;
            try
            {
                using (var conn = DbHelperMySQL.OpenDeviceConnection())
                {
                    id = conn.Insert<ky_import_machine>(import_machine).InsertedId<int>();
                }
            }
            catch (Exception e)
            {
                MyLog.DataBaseException("导入机具异常", e);
            }
            return id;
        }

        /// <summary>
        /// 插入数据到冠字号码文件上传表中
        /// </summary>
        /// <param name="bactchId"></param>
        /// <param name="fileName"></param>
        /// <param name="importTime"></param>
        /// <param name="business"></param>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public static bool InsertImportFile(long bactchId, string fileName, DateTime importTime, string business, int nodeId)
        {
            try
            {
                using (var conn = DbHelperMySQL.OpenDeviceConnection())
                {
                    ky_import_file file = new ky_import_file
                    {
                        kBatchId = bactchId,
                        kFileName = fileName,
                        kImportTime = importTime,
                        kType = business,
                        kNodeId = nodeId
                    };
                    conn.Insert(file);
                }
                return true;
            }
            catch (Exception e)
            {
                MyLog.DataBaseException("上传文件异常", e);
                return false;
            }
        }

        //获取所有的图片服务器
        public static List<ky_imgserver> GetAllImageServer()
        {
            List<ky_imgserver> imgservers;
            using (var conn = DbHelperMySQL.OpenDeviceConnection())
            {
                imgservers = conn.Query<ky_imgserver>().ToList();
            }
            return imgservers;
        }

        /// <summary>
        /// 插入数据到ky_gzh_bundle表中
        /// </summary>
        /// <param name="bundleNumber"></param>
        /// <param name="batchId"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool InsertGzhBundle(string bundleNumber, long batchId, string fileName)
        {
            try
            {
                using (var conn = DbHelperMySQL.OpenDeviceConnection())
                {
                    ky_gzh_bundle gzh_bundle = new ky_gzh_bundle
                    {
                        kBatchId = batchId,
                        kFileName = fileName,
                        kBundleNumber = bundleNumber
                    };
                    conn.Insert(gzh_bundle);
                }
                return true;
            }
            catch (Exception e)
            {
                MyLog.DataBaseException("上传GZH文件异常", e);
                return false;

            }
        }

        /// <summary>
        /// 获取GzhBundle的Id
        /// </summary>
        /// <param name="bundleNumber"></param>
        /// <param name="batchId"></param>
        /// <returns></returns>
        public static int GetGzhBundleId(string bundleNumber, long batchId)
        {
            ky_gzh_bundle gzh_bundle;
            using (var conn = DbHelperMySQL.OpenDeviceConnection())
            {
                gzh_bundle = conn.Get<ky_gzh_bundle>(p => p.kBatchId == batchId && p.kBundleNumber == bundleNumber);
            }
            if (gzh_bundle != null)
                return gzh_bundle.kId;
            else
                return 0;
        }

        /// <summary>
        /// 保存GZH package信息，并返回id
        /// </summary>
        /// <param name="gzhLayer2"></param>
        /// <returns></returns>
        public static int InsertGzhPackage(ky_gzh_package gzh)
        {
            int id = 0;
            try
            {
                using (var conn = DbHelperMySQL.OpenDeviceConnection())
                {

                    id = conn.Insert(gzh).InsertedId<int>();
                }
            }
            catch (Exception e)
            {
                MyLog.DataBaseException("保存GZH的包信息异常", e);
            }
            return id;
        }


        /// <summary>
        /// 保存packageBundle信息
        /// </summary>
        /// <param name="bundleId"></param>
        /// <param name="packageId"></param>
        /// <returns></returns>
        public static bool SavePackageBundle(int bundleId, int packageId)
        {
            if (bundleId == 0 || packageId == 0)
                return false;
            try
            {
                using (var conn = DbHelperMySQL.OpenDeviceConnection())
                {
                    ky_package_bundle pack_bundle = new ky_package_bundle { kBundleId = bundleId, kPackageId = packageId };
                    conn.Insert(pack_bundle);
                }
                return true;
            }
            catch (Exception e)
            {
                MyLog.DataBaseException("保存GZH捆钞异常", e);
                return false;
            }
        }

        #endregion


        #region 测试服务器连接

        /// <summary>
        /// 测试数据服务器的连接
        /// </summary>
        /// <returns></returns>
        public static bool TestConnectServer()
        {
            try
            {
                using (var conn = DbHelperMySQL.OpenSphinxConnection())
                {
                    conn.Execute("select * from ky_agent_sign limit 0,1");
                }
                return true;
            }
            catch (Exception e)
            {
                MyLog.DataBaseException("测试数据服务器连接失败", e);
                return false;
            }
        }

        /// <summary>
        /// 测试设备服务器的连接
        /// </summary>
        /// <returns></returns>
        public static bool TestConnectDevice()
        {
            try
            {
                using (var conn = DbHelperMySQL.OpenDeviceConnection())
                {
                    if (conn.TableExists<ky_user>())
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception e)
            {
                MyLog.DataBaseException("测试设备数据库连接失败", e);
                return false;
            }
        }
        /// <summary>
        /// 测试推送服务器的连接
        /// </summary>
        /// <returns></returns>
        public static bool TestConnectPush(string Ip, int Port)
        {
            System.Net.IPAddress IpAddress = System.Net.IPAddress.Parse(Ip);
            System.Net.IPEndPoint IpEndPoint = new System.Net.IPEndPoint(IpAddress, Port);
            try
            {
                System.Net.Sockets.TcpClient tcpClient = new System.Net.Sockets.TcpClient();
                tcpClient.Connect(IpEndPoint);
                return true;
            }
            catch (Exception e)
            {
                MyLog.DataBaseException("测试推送服务器连接失败", e);
                return false;
            }
        }
        /// <summary>
        /// 测试图像服务器的连接
        /// </summary>
        /// <returns></returns>
        public static bool TestConnectImage()
        {
            try
            {
                using (var conn = DbHelperMySQL.OpenImageConnection())
                {
                    if (conn.TableExists<ky_picture>())
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception e)
            {
                MyLog.DataBaseException("测试图像数据库连接失败", e);
                return false;
            }
        }

        #endregion

        //MD5加密
        public static string Md5(String str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = Encoding.Default.GetBytes(str);
            byte[] result = md5.ComputeHash(data);
            String ret = "";
            for (int i = 0; i < result.Length; i++)
                ret += result[i].ToString("x").PadLeft(2, '0');
            return ret;
        }
    }
}
