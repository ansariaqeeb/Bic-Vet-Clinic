using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataModel.Result
{
    public class Result
    {
        int _ResultId;
        string _message;
        string _type;
        string _title;
        string _flag;
        string _extra;
        string _objectName;
        string _objectType;

        public int ResultId
        {
            get
            {
                return _ResultId;
            }

            set
            {
                _ResultId = value;
            }
        }

        public string Message
        {
            get
            {
                return _message;
            }

            set
            {
                _message = value;
            }
        }

        public string Type
        {
            get
            {
                return _type;
            }

            set
            {
                _type = value;
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                _title = value;
            }
        }

        public string Flag
        {
            get
            {
                return _flag;
            }

            set
            {
                _flag = value;
            }
        }

        public string Extra
        {
            get
            {
                return _extra;
            }

            set
            {
                _extra = value;
            }
        }

        public string ObjectName
        {
            get
            {
                return _objectName;
            }

            set
            {
                _objectName = value;
            }
        }

        public string ObjectType
        {
            get
            {
                return _objectType;
            }

            set
            {
                _objectType = value;
            }
        }

        public Result(int id, string Flag)
        {
            this.ResultId = id;
            this.Flag = Flag;
            string MESSAGE = "";

            //ResourceManager rm = new ResourceManager("Resources.Resource", typeof(Resource).Assembly);

            switch (Flag)
            {
                case "I":
                    MESSAGE = id > 0 ? "Record successfully added" : "Error in saving";
                    break;
                case "E":
                    MESSAGE = id > 0 ? "Record successfully updated" : "Error in updation";
                    break;
                case "D":
                    MESSAGE = id > 0 ? "Record successfully deleted" : "Error in deletion";
                    break;
                case "C":
                    MESSAGE = "";
                    break;
                case "S":
                    MESSAGE = id > 0 ? "" : "";
                    break;
                case "A":
                    MESSAGE = id > 0 ? "" : "";
                    break;
                case "R":
                    MESSAGE = id > 0 ? "" : "";
                    break;
            }
             
            this.Message = MESSAGE;
        }
        public Result() { }
        public Result ReadBIErrors(string RESULTXML)
        {
            XDocument xmlSuccess = XDocument.Parse(RESULTXML.ToString());
            var result = (from i in xmlSuccess.Descendants("SYSMSGS")
                          select new
                          {
                              errorID = Convert.ToString(i.Element("ID").Value),
                              errorMsg = Convert.ToString(i.Element("ERRORMSGS").Value).Replace(@"\", @"\\"),
                              extra = i.Element("EXTRA") != null ? Convert.ToString(i.Element("EXTRA").Value) : "",
                              type = i.Element("TYPE").Value,
                              title = i.Element("TITLE").Value
                          });
            this.ResultId = Convert.ToInt32(result.FirstOrDefault().errorID);
            this.Message = result.FirstOrDefault().errorMsg;
            this.Title = result.FirstOrDefault().title;
            this.Extra = result.FirstOrDefault().extra;
            this.Type = result.FirstOrDefault().type;
            return this;
            //return await Task.Factory.StartNew(() => this);
        }

        public void ReadAUTOBIErrors(string RESULTXML)
        {
            XDocument xmlSuccess = XDocument.Parse(RESULTXML.ToString());
            var result = (from i in xmlSuccess.Descendants("SYSMSGS")
                          select new
                          {
                              errorID = Convert.ToString(i.Element("ID").Value),
                              OBJECTNAME = Convert.ToString(i.Element("OBJECTNAME").Value),
                              OBJECTTYPE = Convert.ToString(i.Element("OBJECTTYPE").Value),
                              type = i.Element("TYPE").Value,
                              errorMsg = Convert.ToString(i.Element("ERRORMSGS").Value).Replace(@"\", @"\\")
                          });
            this.ResultId = Convert.ToInt32(result.FirstOrDefault().errorID);
            this.Message = result.FirstOrDefault().errorMsg;
            this.ObjectName = result.FirstOrDefault().OBJECTNAME;
            this.ObjectType = result.FirstOrDefault().OBJECTTYPE;
            this.Type = result.FirstOrDefault().type;
        }
    }
}
