using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CNPM.Model.ClassData
{
    internal class NXB
    {
        private int id;
        private string fullname = "", address = "", phone = "";

        public NXB(int id, string fullname, string address, string phone)
        {
            this.id = id;
            this.fullname = fullname;
            this.address = address;
            this.phone = phone;
        }

        public int Id
        {
            get => id;
            set => id = value;
        }

        public string Fullname
        {
            get => fullname;
            set => fullname = value;
        }

        public string Address
        {
            get => address;
            set => address = value;
        }

        public string Phone
        {
            get => phone;
            set => phone = value;
        }

        public string filter()
        {
            List<String> queryList = new List<string>();
            if (this.fullname != "")
            {
                queryList.Add(string.Format("sTennhaxuatban LIKE '%{0}%'", this.fullname));
            }

            if (this.address != "")
            {
                queryList.Add(string.Format("sDiachi LIKE '%{0}%'", this.address));
            }

            if (this.phone != "")
            {
                queryList.Add(string.Format("sSDT LIKE '%{0}%'", this.phone));
            }

            string query = "";
            if (queryList.Count >= 1)
            {
                query += " WHERE ";
                for (int i = 0; i < queryList.Count; i++)
                {
                    query += queryList[i];

                    if (i < queryList.Count - 1)
                    {
                        query += " AND ";
                    }
                }
            }
            return query;
        }

        public bool requiredFields()
        {
            if (this.fullname != "" && this.address != "" && this.phone != "")
            {
                return true;
            }
            return false;
        }

        public bool isPhoneNumber()
        {
            return Regex.Match(this.phone, @"(84|0[3|5|7|8|9])+([0-9]{8})\b").Success;
        }
    }
}
