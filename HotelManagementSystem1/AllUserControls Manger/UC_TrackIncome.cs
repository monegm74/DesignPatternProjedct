/*using Guna.UI2.WinForms;
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
        private readonly IncomeTracker _incomeTracker;

        public UC_TrackIncome()
        {
            InitializeComponent();

            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\LoginData.mdf;Integrated Security=True;Connect Timeout=30";
            var repository = new SqlIncomeRepository(connectionString);
            _incomeTracker = new IncomeTracker(repository);
        }

        private void txtserchBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var (data, totalIncome) = _incomeTracker.GetIncome(txtserchIncome.SelectedIndex);

                guna2DataGridView1.DataSource = data.Tables[0];
                TextBoxReadOnly.Text = totalIncome.ToString("C");
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Invalid selection. Please try again.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving income data.");
            }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            TextBoxReadOnly.ReadOnly = true;
        }

        private void UC_TrackIncome_Load(object sender, EventArgs e)
        {
        }
    }



    public interface IIncomeStrategy
    {
        string GetIncomeQuery();
        string GetTotalIncomeQuery();
        string GetPeriodName();
    }

    // WeeklyIncomeStrategy.cs
    public class WeeklyIncomeStrategy : IIncomeStrategy
    {
        public string GetIncomeQuery()
        {
            return @"SELECT ResidentEmail, SUM(Amount) AS TotalIncome, DATEPART(WEEK, Date) AS Week 
                FROM Income 
                WHERE Date >= DATEADD(WEEK, -1, GETDATE()) 
                GROUP BY ResidentEmail, DATEPART(WEEK, Date)";
        }

        public string GetTotalIncomeQuery()
        {
            return @"SELECT SUM(Amount) AS TotalIncome 
                FROM Income 
                WHERE Date >= DATEADD(WEEK, -1, GETDATE())";
        }

        public string GetPeriodName() => "Weekly";
    }

    // MonthlyIncomeStrategy.cs
    public class MonthlyIncomeStrategy : IIncomeStrategy
    {
        public string GetIncomeQuery()
        {
            return @"SELECT ResidentEmail, SUM(Amount) AS TotalIncome, MONTH(Date) AS Month, YEAR(Date) AS Year 
                FROM Income 
                WHERE Date >= DATEADD(MONTH, -1, GETDATE()) 
                GROUP BY ResidentEmail, MONTH(Date), YEAR(Date)";
        }

        public string GetTotalIncomeQuery()
        {
            return @"SELECT SUM(Amount) AS TotalIncome 
                FROM Income 
                WHERE Date >= DATEADD(MONTH, -1, GETDATE())";
        }

        public string GetPeriodName() => "Monthly";
    }

    // YearlyIncomeStrategy.cs
    public class YearlyIncomeStrategy : IIncomeStrategy
    {
        public string GetIncomeQuery()
        {
            return @"SELECT ResidentEmail, SUM(Amount) AS TotalIncome, YEAR(Date) AS Year 
                FROM Income 
                WHERE Date >= DATEADD(YEAR, -1, GETDATE()) 
                GROUP BY ResidentEmail, YEAR(Date)";
        }

        public string GetTotalIncomeQuery()
        {
            return @"SELECT SUM(Amount) AS TotalIncome 
                FROM Income 
                WHERE Date >= DATEADD(YEAR, -1, GETDATE())";
        }

        public string GetPeriodName() => "Yearly";
    }

    // IIncomeRepository.cs
    public interface IIncomeRepository
    {
        DataSet GetIncomeData(string query);
        decimal GetTotalIncome(string query);
    }

    // SqlIncomeRepository.cs
    public class SqlIncomeRepository : IIncomeRepository
    {
        private readonly string _connectionString;

        public SqlIncomeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataSet GetIncomeData(string query)
        {
            // Using your existing DPFunctions class
            return DPFunctions.Instance.getData(query);
        }

        public decimal GetTotalIncome(string query)
        {
            decimal totalIncome = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
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
    }

    // IncomeTracker.cs
    public class IncomeTracker
    {
        private readonly IIncomeRepository _repository;
        private readonly Dictionary<int, IIncomeStrategy> _strategies;

        public IncomeTracker(IIncomeRepository repository)
        {
            _repository = repository;
            _strategies = new Dictionary<int, IIncomeStrategy>
        {
            { 0, new WeeklyIncomeStrategy() },
            { 1, new MonthlyIncomeStrategy() },
            { 2, new YearlyIncomeStrategy() }
        };
        }

        public (DataSet Data, decimal TotalIncome) GetIncome(int strategyIndex)
        {
            if (!_strategies.TryGetValue(strategyIndex, out var strategy))
            {
                throw new ArgumentException("Invalid strategy index", nameof(strategyIndex));
            }

            var data = _repository.GetIncomeData(strategy.GetIncomeQuery());
            var totalIncome = _repository.GetTotalIncome(strategy.GetTotalIncomeQuery());

            return (data, totalIncome);
        }
    }
}*/


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
        private readonly IncomeTracker _incomeTracker;

        public UC_TrackIncome()
        {
            InitializeComponent();

            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\LoginData.mdf;Integrated Security=True;Connect Timeout=30";
            var repository = new SqlIncomeRepository(connectionString);
            _incomeTracker = new IncomeTracker(repository);
        }

        private void txtserchBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var (data, totalIncome) = _incomeTracker.GetIncome(txtserchIncome.SelectedIndex);

                guna2DataGridView1.DataSource = data.Tables[0];
                TextBoxReadOnly.Text = totalIncome.ToString("C");

                //disable resizing of header row
                guna2DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                // Add a margin or padding to allow the header to show
                guna2DataGridView1.RowHeadersVisible = false;
                guna2DataGridView1.ColumnHeadersHeight = 25;
                guna2DataGridView1.Padding = new Padding(0, 25, 0, 0);

                // Visual adjustments for Guna2DataGridView
                guna2DataGridView1.BackgroundColor = Color.White;
                guna2DataGridView1.GridColor = Color.LightGray;
                guna2DataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSteelBlue;
                guna2DataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                guna2DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
                guna2DataGridView1.EnableHeadersVisualStyles = false; // important to use custom styles
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Invalid selection. Please try again.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving income data.");
            }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            TextBoxReadOnly.ReadOnly = true;
        }

        private void UC_TrackIncome_Load(object sender, EventArgs e)
        {
        }
    }

    public interface IIncomeStrategy
    {
        string GetIncomeQuery();
        string GetTotalIncomeQuery();
        string GetPeriodName();
    }

    // WeeklyIncomeStrategy.cs
    public class WeeklyIncomeStrategy : IIncomeStrategy
    {
        public string GetIncomeQuery()
        {
            return @"SELECT ResidentEmail, SUM(Amount) AS TotalIncome, DATEPART(WEEK, Date) AS Week 
                FROM Income 
                WHERE Date >= DATEADD(WEEK, -1, GETDATE()) 
                GROUP BY ResidentEmail, DATEPART(WEEK, Date)";
        }

        public string GetTotalIncomeQuery()
        {
            return @"SELECT SUM(Amount) AS TotalIncome 
                FROM Income 
                WHERE Date >= DATEADD(WEEK, -1, GETDATE())";
        }

        public string GetPeriodName() => "Weekly";
    }

    // MonthlyIncomeStrategy.cs
    public class MonthlyIncomeStrategy : IIncomeStrategy
    {
        public string GetIncomeQuery()
        {
            return @"SELECT ResidentEmail, SUM(Amount) AS TotalIncome, MONTH(Date) AS Month, YEAR(Date) AS Year 
                FROM Income 
                WHERE Date >= DATEADD(MONTH, -1, GETDATE()) 
                GROUP BY ResidentEmail, MONTH(Date), YEAR(Date)";
        }

        public string GetTotalIncomeQuery()
        {
            return @"SELECT SUM(Amount) AS TotalIncome 
                FROM Income 
                WHERE Date >= DATEADD(MONTH, -1, GETDATE())";
        }

        public string GetPeriodName() => "Monthly";
    }

    // YearlyIncomeStrategy.cs
    public class YearlyIncomeStrategy : IIncomeStrategy
    {
        public string GetIncomeQuery()
        {
            return @"SELECT ResidentEmail, SUM(Amount) AS TotalIncome, YEAR(Date) AS Year 
                FROM Income 
                WHERE Date >= DATEADD(YEAR, -1, GETDATE()) 
                GROUP BY ResidentEmail, YEAR(Date)";
        }

        public string GetTotalIncomeQuery()
        {
            return @"SELECT SUM(Amount) AS TotalIncome 
                FROM Income 
                WHERE Date >= DATEADD(YEAR, -1, GETDATE())";
        }

        public string GetPeriodName() => "Yearly";
    }

    // IIncomeRepository.cs
    public interface IIncomeRepository
    {
        DataSet GetIncomeData(string query);
        decimal GetTotalIncome(string query);
    }

    // SqlIncomeRepository.cs
    public class SqlIncomeRepository : IIncomeRepository
    {
        private readonly string _connectionString;

        public SqlIncomeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataSet GetIncomeData(string query)
        {
            // Using your existing DPFunctions class
            return DPFunctions.Instance.getData(query);
        }

        public decimal GetTotalIncome(string query)
        {
            decimal totalIncome = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
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
    }

    // IncomeTracker.cs
    public class IncomeTracker
    {
        private readonly IIncomeRepository _repository;
        private readonly Dictionary<int, IIncomeStrategy> _strategies;

        public IncomeTracker(IIncomeRepository repository)
        {
            _repository = repository;
            _strategies = new Dictionary<int, IIncomeStrategy>
        {
            { 0, new WeeklyIncomeStrategy() },
            { 1, new MonthlyIncomeStrategy() },
            { 2, new YearlyIncomeStrategy() }
        };
        }

        public (DataSet Data, decimal TotalIncome) GetIncome(int strategyIndex)
        {
            if (!_strategies.TryGetValue(strategyIndex, out var strategy))
            {
                throw new ArgumentException("Invalid strategy index", nameof(strategyIndex));
            }

            var data = _repository.GetIncomeData(strategy.GetIncomeQuery());
            var totalIncome = _repository.GetTotalIncome(strategy.GetTotalIncomeQuery());

            return (data, totalIncome);
        }
    }
}


/*    public partial class UC_TrackIncome : UserControl
       {

           DPFunctions fn = DPFunctions.Instance;

           public UC_TrackIncome()
           {
               InitializeComponent();
           }
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
       }*/