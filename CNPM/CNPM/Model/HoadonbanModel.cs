using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNPM.Model
{
    internal class HoadonbanModel
    {
        DataTable dataTable = new DataTable();
        private static string constr = System.Configuration.ConfigurationManager.ConnectionStrings["BanhangnhasachTienPhong"].ToString();
      
        public DataTable getdulieuHoadon()
        {
            
            dataTable.Clear();
            using (SqlConnection connection = new SqlConnection(constr))
            {
                using (SqlCommand command = new SqlCommand("delete  bangmua select  PK_iSach as 'Mã Sách',sTensach as 'Tên sách',sTentacgia as'Tên Tác giả',FK_iNhaxuatban as 'Nhà xuất bản',iDongia as 'Đơn giá',iSoluong as 'Số lượng' from bangmua  "
                    , connection))
                {
                    connection.Open();
                    dataTable.Load(command.ExecuteReader());
                    connection.Close();
                }
            }
            return dataTable;
        }
        public DataTable getdulieu()
        {

            dataTable.Clear();
            using (SqlConnection connection = new SqlConnection(constr))
            {
                using (SqlCommand command = new SqlCommand("select  PK_iSach as 'Mã Sách',sTensach as 'Tên sách',sTentacgia as'Tên Tác giả',FK_iNhaxuatban as 'Nhà xuất bản',iDongia as 'Đơn giá',iSoluong as 'Số lượng' from bangmua "
                    , connection))
                {
                    connection.Open();
                    dataTable.Load(command.ExecuteReader());
                    connection.Close();
                }
            }
            return dataTable;
        }
        public DataTable timkiemhoadon(string tenkh, string tensach, string tennv)
        {
            string cmd = "select sTennhanvien,sTenkhachhang,iSoluongban,sTensach,iDongia from tblKhachhang " +
                "join tblHoadonban on tblKhachhang.PK_iKhachhang=tblHoadonban.FK_iKhachhang " +
                " join tblNhanvien on tblNhanvien.PK_iNhanvien=tblHoadonban.FK_iNhanvien" +
                " join tblChitietHDB on tblChitietHDB.FK_iMahoadonban=tblHoadonban.PK_iMahoadonban  join tblSach on tblSach.PK_iSach=tblChitietHDB.FK_iSach";
            string dieukienLoc = "";
            if (!string.IsNullOrEmpty(tenkh))
                dieukienLoc += string.Format(" where sTenkhachhang like N'%{0}%'", tenkh);

            if (!string.IsNullOrEmpty(tensach))
                dieukienLoc += string.Format(" AND sTensach LIKE N'%{0}%'", tensach);
            if (!string.IsNullOrEmpty(tennv))
                dieukienLoc += string.Format(" AND sTennhanvien LIKE N'%{0}%'", tennv);
            using (SqlConnection connection = new SqlConnection(constr))
            {
                using (SqlCommand command = new SqlCommand(cmd + dieukienLoc, connection))
                {
                    connection.Open();
                    dataTable.Load(command.ExecuteReader());
                    connection.Close();
                }
            }
            Console.WriteLine(cmd + dieukienLoc);
            return dataTable;
        }

        public void loadcombobox(ComboBox comboBox1, ComboBox comboBox2)
        {
            string tacgia = "";
            string cmd = "select * from tblsach";
            using (SqlConnection connection = new SqlConnection(constr))
            {
                using (SqlCommand command = new SqlCommand(cmd, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        comboBox1.Items.Add((reader["PK_iSach"].ToString()));
                        tacgia = reader["sTentacgia"].ToString();
                    }
                    connection.Close();
                }
            }
            string cmdKH = "select * from tblKhachhang";
            using (SqlConnection connection = new SqlConnection(constr))
            {
                using (SqlCommand command = new SqlCommand(cmdKH, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        comboBox2.Items.Add((reader["sTenkhachhang"].ToString()));
                    }
                    connection.Close();
                }
            }

        }
        public string loadtentacgia()
        {
            string tacgia = "";
            string cmd = "select * from tblsach";
            using (SqlConnection connection = new SqlConnection(constr))
            {
                using (SqlCommand command = new SqlCommand(cmd, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        tacgia = reader["sTentacgia"].ToString();
                    }
                    connection.Close();
                }
            }
            return tacgia;
        }
        public string loadtensach()
        {
            string tensach = "";
            string cmd = "select * from tblsach";
            using (SqlConnection connection = new SqlConnection(constr))
            {
                using (SqlCommand command = new SqlCommand(cmd, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        tensach = reader["sTensach"].ToString();
                    }
                    connection.Close();
                }
            }
            return tensach;
        }
        public void insermua(string TKH, string TNV, string MS, string soluong, string dongia)
        {
            string cmd = string.Format("insert into bangmua values(N'{0}',N'{1}','{2}',N'{3}',N'{4}',{5},{6},1,1)", TKH, TNV, MS, loadtensach(), loadtentacgia(), dongia, soluong);

            using (SqlConnection connection = new SqlConnection(constr))
            {
                using (SqlCommand command = new SqlCommand(cmd, connection))
                {
                    connection.Open();
                    int n = command.ExecuteNonQuery();
                    connection.Close();
                }
            }

        }
        public int thanhtien()
        {
            int dongia = 0, soluong = 0, tongthanhtien = 0;
            dataTable.Clear();
            using (SqlConnection connection = new SqlConnection(constr))
            {
                
                using (SqlCommand command = new SqlCommand("select  PK_iSach,sTensach,sTentacgia,FK_iNhaxuatban,iDongia,iSoluong from bangmua "
                    , connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        dongia = int.Parse(reader["iDongia"].ToString());
                        soluong = int.Parse(reader["iSoluong"].ToString());
                        tongthanhtien=tongthanhtien+(dongia*soluong);
                    }
                    connection.Close();
                }
            }
            return tongthanhtien ;
        }

    }
}
