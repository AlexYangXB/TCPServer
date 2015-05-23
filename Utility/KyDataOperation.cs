using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using MySql.Data.MySqlClient;
using Utility.DBUtility;
using ServiceStack.OrmLite.MySql;
using ServiceStack.OrmLite;
using KyModel;
using System.Linq;
using System.Data.Entity;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
namespace Utility
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
            batch.kmachine = "(1,23)";
            try
            {
                using (IDbConnection conn = DbHelperMySQL.OpenSphinxConnection())
                {
                    try
                    {
                      conn.Insert(batch);
                    }
                    catch (Exception e) { 
                        string sdd = e.ToString();
                    }
                }
                return true;
            } 
            catch (Exception e)
            {
                Log.DataBaseException(e, "保存冠字号码异常");
                return false;
            }
        }


        //public class ky_sign_sphinx
        //{
        //    public Int64 id;
        //    public int date;//时间戳
        //    public string sign;//冠字号码
        //    public Int64 batchId;//批次Id
        //    public UInt32 value;//面额
        //    public UInt32 version;//版本
        //    public UInt32 currency;//币种ID
        //    public UInt32 status;//钞票状态
        //}
        /// <summary>
        /// 往sphinx中插入冠字号码数据，往图片数据库中插入图片数据
        /// </summary>
        /// <param name="batchId">批次号</param>
        /// <param name="startIndex">批内起始序号 </param>
        /// <param name="signs">冠字号码数据</param>
        /// <returns></returns>
        public static bool InsertSign(Int64 batchId, int startIndex, List<KyData.DataBase.KYDataLayer1.SignTypeL2> signs)
        {
            if (signs.Count > 0)
            {
                string strSQL =
                    "INSERT INTO ky_sign(id,kdate,ksign,kbatchid,kvalue,kversion,kcurrency,kstatus,knumber,hjson) values";
                //用于图像数据库
                string strSqlImage = "INSERT INTO ky_picture(kId,kInsertTime,kImageType,kImageSNo) values";
                List<string> lstSQL = new List<string>();
                List<MySqlParameter[]> lstSQLpara = new List<MySqlParameter[]>();
                MySqlParameter[] para = new MySqlParameter[signs.Count];

                for (int i = 0; i < signs.Count; i++)
                {
                    if (i != 0)
                    {
                        strSQL += ",";
                        strSqlImage += ",";
                    }
                    Int64 id = KyData.KyDataLayer2.GuidToLongID();
                    int time = KyData.DateTimeAndTimeStamp.ConvertDateTimeInt(signs[i].Date);
                    string strSql = string.Format("({0},{1},'{2}',{3},{4},{5},{6},{7},{8},{9})", id, time,
                                                  signs[i].Sign, batchId, signs[i].Value, signs[i].Version,
                                                  signs[i].Currency, signs[i].True, startIndex + i, 0);
                    strSQL += strSql;

                    //图片数据库
                    para[i] = new MySqlParameter();
                    para[i].MySqlDbType = MySqlDbType.MediumBlob;
                    para[i].ParameterName = "?imgbindata" + i.ToString();
                    para[i].Size = signs[i].imageData.Length;
                    para[i].Value = signs[i].imageData;
                    string strImageSql = string.Format("({0},'{1}','{2}',{3})", id,
                                                       DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), signs[i].ImageType,
                                                       para[i].ParameterName);
                    strSqlImage += strImageSql;
                }
                //sphinx sign
                DBUtility.DbHelperMySQL.SetCurrentDb(DbHelperMySQL.DataBaseServer.Sphinx);
                int result = DBUtility.DbHelperMySQL.ExecuteSql(strSQL);

                //imageDb
                lstSQL.Add(strSqlImage);
                lstSQLpara.Add(para);
                DBUtility.DbHelperMySQL.SetCurrentDb(DbHelperMySQL.DataBaseServer.Image);
                //if (DbHelperMySQL.ExecuteSqlTran(lstSQL, lstSQLpara) > 0)
                DbHelperMySQL.ExecuteSqlTran(lstSQL, lstSQLpara);
                if (result > 0)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }

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
            using (IDbConnection conn = DbHelperMySQL.OpenDeviceConnection())
            {
                nodes = conn.Select<ky_node>(q=>q.kBindIpAddress==bindIpAddress);
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
            using (IDbConnection conn = DbHelperMySQL.OpenDeviceConnection())
            {
                nodes = conn.Select<ky_node>(q => Sql.In(q.kId,id));
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
                using (IDbConnection conn = DbHelperMySQL.OpenDeviceConnection())
                {
                    var selectNodes = conn.SelectByIds<ky_node>(ids);
                    foreach (ky_node selectNode in selectNodes)
                    {
                        selectNode.kBindIpAddress = bindIpAdress;

                    }
                    if(selectNodes!=null)
                        conn.UpdateAll(selectNodes);

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
        public static int GetNodeId(string nodeNumber)
        {
            ky_node node;
            using (IDbConnection conn = DbHelperMySQL.OpenDeviceConnection())
            {
               node = conn.Single<ky_node>(q => q.kNodeNumber == nodeNumber);
            }
            if (node != null)
                return node.kId;
            else
                return 0;
        }

        /// <summary>
        /// 通过银行编号查询银行Id
        /// </summary>
        /// <param name="branchNumber"></param>
        /// <returns></returns>
        public static int GetBranchId(string branchNumber)
        {
            ky_branch branch;
            using (IDbConnection conn = DbHelperMySQL.OpenDeviceConnection())
            {
                branch = conn.Single<ky_branch>(q => q.kBranchNumber == branchNumber);
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
            using (IDbConnection conn = DbHelperMySQL.OpenDeviceConnection())
            {
                node = conn.Single<ky_node>(q => q.kId == nodeId);
            }
            if (node != null)
                return node.kBranchId;
            else
                return 0;
        }

        /// <summary>
        /// 通过厂家编号获取厂家ID
        /// </summary>
        /// <param name="factoryNumber"></param>
        /// <returns></returns>
        public static int GetFactoryId(string factoryNumber)
        {
            ky_factory factory;
            using (IDbConnection conn = DbHelperMySQL.OpenDeviceConnection())
            {
                factory = conn.Single<ky_factory>(q => q.kFactoryCode == factoryNumber);
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
        public static bool InsertFactory(string factoryName,string factoryNumber)
        {
            try
            {
                using (IDbConnection conn = DbHelperMySQL.OpenDeviceConnection())
                {
                    conn.Insert(new ky_factory { kFactoryName = factoryName, kFactoryCode = factoryNumber });
                }
                return true;
            }
            catch(Exception e)
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
            List<ky_machine> machines;
            using (IDbConnection conn = DbHelperMySQL.OpenDeviceConnection())
            {
                machines = conn.Select<ky_machine>(q => Sql.In(q.kNodeId, nodeIds));
            }
            return machines;
        }

        /// <summary>
        /// 通过机具编号获取机具ID
        /// </summary>
        /// <param name="machineNumber"></param>
        /// <returns></returns>
        public static int GetMachineId(string machineNumber)
        {
            ky_machine machine;
            using (IDbConnection conn = DbHelperMySQL.OpenDeviceConnection())
            {
                machine = conn.Single<ky_machine>(q=>q.kMachineNumber==machineNumber);
            }
            if (machine != null)
                return machine.kId;
            else
                return 0;
        }
        /// <summary>
        /// 通过用户编号获取用户ID
        /// </summary>
        /// <param name="userNumber"></param>
        /// <returns></returns>
        public static int GetUserId(string userNumber)
        {
            ky_user user;
            using (IDbConnection conn = DbHelperMySQL.OpenDeviceConnection())
            {
                user = conn.Single<ky_user>(q => q.kUserNumber == userNumber);
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
            using (IDbConnection conn = DbHelperMySQL.OpenDeviceConnection())
            {
                imgserver = conn.Single<ky_imgserver>(q => q.kIpAddress == ip);
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
            using (IDbConnection conn = DbHelperMySQL.OpenDeviceConnection())
            {
                user = conn.Single<ky_user>(q => q.kUserNumber == UserNumber);
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
            using (IDbConnection conn = DbHelperMySQL.OpenDeviceConnection())
            {
                machine = conn.Single<ky_machine>(q => q.kIpAddress == ip);
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
            using (IDbConnection conn = DbHelperMySQL.OpenDeviceConnection())
            {
                machines = conn.Select<ky_machine>(q => q.kNodeId == nodeId);
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
        public static bool UpdateMachine(int id, string machineNumber,string machineModel)
        {
            try
            {
                using (IDbConnection conn = DbHelperMySQL.OpenDeviceConnection())
                {
                    conn.Update(new ky_machine { kId = id, kMachineNumber = machineNumber, kMachineModel = machineModel, kUpdateTime = DateTime.Now });
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
            using (IDbConnection conn = DbHelperMySQL.OpenDeviceConnection())
            {
                machines = conn.Select<ky_machine>(q => Sql.In(q.kNodeId, nodeIds));
            }
            return machines;
        }

        /// <summary>
        /// 通过网点Id获取该网点下的所有ATM数据
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public static List<ky_atm> GetAtmWithNodeId(List<int> nodeIds)
        {
            List<ky_atm> atms;
            using (IDbConnection conn = DbHelperMySQL.OpenDeviceConnection())
            {
                atms = conn.Select<ky_atm>(q => Sql.In(q.kNodeId, nodeIds));
            }
            return atms;
        }

        /// <summary>
        /// 通过网点Id获取该网点下的所有ATM数据
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public static List<ky_cashbox> GetCashBoxWithNodeId(List<int> nodeIds)
        {
            List<ky_cashbox> cashboxs;
            using (IDbConnection conn = DbHelperMySQL.OpenDeviceConnection())
            {
                cashboxs = conn.Select<ky_cashbox>(q => Sql.In(q.kNodeId, nodeIds));
            }
            return cashboxs;
        }
        /// <summary>
        /// 获取全部钞箱信息
        /// </summary>
        /// <returns></returns>
        public static List<ky_cashbox> GetAllCashBox()
        {
            List<ky_cashbox> cashboxs;
            using (IDbConnection conn = DbHelperMySQL.OpenDeviceConnection())
            {
                cashboxs = conn.Select<ky_cashbox>();
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
            using (IDbConnection conn = DbHelperMySQL.OpenDeviceConnection())
            {
                nodes = conn.Select<ky_node>();
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
            using (IDbConnection conn = DbHelperMySQL.OpenDeviceConnection())
            {
                factorys = conn.Select<ky_factory>();
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
            using (IDbConnection conn = DbHelperMySQL.OpenDeviceConnection())
            {
                importmachines = conn.Select<ky_import_machine>();
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
            using (IDbConnection conn = DbHelperMySQL.OpenDeviceConnection())
            {
                machine = conn.Single<ky_import_machine>(p => p.kMachineNumber == machineNumber);
            }
            if (machine != null)
                return machine.kId;
            else
                return 0;
        }

        /// <summary>
        /// 往表ky_import_machine中插入数据，并返回id
        /// </summary>
        /// <param name="machineNumber"></param>
        /// <param name="nodeId"></param>
        /// <param name="factoryId"></param>
        /// <returns></returns>
        public static int InsertMachineToImportMachine(string machineNumber, int nodeId, int factoryId)
        {
            int id = 0;
            try
            {
                using (IDbConnection conn = DbHelperMySQL.OpenDeviceConnection())
                {
                    ky_import_machine machine =new ky_import_machine{ kMachineType = 0, kMachineNumber = machineNumber, kNodeId = nodeId, kFactoryId = factoryId };
                     id=Convert.ToInt32(conn.Insert(machine, selectIdentity: true));
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
                using (IDbConnection conn = DbHelperMySQL.OpenDeviceConnection())
                {
                    ky_import_file file = new ky_import_file
                    {
                        kBatchId = bactchId,
                        kFileName = fileName,
                        kImportTime = importTime,
                        kType = business,
                        kNodeId = nodeId
                    };
                    Convert.ToInt32(conn.Insert(file));
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
            using (IDbConnection conn = DbHelperMySQL.OpenDeviceConnection())
            {
                imgservers = conn.Select<ky_imgserver>();
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
        public static bool InsertGzhBundle(string bundleNumber,long batchId,string fileName)
        {
            try
            {
                using (IDbConnection conn = DbHelperMySQL.OpenDeviceConnection())
                {
                    ky_gzh_bundle file = new ky_gzh_bundle
                    {
                        kBatchId = batchId,
                        kFileName = fileName,
                        kBundleNumber = bundleNumber
                    };
                    Convert.ToInt32(conn.Insert(file));
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
        public static int GetGzhBundleId(string bundleNumber,long batchId)
        {
            ky_gzh_bundle gzh_bundle;
            using (IDbConnection conn = DbHelperMySQL.OpenDeviceConnection())
            {
                gzh_bundle=conn.Single<ky_gzh_bundle>(p=>p.kBatchId==batchId&&p.kBundleNumber==bundleNumber);
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
            using (IDbConnection conn = DbHelperMySQL.OpenDeviceConnection())
            {
                id=Convert.ToInt32(conn.Insert(gzh,selectIdentity: true));
            }
            return id;
        }
        
      
        /// <summary>
        /// 保存packageBundle信息
        /// </summary>
        /// <param name="bundleId"></param>
        /// <param name="packageId"></param>
        /// <returns></returns>
        public static bool SavePackageBundle(int bundleId,int packageId)
        {
            if (bundleId == 0 || packageId == 0)
                return false;
            try
            {
                using (IDbConnection conn = DbHelperMySQL.OpenDeviceConnection())
                {
                    ky_package_bundle pack_bundle = new ky_package_bundle { kBundleId = bundleId, kPackageId = packageId };
                    conn.Insert(pack_bundle);
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
                using (IDbConnection conn = DbHelperMySQL.OpenSphinxConnection())
                {
                       conn.Select<ky_batch>().FirstOrDefault();
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
                using (IDbConnection conn = DbHelperMySQL.OpenDeviceConnection())
                {
                    conn.Select<ky_machine>().FirstOrDefault();
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
        public static bool TestConnectPush(string Ip,int Port)
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
                using (IDbConnection conn = DbHelperMySQL.OpenImageConnection())
                {
                    conn.Select<ky_picture>().FirstOrDefault();
                }
                return true;
            }
            catch(Exception e)
            {
                Log.DataBaseException(e,"测试图像数据库连接失败");
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
