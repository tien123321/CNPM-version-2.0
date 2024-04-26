using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNPM.Model
{
    internal class NhanvienModel
    {

        string connectionString;
        public NhanvienModel()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["BanhangnhasachTienPhong"].ConnectionString;
        }
        public void timKiem(string tenNhanVien, string ngaySinh, int? gioiTinh, string queQuan, string ngayVaoLam, string SDT, int? trangThai, float? luong, DataGridView dgv_NhanVien)
        {
            string dieukienLoc = "PK_iNhanvien>0";
            if (!string.IsNullOrEmpty(tenNhanVien))
                dieukienLoc += string.Format(" AND sTennhanvien LIKE '%{0}%'", tenNhanVien);
            if (!string.IsNullOrEmpty(queQuan))
                dieukienLoc += string.Format(" AND sQuequan LIKE '%{0}%'", queQuan);
            if (!string.IsNullOrEmpty(SDT))
                dieukienLoc += string.Format(" AND sSDT LIKE '%{0}%'", SDT);
            if (!string.IsNullOrEmpty(gioiTinh.ToString()))
                dieukienLoc += string.Format(" AND bGioitinh = {0}", gioiTinh);
            if (!string.IsNullOrEmpty(trangThai.ToString()))
                dieukienLoc += string.Format(" AND bTrangthai = {0}", trangThai);
            if (!string.IsNullOrEmpty(luong.ToString()))
                dieukienLoc += string.Format(" AND Convert(fLuong, System.String) LIKE '%{0}%'", luong);
            dieukienLoc += string.Format("AND dNgaysinh = '{0}'", ngaySinh);
            dieukienLoc += string.Format("AND dNgayvaolam = '{0}'", ngayVaoLam);
            DataView dvNhanVien = (DataView)dgv_NhanVien.DataSource;
            dvNhanVien.RowFilter = dieukienLoc;
            dgv_NhanVien.DataSource = dvNhanVien;
        }
        private DataTable Get_Dulieu(string procname, string tablename)
        {
            SqlConnection cnn = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(procname, cnn);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable tbl = new DataTable(tablename);
            da.Fill(tbl);
            return tbl;
        }
        private void hienthi(string procname, string tablename, DataGridView dgv)
        {
            DataTable table = Get_Dulieu(procname, tablename);
            DataView view = new DataView(table);
            dgv.AutoGenerateColumns = false;
            dgv.DataSource = view;
        }
        public void layDuLieu(DataGridView dgv_NhanVien)
        {
            hienthi("sp_get_nhanvien", "tblNhanvien", dgv_NhanVien);
        }
        public void themNhanVien(string tenNhanVien, string ngaySinh, int gioiTinh, string queQuan, string ngayVaoLam, string SDT, int trangThai, float luong, DataGridView dgv_NhanVien)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                using (SqlDataAdapter adp = new SqlDataAdapter())
                {

                    adp.SelectCommand = new SqlCommand("select * from tblNhanvien", cnn);
                    DataTable table = new DataTable();
                    adp.Fill(table);

                    string sql = "insert into tblNhanvien (sTennhanvien,dNgaysinh,bGioitinh, sQuequan, dNgayvaolam, sSDT, bTrangthai, fLuong)" +
                    "values (@sTennhanvien,@dNgaysinh,@bGioitinh, @sQuequan, @dNgayvaolam, @sSDT, @bTrangthai, @fLuong)";
                    adp.InsertCommand = new SqlCommand(sql, cnn);
                    adp.InsertCommand.Parameters.Add("@sTennhanvien", SqlDbType.NVarChar, 70).Value = tenNhanVien;
                    adp.InsertCommand.Parameters.Add("@dNgaysinh", SqlDbType.Date).Value = ngaySinh;
                    adp.InsertCommand.Parameters.Add("@dNgayvaolam", SqlDbType.Date).Value = ngayVaoLam;
                    adp.InsertCommand.Parameters.Add("@bGioitinh", SqlDbType.Bit).Value = gioiTinh;
                    adp.InsertCommand.Parameters.Add("@bTrangthai", SqlDbType.Bit).Value = trangThai;
                    adp.InsertCommand.Parameters.Add("@sQuequan", SqlDbType.NVarChar, 255).Value = queQuan;
                    adp.InsertCommand.Parameters.Add("@sSDT", SqlDbType.VarChar, 10).Value = SDT;
                    adp.InsertCommand.Parameters.Add("@fLuong", SqlDbType.Float).Value = luong;

                    table.Rows.Add();
                    adp.Update(table);
                    table.Clear();
                    adp.Fill(table);
                    dgv_NhanVien.DataSource = table;
                }
                cnn.Close();
            }
            hienthi("sp_get_nhanvien", "tblNhanVien", dgv_NhanVien);
        }
        public void suaNhanVien(string tenNhanVien, string ngaySinh, int? gioiTinh, string queQuan, string ngayVaoLam, string SDT, int? trangThai, float? luong, DataGridView dgv_NhanVien)
        {
            string maNhanVien = dgv_NhanVien.CurrentRow.Cells["dgv_tb_MaNhanVien_NV"].Value.ToString();
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_update_nhanvien";
                    cmd.Parameters.AddWithValue("@iNhanvien", maNhanVien);
                    cmd.Parameters.AddWithValue("@sTennhanvien", tenNhanVien);
                    cmd.Parameters.AddWithValue("@dNgaysinh", ngaySinh);
                    cmd.Parameters.AddWithValue("@bGioitinh", gioiTinh);
                    cmd.Parameters.AddWithValue("@sQuequan", queQuan);
                    cmd.Parameters.AddWithValue("@dNgayvaolam", ngayVaoLam);
                    cmd.Parameters.AddWithValue("@sSDT", SDT);
                    cmd.Parameters.AddWithValue("@bTrangthai", trangThai);
                    cmd.Parameters.AddWithValue("@fLuong", luong);
                    cnn.Open();
                    cmd.ExecuteNonQuery();
                    cnn.Close();
                }
            }
            hienthi("sp_get_nhanvien", "tblNhanvien", dgv_NhanVien);
        }
    }
}
