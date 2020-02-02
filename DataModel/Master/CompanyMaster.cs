using DataAccess;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataModel.Master
{
    public class CompanyMaster
    {
        string dbConnectionString = ConfigurationManager.AppSettings["dbConnectionString"];
        XDocument Xdoc;
        DataTable dt;
        #region Properties 
        string _cflag;
        string _serverName;
        string _AuthType;
        string _userName;
        string _PASSWORD;
        string _databaseName;

        int _compId;
        string _compName;

        public int CompId
        {
            get
            {
                return _compId;
            }

            set
            {
                _compId = value;
            }
        }

        public string CompName
        {
            get
            {
                return _compName;
            }

            set
            {
                _compName = value;
            }
        }

        public string ServerName
        {
            get
            {
                return _serverName;
            }

            set
            {
                _serverName = value;
            }
        }

        public string AuthType
        {
            get
            {
                return _AuthType;
            }

            set
            {
                _AuthType = value;
            }
        }

        public string UserName
        {
            get
            {
                return _userName;
            }

            set
            {
                _userName = value;
            }
        }

        public string DatabaseName
        {
            get
            {
                return _databaseName;
            }

            set
            {
                _databaseName = value;
            }
        }

        public string PASSWORD
        {
            get
            {
                return _PASSWORD;
            }

            set
            {
                _PASSWORD = value;
            }
        }


        public string Cflag
        {
            get
            {
                return _cflag;
            }

            set
            {
                _cflag = value;
            }
        }
        #endregion

        public async Task<CompanyMaster> getComapnyDtl(int COMPID)
        {
            try
            {
                CompanyMaster obj = new CompanyMaster();

                Xdoc = DBXML.CLIENTCONNECTION_h(COMPID, 0);
                dt = SqlExev2.GetDT(Xdoc, dbConnectionString);

                obj = (from row in dt.AsEnumerable()
                                select new CompanyMaster
                                {
                                    CompName = row.Field<string>("compName"),
                                    ServerName = row.Field<string>("serverName"),
                                    AuthType = row.Field<string>("AuthType"),
                                    UserName = row.Field<string>("userName"),
                                    PASSWORD = row.Field<string>("PASSWORD"),
                                    DatabaseName = row.Field<string>("databaseName")
                                    //isActive = row.Field<string>("isActive"),
                                    //createdOn = row.Field<string>("createdOn"),
                                    //ceatedBy = row.Field<string>("ceatedBy"),
                                }).FirstOrDefault();

                return await Task.Factory.StartNew(() => obj);
                //return obj;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<string> getComapnyConnectionstr(int COMPID)
        {
            try
            {
                CompanyMaster obj = new CompanyMaster();

                Xdoc = DBXML.CLIENTCONNECTION_h(COMPID, 0);
                dt = SqlExev2.GetDT(Xdoc, dbConnectionString);

                obj = (from row in dt.AsEnumerable()
                       select new CompanyMaster
                       {
                           CompName = row.Field<string>("compName"),
                           ServerName = row.Field<string>("serverName"),
                           AuthType = row.Field<string>("AuthType"),
                           UserName = row.Field<string>("userName"),
                           PASSWORD = row.Field<string>("PASSWORD"),
                           DatabaseName = row.Field<string>("databaseName") 
                       }).FirstOrDefault();

                string connectionString="";
                if (obj != null)
                {
                    string connetion = "";
                    if (obj.AuthType == "SQL Server Authentication")
                    {
                        connetion = "Data Source=" + obj.ServerName + ";Initial Catalog=" + obj.DatabaseName + ";User ID=" + obj.UserName + ";Password=" + obj.PASSWORD + ";";
                    }
                    else
                    {
                        connetion = "Data Source=" + obj.ServerName + ";Initial Catalog=" + obj.DatabaseName + ";Integrated Security=true;";
                    }

                    connectionString = connetion;
                }

                return await Task.Factory.StartNew(() => connectionString);
                //return obj;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
