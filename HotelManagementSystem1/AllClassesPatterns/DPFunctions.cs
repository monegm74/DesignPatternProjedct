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
}



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
    public class DPFunctions
    {
        // Singleton instance
        private static DPFunctions _instance;

        // Private constructor to prevent direct instantiation
        private DPFunctions() { }

        // Public method to get the singleton instance
        public static DPFunctions GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DPFunctions();
            }
            return _instance;
        }

        // Database connection string
        private readonly string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\LoginData.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True";

        /// <summary>
        /// Executes a query and returns the result as a DataSet.
        /// </summary>
        /// <param name="query">The SQL query to execute.</param>
        /// <returns>DataSet containing the result of the query.</returns>
        public DataSet getData(string query)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        adapter.Fill(ds);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching data: " + ex.Message);
            }
            return ds;
        }

        /// <summary>
        /// Executes a non-query SQL command (e.g., INSERT, UPDATE, DELETE).
        /// </summary>
        /// <param name="query">The SQL command to execute.</param>
        public void ExecuteNonQuery(string query)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error executing query: " + ex.Message);
            }
        }

        /// <summary>
        /// Executes a query with parameters and returns a scalar value.
        /// </summary>
        /// <param name="query">The SQL query to execute.</param>
        /// <param name="parameters">An array of SqlParameter objects.</param>
        /// <returns>The scalar value returned by the query.</returns>
        public object ExecuteScalar(string query, SqlParameter[] parameters)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        return cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error executing scalar query: " + ex.Message);
            }
        }

        /// <summary>
        /// Executes a query with parameters and returns the result as a DataSet.
        /// </summary>
        /// <param name="query">The SQL query to execute.</param>
        /// <param name="parameters">An array of SqlParameter objects.</param>
        /// <returns>DataSet containing the result of the query.</returns>
        public DataSet GetDataWithParameters(string query, SqlParameter[] parameters)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(ds);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching data with parameters: " + ex.Message);
            }
            return ds;
        }
    }
}*/
