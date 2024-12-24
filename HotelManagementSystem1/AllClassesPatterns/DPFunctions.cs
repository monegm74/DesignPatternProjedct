/*using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelManagementSystem1
{
    internal class DPFunctions
    {
        protected SqlConnection getConnection()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\LoginData.mdf;Integrated Security=True;Connect Timeout=30";
            return con;
        }

        public DataSet getData(String query)
        {
            SqlConnection con = getConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = query;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public void setData(String query, String message)
        {
            SqlConnection con = getConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
            con.Close();


            MessageBox.Show("\"" + message + "\"", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}*/

using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System;
using System.Collections.Generic;

/*namespace HotelManagementSystem1
{
    internal class DPFunctions
    {
        private static DPFunctions _instance;
        private static readonly object _lock = new object();

        private DPFunctions() { }

        public static DPFunctions Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new DPFunctions();
                }
                return _instance;
            }
        }

        // Change 'protected' to 'public'
        public SqlConnection getConnection()
        {
            SqlConnection con = new SqlConnection
            {
                ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\LoginData.mdf;Integrated Security=True;Connect Timeout=30"
            };
            return con;
        }

        public DataSet getData(String query)
        {
            using (SqlConnection con = getConnection())
            {
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }

        public void setData(String query, String message)
        {
            using (SqlConnection con = getConnection())
            {
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}*/


namespace HotelManagementSystem1
{
    internal class DPFunctions
    {
        private static DPFunctions _instance;
        private static readonly object _lock = new object();

        private DPFunctions() { }

        public static DPFunctions Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new DPFunctions();
                    }
                }
                return _instance;
            }
        }


        public SqlConnection getConnection()
        {
            return new SqlConnection
            {
                ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\LoginData.mdf;Integrated Security=True;Connect Timeout=30"
            };
        }

        public DataSet getData(string query, Dictionary<string, object> parameters = null)
        {
            using (var con = getConnection())
            {
                var cmd = new SqlCommand(query, con);

                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        cmd.Parameters.AddWithValue(param.Key, param.Value);
                    }
                }

                var adapter = new SqlDataAdapter(cmd);
                var dataSet = new DataSet();
                adapter.Fill(dataSet);
                return dataSet;
            }
        }

        public void setData(string query, Dictionary<string, object> parameters = null, string successMessage = null)
        {
            using (var con = getConnection())
            {
                var cmd = new SqlCommand(query, con);

                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        cmd.Parameters.AddWithValue(param.Key, param.Value);
                    }
                }

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                if (!string.IsNullOrEmpty(successMessage))
                {
                    MessageBox.Show(successMessage, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}





