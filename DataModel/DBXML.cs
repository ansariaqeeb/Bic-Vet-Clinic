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
        static public XDocument petMaster_C(string flag, int Id, int branchId, string code, string petName, string registrationNo, DateTime dob, string sex, string species, string breed, bool isSterilized, bool isActive, int userId, XElement LOGXML)
        {
            XElement MAINXML = new XElement("SPXML",
           new XElement("SPDETAILS",
           new XAttribute("flag", flag),
           new XAttribute("Id", Id),
           new XAttribute("branchId", branchId),
           new XAttribute("code", code),
           new XAttribute("petName", petName),
           new XAttribute("registrationNo", registrationNo),
           new XAttribute("dob", dob),
           new XAttribute("sex", sex),
           new XAttribute("species", species),
           new XAttribute("breed", breed),
           new XAttribute("isActive", isActive),
           new XAttribute("isSterilized", isSterilized),
           new XAttribute("USERID", userId)
           ));
            XDocument CreateXml = CommonXML("petMaster_C", MAINXML, LOGXML);
            return CreateXml;
        }


        static public XDocument bookAppointment_C(string flag, int Id, int branchId, string bookingNo, string custCode, string docCode, string petCode, DateTime bookDateTime, string bookingStatus, bool isActive, int userId, XElement LOGXML)
        {
            XElement MAINXML = new XElement("SPXML",
           new XElement("SPDETAILS",
           new XAttribute("flag", flag),
           new XAttribute("Id", Id),
           new XAttribute("branchId", branchId),
           new XAttribute("bookingNo", bookingNo),
           new XAttribute("custCode", custCode),
           new XAttribute("docCode", docCode),
           new XAttribute("petCode", petCode),
           new XAttribute("bookDateTime", bookDateTime),
           new XAttribute("bookingStatus", bookingStatus),
           new XAttribute("isActive", isActive),
           new XAttribute("userId", userId)
           ));
            XDocument CreateXml = CommonXML("bookAppointment_C", MAINXML, LOGXML);
            return CreateXml;
        }


        static public XDocument treatmentService_C(string flag, int RTid, int RMid, int serviceItemId, string serviceItem, string serviceItemDesc, string uomDesc,
            decimal qty, decimal rate, decimal tax, string taxCode, decimal amount, decimal taxAmount, decimal exclusiveAmount, decimal inclusiveAmount, string remark,
            bool isActive, int branchId, int userId, XElement LOGXML)
        {
            XElement MAINXML = new XElement("SPXML",
           new XElement("SPDETAILS",
           new XAttribute("flag", flag),
           new XAttribute("RTid", RTid),
           new XAttribute("RMid", RMid),
           new XAttribute("serviceItemId", serviceItemId),
           new XAttribute("serviceItem", serviceItem),
           new XAttribute("serviceItemDesc", serviceItemDesc),
           new XAttribute("uomDesc", uomDesc),
           new XAttribute("qty", qty),
           new XAttribute("rate", rate),
           new XAttribute("tax", tax),
           new XAttribute("rate", rate),
           new XAttribute("taxCode", taxCode),
           new XAttribute("amount", amount),
           new XAttribute("taxAmount", taxAmount),
           new XAttribute("exclusiveAmount", exclusiveAmount),
           new XAttribute("inclusiveAmount", inclusiveAmount),
           new XAttribute("remark", remark),
           new XAttribute("isActive", isActive),
           new XAttribute("userId", userId)
           ));
            XDocument CreateXml = CommonXML("treatmentService_C", MAINXML, LOGXML);
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


        static public XDocument petMaster_g(int flag, int id, string name, string custCode, int branchId, int userId, XElement LOGXML)
        {
            XElement MAINXML = new XElement("SPXML",
           new XElement("SPDETAILS",
           new XAttribute("flag", flag),
           new XAttribute("id", id),
           new XAttribute("name", name),
           new XAttribute("custCode", custCode),
           new XAttribute("branchId", branchId),
           new XAttribute("userId", userId)
           ));
            XDocument CreateXml = CommonXML("petMaster_g", MAINXML, LOGXML);
            return CreateXml;
        }


        static public XDocument bookAppointment_g(int flag, int id, string bookingNo, string docCode, string custCode, int branchId, int userId, XElement LOGXML)
        {
            XElement MAINXML = new XElement("SPXML",
           new XElement("SPDETAILS",
           new XAttribute("flag", flag),
           new XAttribute("id", id),
           new XAttribute("bookingNo", bookingNo),
           new XAttribute("docCode", docCode),
           new XAttribute("custCode", custCode),
           new XAttribute("branchId", branchId),
           new XAttribute("userId", userId)
           ));
            XDocument CreateXml = CommonXML("bookAppointment_g", MAINXML, LOGXML);
            return CreateXml;
        }
        static public XDocument treatmentService_g(int flag, int rTid, int rMid, int serviceItemId, string serviceItem, string serviceItemDesc, int branchId, int userId, XElement LOGXML)
        {
            XElement MAINXML = new XElement("SPXML",
           new XElement("SPDETAILS",
           new XAttribute("flag", flag),
           new XAttribute("rTid", rTid),
           new XAttribute("rMid", rMid),
           new XAttribute("serviceItemId", serviceItemId),
           new XAttribute("serviceItem", serviceItem),
           new XAttribute("serviceItemDesc", serviceItemDesc),
             new XAttribute("branchId", branchId),
           new XAttribute("userId", userId)
           ));
            XDocument CreateXml = CommonXML("treatmentService_g", MAINXML, LOGXML);
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
