using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
using System.Data.Common;
using System.Xml.Linq;
using System.Data;
using System.Linq;
using System;

namespace DataAccess
{
    public static class SqlExev2
    {
        //private static SqlExe instance = null;
        //private static readonly object padlock = new object();
         

        //static DatabaseProviderFactory factory = new DatabaseProviderFactory();
        //static Database db = factory.Create("Connection String");
        //static DbParameter parameter;
        //static DbCommand cmd;
        //static IEnumerable<XElement> elems;
        //static IEnumerable<XElement> elem_list; 

        /// <summary>
        /// For setting command time out for big amount of data processing 
        /// </summary>
        private static void SetCommandTimeOut(DbCommand cmd)
        {
            string strCmdTime = System.Configuration.ConfigurationManager.AppSettings["SetCommandTimeOut"] == null ? "30" : System.Configuration.ConfigurationManager.AppSettings["SetCommandTimeOut"].ToString();

            if (strCmdTime.Trim() != "")
            {
                cmd.CommandTimeout = Convert.ToInt32(strCmdTime);
            }

        }
        /// <summary>
        /// For getting procedure name from xml file 
        /// </summary>
        /// <param name="Xdoc"></param>
        /// <returns></returns>
        private static string GetProcName(XDocument Xdoc)
        {
            return Xdoc.Descendants("BIC").Elements("procName").FirstOrDefault().Value;
        }

