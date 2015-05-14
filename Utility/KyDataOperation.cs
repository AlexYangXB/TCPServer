using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using MySql.Data.MySqlClient;
using Utility.DBUtility;

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
        public static bool InsertSignBatch(KyData.DbTable.ky_batch_sphinx batch)
        {
            string machine = "";
            for (int i = 0; i < batch.machine.Length; i++)
            {
                if (i == batch.machine.Length - 1)
                    machine += batch.machine[i].ToString();
                else
                    machine += batch.machine[i].ToString() + ",";
            }
            string strSql =
                string.Format(
                    "INSERT INTO ky_batch(id,ktype,kdate,knode,kfactory,kmachine,ktotalnumber,ktotalvalue,kuser,kimgserver,hjson) values({0},'{1}',{2},{3},{4},({5}),{6},{7},{8},{9},'{10}')",
                    batch.id, batch.type, batch.date, batch.node, batch.factory, machine, batch.totalnumber,
                    batch.totalvalue, batch.recorduser, batch.imgipaddress, batch.hjson);
            DBUtility.DbHelperMySQL.SetCurrentDb(DbHelperMySQL.DataBaseServer.Sphinx);
            int result = DBUtility.DbHelperMySQL.ExecuteSql(strSql);
            if (result > 0)
                return true;
            else
                return false;
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
        public static DataTable GetNodeWithBindIp(string bindIpAddress)
        {
            string strSql = string.Format("SELECT * FROM ky_node where kBindIpAddress='{0}'", bindIpAddress);
            DbHelperMySQL.SetCurrentDb(DbHelperMySQL.DataBaseServer.Device);
            return DbHelperMySQL.Query(strSql).Tables[0];
        }
        /// <summary>
        /// 根据ID获取ky_node 表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataTable GetNodeWithIds(int[] id)
        {
            string strSql = "";
            for(int i=0;i<id.Length;i++)
            {
                if (i == 0)
                    strSql = string.Format("SELECT * FROM ky_node where kId={0} ", id[i]);
                else
                    strSql += string.Format("or kId={0} ", id[i]);
            }
            strSql += ";";
            DbHelperMySQL.SetCurrentDb(DbHelperMySQL.DataBaseServer.Device);
            return DbHelperMySQL.Query(strSql).Tables[0];
        }


        /// <summary>
        /// 更新ky_node表中的kBindIpAddress项
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="bindIpAdress"></param>
        /// <returns></returns>
        public static bool UpdateNodeTable(int[] ids, string bindIpAdress)
        {
            List<string>  ListSql=new List<string>();
            for(int i=0;i<ids.Length;i++)
            {
                string strSql = string.Format("UPDATE ky_Node set kBindIpAddress='{0}' where kId={1};", bindIpAdress,
                                              ids[i]);
                ListSql.Add(strSql);
            }
            DbHelperMySQL.SetCurrentDb(DbHelperMySQL.DataBaseServer.Device);
            int result = DbHelperMySQL.ExecuteSqlTran(ListSql);
            if(result>0)
            {
                return true;
            }
            else
            {
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
            string strSql = "SELECT kid FROM ky_node where kNodeNumber='" + nodeNumber + "';";
            DbHelperMySQL.SetCurrentDb(DbHelperMySQL.DataBaseServer.Device);
            DataTable dt = DbHelperMySQL.Query(strSql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                return int.Parse(dt.Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 通过银行编号查询银行Id
        /// </summary>
        /// <param name="branchNumber"></param>
        /// <returns></returns>
        public static int GetBranchId(string branchNumber)
        {
            string strSql = "SELECT kId FROM ky_branch where kBranchNumber ='" + branchNumber + "';";
            DataTable dt = DbHelperMySQL.Query(strSql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                return int.Parse(dt.Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        public static int GetBranchIdWithNodeId(int nodeId)
        {
            string strSql = "SELECT kBranchId FROM ky_node where kId = " + nodeId + " ;";
            DataTable dt = DbHelperMySQL.Query(strSql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                return int.Parse(dt.Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 通过厂家编号获取厂家ID
        /// </summary>
        /// <param name="factoryNumber"></param>
        /// <returns></returns>
        public static int GetFactoryId(string factoryNumber)
        {
            string strSql = "SELECT kid FROM ky_factory where kFactoryCode='" + factoryNumber + "';";
            DbHelperMySQL.SetCurrentDb(DbHelperMySQL.DataBaseServer.Device);
            DataTable dt = DbHelperMySQL.Query(strSql).Tables[0];
            if (dt.Rows.Count > 0)
            { 
                return int.Parse(dt.Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }
        
        /// <summary>
        /// 添加厂家信息
        /// </summary>
        /// <param name="factoryName"></param>
        /// <param name="factoryNumber"></param>
        /// <returns></returns>
        public static bool InsertFactory(string factoryName,string factoryNumber)
        {
            string strSql = string.Format("Insert into ky_factory(kFactoryName,kFactoryCode)value('{0}','{1}')",
                                          factoryName, factoryNumber);
            DbHelperMySQL.SetCurrentDb(DbHelperMySQL.DataBaseServer.Device);
            int result = DBUtility.DbHelperMySQL.ExecuteSql(strSql);
            if (result > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 根据网点编号获取机具
        /// </summary>
        /// <param name="nodeIds"></param>
        /// <returns></returns>
        public static DataTable GetMachineWithNodeIds(int[] nodeIds)
        {
            string strSql = "";
            for (int i = 0; i < nodeIds.Length; i++)
            {
                if (i == 0)
                    strSql = string.Format("SELECT * FROM ky_machine where kNodeId={0} ", nodeIds[i]);
                else
                    strSql += string.Format("or kNodeId={0} ", nodeIds[i]);
            }
            strSql += ";";
            DbHelperMySQL.SetCurrentDb(DbHelperMySQL.DataBaseServer.Device);
            return DbHelperMySQL.Query(strSql).Tables[0];
        }

        /// <summary>
        /// 通过机具编号获取机具ID
        /// </summary>
        /// <param name="machineNumber"></param>
        /// <returns></returns>
        public static int GetMachineId(string machineNumber)
        {
            string strSql = "SELECT kid FROM ky_machine where kMachineNumber='" + machineNumber + "';";
            DbHelperMySQL.SetCurrentDb(DbHelperMySQL.DataBaseServer.Device);
            DataTable dt = DbHelperMySQL.Query(strSql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                return int.Parse(dt.Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 通过用户编号获取用户ID
        /// </summary>
        /// <param name="userNumber"></param>
        /// <returns></returns>
        public static int GetUserId(string userNumber)
        {
            string strSql = "SELECT kid FROM ky_user where kUserNumber='" + userNumber + "';";
            DbHelperMySQL.SetCurrentDb(DbHelperMySQL.DataBaseServer.Device);
            DataTable dt = DbHelperMySQL.Query(strSql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                return int.Parse(dt.Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 通过图像服务器IP获取图像服务器ID
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static int GetPictureServerId(string ip)
        {
            string strSql = "SELECT kid FROM ky_imgserver WHERE kIpAddress='" + ip + "';";
            DbHelperMySQL.SetCurrentDb(DbHelperMySQL.DataBaseServer.Device);
            DataTable dt = DbHelperMySQL.Query(strSql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                return int.Parse(dt.Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }


        /// <summary>
        /// 根据用户编号查询用户，返回用户资料；
        /// </summary>
        /// <param name="UserNumber"></param>
        /// <returns></returns>
        public static DataTable GetUser(string UserNumber)
        {
            string strSql = "select * from ky_user where kUserNumber ='" + UserNumber + "';";
            DBUtility.DbHelperMySQL.SetCurrentDb(DbHelperMySQL.DataBaseServer.Device);
            return DbHelperMySQL.Query(strSql).Tables[0];
        }

        /// <summary>
        /// 查询机器列表获得所有的机器数据。
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllMachine()
        {
            string strSql = "select * from ky_machine;";
            DBUtility.DbHelperMySQL.SetCurrentDb(DbHelperMySQL.DataBaseServer.Device);
            return DbHelperMySQL.Query(strSql).Tables[0];
        }

        /// <summary>
        /// 通过IP获取机具信息
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static DataTable GetMachineWithIp(string ip)
        {
            string strSql = string.Format("select * from ky_machine where kIpAddress = '{0}';", ip);
            DBUtility.DbHelperMySQL.SetCurrentDb(DbHelperMySQL.DataBaseServer.Device);
            return DbHelperMySQL.Query(strSql).Tables[0];
        }

        /// <summary>
        /// 通过网点ID查询该网点下所有机器数据。
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public static DataTable GetMachineWithNodeId(int nodeId)
        {
            string strSql = string.Format("select * from ky_machine where kNodeId = {0};", nodeId);
            DBUtility.DbHelperMySQL.SetCurrentDb(DbHelperMySQL.DataBaseServer.Device);
            return DbHelperMySQL.Query(strSql).Tables[0];
        }

        /// <summary>
        /// 更新机器的最后上传时间和机具编号
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="machineNumber"></param>
        /// <param name="machineModel"> </param>
        /// <returns></returns>
        public static bool UpdateMachine(string ip, string machineNumber,string machineModel)
        {
            string strSql = "";
            if (machineNumber != "")
                //strSql = "UPDATE ky_machine SET kMachineNumber='" + machineNumber + "',kUpdateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE kIpAddress='" + ip +
                //            "';";
            strSql =
                string.Format(
                    "UPDATE ky_machine SET kMachineNumber='{0}',kMachineModel='{1}', kUpdateTime='{2}' WHERE kIpAddress='{3}' ;",
                    machineNumber, machineModel, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ip);
            else
                strSql = "UPDATE ky_machine SET kUpdateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE kIpAddress='" + ip + "';";
            DBUtility.DbHelperMySQL.SetCurrentDb(DbHelperMySQL.DataBaseServer.Device);
            int result = DBUtility.DbHelperMySQL.ExecuteSql(strSql);
            if (result > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 获取机器的 ID，IP，最后上传时间、状态。用于设备监控
        /// </summary>
        /// <returns></returns>
        public static DataTable GetMachineStatus()
        {
            string strSql = "SELECT kId, kIpAddress,kUpdateTime,kStatus FROM ky_machine;";
            DBUtility.DbHelperMySQL.SetCurrentDb(DbHelperMySQL.DataBaseServer.Device);
            return DbHelperMySQL.Query(strSql).Tables[0];
        }

        /// <summary>
        /// 获取全部ATM信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllAtm()
        {
            string strSql = "SELECT * FROM ky_atm;";
            DBUtility.DbHelperMySQL.SetCurrentDb(DbHelperMySQL.DataBaseServer.Device);
            return DbHelperMySQL.Query(strSql).Tables[0];
        }

        /// <summary>
        /// 通过网点Id获取该网点下的所有ATM数据
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public static DataTable GetAtmWithNodeId(int nodeId)
        {
            string strSql = string.Format("select * from ky_atm where kNodeId = {0}", nodeId);
            DBUtility.DbHelperMySQL.SetCurrentDb(DbHelperMySQL.DataBaseServer.Device);
            return DbHelperMySQL.Query(strSql).Tables[0];
        }

        /// <summary>
        /// 获取全部钞箱信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllCashBox()
        {
            string strSql = "SELECT * FROM ky_cashbox;";
            DBUtility.DbHelperMySQL.SetCurrentDb(DbHelperMySQL.DataBaseServer.Device);
            return DbHelperMySQL.Query(strSql).Tables[0];
        }

        /// <summary>
        /// 获取全部网点信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllNode()
        {
            string strSql = "SELECT * FROM ky_node;";
            DBUtility.DbHelperMySQL.SetCurrentDb(DbHelperMySQL.DataBaseServer.Device);
            return DbHelperMySQL.Query(strSql).Tables[0];
        }

        /// <summary>
        /// 获取全部厂家信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllFactory()
        {
            string strSql = "SELECT * FROM ky_factory;";
            DBUtility.DbHelperMySQL.SetCurrentDb(DbHelperMySQL.DataBaseServer.Device);
            return DbHelperMySQL.Query(strSql).Tables[0];
        }

        /// <summary>
        /// 获取表ky_import_machine内的全部信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllImportMachine()
        {
            string strSql = "SELECT * FROM ky_import_machine;";
            DBUtility.DbHelperMySQL.SetCurrentDb(DbHelperMySQL.DataBaseServer.Device);
            return DbHelperMySQL.Query(strSql).Tables[0];
        }

        /// <summary>
        /// 往表ky_import_machine中插入数据
        /// </summary>
        /// <param name="machineNumber"></param>
        /// <param name="nodeId"></param>
        /// <param name="factoryId"></param>
        /// <returns></returns>
        public static bool InsertMachineToImportMachine(string machineNumber, int nodeId, int factoryId)
        {
            string strSql =
                string.Format(
                    "INSERT INTO ky_import_machine(kMachineType,kMachineNumber,kNodeId,kFactoryId)value('{0}','{1}',{2},{3})",
                    "其他", machineNumber, nodeId, factoryId);
            DBUtility.DbHelperMySQL.SetCurrentDb(DbHelperMySQL.DataBaseServer.Device);
            int result = DBUtility.DbHelperMySQL.ExecuteSql(strSql);
            if (result > 0)
                return true;
            else
                return false;
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
        public static bool InsertImportFile(long bactchId, string fileName, string importTime, string business, int nodeId)
        {
            string strSql =
                string.Format(
                    "INSERT INTO ky_import_file(kBatchId,kFileName,kImportTime,kType,kNodeId)value({0},'{1}','{2}','{3}',{4})",
                    bactchId, fileName, importTime, business, nodeId);
            DBUtility.DbHelperMySQL.SetCurrentDb(DbHelperMySQL.DataBaseServer.Device);
            int result = DBUtility.DbHelperMySQL.ExecuteSql(strSql);
            if (result > 0)
                return true;
            else
                return false;
        }

        //获取所有的图片服务器
        public static DataTable GetAllImageServer()
        {
            string strSql = string.Format("select * from ky_imgserver;");
            DBUtility.DbHelperMySQL.SetCurrentDb(DbHelperMySQL.DataBaseServer.Device);
            return DbHelperMySQL.Query(strSql).Tables[0];
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
            string strSql =
                string.Format("Insert into ky_gzh_bundle(kBundleNumber,kBatchId,kFileName)value('{0}',{1},'{2}')",
                              bundleNumber, batchId, fileName);
            DBUtility.DbHelperMySQL.SetCurrentDb(DbHelperMySQL.DataBaseServer.Device);
            int result = DBUtility.DbHelperMySQL.ExecuteSql(strSql);
            if (result > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 获取GzhBundle的Id
        /// </summary>
        /// <param name="bundleNumber"></param>
        /// <param name="batchId"></param>
        /// <returns></returns>
        public static int GetGzhBundleId(string bundleNumber,long batchId)
        {
            string strSql = string.Format("Select kId From ky_gzh_bundle where kBundleNumber='{0}' and kBatchId={1};",
                                          bundleNumber, batchId);
            DbHelperMySQL.SetCurrentDb(DbHelperMySQL.DataBaseServer.Device);
            DataTable dt = DbHelperMySQL.Query(strSql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                return int.Parse(dt.Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 保存GZH package信息
        /// </summary>
        /// <param name="gzhLayer2"></param>
        /// <returns></returns>
        public static bool InsertGzhPackage(KyData.DataBase.KYDataLayer1.GzhLayer2 gzhLayer2)
        {
            string strSql =
                string.Format(
                    "Insert into ky_gzh_package(kDate,kBranchId,kNodeId,kType,kFSNNumber,kCashCenter,kVersion,kPackageNumber,kCurrency,kFileName,kUserId,kTotalNumber,kTotalValue)value('{0}',{1},{2},{3},{4},'{5}','{6}','{7}','{8}','{9}',{10},{11},{12})",
                    gzhLayer2.Date.ToString("yyyy-MM-dd HH:mm:ss"), gzhLayer2.BranchId, gzhLayer2.NodeId,
                    gzhLayer2.Business, gzhLayer2.FileCnt, gzhLayer2.IsCashCenter, gzhLayer2.FileVersion,
                    gzhLayer2.PackageNumber, gzhLayer2.Currency, gzhLayer2.FileName,gzhLayer2.UserId,gzhLayer2.TotalNumber,gzhLayer2.TotalValue);
            DBUtility.DbHelperMySQL.SetCurrentDb(DbHelperMySQL.DataBaseServer.Device);
            int result = DBUtility.DbHelperMySQL.ExecuteSql(strSql);
            if (result > 0)
                return true;
            else
                return false;
        }
        
        /// <summary>
        /// 获取GzhPackage的Id
        /// </summary>
        /// <param name="gzhLayer2"></param>
        /// <returns></returns>
        public static int GetGzhPackageId(KyData.DataBase.KYDataLayer1.GzhLayer2 gzhLayer2)
        {
            string strSql = string.Format("Select kId From ky_gzh_package where kDate='{0}' and kPackageNumber='{1}'",
                                          gzhLayer2.Date.ToString("yyyy-MM-dd HH:mm:ss"), gzhLayer2.PackageNumber);
            DbHelperMySQL.SetCurrentDb(DbHelperMySQL.DataBaseServer.Device);
            DataTable dt = DbHelperMySQL.Query(strSql).Tables[0];
            if (dt.Rows.Count > 0)
            {
                return int.Parse(dt.Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 保存packageBundle信息
        /// </summary>
        /// <param name="bundleId"></param>
        /// <param name="packageId"></param>
        /// <returns></returns>
        public static bool SavePackageBundle(int bundleId,int packageId)
        {
            string strSql = string.Format("Insert into ky_package_bundle(kBundleId,kPackageId)value({0},{1})", bundleId,
                                          packageId);
            DBUtility.DbHelperMySQL.SetCurrentDb(DbHelperMySQL.DataBaseServer.Device);
            int result = DBUtility.DbHelperMySQL.ExecuteSql(strSql);
            if (result > 0)
                return true;
            else
                return false;
        }

        #endregion


        #region 测试服务器连接

        /// <summary>
        /// 测试数据服务器的连接
        /// </summary>
        /// <returns></returns>
        public static bool TestConnectServer()
        {
            DBUtility.DbHelperMySQL.SetCurrentDb(DbHelperMySQL.DataBaseServer.Sphinx);
            return DbHelperMySQL.ConnectTest();
            //string strSql =
            //    "select TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_SCHEMA='kydb' and TABLE_NAME='ky_sign' ;";
            //DbHelperMySQL.SetCurrentDb(DbHelperMySQL.DataBaseServer.Sphinx);
            //DataTable dt = DbHelperMySQL.Query(strSql).Tables[0];
            //if (dt.Rows.Count > 0)
            //    return true;
            //else
            //    return false;
        }

        /// <summary>
        /// 测试设备服务器的连接
        /// </summary>
        /// <returns></returns>
        public static bool TestConnectDevice()
        {
            string strSql =
                "select TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_SCHEMA='kydb' and TABLE_NAME='ky_user' ;";
            DbHelperMySQL.SetCurrentDb(DbHelperMySQL.DataBaseServer.Device);
            DataTable dt = DbHelperMySQL.Query(strSql).Tables[0];
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
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
            catch 
            {
                return false;
            }
        }
        /// <summary>
        /// 测试图像服务器的连接
        /// </summary>
        /// <returns></returns>
        public static bool TestConnectImage()
        {
            string strSql =
                "select TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_SCHEMA='kydb' and TABLE_NAME='ky_picture' ;";
            DbHelperMySQL.SetCurrentDb(DbHelperMySQL.DataBaseServer.Image);
            DataTable dt = DbHelperMySQL.Query(strSql).Tables[0];
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
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
