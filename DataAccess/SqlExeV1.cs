using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
using System.Data.Common;
using System.Xml.Linq;
using System.Configuration;
using System.Data;
using System.Linq;
using System;
using Pastel.Evolution;

namespace DataAccess
{
    public sealed class SqlExe
    {
        private static SqlExe instance = null;
        private static readonly object padlock = new object();
         
        /// <summary>
        /// Private constructor for sqlexe class so that it wont allow any instance creation from outside this class
        /// </summary>
        SqlExe()
        {
            
        }

        /// <summary>
        /// Creating instance of sqlexe class by double checking it
        /// </summary>
        public static SqlExe Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new SqlExe();
                        }
                    }
                }
                return instance;
            }
        }
        /// <summary>
        /// For setting command time out for big amount of data processing 
        /// </summary>
        private void SetCommandTimeOut(DbCommand cmd)
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
        private string GetProcName(XDocument Xdoc)
        {
            return Xdoc.Descendants("BIC").Elements("ProcName").FirstOrDefault().Value;
        }

        /// <summary>
        /// For getting data from data base in multiple tables 
        /// </summary>
        /// <param name="spDOM"></param>
        /// <returns></returns>
        public DataSet GetDS(XDocument spDOM)
        {
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                Database db = factory.Create("Connection String");
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
                    if (element.Name.ToString() != "BIC" && element.Name.ToString() != "XsdName" && element.Name.ToString() != "ProcName" && element.Name.ToString() != "pERRORXML")
                    {
                        parameter.ParameterName = "@" + element.Name.ToString();
                        parameter.Value = element.Value;
                        cmd.Parameters.Add(parameter);
                    }
                }

                return db.ExecuteDataSet(cmd);
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// For getting data in single table from database
        /// </summary>
        /// <param name="spDOM"></param>
        /// <returns></returns>
        public DataTable GetDT(XDocument spDOM)
        {
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                Database db = factory.Create("Connection String");
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
                    if (element.Name.ToString() != "BIC" && element.Name.ToString() != "XsdName" && element.Name.ToString() != "ProcName" && element.Name.ToString() != "pERRORXML")
                    {
                        parameter.ParameterName = "@" + element.Name.ToString();
                        parameter.Value = element.Value;
                        cmd.Parameters.Add(parameter);
                    }
                }

                return db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// for getting single value return from database after performing insert, updste or delete
        /// </summary>
        /// <param name="spDOM"></param>
        /// <returns></returns>
        public string ExecuteNonQuery(XDocument spDOM)
        {
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                Database db = factory.Create("Connection String");
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
            catch (Exception)
            {

                throw;
            }


        }
        /// <summary>
        /// For getting string xml from database 
        /// </summary>
        /// <param name="dom"></param>
        /// <returns></returns>
        public string ExecuteScalarforStringXml(XDocument dom)
        {
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                Database db = factory.Create("Connection String");
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
                    if (element.Name.ToString() != "BIC" && element.Name.ToString() != "XsdName" && element.Name.ToString() != "ProcName" && element.Name.ToString() != "pERRORXML")
                    {
                        parameter.ParameterName = "@" + element.Name.ToString();
                        parameter.Value = element.Value;
                        cmd.Parameters.Add(parameter);
                    }
                }

                var retval = db.ExecuteScalar(cmd);
                return Convert.ToString(retval);
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// for getting integer result form database
        /// </summary>
        /// <param name="dom"></param>
        /// <returns></returns>
        public int ExecuteScalarforInt(XDocument dom)
        {
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                Database db = factory.Create("Connection String");
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
                    if (element.Name.ToString() != "BIC" && element.Name.ToString() != "XsdName" && element.Name.ToString() != "ProcName" && element.Name.ToString() != "pERRORXML")
                    {
                        parameter.ParameterName = "@" + element.Name.ToString();
                        parameter.Value = element.Value;
                        cmd.Parameters.Add(parameter);
                    }
                }

                int retval = Convert.ToInt32(db.ExecuteScalar(cmd));
                return retval;
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// For getting xml from database 
        /// </summary>
        /// <param name="dom"></param>
        /// <returns></returns>
        public string GetXml(XDocument dom)
        {
            try
            {
                DatabaseProviderFactory factory = new DatabaseProviderFactory();
                Database db = factory.Create("Connection String");
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
                    if (element.Name.ToString() != "BIC" && element.Name.ToString() != "XsdName" && element.Name.ToString() != "ProcName" && element.Name.ToString() != "pFILESDATA")
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
            catch (Exception)
            {

                throw;
            }

        }
    }
}
