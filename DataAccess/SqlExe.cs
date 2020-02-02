using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Xml.Linq;

namespace DataAccess
{
    static public class SqlExe
    { 
        static DatabaseProviderFactory factory = new DatabaseProviderFactory();
        static Database db = factory.Create("Connection String");
        static int SetCommandTimeOut()
        {
            string strCmdTime = System.Configuration.ConfigurationManager.AppSettings["SetCommandTimeOut"] == null ? "30" : System.Configuration.ConfigurationManager.AppSettings["SetCommandTimeOut"].ToString();

            if (strCmdTime.Trim() != "")
            {
                return Convert.ToInt32(strCmdTime);
            }
            else
            {
                return 30;
            }
        }

        static public DataSet GetDS(XDocument spDOM)
        {
            string spName = GetprocName(spDOM);
            DbCommand cmd = db.GetStoredProcCommand(spName);
            cmd.CommandTimeout = SetCommandTimeOut();
            DbParameter parameter;
            IEnumerable<XElement> elems = spDOM.Descendants();
            IEnumerable<XElement> elem_list = from elem in elems
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

            return db.ExecuteDataSet(cmd);
        }
        static public DataTable GetDT(XDocument spDOM)
        {
            string spName = GetprocName(spDOM);
            DbCommand cmd = db.GetStoredProcCommand(spName);
            cmd.CommandTimeout = SetCommandTimeOut();
            DbParameter parameter;
            IEnumerable<XElement> elems = spDOM.Descendants();
            IEnumerable<XElement> elem_list = from elem in elems
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

            return db.ExecuteDataSet(cmd).Tables[0];
        }  
        static public string ExecuteNonQuery(XDocument spDOM)
        {

            string spName = GetprocName(spDOM);
            DbCommand cmd = db.GetStoredProcCommand(spName);
            cmd.CommandTimeout = SetCommandTimeOut();
            DbParameter parameter;
            string OutParameterName = "";
            IEnumerable<XElement> elems = spDOM.Descendants();
            IEnumerable<XElement> elem_list = from elem in elems
                                              select elem;
            foreach (XElement element in elem_list)
            {
                parameter = cmd.CreateParameter();
                if (element.Name.ToString() != "BIC" && element.Name.ToString() != "XsdName" && element.Name.ToString() != "procName" && element.Name.ToString() != "pERRORXML")
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
        static public string ExecuteScalarforStringXml(XDocument dom)
        {
            string procName = GetprocName(dom);
            DbCommand cmd = db.GetStoredProcCommand(procName);
            cmd.CommandTimeout = SetCommandTimeOut();
            DbParameter parameter;
            IEnumerable<XElement> elems = dom.Descendants();
            IEnumerable<XElement> elem_list = from elem in elems
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
        static public int ExecuteScalarforInt(XDocument dom)
        {
            string procName = GetprocName(dom);
            DbCommand cmd = db.GetStoredProcCommand(procName);
            cmd.CommandTimeout = SetCommandTimeOut();
            DbParameter parameter;
            IEnumerable<XElement> elems = dom.Descendants();
            IEnumerable<XElement> elem_list = from elem in elems
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
        static public string GetXml(XDocument dom)
        {
            string procName = GetprocName(dom);
            DbCommand cmd = db.GetStoredProcCommand(procName);
            cmd.CommandTimeout = SetCommandTimeOut();
            string OutParameterName = "";
            DbParameter parameter;
            IEnumerable<XElement> elems = dom.Descendants();
            IEnumerable<XElement> elem_list = from elem in elems
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
           // string txtcmd = SqlCommandDumper.GetCommandText((SqlCommand)cmd);
            return Convert.ToString(retval);
        }

        static public string SaveFileInByte(XDocument dom, byte[] Data)
        {
            string procName = GetprocName(dom);
            DbCommand cmd = db.GetStoredProcCommand(procName);
            cmd.CommandTimeout = SetCommandTimeOut();
            string OutParameterName = "";
            DbParameter parameter;
            IEnumerable<XElement> elems = dom.Descendants();
            IEnumerable<XElement> elem_list = from elem in elems
                                              select elem;
            foreach (XElement element in elem_list)
            {
                parameter = cmd.CreateParameter();
                if (element.Name.ToString() != "BIC" && element.Name.ToString() != "XsdName" && element.Name.ToString() != "procName")
                {
                    OutParameterName = element.Name.ToString();
                    parameter.ParameterName = "@" + element.Name.ToString();
                    if (element.Name.ToString().Trim() == "pERRORXML")
                    {
                        parameter.DbType = DbType.Xml;
                        parameter.Direction = ParameterDirection.Output;
                    }
                    else if (element.Name.ToString() == "pFILESDATA" && Data != null)
                    {
                        parameter.Value = Data;
                        parameter.Direction = ParameterDirection.Input;
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
        //static public string SaveFileInByte(XDocument dom, byte[] Data)
        //{

        //    string procName = GetprocName(dom);
        //    SqlParameter parameter;
        //    SqlConnection _con = new SqlConnection(db.ConnectionString);
        //    SqlCommand sqlcmd = new SqlCommand(procName, _con);
        //    sqlcmd.CommandType = CommandType.StoredProcedure;
        //    IEnumerable<XElement> elems = dom.Descendants();
        //    IEnumerable<XElement> elem_list = from elem in elems
        //                                      select elem; 
        //    foreach (XElement element in elem_list)
        //    {
        //        parameter = sqlcmd.CreateParameter();
        //        if (element.Name.ToString() != "BIC" && element.Name.ToString() != "XsdName" && element.Name.ToString() != "procName")
        //        {
        //            parameter.ParameterName = "@" + element.Name.ToString();

        //            if (element.Name.ToString().Trim() == "pERRORXML")
        //            {
        //                parameter.DbType = DbType.Xml;
        //                parameter.Direction = ParameterDirection.Output;
        //            }
        //            else if (element.Name.ToString() == "pFILESDATA" && Data != null)
        //            {
        //                sqlcmd.Parameters.Add(parameter.ParameterName, SqlDbType.VarBinary).Value = Data;
        //                parameter.Direction = ParameterDirection.Input;
        //            }
        //            else
        //            {
        //                parameter.Value = element.Value;
        //                parameter.Direction = ParameterDirection.Input;
        //            } 
        //        }
        //    } 
        //    sqlcmd.Connection.Open();
        //    db.ExecuteNonQuery(sqlcmd);
        //    var retval = db.GetParameterValue(sqlcmd, "@pERRORXML");
        //    return Convert.ToString(retval);
        //}
        static public string GetprocName(XDocument Xdoc)
        {  
            return Xdoc.Descendants("BIC").Elements("procName").FirstOrDefault().Value;
        }

        static public DataSet GetDTOTHERDB(XDocument dom)
        {  
            string spName = GetprocName(dom); 
            DbCommand cmd = db.GetStoredProcCommand(spName);
            cmd.CommandTimeout = SetCommandTimeOut();
            DbParameter parameter;
            IEnumerable<XElement> elems = dom.Descendants();
            IEnumerable<XElement> elem_list = from elem in elems
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

            return db.ExecuteDataSet(cmd);
        }
        static public DataSet GetDSFromQuery(string CMD)
        {
            DbCommand cmd = db.GetSqlStringCommand(CMD);
            cmd.CommandTimeout = SetCommandTimeOut();
            return db.ExecuteDataSet(cmd);
        }
       
    }
}
