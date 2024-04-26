using CNPM.Model.ClassData;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNPM.Model
{
    internal class NXBModel
    {
        private static string constr = System.Configuration.ConfigurationManager.ConnectionStrings["BanhangnhasachTienPhong"].ToString();

        public List<NXB> _NXBs;
        public NXBModel()
        {
            this._NXBs = new List<NXB>();
        }
        public List<NXB> find(NXB filterCustomer)
        {
            this._NXBs = new List<NXB>();
            //Declare the SqlDataReader
            SqlDataReader rdr = null;

            //Create connection
            SqlConnection conn = new SqlConnection(constr);
            Console.Write(filterCustomer.filter());
            //Create command
            SqlCommand cmd = new SqlCommand("SELECT * FROM tblNhaxuatban" + filterCustomer.filter(), conn);

            try
            {
                //Open the connection
                conn.Open();

                // 1. get an instance of the SqlDataReader
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    // get the results of each column
                    int id = (int)rdr["PK_iNhaxuatban"];
                    string fullname = (string)rdr["sTennhaxuatban"];
                    string address = (string)rdr["sDiachi"];
                    string phone = (string)rdr["sSDT"];
                    _NXBs.Add(new NXB(id, fullname, address, phone));
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

            return this._NXBs;
        }
        public List<NXB> findAll()
        {
            this._NXBs = new List<NXB>();
            //Declare the SqlDataReader
            SqlDataReader rdr = null;

            //Create connection
            SqlConnection conn = new SqlConnection(constr);

            //Create command
            SqlCommand cmd = new SqlCommand("SELECT * FROM tblNhaxuatban", conn);

            try
            {
                //Open the connection
                conn.Open();

                // 1. get an instance of the SqlDataReader
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    // get the results of each column
                    int id = (int)rdr["PK_iNhaxuatban"];
                    string fullname = (string)rdr["sTennhaxuatban"];
                    string address = (string)rdr["sDiachi"];
                    string phone = (string)rdr["sSDT"];
                    _NXBs.Add(new NXB(id, fullname, address, phone));
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

            return this._NXBs;
        }
        public bool exist(NXB nxb)
        {
            int totalRecord = 0;
            //Declare the SqlDataReader
            SqlDataReader rdr = null;

            //Create connection
            SqlConnection conn = new SqlConnection(constr);

            //Create command
            SqlCommand cmd = new SqlCommand("SELECT * FROM tblNhaxuatban WHERE sSDT = " + nxb.Phone, conn);

            try
            {
                //Open the connection
                conn.Open();

                // 1. get an instance of the SqlDataReader
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    // get the results of each column
                    int id = (int)rdr["PK_iNhaxuatban"];
                    string fullname = (string)rdr["sTennhaxuatban"];
                    string address = (string)rdr["sDiachi"];
                    string phone = (string)rdr["sSDT"];
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
        public NXB create(NXB nxb)
        {
            this._NXBs = new List<NXB>();
            NXB createdNXB;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                cnn.Open();
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO tblNhaxuatban (sTennhaxuatban, sDiachi, sSDT) VALUES (@fullname, @address, @phone)";
                    cmd.Parameters.AddWithValue("@fullname", nxb.Fullname);
                    cmd.Parameters.AddWithValue("@address", nxb.Address);
                    cmd.Parameters.AddWithValue("@phone", nxb.Phone);
                    cmd.ExecuteNonQuery();
                    createdNXB = new NXB(999, "", "", "");

                }
                cnn.Close();
            }

            return createdNXB;
        }
        public NXB update(NXB nxb)
        {
            NXB createdNXB;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                cnn.Open();
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE tblNhaxuatban
                    SET sTennhaxuatban = @fullname, sDiachi = @address Where sSDT = @phone";
                    cmd.Parameters.AddWithValue("@fullname", nxb.Fullname);
                    cmd.Parameters.AddWithValue("@address", nxb.Address);
                    cmd.Parameters.AddWithValue("@phone", nxb.Phone);
                    cmd.ExecuteNonQuery();
                    createdNXB = new NXB(999, "", "", "");

                }
                cnn.Close();
            }

            return createdNXB;
        }
        public NXB delete(NXB nxb)
        {
            NXB createdNXB;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                cnn.Open();
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE tblNhaxuatban Where sSDT = @phone";
                    cmd.Parameters.AddWithValue("@phone", nxb.Phone);
                    cmd.ExecuteNonQuery();
                    createdNXB = new NXB(999, "", "", "");

                }
                cnn.Close();
            }
            return createdNXB;
        }
    }
}
