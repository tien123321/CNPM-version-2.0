using CNPM.Model.ClassData;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNPM.Model
{
    internal class CustomerModel
    {
        private static string constr = System.Configuration.ConfigurationManager.ConnectionStrings["BanhangnhasachTienPhong"].ToString();
        private Custemer _customer;
        public List<Custemer> _customers;
        public CustomerModel()
        {
            this._customers = new List<Custemer>();
        }
        public List<Custemer> find(Custemer filterCustomer)
        {
            this._customers = new List<Custemer>();
            //Declare the SqlDataReader
            SqlDataReader rdr = null;

            //Create connection
            SqlConnection conn = new SqlConnection(constr);
            Console.Write(filterCustomer.filter());
            //Create command
            SqlCommand cmd = new SqlCommand("SELECT * FROM tblKhachhang" + filterCustomer.filter(), conn);

            try
            {
                //Open the connection
                conn.Open();

                // 1. get an instance of the SqlDataReader
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    // get the results of each column
                    int id = (int)rdr["PK_iKhachhang"];
                    string fullname = (string)rdr["sTenkhachhang"];
                    string address = (string)rdr["sDiachi"];
                    string phone = (string)rdr["sSDTKH"];
                    _customers.Add(new Custemer(id, fullname, address, phone));
                }
            }
            finally
            {
                // 3. close the reader
                if (rdr != null)
                {
                    rdr.Close();
                }

                // close the connection
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return this._customers;
        }
        public List<Custemer> findAll()
        {
            this._customers = new List<Custemer>();
            //Declare the SqlDataReader
            SqlDataReader rdr = null;

            //Create connection
            SqlConnection conn = new SqlConnection(constr);

            //Create command
            SqlCommand cmd = new SqlCommand("SELECT * FROM tblKhachhang", conn);

            try
            {
                //Open the connection
                conn.Open();

                // 1. get an instance of the SqlDataReader
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    // get the results of each column
                    int id = (int)rdr["PK_iKhachhang"];
                    string fullname = (string)rdr["sTenkhachhang"];
                    string address = (string)rdr["sDiachi"];
                    string phone = (string)rdr["sSDTKH"];
                    _customers.Add(new Custemer(id, fullname, address, phone));
                }
            }
            finally
            {
                // 3. close the reader
                if (rdr != null)
                {
                    rdr.Close();
                }

                // close the connection
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return this._customers;
        }
        public bool exist(Custemer customer)
        {
            int totalRecord = 0;
            //Declare the SqlDataReader
            SqlDataReader rdr = null;

            //Create connection
            SqlConnection conn = new SqlConnection(constr);

            //Create command
            SqlCommand cmd = new SqlCommand("SELECT * FROM tblKhachhang WHERE sSDTKH = " + customer.Phone, conn);

            try
            {
                //Open the connection
                conn.Open();

                // 1. get an instance of the SqlDataReader
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    // get the results of each column
                    int id = (int)rdr["PK_iKhachhang"];
                    string fullname = (string)rdr["sTenkhachhang"];
                    string address = (string)rdr["sDiachi"];
                    string phone = (string)rdr["sSDTKH"];
                    totalRecord++;
                }
            }
            finally
            {
                // 3. close the reader
                if (rdr != null)
                {
                    rdr.Close();
                }

                // close the connection
                if (conn != null)
                {
                    conn.Close();
                }
            }

            if (totalRecord > 0)
            {
                return true;
            }
            return false;
        }
        public Custemer create(Custemer customer)
        {
            this._customers = new List<Custemer>();
            Custemer createdCustomer;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                cnn.Open();
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO tblKhachhang (sTenKhachhang, sDiachi, sSDTKH) VALUES (@fullname, @address, @phone)";
                    cmd.Parameters.AddWithValue("@fullname", customer.Fullname);
                    cmd.Parameters.AddWithValue("@address", customer.Address);
                    cmd.Parameters.AddWithValue("@phone", customer.Phone);
                    cmd.ExecuteNonQuery();
                    createdCustomer = new Custemer(999, "", "", "");

                }
                cnn.Close();
            }

            return createdCustomer;
        }
        public Custemer update(Custemer customer)
        {
            Custemer createdCustomer;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                cnn.Open();
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE tblKhachhang 
                    SET sTenKhachhang = @fullname, sDiachi = @address Where sSDTKH = @phone";
                    cmd.Parameters.AddWithValue("@fullname", customer.Fullname);
                    cmd.Parameters.AddWithValue("@address", customer.Address);
                    cmd.Parameters.AddWithValue("@phone", customer.Phone);
                    cmd.ExecuteNonQuery();
                    createdCustomer = new Custemer(999, "", "", "");

                }
                cnn.Close();
            }

            return createdCustomer;
        }
        public Custemer delete(Custemer customer)
        {
            Custemer createdCustomer;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                cnn.Open();
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE tblKhachhang Where sSDTKH = @phone";
                    cmd.Parameters.AddWithValue("@phone", customer.Phone);
                    cmd.ExecuteNonQuery();
                    createdCustomer = new Custemer(999, "", "", "");

                }
                cnn.Close();
            }
            return createdCustomer;
        }
    }
}
