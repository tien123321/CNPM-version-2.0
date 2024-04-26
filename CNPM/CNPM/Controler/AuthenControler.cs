using CNPM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNPM.Controler
{
    internal class AuthenControler
    {
        AuthenModel authenModel=new AuthenModel();
        bool complete;

        public AuthenControler()
        {
        }

        public AuthenControler(AuthenModel authenModel, bool complete)
        {
            this.authenModel = authenModel;
            this.complete = complete;
        }

        public bool Dangnhap(string username, string password)
        {
            complete =authenModel.isCheckaccount(username, password);
            return complete;
        }
        public bool changePassword(String oldPassword, String newPassword)
        {
            complete= authenModel.changePassword(Properties.Settings.Default.Taikhoan, oldPassword, newPassword);
            return complete;
        }
        public string getUserName(String username, String Password) {
            string Userauthen=authenModel.userauthen(username, Password);
            return Userauthen;
        }

    }
}
