using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class userMaster
    {
        #region properties
        int _userId;
        string _loginId;
        int _uTypeId;
        bool _isActive;
        string _email;
        string _mobileNo;
        string _password;
        string _userName;
        string _fName;
        string _mName;
        string _lName;
        DateTime _dob;
        string _address;

        public int UserId
        {
            get
            {
                return _userId;
            }

            set
            {
                _userId = value;
            }
        }

        public string LoginId
        {
            get
            {
                return _loginId;
            }

            set
            {
                _loginId = value;
            }
        }

        public int UTypeId
        {
            get
            {
                return _uTypeId;
            }

            set
            {
                _uTypeId = value;
            }
        }

        public bool IsActive
        {
            get
            {
                return _isActive;
            }

            set
            {
                _isActive = value;
            }
        }

        public string Email
        {
            get
            {
                return _email;
            }

            set
            {
                _email = value;
            }
        }

        public string MobileNo
        {
            get
            {
                return _mobileNo;
            }

            set
            {
                _mobileNo = value;
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }

            set
            {
                _password = value;
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

        public string FName
        {
            get
            {
                return _fName;
            }

            set
            {
                _fName = value;
            }
        }

        public string MName
        {
            get
            {
                return _mName;
            }

            set
            {
                _mName = value;
            }
        }

        public string LName
        {
            get
            {
                return _lName;
            }

            set
            {
                _lName = value;
            }
        }

        public DateTime Dob
        {
            get
            {
                return _dob;
            }

            set
            {
                _dob = value;
            }
        }

        public string Address
        {
            get
            {
                return _address;
            }

            set
            {
                _address = value;
            }
        }


        #endregion
    }
}
