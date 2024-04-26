using CNPM.Controler;
using CNPM.Model.ClassData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNPM.View
{
    public partial class Quanlycuahangview : Form
    {
        string mahd = "1";
        public Quanlycuahangview()
        {
            InitializeComponent();
        }
        DataTable tbl=new DataTable();
        HoadonnhapControler hoadonnhap = new HoadonnhapControler();
        QUanlysachControler quanlysachControler= new QUanlysachControler();
        CustomerController _customerController=new CustomerController();
        NhanvienController nhanVienController= new NhanvienController();
        taiKhaonCOntroller taiKhoanController=new taiKhaonCOntroller();
        HoadonbanControler hoadonban = new HoadonbanControler();
        NXBControler NXBControler = new NXBControler();
        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label36_Click(object sender, EventArgs e)
        {

        }

        private void btnTimKiemDT_Click(object sender, EventArgs e)
        {
            this.quanlysachControler.TimKiemSach(mtbTenDT.Text, mtbRom.Text, mtbGiaFrom.Text, soluong.Text, cBHangSX.Text, comboBox3.Text, dataGridView1);
        }
        public void LoadQuanlysach()
        {
            quanlysachControler.QuanLyLoad(cBHangSX, comboBox3, dataGridView1);
        }
        private void Quanlycuahangview_Load(object sender, EventArgs e)
        {
            string Tabquanly = Properties.Settings.Default.tabdulieu;
            string loaitk = Properties.Settings.Default.Loaitk;
            if(Tabquanly== "Quanlythongtin")
            {
                tabControl1.TabPages.RemoveAt(5);
                tabControl1.TabPages.RemoveAt(5);
                
                //tài khoản
                if (loaitk == "Nhanvien")
                {
                    tabControl1.TabPages.RemoveAt(3);
                    tabControl1.TabPages.RemoveAt(3);
                }
                else
                {
                    this.nhanVienController.layDuLieuController(dgv_NhanVien);

                    this.taiKhoanController.dulieuCBController("Select PK_iLoaitaikhoan,sTenloaitaikhoan from tblLoaitaikhoan", "sTenloaitaikhoan", "PK_iLoaitaikhoan", cb_LoaiTaiKhoan);
                    this.taiKhoanController.dulieuCBController("Select PK_iNhanvien from tblNhanvien", "PK_iNhanvien", "PK_iNhanvien", cb_MaNhanVien);
                    this.taiKhoanController.layDuLieuController(dgv_TaiKhoan);
                   
                }
                LoadQuanlysach();
                List<Custemer> customers = this._customerController.allCustomers();
                int customerQuantity = customers.Count;
                if (customerQuantity > 0)
                {
                    foreach (var customer in customers)
                    {
                        dgvCustomer.Rows.Add(customer.Id, customer.Fullname, customer.Phone, customer.Address);
                    }
                }
                List<NXB> nxbs = this.NXBControler.allNXBs();
                int nxbQuantity = nxbs.Count;
                if (nxbQuantity > 0)
                {
                    foreach (var nxb in nxbs)
                    {
                        dataGridView2.Rows.Add(nxb.Id, nxb.Fullname, nxb.Phone, nxb.Address);
                    }
                }
            }
            else
            {
                for(int i = 0; i < 5; i++)
                {
                    tabControl1.TabPages.RemoveAt(0);
                }
                DataTable tbl = new DataTable();
                tbl = hoadonnhap.HoadonnhapTable();
                dataGridView8.DataSource = tbl;
                DataTable dt=new DataTable();
               
                dt=hoadonban.HoadonbanTable();
                dataGridView3.DataSource = dt;
                hoadonban.loadcombobox(comboBox2,comboBox1);
                label44.Text = hoadonban.tongthanhtien();
            }
         
        }

        private void btnThemDT_Click(object sender, EventArgs e)
        {
            if (this.quanlysachControler.checktensach(mtbTenDT.Text))
            {
                MessageBox.Show("Không được nhập trùng tên sách");
                return;
            }

            int dongia = int.Parse(mtbGiaFrom.Text.ToString());
            int sl = int.Parse(soluong.Text.ToString());

            if (dongia < 0 || sl < 0)
            {
                MessageBox.Show("Không được nhập số âm ");
                return;
            }

            if (mtbRom.Text == "" || mtbTenDT.Text == "" || mtbGiaFrom.Text == "" || soluong.Text == "")
            {
                MessageBox.Show("Không được để rỗng !");
            }
            else
            {
                try
                {
                    this.quanlysachControler.ThemSachController(mtbTenDT.Text, mtbRom.Text, mtbGiaFrom.Text, soluong.Text, cBHangSX.SelectedValue.ToString(), comboBox3.SelectedValue.ToString(), dataGridView1);
                    MessageBox.Show("Thêm thành công ");
                }
                catch
                {
                    MessageBox.Show(" Thêm không thành công");
                }
                hienSach();
            }
        }
        private void hienSach()
        {
            /* start */
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            this.quanlysachControler.hienSach(dataGridView1);
        }

        private void btnSuaDT_Click(object sender, EventArgs e)
        {
            int dongia = int.Parse(mtbGiaFrom.Text.ToString());
            int sl = int.Parse(soluong.Text.ToString());

            if (dongia < 0 || sl < 0)
            {
                MessageBox.Show("Không được cập nhập số âm ");
                return;
            }

            if (mtbRom.Text == "" || mtbTenDT.Text == "" || mtbGiaFrom.Text == "" || soluong.Text == "")
            {
                MessageBox.Show("Không được để rỗng !");
            }
            else
            {
                try
                {
                    this.quanlysachControler.SuaSach(mtbTenDT.Text, mtbRom.Text, mtbGiaFrom.Text, soluong.Text, cBHangSX.SelectedValue.ToString(), comboBox3.SelectedValue.ToString(), dataGridView1);
                    MessageBox.Show("Sửa thành công");
                }
                catch
                {
                    MessageBox.Show("Sửa không thành công");
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            mtbTenDT.Text = dataGridView1.CurrentRow.Cells["sTensach"].Value.ToString();
            mtbRom.Text = dataGridView1.CurrentRow.Cells["sTentacgia"].Value.ToString();
            mtbGiaFrom.Text = dataGridView1.CurrentRow.Cells["iDongia"].Value.ToString();
            soluong.Text = dataGridView1.CurrentRow.Cells["iSoluong"].Value.ToString();
            cBHangSX.Text = dataGridView1.CurrentRow.Cells["sTennhaxuatban"].Value.ToString();
            comboBox3.Text = dataGridView1.CurrentRow.Cells["sTenloaisach"].Value.ToString();
        }

        private void btnXoaDT_Click(object sender, EventArgs e)
        {
            this.quanlysachControler.XoaSach(mtbRom.Text, dataGridView1);
        }

        private void btnLamMoiDT_Click(object sender, EventArgs e)
        {
            mtbTenDT.Text = "";
            mtbRom.Text = "";
            mtbGiaFrom.Text = "";
            soluong.Text = "";
            hienSach();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {

        }

        private void btnSearchCustomer_Click(object sender, EventArgs e)
        {
            dgvCustomer.DataSource = null;
            dgvCustomer.Rows.Clear();
            String customerName, customerAddress, customerPhone;
            customerName = txtCustomerName.Text;
            customerAddress = txtCustomerAddress.Text;
            customerPhone = txtCustomerPhone.Text;

            Custemer filterCustomer = new Custemer(1, customerName, customerAddress, customerPhone);
            List<Custemer> customers = this._customerController.searchCustomers(filterCustomer);
            int customerQuantity = customers.Count;
            if (customerQuantity > 0)
            {
                foreach (var customer in customers)
                {

                    dgvCustomer.Rows.Add(customer.Id, customer.Fullname, customer.Phone, customer.Address);
                }
            }
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            String customerName, customerAddress, customerPhone;
            customerName = txtCustomerName.Text;
            customerAddress = txtCustomerAddress.Text;
            customerPhone = txtCustomerPhone.Text;

            Custemer customer = new Custemer(1, customerName, customerAddress, customerPhone);

            bool isValid = customer.requiredFields();
            bool isPhone = customer.isPhoneNumber();
            bool isExistCustomer = this._customerController.isExistCustomer(customer);
            if (!isValid)
            {
                MessageBox.Show("Điền đầy đủ thông tin khách hàng");
                return;
            }
            if (!isPhone)
            {

                MessageBox.Show("Số điện thoại không đúng định dạng");
                return;
            }

            if (isExistCustomer)
            {
                MessageBox.Show("Số điện thoại đã tồn tại trong hệ thống");
                return;
            }

            Custemer createdCustomer = this._customerController.createCustomer(customer);
            if (createdCustomer.Id == 1)
            {
                MessageBox.Show("Tạo khách hàng thất bại");
            }
            else
            {
                MessageBox.Show("Tạo khách hàng thành công");

            }

            //refresh dataGridView
            dgvCustomer.DataSource = null;
            dgvCustomer.Rows.Clear();
            List<Custemer> afterCustomerCreatedList = this._customerController.allCustomers();
            int customerQuantity = afterCustomerCreatedList.Count;
            if (customerQuantity > 0)
            {
                foreach (var item in afterCustomerCreatedList)
                {
                    dgvCustomer.Rows.Add(item.Id, item.Fullname, item.Phone, item.Address);
                }
            }
        }

        private void btnUpdateCustomer_Click(object sender, EventArgs e)
        {
            String customerName, customerAddress, customerPhone;
            customerName = txtCustomerName.Text;
            customerAddress = txtCustomerAddress.Text;
            customerPhone = txtCustomerPhone.Text;

            Custemer customer = new Custemer(1, customerName, customerAddress, customerPhone);

            bool isValid = customer.requiredFields();
            bool isPhone = customer.isPhoneNumber();
            Console.Write("editing");
            if (!isValid)
            {
                MessageBox.Show("Điền đầy đủ thông tin khách hàng");
                return;
            }
            if (!isPhone)
            {

                MessageBox.Show("Số điện thoại không đúng định dạng");
                return;
            }



            Custemer createdCustomer = this._customerController.updateCustomer(customer);
            if (createdCustomer.Id == 1)
            {
                MessageBox.Show("Cập nhật khách hàng thất bại");
            }
            else
            {
                MessageBox.Show("Cập khách hàng thành công");

            }

            //refresh dataGridView
            dgvCustomer.DataSource = null;
            dgvCustomer.Rows.Clear();
            List<Custemer> afterUpdateList = this._customerController.allCustomers();
            int customerQuantity = afterUpdateList.Count;
            if (customerQuantity > 0)
            {
                foreach (var item in afterUpdateList)
                {
                    dgvCustomer.Rows.Add(item.Id, item.Fullname, item.Phone, item.Address);
                }
            }
        }

        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCustomerName.Text = dgvCustomer.CurrentRow.Cells[1].Value.ToString();
            txtCustomerPhone.Text = dgvCustomer.CurrentRow.Cells[2].Value.ToString();
            txtCustomerAddress.Text = dgvCustomer.CurrentRow.Cells[3].Value.ToString();
        }

        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            String customerName, customerAddress, customerPhone;
            customerName = txtCustomerName.Text;
            customerAddress = txtCustomerAddress.Text;
            customerPhone = txtCustomerPhone.Text;

            Custemer customer = new Custemer(1, customerName, customerAddress, customerPhone);
            this._customerController.deleteCustomer(customer);

            //refresh dataGridView
            dgvCustomer.DataSource = null;
            dgvCustomer.Rows.Clear();
            List<Custemer> afterList = this._customerController.allCustomers();
            int customerQuantity = afterList.Count;
            if (customerQuantity > 0)
            {
                foreach (var item in afterList)
                {
                    dgvCustomer.Rows.Add(item.Id, item.Fullname, item.Phone, item.Address);
                }
            }
        }

        private void btnRefreshCustomer_Click(object sender, EventArgs e)
        {

            txtCustomerName.Text = "";
            txtCustomerPhone.Text = "";
            txtCustomerAddress.Text = "";
        }

        private void btnTimKiemNhanVien_Click(object sender, EventArgs e)
        {
            int? gioitinh = null;
            int? trangthai = null;
            float? luong = 0;
            if (String.IsNullOrEmpty(tb_Luong.Text))
            {
                luong = null;
            }
            if (rd_Nam.Checked)
            {
                gioitinh = 1;
            }
            if (rd_Nu.Checked)
            {
                gioitinh = 0;
            }
            if (rd_DaNghi.Checked)
            {
                trangthai = 0;
            }
            if (rd_ChuaNghi.Checked)
            {
                trangthai = 1;
            }
            this.nhanVienController.timKiemController(tb_TenNhanVien.Text, dtp_NgaySinh.Value.ToShortDateString(), gioitinh, tb_QueQuan.Text, dtp_NgayVaoLam.Value.ToShortDateString(), tb_SoDienThoai.Text, trangthai, luong, dgv_NhanVien);
            rd_ChuaNghi.Checked = rd_DaNghi.Checked = rd_Nam.Checked = rd_Nu.Checked = false;
        }

        private void btnLamMoiNhanVien_Click(object sender, EventArgs e)
        {
            this.nhanVienController.layDuLieuController(dgv_NhanVien);
            tb_TenNhanVien.Text = "";
            tb_QueQuan.Text = "";
            tb_Luong.Text = "";
            tb_SoDienThoai.Text = "";
            rd_ChuaNghi.Checked = false;
            rd_DaNghi.Checked = false;
            rd_Nam.Checked = false;
            rd_Nu.Checked = false;
            dtp_NgaySinh.Value = DateTime.Now;
            dtp_NgayVaoLam.Value = DateTime.Now;
            MessageBox.Show("Dữ liệu làm mới thành công");
        }

        private void btnThemNhanVien_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tb_Luong.Text) || String.IsNullOrEmpty(tb_TenNhanVien.Text) || String.IsNullOrEmpty(tb_QueQuan.Text) || String.IsNullOrEmpty(tb_SoDienThoai.Text))
            {
                MessageBox.Show("Thiếu thông tin bắt buộc");
                return;
            }
            if (!rd_Nam.Checked && !rd_Nu.Checked)
            {
                MessageBox.Show("Thiếu thông tin bắt buộc");
                return;
            }
            if (!rd_DaNghi.Checked && !rd_ChuaNghi.Checked)
            {
                MessageBox.Show("Thiếu thông tin bắt buộc");
                return;
            }
            int gioitinh = 0;
            int trangthai = 0;
            if (rd_Nam.Checked)
            {
                gioitinh = 1;
            }
            if (rd_Nu.Checked)
            {
                gioitinh = 0;
            }
            if (rd_DaNghi.Checked)
            {
                trangthai = 0;
            }
            if (rd_ChuaNghi.Checked)
            {
                trangthai = 1;
            }
            this.nhanVienController.ThemNhanVienController(tb_TenNhanVien.Text, dtp_NgaySinh.Value.ToShortDateString(), gioitinh, tb_QueQuan.Text, dtp_NgayVaoLam.Value.ToShortDateString(), tb_SoDienThoai.Text, trangthai, float.Parse(tb_Luong.Text), dgv_NhanVien);
            MessageBox.Show("Thêm dữ liệu thành công");
        }

        private void btnSuaNhanVien_Click(object sender, EventArgs e)
        {
            int? gioitinh = null;
            int? trangthai = null;
            float? luong = 0;
            if (String.IsNullOrEmpty(tb_Luong.Text) || String.IsNullOrEmpty(tb_TenNhanVien.Text) || String.IsNullOrEmpty(tb_QueQuan.Text) || String.IsNullOrEmpty(tb_SoDienThoai.Text))
            {
                MessageBox.Show("Thiếu thông tin bắt buộc");
                return;
            }
            if (!rd_Nam.Checked && !rd_Nu.Checked)
            {
                MessageBox.Show("Thiếu thông tin bắt buộc");
                return;
            }
            if (!rd_DaNghi.Checked && !rd_ChuaNghi.Checked)
            {
                MessageBox.Show("Thiếu thông tin bắt buộc");
                return;
            }
            luong = float.Parse(tb_Luong.Text);
            if (rd_Nam.Checked)
            {
                gioitinh = 1;
            }
            if (rd_Nu.Checked)
            {
                gioitinh = 0;
            }
            if (rd_DaNghi.Checked)
            {
                trangthai = 0;
            }
            if (rd_ChuaNghi.Checked)
            {
                trangthai = 1;
            }
            this.nhanVienController.suaNhanVienController(tb_TenNhanVien.Text, dtp_NgaySinh.Value.ToShortDateString(), gioitinh, tb_QueQuan.Text, dtp_NgayVaoLam.Value.ToShortDateString(), tb_SoDienThoai.Text, trangthai, luong, dgv_NhanVien);
            MessageBox.Show("Sửa Thành Công");
        }

        private void dgv_NhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow dr = dgv_NhanVien.Rows[index];
            tb_TenNhanVien.Text = dr.Cells["dgv_tb_TenNhanVien"].Value.ToString();
            tb_QueQuan.Text = dr.Cells["dgv_tb_QueQuan"].Value.ToString();
            tb_SoDienThoai.Text = dr.Cells["dgv_tb_SoDienThoai"].Value.ToString();
            tb_Luong.Text = dr.Cells["dgv_tb_Luong"].Value.ToString();
            dtp_NgaySinh.Value = Convert.ToDateTime(dr.Cells["dgv_tb_NgaySinh"].Value.ToString());
            dtp_NgayVaoLam.Value = Convert.ToDateTime(dr.Cells["dgv_tb_NgayVaoLam"].Value.ToString());
            if (dr.Cells["dgv_tb_GioiTinh"].Value.ToString() == "True")
            {
                rd_Nam.Checked = true;
            }
            if (dr.Cells["dgv_tb_GioiTinh"].Value.ToString() == "False")
            {
                rd_Nu.Checked = true;
            }
            if (dr.Cells["dgv_tb_TrangThai_NV"].Value.ToString() == "True")
            {
                rd_ChuaNghi.Checked = true;
            }
            if (dr.Cells["dgv_tb_TrangThai_NV"].Value.ToString() == "False")
            {
                rd_DaNghi.Checked = true;
            }
            btnLamMoiNhanVien.Enabled = true;
        }

        private void cb_LoaiTaiKhoan_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgv_TaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow dr = dgv_TaiKhoan.Rows[index];
            tb_TenDangNhap.Text = dr.Cells["dgv_tb_TenDangNhap"].Value.ToString();
            tb_MatKhau.Text = dr.Cells["dgv_tb_MatKhau"].Value.ToString();
            cb_MaNhanVien.SelectedValue = dr.Cells["dgv_tb_MaNhanVien"].Value.ToString();
            cb_LoaiTaiKhoan.SelectedValue = dr.Cells["dgv_tb_LoaiTaiKhoan"].Value.ToString();
            
        }

        private void btnTimKiemTaiKhoan_Click(object sender, EventArgs e)
        {
            this.taiKhoanController.timKiemController(tb_TenDangNhap.Text, tb_MatKhau.Text, cb_LoaiTaiKhoan.SelectedValue.ToString(), cb_MaNhanVien.SelectedValue.ToString(), dgv_TaiKhoan);
        }
        private bool validateAccount(string username, string password)
        {
            Regex validateGuidRegex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            if (username.Length < 6)
            {
                MessageBox.Show("Tên đăng nhập phải có ít nhất 6 kí tự", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (!validateGuidRegex.IsMatch(password))
            {
                MessageBox.Show("Mật khẩu phải thỏa mãn có ít nhất 8 kí tự và ít nhất 1 kí tự viết hoa, 1 kí tự viết thường, 1 kí tự đặc biệt, 1 kí tự số", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }
        private void btnThemTaiKhoan_Click(object sender, EventArgs e)
        {
            Regex validateGuidRegex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,20}$");
            try
            {
                string loaiTaiKhoan = cb_LoaiTaiKhoan.SelectedValue.ToString().Trim();
                string maNhanVien = cb_MaNhanVien.SelectedValue.ToString().Trim();
                string matKhau = tb_MatKhau.Text.Trim();
                string tenDangNhap = tb_TenDangNhap.Text.Trim();
                if (loaiTaiKhoan == "" || maNhanVien == "" || matKhau == "" || tenDangNhap == "")
                {
                    MessageBox.Show("Thiếu thông tin bắt buộc");
                }
                else if (!validateGuidRegex.IsMatch(matKhau) || !validateGuidRegex.IsMatch(matKhau) || !validateGuidRegex.IsMatch(matKhau))
                {
                    MessageBox.Show("Mật khẩu phải thỏa mãn có ít nhất 8 kí tự, nhiêu nhất 20 kí tự và ít nhất 1 kí tự viết hoa, 1 kí tự viết thường, 1 kí tự đặc biệt, 1 kí tự số", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else
                {
                    if (!validateAccount(tb_TenDangNhap.Text, tb_MatKhau.Text))
                    {
                        return;
                    }
                    this.taiKhoanController.ThemTaiKhoanController(loaiTaiKhoan, maNhanVien, matKhau, tenDangNhap, dgv_TaiKhoan);
                    tb_TenDangNhap.Text = null;
                    tb_MatKhau.Text = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thao tác thất bại");
            }
        }

        private void cb_MaNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSuaTaiKhoan_Click(object sender, EventArgs e)
        {
            int check = 0;
            if (!validateAccount(tb_TenDangNhap.Text, tb_MatKhau.Text))
            {
                return;
            }
            foreach (DataGridViewRow row in dgv_TaiKhoan.Rows)
            {
                if (row.Cells["dgv_tb_TenDangNhap"].Value.ToString() == tb_TenDangNhap.Text)
                {
                    check++;
                }
            }
            if (check >= 1)
            {
                MessageBox.Show("Tên Tài Khoản Đã Tồn Tại");
            }
            else
            {
                this.taiKhoanController.suaTaiKhoanController(Convert.ToInt32(cb_LoaiTaiKhoan.SelectedValue), Convert.ToInt32(cb_MaNhanVien.SelectedValue), tb_TenDangNhap.Text, tb_MatKhau.Text, 1, dgv_TaiKhoan);
                btnSuaTaiKhoan.Enabled = false;
                cb_MaNhanVien.Enabled = true;
                tb_MatKhau.Text = null;
                tb_TenDangNhap.Text = null;
            }
        }

        private void btnKhoaTaiKhoan_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn khóa tài khoản không ?", "Khóa tài khoản", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                if (this.taiKhoanController.checkTrangThaiController(Convert.ToInt32(cb_MaNhanVien.SelectedValue.ToString())) == true)
                {
                    DialogResult result1 = MessageBox.Show("Nhân viên hiện chưa nghỉ làm bạn có chắc muốn khóa ?", "Khóa tài khoản", MessageBoxButtons.OKCancel);
                    if (result1 == DialogResult.OK)
                    {
                        try
                        {
                            this.taiKhoanController.suaTaiKhoanController(Convert.ToInt32(cb_LoaiTaiKhoan.SelectedValue), Convert.ToInt32(cb_MaNhanVien.SelectedValue), tb_TenDangNhap.Text, tb_MatKhau.Text, 0, dgv_TaiKhoan);
                            btnKhoaTaiKhoan.Enabled = false;
                            tb_TenDangNhap.Text = null;
                            tb_MatKhau.Text = null;
                        }
                        catch
                        {
                            MessageBox.Show("Khóa tài khoản thất bại");
                        }
                    }
                }
            }
        }

        private void btnLamMoiTaiKhoan_Click(object sender, EventArgs e)
        {
            this.taiKhoanController.layDuLieuController(dgv_TaiKhoan);
            tb_TenDangNhap.Text = "";
            tb_MatKhau.Text = "";
            MessageBox.Show("Dữ liệu làm mới thành công");
        }

        private void dataGridView8_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            try
            {
                DataGridViewRow dr = dataGridView8.Rows[index];
                textBox2.Text = dr.Cells["sTennhanvien"].Value.ToString();
                textBox1.Text = dr.Cells["dNgaylap"].Value.ToString();
                mahd= dr.Cells["PK_iMayeucaunhap"].Value.ToString();
                
            }
            catch { }
           
        }

        private void button31_Click(object sender, EventArgs e)
        {
            DataTable tbl = new DataTable();
            dataGridView8.DataSource = tbl;
            DataTable tbl2 = new DataTable();
            tbl2 = hoadonnhap.getSachhoadon(mahd);
            dtgvDTHDX.DataSource = tbl2;
        }

        private void button30_Click(object sender, EventArgs e)
        {
            DataTable tbl = new DataTable();
            tbl = hoadonnhap.HoadonnhapTable();
            dataGridView8.DataSource = tbl;
            DataTable tbl2 = new DataTable();
            dtgvDTHDX.DataSource = tbl2;
         

        }

        private void button7_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            
            dt = hoadonban.HoadonbanTable();
            dataGridView3.DataSource = dt;
            label44.Text = hoadonban.tongthanhtien();
        }

        private void button6_Click(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();

            
            dataGridView3.DataSource = dt;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            dataGridView2.DataSource = null;
            dataGridView2.Rows.Clear();
            String nxbName, nxbAddress, nxbPhone;
            nxbName = maskedTextBox1.Text;
            nxbAddress = maskedTextBox2.Text;
            nxbPhone = maskedTextBox3.Text;

            NXB filternxb = new NXB(1, nxbName, nxbAddress, nxbPhone);
            List<NXB> nxbs = this.NXBControler.searchNXBs(filternxb);
            int nxbQuantity = nxbs.Count;
            if (nxbQuantity > 0)
            {
                foreach (var nxb in nxbs)
                {

                    dataGridView2.Rows.Add(nxb.Id, nxb.Fullname, nxb.Phone, nxb.Address);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            maskedTextBox3.Text = "";
            maskedTextBox1.Text = "";
            maskedTextBox2.Text = "";

            //refesh dataGridView
            dataGridView2.DataSource = null;
            dataGridView2.Rows.Clear();
            List<NXB> afternxbCreatedList = this.NXBControler.allNXBs();
            int nxbQuantity = afternxbCreatedList.Count;
            if (nxbQuantity > 0)
            {
                foreach (var item in afternxbCreatedList)
                {
                    dataGridView2.Rows.Add(item.Id, item.Fullname, item.Phone, item.Address);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            String nxbName, nxbAddress, nxbPhone;
            nxbName = maskedTextBox1.Text;
            nxbAddress = maskedTextBox3.Text;
            nxbPhone = maskedTextBox2.Text;

            NXB nxb = new NXB(1, nxbName, nxbAddress, nxbPhone);

            bool isValid = nxb.requiredFields();
            bool isPhone = nxb.isPhoneNumber();
            bool isExistnxb = this.NXBControler.isExistNXB(nxb);
            if (!isValid)
            {
                MessageBox.Show("Điền đầy đủ thông tin ");
                return;
            }
            if (!isPhone)
            {

                MessageBox.Show("Số điện thoại không đúng định dạng");
                return;
            }

            if (isExistnxb)
            {
                MessageBox.Show("Số điện thoại đã tồn tại trong hệ thống");
                return;
            }

            NXB creatednxb = this.NXBControler.createNXB(nxb);
            if (creatednxb.Id == 1)
            {
                MessageBox.Show("Tạo thất bại");
            }
            else
            {
                MessageBox.Show("Tạo thành công");

            }

            //refresh dataGridView
            dataGridView2.DataSource = null;
            dataGridView2.Rows.Clear();
            List<NXB> afternxbCreatedList = this.NXBControler.allNXBs();
            int nxbQuantity = afternxbCreatedList.Count;
            if (nxbQuantity > 0)
            {
                foreach (var item in afternxbCreatedList)
                {
                    dataGridView2.Rows.Add(item.Id, item.Fullname, item.Phone, item.Address);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            String nxbName, nxbAddress, nxbPhone;
            nxbName = maskedTextBox1.Text;
            nxbAddress = maskedTextBox3.Text;
            nxbPhone = maskedTextBox2.Text;

            NXB nxb = new NXB(1, nxbName, nxbAddress, nxbPhone);

            bool isValid = nxb.requiredFields();
            bool isPhone = nxb.isPhoneNumber();
            Console.Write("editing");
            if (!isValid)
            {
                MessageBox.Show("Điền đầy đủ thông tin ");
                return;
            }
            if (!isPhone)
            {

                MessageBox.Show("Số điện thoại không đúng định dạng");
                return;
            }



            NXB creatednxb = this.NXBControler.updateNXB(nxb);
            if (creatednxb.Id == 1)
            {
                MessageBox.Show("Cập nhật NXB thất bại");
            }
            else
            {
                MessageBox.Show("Cập NXB thành công");

            }
            //refresh dataGridView
            dataGridView2.DataSource = null;
            dataGridView2.Rows.Clear();
            List<NXB> afternxbCreatedList = this.NXBControler.allNXBs();
            int nxbQuantity = afternxbCreatedList.Count;
            if (nxbQuantity > 0)
            {
                foreach (var item in afternxbCreatedList)
                {
                    dataGridView2.Rows.Add(item.Id, item.Fullname, item.Phone, item.Address);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String nxbName, nxbAddress, nxbPhone;
            nxbName = maskedTextBox1.Text;
            nxbAddress = maskedTextBox3.Text;
            nxbPhone = maskedTextBox2.Text;

            NXB nxb = new NXB(1, nxbName, nxbAddress, nxbPhone);
            this.NXBControler.deleteNXB(nxb);


            //refresh dataGridView
            dataGridView2.DataSource = null;
            dataGridView2.Rows.Clear();
            List<NXB> afternxbCreatedList = this.NXBControler.allNXBs();
            int nxbQuantity = afternxbCreatedList.Count;
            if (nxbQuantity > 0)
            {
                foreach (var item in afternxbCreatedList)
                {
                    dataGridView2.Rows.Add(item.Id, item.Fullname, item.Phone, item.Address);
                }
            }
        }

        private void label34_Click(object sender, EventArgs e)
        {

        }

        private void label34_Click_1(object sender, EventArgs e)
        {

        }

        private void tabPage9_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if(comboBox2.Text=="" || numericUpDown1.Value==0|| maskedTextBox6.Text==""|| comboBox1.Text == "") { 
                MessageBox.Show("Vui lòng nhập đủ thông tin kiện hàng","Thông báo", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                AuthenControler authen = new AuthenControler();
                hoadonban.insert(comboBox1.Text,
                    authen.getUserName(Properties.Settings.Default.Taikhoan, Properties.Settings.Default.Matkhau),
                    comboBox2.Text, numericUpDown1.Value.ToString(), maskedTextBox6.Text);
                label44.Text = hoadonban.tongthanhtien();
                DataTable dt = new DataTable();

                dt = hoadonban.HoadonbanTable1();
                dataGridView3.DataSource = dt;
                hoadonban.loadcombobox(comboBox2, comboBox1);
            }
            
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            dt = hoadonban.HoadonbanTable();
            dataGridView3.DataSource = dt;
            label44.Text = hoadonban.tongthanhtien();
            label44.Text = hoadonban.tongthanhtien();
        }
    }
}
