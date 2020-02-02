using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataModel
{
    static public class DBXML
    {
        //Xml for login and user authentication
        static public XDocument userAuthentication_g(int flag, string loginId, string password, int controlNo, int posId, int orgStructId, string compName, string compUser, string compIpAdress, string browserName, string browServer, string ips, string isDomainValid, string email)
        {
            //USERAUTHENTICATION_g    
            XElement logXml = loginXml(compName, compUser, compIpAdress, browserName, browServer, ips, isDomainValid, email);

            XElement MAINXML = new XElement("SPXML",
               new XElement("SPDETAILS",
               new XAttribute("flag", flag),
               new XAttribute("loginId", loginId),
               new XAttribute("password", password),
               new XAttribute("controlNo", controlNo),
               new XAttribute("posId", posId),
               new XAttribute("orgStructId", orgStructId)
               ));
            XDocument CreateXml = CommonXML("userAuthentication_g", MAINXML, logXml);
            return CreateXml;
        }
        /// <summary>
        /// Common xml function for all sp should be call in all functions
        /// </summary>
        /// <param name="spName">Store procedure Name</param>
        /// <param name="spDetails">Store Procedure required parameters</param>
        /// <param name="logInfo">log xml for user details</param>
        /// <returns></returns>
        static XDocument CommonXML(string spName, XElement spDetails, XElement logInfo)
        {
            XElement createXml = new XElement("XMLFILE");
            createXml.Add(spDetails);
            createXml.Add(logInfo);

            XDocument retXml = new XDocument(
              new XDeclaration("1.0", "utf-8", ""),
              new XElement("BIC",
              new XElement("procName", spName),
              new XElement("pXMLFILE", Convert.ToString(createXml)),
              new XElement("pERRORXML", "")
               ));
            return retXml;
        }
        /// <summary>
        /// Xml for maintaning user log information 
        /// </summary>
        /// <param name="compName">User computer name</param>
        /// <param name="compUser">User name on that computer</param>
        /// <param name="compIpAddress">computer ip address</param>
        /// <param name="browserName">browser name</param>
        /// <param name="browServer">browser server</param>
        /// <param name="ips"></param>
        /// <param name="isDomainValid">valid domain or not</param>
        /// <param name="email">email</param>
        /// <param name="userId">user id</param>
        /// <returns></returns>
        static public XElement loginXml(string compName, string compUser, string compIpAddress, string browserName, string browServer, string ips, string isDomainValid, string email, int userId = 0)
        {
            XElement createXml = new XElement("loginLog");
            createXml.SetAttributeValue("compName", compName);
            createXml.SetAttributeValue("compUser", compUser);
            createXml.SetAttributeValue("compIpAddress", compIpAddress);
            createXml.SetAttributeValue("browserName", browserName);
            createXml.SetAttributeValue("browServer", browServer);
            createXml.SetAttributeValue("ips", ips);
            createXml.SetAttributeValue("isDomainValid", isDomainValid == null ? "" : isDomainValid);
            createXml.SetAttributeValue("email", email == null ? "" : email);
            createXml.SetAttributeValue("userId", userId);
            return createXml;
        }

        #region C section
        static public XDocument ST_workingCalendar_C(string flag, int MID, int year, int userId, XElement xmlWorkDays, XElement LOGXML)
        {
            XElement MAINXML = new XElement("SPXML",
           new XElement("SPDETAILS",
           new XAttribute("flag", flag),
           new XAttribute("MID", MID),
           new XAttribute("year", year),
           new XAttribute("userId", userId)
           ));
            MAINXML.Add(xmlWorkDays);
            XDocument CreateXml = CommonXML("ST_workingCalendar_C", MAINXML, LOGXML);
            return CreateXml;
        }
        static public XDocument ST_PriceListMapping_C(string flag, int userId, XElement xmlWorkDays, XElement LOGXML)
        {
            XElement MAINXML = new XElement("SPXML",
           new XElement("SPDETAILS",
           new XAttribute("FLAG", flag),
           new XAttribute("USERID", userId)
           ));
            MAINXML.Add(xmlWorkDays);
            XDocument CreateXml = CommonXML("ST_PriceListMapping_C", MAINXML, LOGXML);
            return CreateXml;
        }
        static public XDocument ST_Invoice_C(string FLAG, int MID, string INVCNUMB, DateTime INVCDATE, int JOBID, string VOYAGENO, string VOYAGEDESC, string VESSEL, DateTime ETA, DateTime ETD, int CUSTOMERID, string CUSTOMER, string ADDRESSDESC, string CURRENCY, decimal CONVRATE, int USERID, int MENUID, XElement TRANSXML, XElement LOGXML)
        {
            XElement MAINXML = new XElement("SPXML",
           new XElement("SPDETAILS",
           new XAttribute("FLAG", FLAG),
           new XAttribute("MID", MID),
           new XAttribute("INVCNUMB", INVCNUMB),
           new XAttribute("INVCDATE", INVCDATE),
           new XAttribute("JOBID", JOBID),
           new XAttribute("VOYAGENO", VOYAGENO),
           new XAttribute("VOYAGEDESC", VOYAGEDESC),
           new XAttribute("VESSEL", VESSEL),
           new XAttribute("ETA", ETA),
           new XAttribute("ETD", ETD),
           new XAttribute("CUSTOMERID", CUSTOMERID),
           new XAttribute("CUSTOMER", CUSTOMER),
           new XAttribute("ADDRESSDESC", ADDRESSDESC),
           new XAttribute("CURRENCY", CURRENCY),
           new XAttribute("CONVRATE", CONVRATE),
           new XAttribute("USERID", USERID),
           new XAttribute("MENUID", MENUID)
           ));
            MAINXML.Add(TRANSXML);
            XDocument CreateXml = CommonXML("ST_Invoice_C", MAINXML, LOGXML);
            return CreateXml;
        }
        static public XDocument st_ShInvoice_C(string FLAG, int MID, string INVCNUMB, DateTime INVCDATE, int JOBID, string VOYAGENO, string VOYAGEDESC, string VESSEL, DateTime ETA, DateTime ETD, int CUSTOMERID, string CUSTOMER, string ADDRESSDESC, string CURRENCY, decimal CONVRATE, int USERID, int MENUID, XElement TRANSXML, XElement LOGXML)
        {
            XElement MAINXML = new XElement("SPXML",
           new XElement("SPDETAILS",
           new XAttribute("FLAG", FLAG),
           new XAttribute("MID", MID),
           new XAttribute("INVCNUMB", INVCNUMB),
           new XAttribute("INVCDATE", INVCDATE),
           new XAttribute("JOBID", JOBID),
           new XAttribute("VOYAGENO", VOYAGENO),
           new XAttribute("VESSEL", VESSEL),
           new XAttribute("VOYAGEDESC", VOYAGEDESC),
           new XAttribute("ETA", ETA),
           new XAttribute("ETD", ETD),
           new XAttribute("CUSTOMERID", CUSTOMERID),
           new XAttribute("CUSTOMER", CUSTOMER),
           new XAttribute("ADDRESSDESC", ADDRESSDESC),
           new XAttribute("CURRENCY", CURRENCY),
           new XAttribute("CONVRATE", CONVRATE),
           new XAttribute("USERID", USERID),
           new XAttribute("MENUID", MENUID)
           ));
            MAINXML.Add(TRANSXML);
            XDocument CreateXml = CommonXML("st_ShInvoice_C", MAINXML, LOGXML);
            return CreateXml;
        }


        static public XDocument st_AgencyFeeInvoice_C(string FLAG, int MID, string INVCNUMB, DateTime INVCDATE, int JOBID, string VOYAGENO, string VOYAGEDESC, string VESSELTYPE, string VESSELNAME, DateTime AADATE, DateTime ADDATE, int CUSTOMERID, string CUSTOMER, string ADDRESSDESC, string CURRENCY, string InvcType,
            string PSItemCode1, string PSItemCode2, string PSItemCode3, string RVItemCode1, string RVItemCode2, string TransportGorup, string CrewChangeGroup, int USERID, int MENUID, XElement TRANSXML, XElement LOGXML)
        {
            XElement MAINXML = new XElement("SPXML",
           new XElement("SPDETAILS",
           new XAttribute("FLAG", FLAG),
           new XAttribute("MID", MID),
           new XAttribute("INVCNUMB", INVCNUMB),
           new XAttribute("INVCDATE", INVCDATE),
           new XAttribute("JOBID", JOBID),
           new XAttribute("VOYAGENO", VOYAGENO),
           new XAttribute("VOYAGEDESC", VOYAGEDESC),
           new XAttribute("VESSELTYPE", VESSELTYPE),
           new XAttribute("VESSELNAME", VESSELNAME),
           new XAttribute("AADATE", AADATE),
           new XAttribute("ADDATE", ADDATE),
           new XAttribute("CUSTOMERID", CUSTOMERID),
           new XAttribute("CUSTOMER", CUSTOMER),
           new XAttribute("ADDRESSDESC", ADDRESSDESC),
           new XAttribute("CURRENCY", CURRENCY),
           new XAttribute("INVCTYPE", InvcType),
           new XAttribute("PSItemCode1", PSItemCode1),
           new XAttribute("PSItemCode2", PSItemCode2),
           new XAttribute("PSItemCode3", PSItemCode3),
           new XAttribute("RVItemCode1", RVItemCode1),
           new XAttribute("RVItemCode2", RVItemCode2),
           new XAttribute("TransportGorup", TransportGorup),
           new XAttribute("CrewChangeGroup", CrewChangeGroup),
           new XAttribute("USERID", USERID),
           new XAttribute("MENUID", MENUID)
           ));
            MAINXML.Add(TRANSXML);
            XDocument CreateXml = CommonXML("st_AgencyFeeInvoice_C", MAINXML, LOGXML);
            return CreateXml;
        }

        static public XDocument st_AgencyFeeInvoiceTnas_C(string FLAG, int USERID, XElement TRANSXML, XElement LOGXML)
        {
            XElement MAINXML = new XElement("SPXML",
           new XElement("SPDETAILS",
           new XAttribute("FLAG", FLAG),
           new XAttribute("USERID", USERID)
           ));
            MAINXML.Add(TRANSXML);
            XDocument CreateXml = CommonXML("st_AgencyFeeInvoiceTnas_C", MAINXML, LOGXML);
            return CreateXml;
        }


        static public XDocument ST_InvoiceTrans_C(string FLAG, int TID, int RMID, DateTime TRANSDATE, int ITEMID, int DAYTYPE, string ITEMDESC, string ITEMCODE, bool ISOVERTIME, decimal QTY1, string UOM1, decimal QTY2, string UOM2, decimal RATE, decimal TAX, decimal TAXAMOUNT, decimal AMOUNT, string REMARK,
           decimal DAYTYPE1MINCHARGE, decimal DAYTYPE2MINCHARGE, decimal DAYTYPE3MINCHARGE, decimal DAYTYPE4MINCHARGE, decimal DAYTYPE1RATE, decimal DAYTYPE2RATE, decimal DAYTYPE3RATE, decimal DAYTYPE4RATE, int ISMINCHRGAPPL, int USERID, int MENUID, XElement LOGXML)
        {
            XElement MAINXML = new XElement("SPXML",
           new XElement("SPDETAILS",
           new XAttribute("FLAG", FLAG),
           new XAttribute("TID", TID),
           new XAttribute("RMID", RMID),
           new XAttribute("TRANSDATE", TRANSDATE),
           new XAttribute("ITEMID", ITEMID),
           new XAttribute("ITEMDESC", ITEMDESC),
           new XAttribute("ITEMCODE", ITEMCODE),
           new XAttribute("ISOVERTIME", ISOVERTIME),
           new XAttribute("DAYTYPE", DAYTYPE),
           new XAttribute("QTY1", QTY1),
           new XAttribute("UOM1", UOM1),
           new XAttribute("QTY2", QTY2),
           new XAttribute("UOM2", UOM2),
           new XAttribute("RATE", RATE),
           new XAttribute("TAX", TAX),
           new XAttribute("TAXAMOUNT", TAXAMOUNT),
           new XAttribute("AMOUNT", AMOUNT),
           new XAttribute("REMARK", REMARK),
           new XAttribute("DAYTYPE1RATE", DAYTYPE1RATE),
           new XAttribute("DAYTYPE2RATE", DAYTYPE2RATE),
           new XAttribute("DAYTYPE3RATE", DAYTYPE3RATE),
           new XAttribute("DAYTYPE4RATE", DAYTYPE4RATE),
           new XAttribute("DAYTYPE1MINCHARGE", DAYTYPE1MINCHARGE),
           new XAttribute("DAYTYPE2MINCHARGE", DAYTYPE2MINCHARGE),
           new XAttribute("DAYTYPE3MINCHARGE", DAYTYPE3MINCHARGE),
           new XAttribute("DAYTYPE4MINCHARGE", DAYTYPE4MINCHARGE),
           new XAttribute("ISMINCHRGAPPL", ISMINCHRGAPPL),
           new XAttribute("USERID", USERID),
           new XAttribute("MENUID", MENUID)
           ));
            XDocument CreateXml = CommonXML("ST_InvoiceTrans_C", MAINXML, LOGXML);
            return CreateXml;
        }

        static public XDocument ST_ShInvoiceTrans_C(string FLAG, int TID, int RMID, DateTime TRANSDATE, int ITEMID, int DAYTYPE, string ITEMDESC, string ITEMCODE, bool ISOVERTIME, decimal QTY1, string UOM1, decimal QTY2, string UOM2, decimal RATE, decimal TAX, decimal TAXAMOUNT, decimal AMOUNT, string REMARK,
           decimal DAYTYPE1MINCHARGE, decimal DAYTYPE2MINCHARGE, decimal DAYTYPE3MINCHARGE, decimal DAYTYPE4MINCHARGE, decimal DAYTYPE1RATE, decimal DAYTYPE2RATE, decimal DAYTYPE3RATE, decimal DAYTYPE4RATE, int ISMINCHRGAPPL, int USERID, int MENUID, XElement LOGXML)
        {
            XElement MAINXML = new XElement("SPXML",
           new XElement("SPDETAILS",
           new XAttribute("FLAG", FLAG),
           new XAttribute("TID", TID),
           new XAttribute("RMID", RMID),
           new XAttribute("TRANSDATE", TRANSDATE),
           new XAttribute("ITEMID", ITEMID),
           new XAttribute("ITEMDESC", ITEMDESC),
           new XAttribute("ITEMCODE", ITEMCODE),
           new XAttribute("ISOVERTIME", ISOVERTIME),
           new XAttribute("DAYTYPE", DAYTYPE),
           new XAttribute("QTY1", QTY1),
           new XAttribute("UOM1", UOM1),
           new XAttribute("QTY2", QTY2),
           new XAttribute("UOM2", UOM2),
           new XAttribute("RATE", RATE),
           new XAttribute("TAX", TAX),
           new XAttribute("TAXAMOUNT", TAXAMOUNT),
           new XAttribute("AMOUNT", AMOUNT),
           new XAttribute("REMARK", REMARK),
           new XAttribute("DAYTYPE1RATE", DAYTYPE1RATE),
           new XAttribute("DAYTYPE2RATE", DAYTYPE2RATE),
           new XAttribute("DAYTYPE3RATE", DAYTYPE3RATE),
           new XAttribute("DAYTYPE4RATE", DAYTYPE4RATE),
           new XAttribute("DAYTYPE1MINCHARGE", DAYTYPE1MINCHARGE),
           new XAttribute("DAYTYPE2MINCHARGE", DAYTYPE2MINCHARGE),
           new XAttribute("DAYTYPE3MINCHARGE", DAYTYPE3MINCHARGE),
           new XAttribute("DAYTYPE4MINCHARGE", DAYTYPE4MINCHARGE),
           new XAttribute("ISMINCHRGAPPL", ISMINCHRGAPPL),
           new XAttribute("USERID", USERID),
           new XAttribute("MENUID", MENUID)
           ));
            XDocument CreateXml = CommonXML("ST_ShInvoiceTrans_C", MAINXML, LOGXML);
            return CreateXml;
        }


        static public XDocument st_AgencyFeeInvoiceTrans_C(string FLAG, int TID, int RMID, DateTime TRANSDATE, int ITEMID, int DAYTYPE, string ITEMDESC, decimal QTY, string UOM, decimal RATE, decimal TAX, decimal TAXAMOUNT, decimal AMOUNT, string REMARK,
       decimal DAYTYPE1MINCHARGE, decimal DAYTYPE2MINCHARGE, decimal DAYTYPE3MINCHARGE, decimal DAYTYPE4MINCHARGE, decimal DAYTYPE1RATE, decimal DAYTYPE2RATE, decimal DAYTYPE3RATE, decimal DAYTYPE4RATE, int USERID, int MENUID, XElement LOGXML)
        {
            XElement MAINXML = new XElement("SPXML",
           new XElement("SPDETAILS",
           new XAttribute("FLAG", FLAG),
           new XAttribute("TID", TID),
           new XAttribute("RMID", RMID),
           new XAttribute("TRANSDATE", TRANSDATE),
           new XAttribute("ITEMID", ITEMID),
           new XAttribute("ITEMDESC", ITEMDESC),
           new XAttribute("DAYTYPE", DAYTYPE),
           new XAttribute("QTY", QTY),
           new XAttribute("UOM", UOM),
           new XAttribute("RATE", RATE),
           new XAttribute("TAX", TAX),
           new XAttribute("TAXAMOUNT", TAXAMOUNT),
           new XAttribute("AMOUNT", AMOUNT),
           new XAttribute("REMARK", REMARK),
           new XAttribute("DAYTYPE1RATE", DAYTYPE1RATE),
           new XAttribute("DAYTYPE2RATE", DAYTYPE2RATE),
           new XAttribute("DAYTYPE3RATE", DAYTYPE3RATE),
           new XAttribute("DAYTYPE4RATE", DAYTYPE4RATE),
           new XAttribute("DAYTYPE1MINCHARGE", DAYTYPE1MINCHARGE),
           new XAttribute("DAYTYPE2MINCHARGE", DAYTYPE2MINCHARGE),
           new XAttribute("DAYTYPE3MINCHARGE", DAYTYPE3MINCHARGE),
           new XAttribute("DAYTYPE4MINCHARGE", DAYTYPE4MINCHARGE),
           new XAttribute("USERID", USERID),
           new XAttribute("MENUID", MENUID)
           ));
            XDocument CreateXml = CommonXML("st_AgencyFeeInvoiceTrans_C", MAINXML, LOGXML);
            return CreateXml;
        }


        static public XDocument dbConfig_C(string flag, int dbConfigId, string dbName, string sqlDBAuthType, string dbServerName, string dbUserId, string dbPassword, string dbDatabaseName,
            string sqlDBCommAuthType, string dbCommServerName, string dbCommDatabaseName, string dbCommUserId, string dbCommPassword, string serialNumber, string authCode, bool isActive,
            string adminGroup,
            int USERID, XElement LOGXML)
        {
            XElement MAINXML = new XElement("SPXML",
           new XElement("SPDETAILS",
           new XAttribute("flag", flag),
           new XAttribute("dbConfigId", dbConfigId),
           new XAttribute("dbName", dbName ?? ""),
           new XAttribute("dbServerName", dbServerName ?? ""),
           new XAttribute("sqlDBAuthType", sqlDBAuthType ?? ""),
           new XAttribute("dbUserId", dbUserId ?? ""),
           new XAttribute("dbPassword", dbPassword ?? ""),
           new XAttribute("dbDatabaseName", dbDatabaseName ?? ""),
           new XAttribute("dbCommServerName", dbCommServerName ?? ""),
           new XAttribute("sqlDBCommAuthType", sqlDBCommAuthType ?? ""),
           new XAttribute("dbCommDatabaseName", dbCommDatabaseName ?? ""),
           new XAttribute("dbCommUserId", dbCommUserId ?? ""),
           new XAttribute("dbCommPassword", dbCommPassword ?? ""),
           new XAttribute("serialNumber", serialNumber ?? ""),
           new XAttribute("authCode", authCode ?? ""),
           new XAttribute("isActive", isActive),
           new XAttribute("adminGroup", adminGroup ?? ""),
           new XAttribute("USERID", USERID)
           ));
            XDocument CreateXml = CommonXML("dbConfig_C", MAINXML, LOGXML);
            return CreateXml;
        }



        static public XDocument ST_holidayCalendar_C(string flag, int MID, DateTime holidayDate, string description, int year, bool isActive, int userId, XElement LOGXML)
        {
            XElement MAINXML = new XElement("SPXML",
           new XElement("SPDETAILS",
           new XAttribute("flag", flag),
           new XAttribute("MID", MID),
           new XAttribute("holidayDate", holidayDate),
           new XAttribute("description", description),
           new XAttribute("isActive", isActive),
           new XAttribute("year", year),
           new XAttribute("userId", userId)
           ));
            XDocument CreateXml = CommonXML("ST_holidayCalendar_C", MAINXML, LOGXML);
            return CreateXml;
        }


        #endregion


        #region G Saction


        static public XDocument userMaster_g(int userId, string loginId, string password, XElement LOGXML)
        {
            XElement MAINXML = new XElement("SPXML",
           new XElement("SPDETAILS",
           new XAttribute("userId", userId),
           new XAttribute("loginId", loginId),
           new XAttribute("password", password)
           ));
            XDocument CreateXml = CommonXML("userMaster_g", MAINXML, LOGXML);
            return CreateXml;
        }
        static public XDocument dbConfig_g(int dbConfigId, string dbName, int userId, int flag, XElement LOGXML)
        {
            XElement MAINXML = new XElement("SPXML",
           new XElement("SPDETAILS",
           new XAttribute("dbConfigId", dbConfigId),
           new XAttribute("flag", flag),
           new XAttribute("dbName", dbName),
           new XAttribute("userId", userId)
           ));
            XDocument CreateXml = CommonXML("dbConfig_g", MAINXML, LOGXML);
            return CreateXml;
        }
        static public XDocument serviceConfig_g(int serviceId, string serviceName, int userId, XElement LOGXML)
        {
            XElement MAINXML = new XElement("SPXML",
           new XElement("SPDETAILS",
           new XAttribute("serviceId", serviceId),
           new XAttribute("serviceName", serviceName),
           new XAttribute("userId", userId)
           ));
            XDocument CreateXml = CommonXML("serviceConfig_g", MAINXML, LOGXML);
            return CreateXml;
        }


        static public XDocument ST_workingCaledar_G(int flag, int MID, string desc, int userId, XElement LOGXML)
        {
            XElement MAINXML = new XElement("SPXML",
           new XElement("SPDETAILS",
           new XAttribute("flag", flag),
           new XAttribute("MID", MID),
           new XAttribute("desc", desc),
           new XAttribute("userId", userId)
           ));
            XDocument CreateXml = CommonXML("ST_workingCaledar_G", MAINXML, LOGXML);
            return CreateXml;
        }


        static public XDocument ST_INVOICE_g(int FLAG, int MID, string DESC, int USERID, XElement LOGXML)
        {
            XElement MAINXML = new XElement("SPXML",
           new XElement("SPDETAILS",
           new XAttribute("FLAG", FLAG),
           new XAttribute("MID", MID),
           new XAttribute("DESC", DESC),
           new XAttribute("USERID", USERID)
           ));
            XDocument CreateXml = CommonXML("ST_INVOICE_g", MAINXML, LOGXML);
            return CreateXml;
        }
        static public XDocument st_ShInvoice_g(int FLAG, int MID, string DESC, int USERID, XElement LOGXML)
        {
            XElement MAINXML = new XElement("SPXML",
           new XElement("SPDETAILS",
           new XAttribute("FLAG", FLAG),
           new XAttribute("MID", MID),
           new XAttribute("DESC", DESC),
           new XAttribute("USERID", USERID)
           ));
            XDocument CreateXml = CommonXML("st_ShInvoice_g", MAINXML, LOGXML);
            return CreateXml;
        }

        static public XDocument st_AgencyFeeInvoice_g(int FLAG, int MID, string DESC, int USERID, XElement LOGXML)
        {
            XElement MAINXML = new XElement("SPXML",
           new XElement("SPDETAILS",
           new XAttribute("FLAG", FLAG),
           new XAttribute("MID", MID),
           new XAttribute("DESC", DESC),
           new XAttribute("USERID", USERID)
           ));
            XDocument CreateXml = CommonXML("st_AgencyFeeInvoice_g", MAINXML, LOGXML);
            return CreateXml;
        }


        static public XDocument INVOICEDOCAMOUNT_g(int MID, int USERID, XElement LOGXML)
        {
            XElement MAINXML = new XElement("SPXML",
           new XElement("SPDETAILS",
           new XAttribute("MID", MID),
           new XAttribute("USERID", USERID)
           ));
            XDocument CreateXml = CommonXML("INVOICEDOCAMOUNT_g", MAINXML, LOGXML);
            return CreateXml;
        }
        static public XDocument st_ShInvoiceDocAmount_g(int MID, int USERID, XElement LOGXML)
        {
            XElement MAINXML = new XElement("SPXML",
           new XElement("SPDETAILS",
           new XAttribute("MID", MID),
           new XAttribute("USERID", USERID)
           ));
            XDocument CreateXml = CommonXML("st_ShInvoiceDocAmount_g", MAINXML, LOGXML);
            return CreateXml;
        }
        static public XDocument ST_INVOICETRANS_g(int FLAG, int MID, int TID, string DESC, int USERID, XElement LOGXML)
        {
            XElement MAINXML = new XElement("SPXML",
           new XElement("SPDETAILS",
           new XAttribute("FLAG", FLAG),
           new XAttribute("MID", MID),
            new XAttribute("TID", TID),
           new XAttribute("DESC", DESC),
           new XAttribute("USERID", USERID)
           ));
            XDocument CreateXml = CommonXML("ST_INVOICETRANS_g", MAINXML, LOGXML);
            return CreateXml;
        }
        static public XDocument st_ShInvoiceTrans_g(int FLAG, int MID, int TID, string DESC, int USERID, XElement LOGXML)
        {
            XElement MAINXML = new XElement("SPXML",
           new XElement("SPDETAILS",
           new XAttribute("FLAG", FLAG),
           new XAttribute("MID", MID),
            new XAttribute("TID", TID),
           new XAttribute("DESC", DESC),
           new XAttribute("USERID", USERID)
           ));
            XDocument CreateXml = CommonXML("st_ShInvoiceTrans_g", MAINXML, LOGXML);
            return CreateXml;
        }

        static public XDocument st_AgencyInvoiceTrans_g(int FLAG, int MID, int TID, string DESC, int USERID, XElement LOGXML)
        {
            XElement MAINXML = new XElement("SPXML",
           new XElement("SPDETAILS",
           new XAttribute("FLAG", FLAG),
           new XAttribute("MID", MID),
            new XAttribute("TID", TID),
           new XAttribute("DESC", DESC),
           new XAttribute("USERID", USERID)
           ));
            XDocument CreateXml = CommonXML("st_AgencyInvoiceTrans_g", MAINXML, LOGXML);
            return CreateXml;
        }

        static public XDocument ST_INVOICETRANSDTLPOSTING_g(int MID, int USERID, XElement LOGXML)
        {
            XElement MAINXML = new XElement("SPXML",
           new XElement("SPDETAILS",
           new XAttribute("MID", MID),
           new XAttribute("USERID", USERID)
           ));
            XDocument CreateXml = CommonXML("ST_INVOICETRANSDTLPOSTING_g", MAINXML, LOGXML);
            return CreateXml;
        }
        static public XDocument st_ShInvoiceTransDtlPosting_g(int MID, int USERID, XElement LOGXML)
        {
            XElement MAINXML = new XElement("SPXML",
           new XElement("SPDETAILS",
           new XAttribute("MID", MID),
           new XAttribute("USERID", USERID)
           ));
            XDocument CreateXml = CommonXML("st_ShInvoiceTransDtlPosting_g", MAINXML, LOGXML);
            return CreateXml;
        }

        static public XDocument st_AgencyInvTransDtlPosting_g(int MID, int USERID, XElement LOGXML)
        {
            XElement MAINXML = new XElement("SPXML",
           new XElement("SPDETAILS",
           new XAttribute("MID", MID),
           new XAttribute("USERID", USERID)
           ));
            XDocument CreateXml = CommonXML("st_AgencyInvTransDtlPosting_g", MAINXML, LOGXML);
            return CreateXml;
        }


        static public XDocument holidayCalendar_G(int flag, int MID, string desc, int userId, XElement LOGXML)
        {
            XElement MAINXML = new XElement("SPXML",
           new XElement("SPDETAILS",
           new XAttribute("flag", flag),
           new XAttribute("MID", MID),
           new XAttribute("desc", desc),
           new XAttribute("userId", userId)
           ));
            XDocument CreateXml = CommonXML("holidayCalendar_G", MAINXML, LOGXML);
            return CreateXml;
        }

        static public XDocument STATUSMASTER_h(int TYPEID, int STATUSID, string DESC, int Flag, XElement LOGXML, string CONDITION)
        {
            XElement MAINXML = new XElement("SPXML",
            new XElement("SPDETAILS",
            new XAttribute("TYPEID", TYPEID),
            new XAttribute("FLAG", Flag),
            new XAttribute("STATUSID", STATUSID),
            new XAttribute("CONDITION", CONDITION),
            new XAttribute("DESC", DESC)));
            XDocument CreateXml = CommonXML("STATUSMASTER_h", MAINXML, LOGXML);//NO
            return CreateXml;
        }

        static public XDocument ST_SYSCONTROLENO_h(int CONTROLNO, string DESC, int FLAG, XElement LOGXML)
        {
            XElement MAINXML = new XElement("SPXML",
            new XElement("SPDETAILS",
            new XAttribute("CONTROLNO", CONTROLNO),
            new XAttribute("DESC", DESC),
            new XAttribute("FLAG", FLAG)));
            XDocument CreateXml = CommonXML("ST_SYSCONTROLENO_h", MAINXML, LOGXML);
            return CreateXml;
        }

        static public XDocument ST_CHECKDATE_g(DateTime SERVICEDATE, int USERID, XElement LOGXML)
        {
            XElement MAINXML = new XElement("SPXML",
            new XElement("SPDETAILS",
            new XAttribute("SERVICEDATE", SERVICEDATE),
            new XAttribute("USERID", USERID)));
            XDocument CreateXml = CommonXML("ST_CHECKDATE_g", MAINXML, LOGXML);
            return CreateXml;
        }

        static public XDocument st_dashBoardCount(DateTime fromDate, DateTime toDate, bool isStevedoringAppl, bool isShoreHandlingAppl, bool isAgencyFeeAppl, XElement LOGXML)
        {
            XElement MAINXML = new XElement("SPXML",
            new XElement("SPDETAILS",
            new XAttribute("fromDate", fromDate),
            new XAttribute("toDate", toDate),
            new XAttribute("isStevedoringAppl", isStevedoringAppl),
            new XAttribute("isShoreHandlingAppl", isShoreHandlingAppl),
            new XAttribute("isAgencyFeeAppl", isAgencyFeeAppl)));
            XDocument CreateXml = CommonXML("st_dashBoardCount", MAINXML, LOGXML);
            return CreateXml;
        }

        static public XDocument companyMaster_h(int COMPID, string DESC, int Flag, XElement LOGXML)
        {
            XElement MAINXML = new XElement("SPXML",
            new XElement("SPDETAILS",
            new XAttribute("COMPID", COMPID),
            new XAttribute("DESC", DESC)));
            XDocument CreateXml = CommonXML("companyMaster_h", MAINXML, LOGXML);
            return CreateXml;
        }

        static public XDocument ST_GetLookupData(int MENUID, int flag, int USERID, int PAGENO, XElement FILTERXML, XElement LOGXML)
        {
            XElement MAINXML = new XElement("SPXML",
            new XElement("SPDETAILS",
            new XAttribute("MENUID", MENUID),
            new XAttribute("flag", flag),
            new XAttribute("USERID", USERID),
            new XAttribute("PAGENO", PAGENO)));
            MAINXML.Add(FILTERXML);
            XDocument CreateXml = CommonXML("ST_GetLookupData", MAINXML, LOGXML);
            return CreateXml;
        }

        static public XDocument ST_Invoice_Export(int MENUID, int flag, int USERID, int PAGENO, XElement FILTERXML, XElement LOGXML)
        {
            XElement MAINXML = new XElement("SPXML",
            new XElement("SPDETAILS",
            new XAttribute("MENUID", MENUID),
            new XAttribute("flag", flag),
            new XAttribute("USERID", USERID),
            new XAttribute("PAGENO", PAGENO)));
            MAINXML.Add(FILTERXML);
            XDocument CreateXml = CommonXML("ST_Invoice_Export", MAINXML, LOGXML);
            return CreateXml;
        }


        //For Company Initial Config
        static public XDocument ComInitialConfig_g(string COMPCODE, string DEVICEID, string APPID, XElement LOGXML)
        {
            XElement MAINXML = new XElement("SPXML",
           new XElement("SPDETAILS",
           new XAttribute("COMPCODE", COMPCODE),
           new XAttribute("DEVICEID", DEVICEID),
           new XAttribute("APPID", APPID)
           ));
            XDocument CreateXml = CommonXML("GetClientDetails_g", MAINXML, LOGXML);//NO
            return CreateXml;
        }

        //Save token into Tokens table
        static public XDocument Token_c(string FLAG, int USERID, string TOKEN, DateTime ISSUEDON, DateTime EXPIREDON, int ISDOMAIN, XElement LOGXML = null)
        {
            XElement MAINXML = new XElement("SPXML",
           new XElement("SPDETAILS",
           new XAttribute("FLAG", FLAG),
           new XAttribute("USERID", USERID),
           new XAttribute("TOKEN", TOKEN),
           new XAttribute("ISSUEDON", ISSUEDON),
           new XAttribute("EXPIREDON", EXPIREDON),
           new XAttribute("ISDOMAIN", ISDOMAIN)
           ));
            XDocument CreateXml = CommonXML("Token_c", MAINXML, LOGXML);
            return CreateXml;
        }

        //Validate Token from tbl
        static public XDocument ValidateToken_c(string FLAG, string TOKEN, XElement LOGXML = null)
        {
            XElement MAINXML = new XElement("SPXML",
           new XElement("SPDETAILS",
           new XAttribute("FLAG", FLAG),
           new XAttribute("TOKEN", TOKEN)
           ));
            XDocument CreateXml = CommonXML("Token_c", MAINXML, LOGXML);
            return CreateXml;
        }

        //Get token details (need to write new sp for that, we don't have code for this sp)
        static public XDocument GetTokenDetails_g(string TOKEN, XElement LOGXML = null)//NO
        {
            XElement MAINXML = new XElement("SPXML",
           new XElement("SPDETAILS",
           new XAttribute("TOKEN", TOKEN)
           ));
            XDocument CreateXml = CommonXML("GetTokenDetails_g", MAINXML, LOGXML);//GetTokenDetails_g sp needs to create 
            return CreateXml;
        }

        //Get token details (need to write new sp for that, we don't have code for this sp)
        static public XDocument ST_PriceListMapping_g(int MAPPINGID, int USERID, XElement LOGXML = null)
        {
            XElement MAINXML = new XElement("SPXML",
           new XElement("SPDETAILS",
           new XAttribute("MAPPINGID", MAPPINGID),
           new XAttribute("USERID", USERID)
           ));
            XDocument CreateXml = CommonXML("ST_PriceListMapping_g", MAINXML, LOGXML);//GetTokenDetails_g sp needs to create 
            return CreateXml;
        }
        //Get token details (need to write new sp for that, we don't have code for this sp)
        static public XDocument CLIENTCONNECTION_h(int COMPID, int USERID, XElement LOGXML = null)
        {
            XElement MAINXML = new XElement("SPXML",
           new XElement("SPDETAILS",
           new XAttribute("COMPID", COMPID),
           new XAttribute("USERID", USERID)
           ));
            XDocument CreateXml = CommonXML("CLIENTCONNECTION_h", MAINXML, LOGXML);//GetTokenDetails_g sp needs to create 
            return CreateXml;
        }


        #endregion




    }
}