        /// <summary>
        /// For getting data from data base in multiple tables 
        /// </summary>
        /// <param name="spDOM"></param>
        /// <returns></returns>
        public static DataSet GetDS(XDocument spDOM, string ConnectionStr)
        {
            try
            {
                Database db = new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnectionStr);
                DbParameter parameter;
                DbCommand cmd;
                IEnumerable<XElement> elems;
                IEnumerable<XElement> elem_list;
                string spName = GetProcName(spDOM);
                cmd = db.GetStoredProcCommand(spName);
                elems = spDOM.Descendants();
                elem_list = from elem in elems select elem;
                foreach (XElement element in elem_list)
                {
                    parameter = cmd.CreateParameter();
                    if (element.Name.ToString() != "BIC" && element.Name.ToString() != "XsdName" && element.Name.ToString() != "procName" && element.Name.ToString() != "pERRORXML")
                    {
                        parameter.ParameterName = "@" + element.Name.ToString();
                        parameter.Value = element.Value;
                        cmd.Parameters.Add(parameter);
                    }
                }

                return db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// For getting data in single table from database
        /// </summary>
        /// <param name="spDOM"></param>
        /// <returns></returns>
        public static DataTable GetDT(XDocument spDOM,string ConnectionStr)
        {
            try
            { 
                Database db = new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnectionStr);
                
                DbParameter parameter;
                DbCommand cmd;
                IEnumerable<XElement> elems;
                IEnumerable<XElement> elem_list;
                string spName = GetProcName(spDOM);
                cmd = db.GetStoredProcCommand(spName);
                SetCommandTimeOut(cmd);
                elems = spDOM.Descendants();
                elem_list = from elem in elems select elem;
                foreach (XElement element in elem_list)
                {
                    parameter = cmd.CreateParameter();
                    if (element.Name.ToString() != "BIC" && element.Name.ToString() != "XsdName" && element.Name.ToString() != "procName" && element.Name.ToString() != "pERRORXML")
                    {
                        parameter.ParameterName = "@" + element.Name.ToString();
                        parameter.Value = element.Value;
                        cmd.Parameters.Add(parameter);
                    }
                }

                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// for getting single value return from database after performing insert, updste or delete
        /// </summary>
        /// <param name="spDOM"></param>
        /// <returns></returns>
        public static string ExecuteNonQuery(XDocument spDOM, string ConnectionStr)
        {
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                Database db = factory.Create(ConnectionStr);
                DbParameter parameter;
                DbCommand cmd;
                IEnumerable<XElement> elems;
                IEnumerable<XElement> elem_list;
                string spName = GetProcName(spDOM);
                cmd = db.GetStoredProcCommand(spName);
                SetCommandTimeOut(cmd);
                string OutParameterName = "";
                elems = spDOM.Descendants();
                elem_list = from elem in elems
                            select elem;
                foreach (XElement element in elem_list)
                {
                    parameter = cmd.CreateParameter();
                    if (element.Name.ToString() != "BIC" && element.Name.ToString() != "XsdName" && element.Name.ToString() != "ProcName" && element.Name.ToString() != "pERRORXML")
                    {
                        OutParameterName = element.Name.ToString();
                        parameter.ParameterName = "@" + element.Name.ToString();
                        if (element.Name.ToString().Trim() == "pERRORXML")
                        {
                            parameter.DbType = DbType.Xml;
                            parameter.Direction = ParameterDirection.Output;
                        }
                        else
                        {
                            parameter.Value = element.Value;
                            parameter.Direction = ParameterDirection.Input;
                        }

                        cmd.Parameters.Add(parameter);

                    }
                }

                db.ExecuteNonQuery(cmd);
                var retval = db.GetParameterValue(cmd, "@pERRORXML");
                return Convert.ToString(retval);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// For getting string xml from database 
        /// </summary>
        /// <param name="dom"></param>
        /// <returns></returns>
        public static string ExecuteScalarforStringXml(XDocument dom, string ConnectionStr)
        {
            try
            {
                Database db = new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnectionStr);
                DbParameter parameter;
                DbCommand cmd;
                IEnumerable<XElement> elems;
                IEnumerable<XElement> elem_list;
                string Procname = GetProcName(dom);
                cmd = db.GetStoredProcCommand(Procname);
                SetCommandTimeOut(cmd);
                elems = dom.Descendants();
                elem_list = from elem in elems
                            select elem;
                foreach (XElement element in elem_list)
                {
                    parameter = cmd.CreateParameter();
                    if (element.Name.ToString() != "BIC" && element.Name.ToString() != "XsdName" && element.Name.ToString() != "procName" && element.Name.ToString() != "pERRORXML")
                    {
                        parameter.ParameterName = "@" + element.Name.ToString();
                        parameter.Value = element.Value;
                        cmd.Parameters.Add(parameter);
                    }
                }

                var retval = db.ExecuteScalar(cmd);
                return Convert.ToString(retval);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// for getting integer result form database
        /// </summary>
        /// <param name="dom"></param>
        /// <returns></returns>
        public static int ExecuteScalarforInt(XDocument dom, string ConnectionStr)
        {
            try
            {
                Database db = new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnectionStr);
                DbParameter parameter;
                DbCommand cmd;
                IEnumerable<XElement> elems;
                IEnumerable<XElement> elem_list;
                string Procname = GetProcName(dom);
                cmd = db.GetStoredProcCommand(Procname);
                SetCommandTimeOut(cmd);
                elems = dom.Descendants();
                elem_list = from elem in elems
                            select elem;
                foreach (XElement element in elem_list)
                {
                    parameter = cmd.CreateParameter();
                    if (element.Name.ToString() != "BIC" && element.Name.ToString() != "XsdName" && element.Name.ToString() != "procName" && element.Name.ToString() != "pERRORXML")
                    {
                        parameter.ParameterName = "@" + element.Name.ToString();
                        parameter.Value = element.Value;
                        cmd.Parameters.Add(parameter);
                    }
                }

                int retval = Convert.ToInt32(db.ExecuteScalar(cmd));
                return retval;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// For getting xml from database 
        /// </summary>
        /// <param name="dom"></param>
        /// <returns></returns>
        public static string GetXml(XDocument dom, string ConnectionStr)
        {
            try
            {
                Database db = new Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase(ConnectionStr);
                DbParameter parameter;
                DbCommand cmd;
                IEnumerable<XElement> elems;
                IEnumerable<XElement> elem_list;
                string Procname = GetProcName(dom);
                cmd = db.GetStoredProcCommand(Procname);
                SetCommandTimeOut(cmd);
                string OutParameterName = "";

                elems = dom.Descendants();
                elem_list = from elem in elems
                            select elem;
                foreach (XElement element in elem_list)
                {
                    parameter = cmd.CreateParameter();
                    if (element.Name.ToString() != "BIC" && element.Name.ToString() != "XsdName" && element.Name.ToString() != "procName" && element.Name.ToString() != "pFILESDATA")
                    {
                        OutParameterName = element.Name.ToString();
                        parameter.ParameterName = "@" + element.Name.ToString();
                        if (element.Name.ToString().Trim() == "pERRORXML")
                        {
                            parameter.DbType = DbType.Xml;
                            parameter.Direction = ParameterDirection.Output;
                        }
                        else
                        {
                            parameter.Value = element.Value;
                            parameter.Direction = ParameterDirection.Input;
                        }

                        cmd.Parameters.Add(parameter);
                    }
                }

                db.ExecuteNonQuery(cmd);
                var retval = db.GetParameterValue(cmd, "@pERRORXML");
                return Convert.ToString(retval);
            }
            catch (Exception ex)
            { 
                throw ex;
            }

        }
    }
}
