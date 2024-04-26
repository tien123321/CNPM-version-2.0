using CNPM.Controler;
using CNPM.Model;
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

namespace CNPM.View
{
    public partial class ChangePasswordview : Form
    {
        public ChangePasswordview()
        {
            InitializeComponent();
        }
        AuthenControler authenControler=new AuthenControler();
        private void textUsername_TextChanged(object sender, EventArgs e)
        {
            Regex validateGuidRegex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,20}$");
            if (textpasswword.Text.Trim() == "")
            {
                Thongbao.Text = "Mật khẩu không được để trống";
            }
            else if (!validateGuidRegex.IsMatch(textpasswword.Text))
            {
                Thongbao.Text = "Mật khẩu phải có [0-9][a-z][A-Z][!@#$%^&*()]";
            }
            else
            {
                Thongbao.Text = "";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Regex validateGuidRegex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,20}$");
            if (textpassword1.Text.Trim() == "")
            {
                Thongbao1.Text = "Mật khẩu không được để trống";
            }
            else if (!validateGuidRegex.IsMatch(textpassword1.Text))
            {
                Thongbao1.Text = "Mật khẩu phải có [0-9][a-z][A-Z][!@#$%^&*()]";
            }
            else
            {
                Thongbao1.Text = "";
            }
        }

        private void textPassword_TextChanged(object sender, EventArgs e)
        {
            Regex validateGuidRegex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,20}$");
            if (textPassword2.Text.Trim() == "")
            {
                Thongbao2.Text = "Mật khẩu không được để trống";
            }
            else if (!validateGuidRegex.IsMatch(textPassword2.Text))
            {
                Thongbao2.Text = "Mật khẩu phải có [0-9][a-z][A-Z][!@#$%^&*()]";
            }
            else if (textpassword1.Text != textPassword2.Text)
            {
                Thongbao2.Text = "Mật khẩu không khớp";
            }
            else
            {
                Thongbao2.Text = "";
            }
        }
        public bool checkthongbao()
        {
            bool check=false;
            if (Thongbao.Text == "")
            {
                return true;
            }
            if(Thongbao1.Text == "")
            {
                return true;
            }
            if (Thongbao2.Text == "")
            {
                return true;
            }
            return check;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Properties.Settings.Default.Taikhoan);
            MessageBox.Show(textpasswword.Text, textPassword2.Text);
            if(checkthongbao())
            {
                AuthenModel authenModel = new AuthenModel();
                bool changepass = authenModel.changePassword(Properties.Settings.Default.Taikhoan, textpasswword.Text, textPassword2.Text);
                    //authenControler.changePassword(textpasswword.Text,textPassword2.Text);
                if (changepass)
                {
                    MessageBox.Show("Đổi mật khẩu thành công","Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Kiểm tra lại mật khẩu", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
