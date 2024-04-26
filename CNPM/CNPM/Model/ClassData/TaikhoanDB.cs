using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNPM.Model.ClassData
{
    internal class TaikhoanDB
    {
        public string username { get; set; }
        public string password { get; set; }
        public string loaitaikhoan { get; set; }
        public string tennguoidung {  get; set; }

        public TaikhoanDB()
        {
        }

        public TaikhoanDB(string username, string password, string loaitaikhoan,string tennguoidung)
        {
            this.username = username;
            this.password = password;
            this.loaitaikhoan = loaitaikhoan;
            this.tennguoidung = tennguoidung;
        }
      
    }
}
