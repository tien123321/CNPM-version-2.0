using CNPM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNPM.Controler
{
    internal class taiKhaonCOntroller
    {
        taiKhoanModel taiKhoanModel = new taiKhoanModel();
        public void dulieuCBController(string query, string display, string value, ComboBox comboBox)
        {
            this.taiKhoanModel.dulieuCB(query, display, value, comboBox);
        }

        public void layDuLieuController(DataGridView dgv_TaiKhoan)
        {
            this.taiKhoanModel.layDuLieu(dgv_TaiKhoan);
        }
        public void timKiemController(string tenDangNhap, string matKhau, string loaiTaiKhoan, string maNhanVien, DataGridView dgv_TaiKhoan)
        {
            this.taiKhoanModel.timKiem(tenDangNhap, matKhau, loaiTaiKhoan, maNhanVien, dgv_TaiKhoan);
        }
        public void ThemTaiKhoanController(string loaiTaiKhoan, string maNhanVien, string matKhau, string tenDangNhap, DataGridView dgv_TaiKhoan)
        {
            this.taiKhoanModel.themTaiKhoan(loaiTaiKhoan, maNhanVien, matKhau, tenDangNhap, dgv_TaiKhoan);
        }
        public void suaTaiKhoanController(int loaiTaiKhoan, int maNhanVien, string tenDangNhap, string matKhau, int trangThai, DataGridView dgv_TaiKhoan)
        {
            int check = 0;
            foreach (DataGridViewRow row in dgv_TaiKhoan.Rows)
            {
                if (row.Cells["dgv_tb_TenDangNhap"].Value.ToString() == tenDangNhap)
                {
                    check++;
                }
            }
            if (check > 1)
            {
                MessageBox.Show("Đã có tài khoản tồn tại");
            }
            else
            {
                this.taiKhoanModel.suaTaiKhoan(loaiTaiKhoan, maNhanVien, tenDangNhap, matKhau, trangThai, dgv_TaiKhoan);
            }
        }
        public bool checkTrangThaiController(int maNhanVien)
        {
            if (this.taiKhoanModel.checkTrangThai(maNhanVien) == 0)
            {
                return false;
            }
            return true;
        }
    }
}
