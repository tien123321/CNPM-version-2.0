using CNPM.Controler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNPM.View
{
    public partial class Trangchucuahangview : Form
    {
        public Trangchucuahangview()
        {
            InitializeComponent();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Taikhoan = "";
            Properties.Settings.Default.Solansai = 0;
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void đổiMậtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePasswordview frm_Forgot = new ChangePasswordview();
            frm_Forgot.ShowDialog();
        }

        private void frm_Trangchu_Load(object sender, EventArgs e)
        {
            //lấy thông tin tài khoản
            AuthenControler authen=new AuthenControler();
            txt_Username.Text = authen.getUserName(Properties.Settings.Default.Taikhoan, Properties.Settings.Default.Matkhau);
        }

        private void quảnLýThôngTinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Quanlycuahangview quanlycuahangview = new Quanlycuahangview();
            Properties.Settings.Default.tabdulieu = "Quanlythongtin";
            Properties.Settings.Default.Save();
            this.Hide();
            quanlycuahangview.ShowDialog();
            this.Show();
        }

        private void quảnLýBánHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Quanlycuahangview quanlycuahangview = new Quanlycuahangview();
            Properties.Settings.Default.tabdulieu = "Quanlybanhang";
            Properties.Settings.Default.Save();
            this.Hide();
            quanlycuahangview.ShowDialog();
            this.Show();
        }
    }
}
