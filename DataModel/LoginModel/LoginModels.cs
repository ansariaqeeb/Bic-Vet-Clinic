using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.DirectoryServices.AccountManagement;
using System.Text;
using System.Xml.Linq;
using Pastel.Evolution;
using System.Configuration;
using DataAccess;
using System.Collections.Generic;
using System.Linq;

namespace DataModel.LoginModel
{
    public class LoginModels
    {
        XDocument xdoc;
        DataModel.Result.Result err = new DataModel.Result.Result();

        XDocument Xdoc;
        DataTable dt;
         

        #region properties 
        public int controlNo { get; set; }
        public int plantId { get; set; }
        public int compId { get; set; }

        int dbConfigId;
        bool isAdmin;
        int branchId;

        [Required(ErrorMessage = "Please Enter User ID")]
        [Display(Name = "User name")]
        public string userName { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }

        [Display(Name = "Remember me?")]
        public bool rememberMe { get; set; }

        public bool IsAdmin
        {
            get
            {
                return isAdmin;
            }

            set
            {
                isAdmin = value;
            }
        }

        public string LoginId
        {
            get
            {
                return loginId;
            }

            set
            {
                loginId = value;
            }
        }
 
        [Required(ErrorMessage = "Please Select Branch")]
        [Display(Name = "Branch")]
        public int BranchId { get => branchId; set => branchId = value; }
        public int DbConfigId { get => dbConfigId; set => dbConfigId = value; }
        public int UserId { get => userId; set => userId = value; }

        string loginId;
        int userId;
        #endregion
        //    #region Methods 
        //Method for checking login authentication and getting user information
        public LoginModel.LoginModels getLoginInfo(LoginModels objLogOnModel)
        {
            try
            {
                XElement LOGXML = null;
                xdoc = DBXML.userMaster_g(0, objLogOnModel.userName.Trim(), objLogOnModel.password.Trim(), LOGXML);

                DataTable dt = SqlExe.GetDT(xdoc);
                LoginModels lst = dt != null && dt.Rows.Count > 0 ? dt.AsEnumerable().Select(s => new LoginModels
                {
                    UserId = s.Field<int>("userId"),
                    userName = s.Field<string>("userName"),
                    loginId= s.Field<string>("loginId"),
                    IsAdmin = s.Field<bool>("isAdmin")
                }).ToList().FirstOrDefault() : null;
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        public Agent validateAget(string loginId, string password,string dbConnectionString,string evolutionCommonDBConnectionString,string serialNumber,string authCode, int isDomain = 0, int expireOn = 0)
        {
            try
            {
                DatabaseContext.Initialise(dbConnectionString, evolutionCommonDBConnectionString, serialNumber, authCode);
                bool valid = Agent.Authenticate(loginId, password);
                Agent agent = new Agent();
                if (valid)
                {
                    agent = Agent.GetByName(loginId);
                    return agent;
                }
                else
                {

                    return agent;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        //    /// <summary>
        //    /// Method to update Logout status in Login Log
        //    /// </summary> 
        //    /// <returns>retirns xml with id and message</returns>
        //    public DataModel.Result.Result LOGOUT(int LOGINLOGID, XElement LOGXML)
        //    {
        //        xdoc = DBXML.CORE_LOGOUT_c(LOGINLOGID, LOGXML);
        //        err.ReadBIErrors(Convert.ToString(SqlExe.GetXml(xdoc)));
        //        return err;
        //    }


        //    public LoginSessionDetails CHANGEPOSITION(int FLAG, int ORGSTRUCID, string LOGINID, int CONTROLNO, int POSID, XElement LOGXML)
        //    {

        //        xdoc = DBXML.USERAUTHENTICATION_g(FLAG, LOGINID, "", CONTROLNO, POSID, ORGSTRUCID, "", "", "", "", "", "", "", "");

        //        DataTable dt = SqlExe.GetDT(xdoc);

        //        LoginSessionDetails objLogSession = new LoginSessionDetails();

        //        if (Convert.ToInt32(dt.Rows.Count) > 0)
        //        {
        //            string timezone = Convert.ToString(dt.Rows[0]["DIFFFROMGMT"]);
        //            objLogSession.GMTminutes = Convert.ToInt32(timezone);
        //            objLogSession.Timezone = timezone + " " + Convert.ToString(dt.Rows[0]["TMZONECODE"]);
        //            objLogSession.LoginName = dt.Rows[0]["LOGINID"].ToString();
        //            objLogSession.UserId = Convert.ToInt32(dt.Rows[0]["USERID"].ToString());
        //            objLogSession.IMGPATH = Convert.ToString(dt.Rows[0]["IMGDESC"]);
        //            objLogSession.USERNAME = Convert.ToString(dt.Rows[0]["USERNAME"]);
        //            objLogSession.USERTYPE = Convert.ToInt32(dt.Rows[0]["USERTYPEID"]);
        //            objLogSession.CONTROLNO = Convert.ToInt32(dt.Rows[0]["CONTROLNO"]);
        //            objLogSession.FINYEARDESC = Convert.ToString(dt.Rows[0]["FINYEARDESC"]);
        //            objLogSession.POSID = Convert.ToInt32(dt.Rows[0]["POSID"]);
        //            objLogSession.POSDESC = Convert.ToString(dt.Rows[0]["POSDESC"]);
        //            objLogSession.ORGSTRUCID = Convert.ToInt32(dt.Rows[0]["ORGSTRUCID"]);
        //            objLogSession.ORGDESC = Convert.ToString(dt.Rows[0]["ORGDESC"]);
        //            objLogSession.ORGPATH = Convert.ToString(dt.Rows[0]["ORGPATH"]);
        //            objLogSession.SDATE = Convert.ToDateTime(dt.Rows[0]["SDATE"]);
        //            objLogSession.EDATE = Convert.ToDateTime(dt.Rows[0]["EDATE"]);
        //            objLogSession.LOGXML = LOGXML;
        //        }

        //        return objLogSession;
        //    }
        //    //Method for decrypting password
        public string Decryptdata(string encryptpwd)
        {
            string decryptpwd = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new String(decoded_char);
            return decryptpwd;
        }
        //Method for encrypting password
        public string Encryptdata(string password)
        {
            string strmsg = string.Empty;
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            strmsg = Convert.ToBase64String(encode);
            return strmsg;
        }
        //Method for checking the authentication of domain name, user name and password 
        public Tuple<bool, string> ADIsValid(string Domain, string Username, string Password)
        {
            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, Domain))
            {
                // validate the credentials
                UserPrincipal user = UserPrincipal.FindByIdentity(pc, IdentityType.SamAccountName, Username);
                //   return pc.ValidateCredentials(Username, Password);
                Tuple<bool, string> obj = Tuple.Create(pc.ValidateCredentials(Username, Password), user.EmailAddress);
                return obj;
            }
        }
    }
}
