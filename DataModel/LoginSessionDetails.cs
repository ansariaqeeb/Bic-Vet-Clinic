using System;
using System.Xml.Linq;

namespace DataModel
{
    public struct LoginSessionDetails
    {
        public int RollId { get; set; }
        public int ModuleId { get; set; }
        public int UserId { get; set; }
        public string LoginName { get; set; }
        public int SLID { get; set; }
        public int USERTYPE { get; set; }
        public string USERNAME { get; set; } 
        public string UserTime { get; set; }
        public DateTime time { get; set; }
        public string Timezone { get; set; }
        public string GMTdiffrence { get; set; }
        public int GMThours { get; set; }
        public int GMTminutes { get; set; }
        public string IMGPATH { get; set; }
        public XElement LOGXML { get; set; }
        public int COMPID { get; set; }
        public string COMPDESC { get; set; }
        public int PLANTID { get; set; }
        public string PLANTDESC { get; set; }
        public int CONTROLNO { get; set; }
        public string FINYEARDESC { get; set; } 
        public int DEPTID { get; set; }
        public string DEPTDESC { get; set; }
        public int DESIGID { get; set; }
        public string DESIDESC { get; set; }
        public int POSID { get; set; }
        public string POSDESC { get; set; }
        public int ORGSTRUCID { get; set; }
        public string ORGDESC { get; set; }
        public string ORGPATH { get; set; }
        public DateTime SVRDATE { get; set; }
        public string SVRKEY { get; set; }
        public DateTime SDATE { get; set; }
        public DateTime EDATE { get; set; }
        //for show layout on Last Run Successful DATE                 by maksud 29-Dec-2017 
        public string LastRunSuccessDATE { get; set; }
        //public string ISLastRunSuccessore { get; set; }
        public int LOGINLOGID { get; set; }
    }
}
