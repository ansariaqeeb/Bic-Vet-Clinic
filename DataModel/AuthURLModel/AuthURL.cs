using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataModel.AuthURLModel
{
    public class AuthURL
    {
        XDocument xdoc;
        public int CHECKURLAUTH(int FLAG, string CONTROLLER, string ACTIONNAME, int USERID, int MENUID, int DOCID, int PLANTID, int DEPTID, int DESIGID, int CONTROLNO, XElement LOGXML)
        {
            //xdoc = DBXML.URLACTION_g(FLAG, CONTROLLER, ACTIONNAME, USERID, MENUID, DOCID, PLANTID, DEPTID, DESIGID, CONTROLNO, LOGXML);
            int res = 1;// SqlExe.ExecuteScalarforInt(xdoc);
            return res;
        }
    }
}
