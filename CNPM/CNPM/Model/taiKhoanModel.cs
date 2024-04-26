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
    internal class taiKhoanModel
    {
        string connectionString;
        public taiKhoanModel()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["BanhangnhasachTienPhong"].ConnectionString;
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
        public void layDuLieu(DataGridView dgv_TaiKhoan)
        {
            hienthi("sp_get_taikhoan", "tblTaiKhoan", dgv_TaiKhoan);
        }
        public void dulieuCB(string query, string display, string value, ComboBox comboBox)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                using (SqlCommand cmd = new SqlCommand(query, cnn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        comboBox.DisplayMember = display;
                        comboBox.ValueMember = value;
                        comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                        dt.Rows.Add();
                        comboBox.DataSource = dt;
                    }

                }
                cnn.Close();
            }
        }
        public void timKiem(string tenDangNhap, string matKhau, string loaiTaiKhoan, string maNhanVien, DataGridView dgv_TaiKhoan)
        {
            string dieukienLoc = "PK_iTaiKhoan>0";
            if (!string.IsNullOrEmpty(tenDangNhap))
                dieukienLoc += string.Format(" AND sTendangnhap LIKE '%{0}%'", tenDangNhap);
            if (!string.IsNullOrEmpty(matKhau))
                dieukienLoc += string.Format(" AND sMatkhau LIKE '%{0}%'", matKhau);
            if (!string.IsNullOrEmpty(loaiTaiKhoan))
                dieukienLoc += string.Format(" AND Convert(FK_iLoaitaikhoan, System.String) LIKE '%{0}%'", loaiTaiKhoan);
            if (!string.IsNullOrEmpty(maNhanVien))
                dieukienLoc += string.Format(" AND Convert(FK_iNhanvien, System.String) LIKE '%{0}%'", maNhanVien);
            DataView dvTaiKhoan = (DataView)dgv_TaiKhoan.DataSource;
            dvTaiKhoan.RowFilter = dieukienLoc;
            dgv_TaiKhoan.DataSource = dvTaiKhoan;
        }
        public void themTaiKhoan(string loaiTaiKhoan, string maNhanVien, string matKhau, string tenDangNhap, DataGridView dgv_TaiKhoan)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                using (SqlDataAdapter adp = new SqlDataAdapter())
                {

                    adp.SelectCommand = new SqlCommand("select * from tblTaiKhoan", cnn);
                    DataTable table = new DataTable();
                    adp.Fill(table);

                    if (table != null)
                    {
                        int check = 0;
                        int checktk = 0;
                        foreach (DataRow dr in table.Rows)
                        {
                            if (dr["sTendangnhap"].ToString() == tenDangNhap)
                            {
                                checktk++;
                            }
                            if (dr["FK_iNhanVien"].ToString() == maNhanVien)
                            {
                                check++;
                            }
                        }
                        if (check != 0)
                        {
                            MessageBox.Show("Nhân Viên Đã Có Tài Khoản");
                            return;
                        }
                        else
                        {
                            if (checktk != 0)
                            {
                                MessageBox.Show("Tên tài khoản đã tồn tại. Vui lòng đổi tên tài khoản khác !");
                                return;
                            }
                            else
                            {
                                string sql = "insert into tblTaikhoan (sTendangnhap,FK_iNhanvien,FK_iLoaitaikhoan, sMatkhau)" +
                                "values (@sTendangnhap,@iNhanvien,@iLoaitaikhoan, @sMatkhau)";
                                adp.InsertCommand = new SqlCommand(sql, cnn);
                                adp.InsertCommand.Parameters.Add("@sTendangnhap", SqlDbType.NVarChar, 50).Value = tenDangNhap;
                                adp.InsertCommand.Parameters.Add("@sMatkhau", SqlDbType.VarChar, 50).Value = matKhau;
                                adp.InsertCommand.Parameters.Add("@iLoaitaikhoan", SqlDbType.Int).Value = Convert.ToInt32(loaiTaiKhoan);
                                adp.InsertCommand.Parameters.Add("@iNhanvien", SqlDbType.Int).Value = Convert.ToInt32(maNhanVien);


                                table.Rows.Add();
                                adp.Update(table);
                                table.Clear();
                                adp.Fill(table);
                                dgv_TaiKhoan.DataSource = table;
                                MessageBox.Show("Thêm Thành Công");
                            }
                        }
                    }
                }
                cnn.Close();
            }
            hienthi("sp_get_taikhoan", "tblTaiKhoan", dgv_TaiKhoan);
        }
        public void suaTaiKhoan(int loaiTaiKhoan, int maNhanVien, string tenDangNhap, string matKhau, int trangThai, DataGridView dgv_TaiKhoan)
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_update_taikhoan";
                    cmd.Parameters.AddWithValue("@Maloaitaikhoan", loaiTaiKhoan);
                    cmd.Parameters.AddWithValue("@Tendangnhap", tenDangNhap);
                    cmd.Parameters.AddWithValue("@Manhanvien", maNhanVien);
                    cmd.Parameters.AddWithValue("@Matkhau", matKhau);
                    cmd.Parameters.AddWithValue("@Trangthai", trangThai);
                    cmd.Parameters.AddWithValue("@Mataikhoan", dgv_TaiKhoan.CurrentRow.Cells["dgv_tb_MaTaiKhoan"].Value);
                    cnn.Open();
                    cmd.ExecuteNonQuery();
                    cnn.Close();
                }
            }
            hienthi("sp_get_taikhoan", "tblTaiKhoan", dgv_TaiKhoan);
        }
        public int checkTrangThai(int maNhanVien)
        {
            int trangThai = 0;
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                using (SqlDataAdapter adp = new SqlDataAdapter())
                {
                    adp.SelectCommand = new SqlCommand(String.Format("select bTrangthai from tblNhanVien where PK_iNhanvien = {0}", maNhanVien), cnn);
                    DataTable table = new DataTable();
                    adp.Fill(table);
                    if (table.Rows[0]["bTrangthai"].ToString() == "True")
                    {
                        trangThai = 1;
                    }
                }
                cnn.Close();
            }
            return trangThai;
        }
    }
}
