using CNPM.Controler;
using CNPM.Model;
using CNPM.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNPM
{
    public partial class Loginview : Form
    {
        public Loginview()
        {
            InitializeComponent();
        }
        
        private void textUsername_TextChanged(object sender, EventArgs e)
        {
            if (textUsername.Text.Trim() == "")
            {
                Thongbao.Text = "Tên đăng nhập không được để trống";
            }
            else if (textUsername.Text.Trim().Length < 6)
            {
                Thongbao.Text = "Tên đăng nhập phải dài hơn 6";
            }
            else
            {
                Thongbao.Text = "";
            }
        }

        private void textPassword_TextChanged(object sender, EventArgs e)
        {
            Regex validateGuidRegex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,20}$");
            if (textPassword.Text.Trim() == "")
            {
                Thongbao1.Text = "Mật khẩu không được để trống";
            }
            else if (!validateGuidRegex.IsMatch(textPassword.Text))
            {
                Thongbao1.Text = "Mật khẩu phải có [0-9][a-z][A-Z][!@#$%^&*()]";
            }
            else
            {
                Thongbao1.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool ktraloi = true;
            bool checkthreeerror = true;
            if (Properties.Settings.Default.Solansai == 3)
            {
                checkthreeerror = false;
            }
            if (textPassword.Text.Trim() == "")
            {
                Thongbao1.Text = "Mật khẩu không được để trống";
                ktraloi = false;
            }
            if (textUsername.Text.Trim() == "")
            {
                ktraloi = false;
                Thongbao.Text = "Tên đăng nhập không được để trống";
            }
            if (textUsername.Text != Properties.Settings.Default.Taikhoan)
            {
                Properties.Settings.Default.Solansai = 0;
                checkthreeerror = true;
            }
            if (Thongbao.Text == "" || Thongbao1.Text == "") {
            

                AuthenControler controler = new AuthenControler();
                bool completelogin = controler.Dangnhap(textUsername.Text, textPassword.Text);
                if (completelogin && checkthreeerror)
                {
                    Properties.Settings.Default.Taikhoan = textUsername.Text;
                    Properties.Settings.Default.Matkhau = textPassword.Text;
                    Trangchucuahangview frm_Trangchu = new Trangchucuahangview();
                    this.Hide();
                    frm_Trangchu.ShowDialog();
                    this.Show();    
                }
                else if (!checkthreeerror)
                {
                    MessageBox.Show("Đã khóa đăng nhập","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if(textUsername.Text== Properties.Settings.Default.Taikhoan) 
                    {
                        Properties.Settings.Default.Solansai = (Properties.Settings.Default.Solansai) + 1;
                    }
                    else
                    {
                        Properties.Settings.Default.Taikhoan=textUsername.Text;
                    }
                    Properties.Settings.Default.Save();
                    MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác", Properties.Settings.Default.Solansai.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Liên hệ với chủ cửa hàng để biết thêm chi tiết","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void frm_Login_Load(object sender, EventArgs e)
        {
            Properties.Settings.Default.Taikhoan = "";
            Properties.Settings.Default.Solansai = 0;
            Properties.Settings.Default.Save();

        }

        private void Thongbao_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Thongbao1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
