using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataModel.Database
{
    public class dbConfig : Result.Result
    {
        #region Properties
        XDocument xdoc;
        DataTable dt;
        string iFlag;
        int dbConfigId;
        string dbName;
        string sqlDBAuthType;
        string dbServerName;
        string dbDatabaseName;
        string dbUserId;
        string dbPassword;
        string dbConStr;
        string dbCommServerName;
        string sqlDBCommAuthType;
        string dbCommDatabaseName;
        string dbCommUserId;
        string dbCommPassword;
        string dbCommonConStr;
        string serialNumber;
        string authCode;
        DateTime createdOn;
        int createdBy;
        bool isActive;
        DateTime updatedOn;
        int updatedBy;
        bool isAdmin;
        string adminGroup;
        string userReceptionistGroup;
        string userDoctorGroup;
        string userCashierGroup;
        bool isReceptionist;
        bool isDoctor;
        bool isCashier;
        

        public string IFlag { get => iFlag; set => iFlag = value; }
        public int DbConfigId { get => dbConfigId; set => dbConfigId = value; }
        public string DbName { get => dbName; set => dbName = value; }
        public string SqlDBAuthType { get => sqlDBAuthType; set => sqlDBAuthType = value; }
        public string DbServerName { get => dbServerName; set => dbServerName = value; }
        public string DbDatabaseName { get => dbDatabaseName; set => dbDatabaseName = value; }
        public string DbUserId { get => dbUserId; set => dbUserId = value; }
        public string DbPassword { get => dbPassword; set => dbPassword = value; }
        public string DbConStr { get => dbConStr; set => dbConStr = value; }
        public string DbCommServerName { get => dbCommServerName; set => dbCommServerName = value; }
        public string SqlDBCommAuthType { get => sqlDBCommAuthType; set => sqlDBCommAuthType = value; }
        public string DbCommDatabaseName { get => dbCommDatabaseName; set => dbCommDatabaseName = value; }
        public string DbCommUserId { get => dbCommUserId; set => dbCommUserId = value; }
        public string DbCommPassword { get => dbCommPassword; set => dbCommPassword = value; }
        public string DbCommonConStr { get => dbCommonConStr; set => dbCommonConStr = value; }
        public string SerialNumber { get => serialNumber; set => serialNumber = value; }
        public string AuthCode { get => authCode; set => authCode = value; }
        public DateTime CreatedOn { get => createdOn; set => createdOn = value; }
        public int CreatedBy { get => createdBy; set => createdBy = value; }
        public bool IsActive { get => isActive; set => isActive = value; }
        public DateTime UpdatedOn { get => updatedOn; set => updatedOn = value; }
        public int UpdatedBy { get => updatedBy; set => updatedBy = value; } 
        public string AdminGroup { get => adminGroup; set => adminGroup = value; }
        public string UserReceptionistGroup { get => userReceptionistGroup; set => userReceptionistGroup = value; }
        public string UserDoctorGroup { get => userDoctorGroup; set => userDoctorGroup = value; }
        public string UserCashierGroup { get => userCashierGroup; set => userCashierGroup = value; }
        public bool IsAdmin { get => isAdmin; set => isAdmin = value; }
        public bool IsReceptionist { get => isReceptionist; set => isReceptionist = value; }
        public bool IsDoctor { get => isDoctor; set => isDoctor = value; }
        public bool IsCashier { get => isCashier; set => isCashier = value; }

        #endregion

        #region Methods
        public List<dbConfig> getdbList(int dbConfigId, string dbName, int userId, int flag = 0)
        {
            try
            {
                XElement logXMl = null;
                xdoc = DBXML.dbConfig_g(dbConfigId, dbName, userId, flag, logXMl);
                dt = SqlExe.GetDT(xdoc);
                List<dbConfig> lst = dt != null && dt.Rows.Count > 0 ? dt.AsEnumerable().Select(s => new dbConfig
                {
                    DbConfigId = s.Field<int>("dbConfigId"),
                    DbName = s.Field<string>("dbName"),
                    DbServerName = s.Field<string>("dbServerName"),
                    DbDatabaseName = s.Field<string>("dbDatabaseName"),
                    DbConStr = s.Field<string>("dbConStr"),
                    DbCommonConStr = s.Field<string>("dbCommonConStr"),
                    SerialNumber = s.Field<string>("serialNumber"),
                    AuthCode = s.Field<string>("authCode"),
                    AdminGroup = s.Field<string>("adminGroup"),
                    UserReceptionistGroup = s.Field<string>("receptionistGroup"),
                    UserDoctorGroup = s.Field<string>("doctorGroup"),
                    UserCashierGroup = s.Field<string>("cashierGroup"),
                    IsActive = s.Field<bool>("isActive")
                }).ToList() : null;
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbConfigId"></param>
        /// <param name="dbName"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public dbConfig getDatabse(int dbConfigId, string dbName, int userId)
        {
            try
            {
                XElement logXMl = null;
                xdoc = DBXML.dbConfig_g(dbConfigId, dbName, userId, 1, logXMl);
                dt = SqlExe.GetDT(xdoc);
                dbConfig lst = dt != null && dt.Rows.Count > 0 ? dt.AsEnumerable().Select(s => new dbConfig
                {
                    DbConfigId = s.Field<int>("dbConfigId"),
                    DbName = s.Field<string>("dbName"),
                    DbServerName = s.Field<string>("dbServerName"),
                    DbDatabaseName = s.Field<string>("dbDatabaseName"),
                    DbUserId = s.Field<string>("dbUserId"),
                    DbPassword = s.Field<string>("dbPassword"),
                    SqlDBAuthType = s.Field<string>("SqlDBAuthType"),
                    DbCommServerName = s.Field<string>("dbCommServerName"),
                    SqlDBCommAuthType = s.Field<string>("SqlDBCommAuthType"),
                    DbCommDatabaseName = s.Field<string>("dbCommDatabaseName"),
                    DbCommUserId = s.Field<string>("dbCommUserId"),
                    DbCommPassword = s.Field<string>("dbCommPassword"),
                    DbConStr = s.Field<string>("dbConStr"),
                    DbCommonConStr = s.Field<string>("dbCommonConStr"),
                    AdminGroup = s.Field<string>("adminGroup"),
                    UserReceptionistGroup = s.Field<string>("receptionistGroup"),
                    UserDoctorGroup = s.Field<string>("doctorGroup"),
                    UserCashierGroup = s.Field<string>("cashierGroup"),
                    IsActive = s.Field<bool>("isActive")
                }).ToList().FirstOrDefault() : null;
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="USERID"></param>
        /// <param name="obj"></param>
        /// <param name="objDb"></param>
        /// <param name="LOGXML"></param>
        /// <returns></returns>
        public Result.Result Save(int USERID, dbConfig obj, XElement LOGXML = null)
        {
            try
            {
                xdoc = DBXML.dbConfig_C(obj.IFlag, obj.DbConfigId, obj.DbName, obj.SqlDBAuthType, obj.DbServerName, obj.DbUserId, obj.DbPassword, obj.DbDatabaseName,
            obj.SqlDBCommAuthType, obj.DbCommServerName, obj.DbCommDatabaseName, obj.DbCommUserId, obj.DbCommPassword, obj.SerialNumber, obj.AuthCode, obj.IsActive,
            obj.AdminGroup,
            USERID, LOGXML);
                return ReadBIErrors(Convert.ToString(SqlExe.GetXml(xdoc)));
            }
            catch (Exception ex)
            {
                throw ex; ;
            }
        }
        #endregion
    }
}
