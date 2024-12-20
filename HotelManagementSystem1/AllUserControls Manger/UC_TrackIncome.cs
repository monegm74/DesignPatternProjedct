using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelManagementSystem1.AllUserControls
{
    public partial class UC_TrackIncome : UserControl
    {
        DPFunctions fn = new DPFunctions();
       
        public UC_TrackIncome()
        {
            InitializeComponent();
        }

        /*        private void txtserchBy_SelectedIndexChanged(object sender, EventArgs e)
                {
                    if (txtserchIncome.SelectedIndex == 0) // Weekly Income
                    {
                        // Query to fetch weekly income grouped by ResidentEmail with total income
                        string query = "SELECT ResidentEmail, SUM(Amount) AS TotalIncome, DATEPART(WEEK, Date) AS Week " +
                                       "FROM Income " +
                                       "WHERE Date >= DATEADD(WEEK, -1, GETDATE()) " + // Fetch records from the last week
                                       "GROUP BY ResidentEmail, DATEPART(WEEK, Date) " +
                                       "UNION ALL " +
                                       "SELECT 'Total', SUM(Amount), NULL " + // Calculate total income
                                       "FROM Income " +
                                       "WHERE Date >= DATEADD(WEEK, -1, GETDATE())";
                        GetRecord(query);
                    }
                    else if (txtserchIncome.SelectedIndex == 1) // Monthly Income
                    {
                        // Query to fetch monthly income grouped by ResidentEmail with total income
                        string query = "SELECT ResidentEmail, SUM(Amount) AS TotalIncome, MONTH(Date) AS Month, YEAR(Date) AS Year " +
                                       "FROM Income " +
                                       "WHERE Date >= DATEADD(MONTH, -1, GETDATE()) " + // Fetch records from the last month
                                       "GROUP BY ResidentEmail, MONTH(Date), YEAR(Date) " +
                                       "UNION ALL " +
                                       "SELECT 'Total', SUM(Amount), NULL, NULL " + // Calculate total income
                                       "FROM Income " +
                                       "WHERE Date >= DATEADD(MONTH, -1, GETDATE())";
                        GetRecord(query);
                    }
                    else if (txtserchIncome.SelectedIndex == 2) // Annual Income
                    {
                        // Query to fetch annual income grouped by ResidentEmail with total income
                        string query = "SELECT ResidentEmail, SUM(Amount) AS TotalIncome, YEAR(Date) AS Year " +
                                       "FROM Income " +
                                       "WHERE Date >= DATEADD(YEAR, -1, GETDATE()) " + // Fetch records from the last year
                                       "GROUP BY ResidentEmail, YEAR(Date) " +
                                       "UNION ALL " +
                                       "SELECT 'Total', SUM(Amount), NULL " + // Calculate total income
                                       "FROM Income " +
                                       "WHERE Date >= DATEADD(YEAR, -1, GETDATE())";
                        GetRecord(query);
                    }
                }*/

        private void txtserchBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            decimal totalIncome = 0;

            if (txtserchIncome.SelectedIndex == 0) // Weekly Income
            {
                // Query to fetch weekly income grouped by ResidentEmail
                string query = "SELECT ResidentEmail, SUM(Amount) AS TotalIncome, DATEPART(WEEK, Date) AS Week " +
                               "FROM Income " +
                               "WHERE Date >= DATEADD(WEEK, -1, GETDATE()) " + // Fetch records from the last week
                               "GROUP BY ResidentEmail, DATEPART(WEEK, Date)";
                GetRecord(query);

                // Query to calculate total weekly income
                string totalQuery = "SELECT SUM(Amount) AS TotalIncome " +
                                    "FROM Income " +
                                    "WHERE Date >= DATEADD(WEEK, -1, GETDATE())";
                totalIncome = GetTotalIncome(totalQuery);
            }
            else if (txtserchIncome.SelectedIndex == 1) // Monthly Income
            {
                // Query to fetch monthly income grouped by ResidentEmail
                string query = "SELECT ResidentEmail, SUM(Amount) AS TotalIncome, MONTH(Date) AS Month, YEAR(Date) AS Year " +
                               "FROM Income " +
                               "WHERE Date >= DATEADD(MONTH, -1, GETDATE()) " + // Fetch records from the last month
                               "GROUP BY ResidentEmail, MONTH(Date), YEAR(Date)";
                GetRecord(query);

                // Query to calculate total monthly income
                string totalQuery = "SELECT SUM(Amount) AS TotalIncome " +
                                    "FROM Income " +
                                    "WHERE Date >= DATEADD(MONTH, -1, GETDATE())";
                totalIncome = GetTotalIncome(totalQuery);
            }
            else if (txtserchIncome.SelectedIndex == 2) // Annual Income
            {
                // Query to fetch annual income grouped by ResidentEmail
                string query = "SELECT ResidentEmail, SUM(Amount) AS TotalIncome, YEAR(Date) AS Year " +
                               "FROM Income " +
                               "WHERE Date >= DATEADD(YEAR, -1, GETDATE()) " + // Fetch records from the last year
                               "GROUP BY ResidentEmail, YEAR(Date)";
                GetRecord(query);

                // Query to calculate total annual income
                string totalQuery = "SELECT SUM(Amount) AS TotalIncome " +
                                    "FROM Income " +
                                    "WHERE Date >= DATEADD(YEAR, -1, GETDATE())";
                totalIncome = GetTotalIncome(totalQuery);
            }

            // Update the read-only TextBox with the total income
            TextBoxReadOnly.Text = totalIncome.ToString("C");
        }

        private decimal GetTotalIncome(string query)
        {
            decimal totalIncome = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\LoginData.mdf;Integrated Security=True;Connect Timeout=30"))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        object result = command.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            totalIncome = Convert.ToDecimal(result);
                        }
                    }
                }
            }
            catch
            {
                // Silent failure: no message box is shown, just keep totalIncome as 0
            }

            return totalIncome;
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            // Ensure the TextBox remains read-only
            TextBoxReadOnly.ReadOnly = true;
        }



        public void GetRecord(String query)
        {
            DataSet ds = fn.getData(query);
            guna2DataGridView1.DataSource = ds.Tables[0];

        }

        private void UC_TrackIncome_Load(object sender, EventArgs e)
        {

        }









        /* private decimal GetTotalIncome(string query)
         {
             decimal totalIncome = 0;

             try
             {
                 using (SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\LoginData.mdf;Integrated Security=True;Connect Timeout=30"))
                 {
                     connection.Open();
                     using (SqlCommand command = new SqlCommand(query, connection))
                     {
                         object result = command.ExecuteScalar(); // Execute the query and fetch the result
                         if (result != DBNull.Value)
                         {
                             totalIncome = Convert.ToDecimal(result);
                         }
                     }
                 }
             }
             catch (Exception ex)
             {
                 MessageBox.Show("Error calculating total income: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
             }

             return totalIncome;
         }*/





    }
}
