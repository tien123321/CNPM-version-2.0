using CNPM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNPM.Controler
{
    internal class NhanvienController
    {
        private NhanvienModel nhanVienModel;
        public NhanvienController()
        {
            this.nhanVienModel = new NhanvienModel();
        }
        public void timKiemController(string tenNhanVien, string ngaySinh, int? gioiTinh, string queQuan, string ngayVaoLam, string SDT, int? trangThai, float? luong, DataGridView dgv_NhanVien)
        {
            this.nhanVienModel.timKiem(tenNhanVien, ngaySinh, gioiTinh, queQuan, ngayVaoLam, SDT, trangThai, luong, dgv_NhanVien);
        }
        public void layDuLieuController(DataGridView dgv_NhanVien)
        {
            this.nhanVienModel.layDuLieu(dgv_NhanVien);
        }
        public void ThemNhanVienController(string tenNhanVien, string ngaySinh, int gioiTinh, string queQuan, string ngayVaoLam, string SDT, int trangThai, float luong, DataGridView dgv_NhanVien)
        {
            this.nhanVienModel.themNhanVien(tenNhanVien, ngaySinh, gioiTinh, queQuan, ngayVaoLam, SDT, trangThai, luong, dgv_NhanVien);
        }
        public void suaNhanVienController(string tenNhanVien, string ngaySinh, int? gioiTinh, string queQuan, string ngayVaoLam, string SDT, int? trangThai, float? luong, DataGridView dgv_NhanVien)
        {
            this.nhanVienModel.suaNhanVien(tenNhanVien, ngaySinh, gioiTinh, queQuan, ngayVaoLam, SDT, trangThai, luong, dgv_NhanVien);
        }

    }
}
