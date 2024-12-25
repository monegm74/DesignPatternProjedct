
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System;
using System.Collections.Generic;




namespace HotelManagementSystem1
{
    internal class DBHelpers
    {
        private static DBHelpers _instance;
        private static readonly object _lock = new object();

        private DBHelpers() { }

        public static DBHelpers Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new DBHelpers();
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





