using CNPM.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNPM.Controler
{
    internal class HoadonnhapControler
    {
        HoadonnhapModel model=new HoadonnhapModel();
        DataTable dt=new DataTable();
        public  DataTable HoadonnhapTable()
        {
            dt.Clear();
            dt = model.getdulieuHoadon();
            return dt;
        }
        public DataTable getSachhoadon(string mahd)
        {
            dt.Clear();
            dt = model.seachhoadonSach(mahd);
            return dt;
        }
    }
}
