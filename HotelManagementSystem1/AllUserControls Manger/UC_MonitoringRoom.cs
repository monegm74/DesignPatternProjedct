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
