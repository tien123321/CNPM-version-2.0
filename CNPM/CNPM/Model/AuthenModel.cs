using CNPM.Model.ClassData;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNPM.Model
{
    internal class AuthenModel
    {
        String cnn= ConfigurationManager.ConnectionStrings["BanhangnhasachTienPhong"].ConnectionString;
        TaikhoanDB taikhoan=new TaikhoanDB();

        public AuthenModel()
        {
        }

        public AuthenModel(string cnn, TaikhoanDB taikhoan)
        {
            this.cnn = cnn;
            this.taikhoan = taikhoan;
        }
        //kiểm tra
        public bool isCheckaccount(string username, string password)
        {

            using (SqlConnection Connect = new SqlConnection(cnn))
            {
                using (SqlCommand cmd = new SqlCommand("prGetAccount", Connect))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    Connect.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                  
                }
            }
        }
        public bool changePassword(string username, string oldPassword, string newPassword)
        {

            using (SqlConnection connect = new SqlConnection(cnn))
            {
                using (SqlCommand cmd = new SqlCommand("prChangePassword", connect))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@oldPassword", oldPassword);
                    cmd.Parameters.AddWithValue("@newPassword", newPassword);

                    connect.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0 ? true : false;
                }

            }
        }
        public string userauthen(string username, string password)
        {
            string userauthen = "";
            using (SqlConnection connect = new SqlConnection(cnn))
            {
                using (SqlCommand cmd = new SqlCommand("[prGetAccount]", connect))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    connect.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        userauthen = (reader["sTennhanvien"].ToString());
                        Properties.Settings.Default.Loaitk = reader["sTenloaitaikhoan"].ToString();
                    }
                }

            }
            return userauthen;
        }

    }
}
