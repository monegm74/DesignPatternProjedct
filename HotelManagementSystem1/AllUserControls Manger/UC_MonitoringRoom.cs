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
    public partial class UC_MonitoringRoom : UserControl
    {
        //  DPFunctions fn = new DPFunctions();
        DPFunctions fn = DPFunctions.Instance;
        public UC_MonitoringRoom()
        {
            InitializeComponent();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string query;

                // Use parameterized query to avoid SQL injection
                if (string.IsNullOrEmpty(txtRoomNO.Text))
                {
                    // Query to fetch all rooms when the TextBox is empty
                    query = "SELECT * FROM Rooms";


                }
                else
                {
                    // Query to filter rooms based on entered RoomID
                    query = "SELECT * FROM Rooms WHERE RoomID LIKE @RoomID + '%'";
                }

                // Fetch data using the existing method
                using (SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\LoginData.mdf;Integrated Security=True;Connect Timeout=30"))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (!string.IsNullOrEmpty(txtRoomNO.Text))
                        {
                            command.Parameters.AddWithValue("@RoomID", txtRoomNO.Text);
                        }

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);

                        // Ensure the dataset is not empty before binding
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            guna2DataGridView3.DataSource = ds.Tables[0];
                        }
                        else
                        {
                            guna2DataGridView3.DataSource = null; // Clear the DataGridView
                            MessageBox.Show("No data found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error: " + sqlEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void SetRooms(DataGridView dg)
        {
            String query = "select * from Rooms";
            DataSet ds = fn.getData(query);
            dg.DataSource = ds.Tables[0];
        }


        private void UC_MonitoringRoom_Load(object sender, EventArgs e)
        {
            SetRooms(guna2DataGridView3);

        }


        private void guna2DataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

     
        }
    }
}
*/

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
    public partial class UC_MonitoringRoom : UserControl
    {
        //  DPFunctions fn = new DPFunctions();
        DPFunctions fn = DPFunctions.Instance;
        public UC_MonitoringRoom()
        {
            InitializeComponent();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string query;

                // Use parameterized query to avoid SQL injection
                if (string.IsNullOrEmpty(txtRoomNO.Text))
                {
                    // Query to fetch all rooms when the TextBox is empty
                    query = "SELECT * FROM Rooms";


                }
                else
                {
                    // Query to filter rooms based on entered RoomID
                    query = "SELECT * FROM Rooms WHERE RoomID LIKE @RoomID + '%'";
                }

                // Fetch data using the existing method
                using (SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\LoginData.mdf;Integrated Security=True;Connect Timeout=30"))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (!string.IsNullOrEmpty(txtRoomNO.Text))
                        {
                            command.Parameters.AddWithValue("@RoomID", txtRoomNO.Text);
                        }

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);

                        // Ensure the dataset is not empty before binding
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            guna2DataGridView3.DataSource = ds.Tables[0];

                            //disable resizing of header row
                            guna2DataGridView3.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                            // Add a margin or padding to allow the header to show
                            guna2DataGridView3.RowHeadersVisible = false;
                            guna2DataGridView3.ColumnHeadersHeight = 25;
                            guna2DataGridView3.Padding = new Padding(0, 25, 0, 0); //  Adjust padding

                            // Visual adjustments for Guna2DataGridView
                            guna2DataGridView3.BackgroundColor = Color.White;
                            guna2DataGridView3.GridColor = Color.LightGray;
                            guna2DataGridView3.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSteelBlue;
                            guna2DataGridView3.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                            guna2DataGridView3.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
                            guna2DataGridView3.EnableHeadersVisualStyles = false; // important to use custom styles
                        }
                        else
                        {
                            guna2DataGridView3.DataSource = null; // Clear the DataGridView
                            MessageBox.Show("No data found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Database error: " + sqlEx.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void SetRooms(DataGridView dg)
        {
            String query = "select * from Rooms";
            DataSet ds = fn.getData(query);
            dg.DataSource = ds.Tables[0];

            //disable resizing of header row
            dg.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            // Add a margin or padding to allow the header to show
            dg.RowHeadersVisible = false;
            dg.ColumnHeadersHeight = 25;
            dg.Padding = new Padding(0, 25, 0, 0); // Adjust padding

            // Visual adjustments for DataGridView
            dg.BackgroundColor = Color.White;
            dg.GridColor = Color.LightGray;
            dg.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSteelBlue;
            dg.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dg.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
            dg.EnableHeadersVisualStyles = false; // important to use custom styles

        }


        private void UC_MonitoringRoom_Load(object sender, EventArgs e)
        {
            SetRooms(guna2DataGridView3);


        }


        private void guna2DataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }
    }
}
