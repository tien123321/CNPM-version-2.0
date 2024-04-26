using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CNPM.Model
{
    internal class QuanlysachModel
    {
        private static string constr = System.Configuration.ConfigurationManager.ConnectionStrings["BanhangnhasachTienPhong"].ToString();
        public void QuanLy_Load(ComboBox cBHangSX, ComboBox comboBox3, DataGridView dataGridView1)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select * from tblNhaxuatban", cnn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        cBHangSX.DisplayMember = "sTennhaxuatban";
                        cBHangSX.ValueMember = "PK_iNhaxuatban";
                        dt.Rows.Add();
                        cBHangSX.DataSource = dt;
                    }
                }

                using (SqlCommand cmd = new SqlCommand("select * from tblLoaisach", cnn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        comboBox3.DisplayMember = "sTenloaisach";
                        comboBox3.ValueMember = "PK_iLoaisach";
                        dt.Rows.Add();
                        comboBox3.DataSource = dt;
                    }

                }

            }
            hienSach(dataGridView1);

        }
        public void hienSach(DataGridView dataGridView1)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select * from tblSach_NhaXB", cnn))
                {
                    cmd.CommandType = CommandType.Text;
                    cnn.Open();
                    using (SqlDataAdapter adp = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adp.Fill(dt);
                        DataView view = new DataView(dt);
                        dataGridView1.DataSource = view;
                        dataGridView1.Columns[0].HeaderText = "Mã Sách";
                        dataGridView1.Columns[1].HeaderText = "Tên sách";
                        dataGridView1.Columns[2].HeaderText = "Tên tác giả";
                        dataGridView1.Columns[3].HeaderText = "Tên loại sách";
                        dataGridView1.Columns[4].HeaderText = "Nhà xuất bản";
                        dataGridView1.Columns[5].HeaderText = "Đơn giá";
                        dataGridView1.Columns[6].HeaderText = "Số lượng";
                    }
                }
            }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine("Test Performance load book: " + elapsedTime);

        }
        public void btnTimKiemDT_Click(string mtbTenDT, string mtbRom, string mtbGiaFrom, string soluong, string cBHangSX, string comboBox3, DataGridView dataGridView1)
        {

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            string dieukienLoc = "PK_iSach>0";
            if (!string.IsNullOrEmpty(mtbTenDT))
                dieukienLoc += string.Format(" AND sTensach LIKE '%{0}%'", mtbTenDT);

            if (!string.IsNullOrEmpty(mtbRom))
                dieukienLoc += string.Format(" AND sTentacgia LIKE '%{0}%'", mtbRom);

            if (!string.IsNullOrEmpty(mtbGiaFrom))
                dieukienLoc += string.Format(" AND Convert(iDongia, System.String) LIKE '%{0}%'", mtbGiaFrom);

            if (!string.IsNullOrEmpty(soluong))
                dieukienLoc += string.Format(" AND Convert(iSoluong, System.String) LIKE '%{0}%'", soluong);

            if (!string.IsNullOrEmpty(cBHangSX))
                dieukienLoc += string.Format(" AND Convert(sTennhaxuatban, System.String) LIKE '%{0}%'", cBHangSX);

            if (!string.IsNullOrEmpty(comboBox3))
                dieukienLoc += string.Format(" AND Convert(sTenloaisach, System.String) LIKE '%{0}%'", comboBox3);

            DataView data1 = (DataView)dataGridView1.DataSource;
            data1.RowFilter = dieukienLoc;
            dataGridView1.DataSource = data1;


        }
        public bool checktensach(string tenSach)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                cnn.Open();
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandText = "select count(*) from tblSach where sTensach = N'" + tenSach + "'";
                    cmd.ExecuteNonQuery();
                    int a = (int)cmd.ExecuteScalar();
                    if (a == 1)
                    {
                        return true;
                    }
                }
                cnn.Close();
            }
            return false;
        }
        public void btnThemDT_Click(string mtbTenDT, string mtbRom, string mtbGiaFrom, string soluong, string cBHangSX, string comboBox3, DataGridView dataGridView1)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                cnn.Open();
                using (SqlDataAdapter adp = new SqlDataAdapter())
                {
                    adp.TableMappings.Add("Tables", "tblSach");

                    adp.SelectCommand = new SqlCommand("select * from tblSach", cnn);
                    DataSet dts = new DataSet();
                    adp.Fill(dts, "tblSach");
                    DataTable table = dts.Tables["tblSach"];

                    string sql = "insert into tblSach (sTensach,sTentacgia,iDongia,iSoluong,FK_iNhaxuatban,FK_iLoaisach)" +
                        "values (@sTensach,@sTentacgia,@iDongia,@iSoluong,@FK_iNhaxuatban,@FK_iLoaisach)";
                    adp.InsertCommand = new SqlCommand(sql, cnn);
                    adp.InsertCommand.Parameters.Add("@sTensach", SqlDbType.NVarChar, 100).Value = mtbTenDT;
                    adp.InsertCommand.Parameters.Add("@sTentacgia", SqlDbType.NVarChar, 100).Value = mtbRom;
                    adp.InsertCommand.Parameters.Add("@iDongia", SqlDbType.Int).Value = int.Parse(mtbGiaFrom);
                    adp.InsertCommand.Parameters.Add("@iSoluong", SqlDbType.Int).Value = int.Parse(soluong);
                    adp.InsertCommand.Parameters.Add("@FK_iNhaxuatban", SqlDbType.Int).Value = int.Parse((cBHangSX).ToString());
                    adp.InsertCommand.Parameters.Add("@FK_iLoaisach", SqlDbType.Int).Value = int.Parse((comboBox3).ToString());

                    table.Rows.Add();
                    adp.Update(dts, "tblSach");
                    adp.Fill(dts, "tblSach");

                }
                cnn.Close();
            }

            hienSach(dataGridView1);

        }
        public void btnSuaDT_Click(string mtbTenDT, string mtbRom, string mtbGiaFrom, string soluong, string cBHangSX, string comboBox3, DataGridView dataGridView1)
        {

            using (SqlConnection cnn = new SqlConnection(constr))
            {
                cnn.Open();
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Sua_Sach";
                    cmd.Parameters.AddWithValue("@PK_iSach", dataGridView1.CurrentRow.Cells[0].Value);
                    cmd.Parameters.AddWithValue("@sTensach", mtbTenDT);
                    cmd.Parameters.AddWithValue("@sTentacgia", mtbRom);
                    cmd.Parameters.AddWithValue("@iDongia", int.Parse(mtbGiaFrom));
                    cmd.Parameters.AddWithValue("@iSoluong", int.Parse(soluong));
                    cmd.Parameters.AddWithValue("@FK_iNhaxuatban", int.Parse((cBHangSX).ToString()));
                    cmd.Parameters.AddWithValue("@FK_iLoaisach", int.Parse((comboBox3).ToString()));

                    cmd.ExecuteNonQuery();
                }
                hienSach(dataGridView1);
                cnn.Close();
            }
        }
        public void btnXoaDT_Click(string mtbRom, DataGridView dataGridView1)
        {
            DialogResult chose = MessageBox.Show("Bạn thực sự muốn xóa sách này không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (chose == DialogResult.OK)
            {
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    cnn.Open();
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = "delete from tblSach where PK_iSach  = '" + dataGridView1.CurrentRow.Cells[0].Value + "'";
                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception e)
                        {
                            if (e.HResult == -2146232060)
                            {
                                MessageBox.Show("Không thể xóa trường này do đã có hóa đơn liên quan đến sách");
                            }
                        }

                        hienSach(dataGridView1);
                    }
                    cnn.Close();
                }
            }


        }


    }
}
