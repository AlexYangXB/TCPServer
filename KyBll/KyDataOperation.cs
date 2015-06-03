using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Data.Entity;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using KyModel.Models;
using System.Data.Objects;
using KyBll;
using KyModel;
using KyBll.DBUtility;
namespace KyBll
{
    public class KyDataOperation
    {
        //public class ky_batch_sphinx
        //{
        //    public Int64 id;//批次ID
        //    public string type;//业务类型，用英文表示1、号码记录(signrecord) 2、柜面取款(counterdraw) 3、柜面存款(counterdeposit) 4、ATM配钞(atmmatch) 5、ATM清钞(atmclear)
        //    public int date;//时间戳
        //    public UInt32 node;//网点ID
        //    public UInt32 factory;//厂家ID
        //    public UInt32[] machine;//机具ID
        //    public UInt32 totalnumber;//总张数
        //    public UInt32 totalvalue;//总金额
        //    public UInt32 kuser;//柜员ID
        //    public string json;
        //}


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
                using (var conn = DbHelperMySQL.OpenSphinxConnection())
                {
                    string strSql = string.Format(
                        "INSERT INTO ky_batch(id,ktype,kdate,knode,kfactory,kmachine,ktotalnumber,ktotalvalue,kuser,kimgserver,hjson) values({0},'{1}',{2},{3},{4},({5}),{6},{7},{8},{9},'{10}')",
                        batch.id, batch.ktype, batch.kdate, batch.knode, batch.kfactory, batch.kmachine, batch.ktotalnumber, batch.ktotalvalue,
                        batch.kuser, batch.kimgserver, batch.hjson);
                    conn.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, strSql);
                }
                return true;
            }
            catch (Exception e)
            {
                KyBll.Log.DataBaseException(e, "保存批次异常");
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
                if (signs.Count > 0)
                {
                    List<ky_picture> pictures = new List<ky_picture>();
                    List<ky_sign> only_signs = new List<ky_sign>();
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
                        ky_picture picture = new ky_picture { kId = id, kImageSNo = sign.imageData, kInsertTime = now, kImageType = sign.ImageType };
                        pictures.Add(picture);

                        strSql += string.Format("({0},{1},'{2}',{3},{4},{5},{6},{7},{8},{9})", id, time,
                                                   sign.Sign, batchId, sign.Value, sign.Version,
                                                   sign.Currency, sign.True, startIndex + count, 0);
                        count++;
                    }
                    using (var conn = DbHelperMySQL.OpenSphinxConnection())
                    {
                        conn.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, strSql);
                    }
                    using (var conn = DbHelperMySQL.OpenImageConnection())
                    {
                        conn.ky_picture.AddRange(pictures);
                        conn.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Log.DataBaseException(e, "保存冠字号码或图像异常");
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
                    string.Format("select count(*) as count from ky_agent_batch where kdate>{0} and kdate<{1}", startTime,endTime);
                using (var conn = DbHelperMySQL.OpenSphinxConnection())
                {
                    totalCount= conn.Database.SqlQuery<ky_count>(strSql).FirstOrDefault().count;
                   
                }
            }
            catch (Exception e)
            {
                Log.DataBaseException(e, "获取批次总数异常");
            }
            return totalCount;
        }


        /// <summary>
        /// 根据时间范围获取批次
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static List<ky_agent_batch> GetBatches(int startTime, int endTime,int count)
        {
            List<ky_agent_batch> batches = new List<ky_agent_batch>();
            try
            {
                string strSql =
                    string.Format("select * from ky_agent_batch where kdate>{0} and kdate<{1} limit 0,{2} option max_matches={2}", startTime, endTime,count); 
                using (var conn = DbHelperMySQL.OpenSphinxConnection())
                {
                    batches = conn.Database.SqlQuery<ky_agent_batch>(strSql).ToList();                    
                }
            }
            catch (Exception e)
            {
                Log.DataBaseException(e, "获取批次异常");
            }
            return batches;
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
                nodes = conn.ky_node.Where(cc => cc.kBindIpAddress == bindIpAddress).ToList();
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
                nodes = conn.ky_node.Where(q => id.Contains(q.kId)).ToList();
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
                    var selectNodes = conn.ky_node.Where(q => ids.Contains(q.kId)).ToList();
                    foreach (ky_node selectNode in selectNodes)
                    {
                        selectNode.kBindIpAddress = bindIpAdress;

                    }
                    if (selectNodes != null)

                        conn.SaveChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                Log.DataBaseException(e, "更新绑定IP地址异常");
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
                node = conn.ky_node.Where(q => q.kNodeNumber == nodeNumber).FirstOrDefault();
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
                node = conn.ky_node.Where(q => q.kId == id).FirstOrDefault();
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
                branch = conn.ky_branch.Where(q => q.kBranchNumber == branchNumber).FirstOrDefault();
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
                node = conn.ky_node.Where(q => q.kId == nodeId).FirstOrDefault();
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
                branch = conn.ky_branch.Where(q => q.kId == nodeId).FirstOrDefault();
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
                factory = conn.ky_factory.Where(q => q.kFactoryCode == factoryNumber).FirstOrDefault();

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
        public static bool InsertFactory(string factoryName, string factoryNumber)
        {
            try
            {
                using (var conn = DbHelperMySQL.OpenDeviceConnection())
                {
                    var factory = new ky_factory { kFactoryName = factoryName, kFactoryCode = factoryNumber };
                    conn.ky_factory.Add(factory);
                    conn.SaveChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                Log.DataBaseException(e, "添加厂家信息异常");
                return false;
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
                    machines = conn.ky_machine.Where(q => nodeIds.Contains(q.kNodeId)).ToList();
                }
                return machines;
            }
            catch (Exception e)
            {
                Log.ConnectionException(e, "获取机具异常");
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
                machine = conn.ky_machine.Where(q => q.kMachineNumber == machineNumber).FirstOrDefault();
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
                machine = conn.ky_machine.Where(q => q.kId == id).FirstOrDefault();
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
                user = conn.ky_user.Where(q => q.kUserNumber == userNumber).FirstOrDefault();
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
                imgserver = conn.ky_imgserver.Where(q => q.kIpAddress == ip).FirstOrDefault();
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
                user = conn.ky_user.Where(q => q.kUserNumber == UserNumber).FirstOrDefault();
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
                machine = conn.ky_machine.Where(q => q.kIpAddress == ip).FirstOrDefault();
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
                machines = conn.ky_machine.Where(q => q.kNodeId == nodeId).ToList();
            }
            return machines;
        }

        /// <summary>
        /// 更新机器的最后上传时间和机具编号
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
                    var machine = new ky_machine { kId = id, kMachineNumber = machineNumber, kMachineModel = machineModel, kUpdateTime = DateTime.Now };
                    conn.Entry(machine).State = System.Data.Entity.EntityState.Modified;
                    conn.SaveChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                Log.DataBaseException(e, "更新机器时间和机具编号异常");
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
                var entity = conn.ky_machine;
                var tt = (from e in entity.Where(q => nodeIds.Contains(q.kNodeId))
                          select new { kIpAddress = e.kIpAddress, kUpdateTime = e.kUpdateTime, kStatus = e.kStatus, kId = e.kId }).ToList();
                machines = tt.ConvertAll<ky_machine>(item => new ky_machine()
                {
                    kIpAddress = item.kIpAddress,
                    kUpdateTime = item.kUpdateTime,
                    kStatus = item.kStatus,
                    kId = item.kId
                });

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
                var atm = conn.ky_atm.Where(q => q.kATMNumber == ATMNumber)
                    .Where(q => q.kNodeId == NodeId).FirstOrDefault();
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
                atms = conn.ky_atm.Where(q => nodeIds.Contains(q.kNodeId)).ToList();
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
                    conn.ky_atm.Add(ATM);
                    conn.SaveChanges();
                }
                return ATM.kId;
            }
            catch (Exception e)
            {
                Log.DataBaseException(e, "添加ATM信息异常");
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
                var cashbox = conn.ky_cashbox.Where(q => q.kCashBoxNumber == CashBoxNumber)
                    .Where(q => q.kNodeId == NodeId).FirstOrDefault();
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
                cashboxs = conn.ky_cashbox.Where(q => nodeIds.Contains(q.kNodeId)).ToList();
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
                    conn.ky_cashbox.Add(CashBox);
                    conn.SaveChanges();
                }
                return CashBox.kId;
            }
            catch (Exception e)
            {
                Log.DataBaseException(e, "添加ATM信息异常");
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
                cashboxs = conn.ky_cashbox.ToList();
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
                nodes = conn.ky_node.ToList();
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
                factorys = conn.ky_factory.ToList();
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
                importmachines = conn.ky_import_machine.ToList();
            }
            return importmachines;
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
                machine = conn.ky_import_machine.Where(p => p.kMachineNumber == machineNumber).FirstOrDefault();
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
                    conn.ky_import_machine.Add(import_machine);
                    conn.SaveChanges();
                    id = Convert.ToInt32(import_machine.kId);
                }
            }
            catch (Exception e)
            {
                Log.DataBaseException(e, "导入机具异常");
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
                    conn.ky_import_file.Add(file);
                    conn.SaveChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                Log.DataBaseException(e, "上传文件异常");
                return false;
            }
        }

        //获取所有的图片服务器
        public static List<ky_imgserver> GetAllImageServer()
        {
            List<ky_imgserver> imgservers;
            using (var conn = DbHelperMySQL.OpenDeviceConnection())
            {
                imgservers = conn.ky_imgserver.ToList();
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
                    conn.ky_gzh_bundle.Add(gzh_bundle);
                    conn.SaveChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                Log.DataBaseException(e, "上传GZH文件异常");
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
                gzh_bundle = conn.ky_gzh_bundle.Where(p => p.kBatchId == batchId && p.kBundleNumber == bundleNumber).FirstOrDefault();
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
                    conn.ky_gzh_package.Add(gzh);
                    conn.GetValidationErrors();
                    conn.SaveChanges();
                    id = Convert.ToInt32(gzh.kId);
                }
            }
            catch (Exception e)
            {
                Log.DataBaseException(e, "保存GZH的包信息异常");
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
                    conn.ky_package_bundle.Add(pack_bundle);
                    conn.SaveChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                Log.DataBaseException(e, "保存GZH捆钞异常");
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
                    conn.ky_sign.FirstOrDefault();
                }
                return true;
            }
            catch (Exception e)
            {
                Log.DataBaseException(e, "测试数据服务器连接失败");
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
                    conn.ky_user.FirstOrDefault();
                }
                return true;
            }
            catch (Exception e)
            {
                Log.DataBaseException(e, "测试设备数据库连接失败");
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
                Log.DataBaseException(e, "测试推送服务器连接失败");
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
                    conn.ky_picture.FirstOrDefault();
                }
                return true;
            }
            catch (Exception e)
            {
                Log.DataBaseException(e, "测试图像数据库连接失败");
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
