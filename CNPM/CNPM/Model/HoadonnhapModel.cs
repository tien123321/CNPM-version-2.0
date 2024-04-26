using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNPM.Model
{
    internal class HoadonnhapModel
    {
        DataTable dataTable = new DataTable();
        private static string constr = System.Configuration.ConfigurationManager.ConnectionStrings["BanhangnhasachTienPhong"].ToString();
        public DataTable getdulieuHoadon()
        {
            dataTable.Clear();
            using(SqlConnection connection = new SqlConnection(constr))
            {
                using(SqlCommand command = new SqlCommand(" select PK_iMayeucaunhap,dNgaylap,sTennhanvien from" +
                    " tblNhanvien join tblPhieuyeucau on tblNhanvien.PK_iNhanvien=tblPhieuyeucau.FK_iNhanVien ",connection))
                {
                    connection.Open();
                    dataTable.Load(command.ExecuteReader());
                    connection.Close();
                }
            }
            return dataTable;
        }
        public DataTable seachhoadonSach(string mahd)
        {
            dataTable.Clear();
            using (SqlConnection connection = new SqlConnection(constr))
            {
                using (SqlCommand command = new SqlCommand("select PK_iMayeucaunhap,dNgaylap,sTennhanvien,tblSach.sTensach,iSoluongyeucau,iDongia,iSoluongyeucau*iDongia as [Tổng tiền] from " +
                    "tblPhieuyeucau join tblNhanvien on tblNhanvien.PK_iNhanvien=tblPhieuyeucau.FK_iNhanVien join tblChitietPYC on tblPhieuyeucau.PK_iMayeucaunhap=tblChitietPYC.FK_iMayeucaunhap\r\njoin" +
                    " tblSach on tblSach.PK_iSach=tblChitietPYC.FK_iSach where tblPhieuyeucau.PK_iMayeucaunhap="+mahd, connection))
                {
                    connection.Open();
                    dataTable.Load(command.ExecuteReader());
                    connection.Close();
                }
            }
            return dataTable;
        } 
    }
}
