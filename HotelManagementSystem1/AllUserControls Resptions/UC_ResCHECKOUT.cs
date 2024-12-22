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

namespace HotelManagementSystem1.AllUserControls_Resptions
{
    public partial class UC_ResCHECKOUT : UserControl
    {
       DPFunctions fn = DPFunctions.Instance;
       // private readonly DPFunctions fn;
        public UC_ResCHECKOUT()
        {
            InitializeComponent();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string query;

                // Check if the textbox is empty
                if (string.IsNullOrEmpty(txtemailres.Text))
                {
                    // Query to fetch all workers when the TextBox is empty
                    query = "SELECT * FROM Residents";
                }
                else
                {
                    // Query to filter workers based on entered email
                    query = "SELECT * FROM Residents WHERE Email LIKE '" + txtemailres.Text + "%'";
                }

                // Fetch data using the existing method
                DataSet ds = fn.getData(query);

                // Bind the result to the DataGridView
                guna2DataGridViewrescheckout.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (guna2DataGridViewrescheckout.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    // Fetch the row where the click occurred
                    DataGridViewRow row = guna2DataGridViewrescheckout.Rows[e.RowIndex];

                    // Populate TextBoxes with respective column values
                    guna2TextBoxidmail.Text = row.Cells["Email"].Value?.ToString();       // Email column
                    txtcnamerescheck.Text = row.Cells["Name"].Value?.ToString();          // Name column

                    if (row.Cells["CheckOutDate"].Value != DBNull.Value && row.Cells["CheckOutDate"].Value != null)
                    {
                        txtDateCheckOut.Value = Convert.ToDateTime(row.Cells["CheckOutDate"].Value);
                    }
                    else
                    {
                        txtDateCheckOut.Value = DateTime.Now; // Default to today's date if null
                    }

                    if (row.Cells["RoomID"].Value != DBNull.Value && row.Cells["RoomID"].Value != null)
                    {
                        txtcromnum.Text = row.Cells["RoomID"].Value?.ToString();

                        // Fetch PricePerNight from the Rooms table based on RoomID
                        int roomId = Convert.ToInt32(row.Cells["RoomID"].Value);
                        decimal pricePerNight = GetPricePerNight(roomId); // Method to fetch price per night

                        if (row.Cells["CheckInDate"].Value != DBNull.Value && row.Cells["CheckInDate"].Value != null)
                        {
                            DateTime checkInDate = Convert.ToDateTime(row.Cells["CheckInDate"].Value);
                            DateTime checkOutDate = txtDateCheckOut.Value;

                            // Calculate total days
                            int totalDays = (checkOutDate - checkInDate).Days;
                            if (totalDays < 0) totalDays = 0; // Ensure no negative days

                            // Calculate total price
                            decimal totalPrice = totalDays * pricePerNight;
                            txtPriceofchecout.Text = totalPrice.ToString("C"); // Display as currency
                        }
                        else
                        {
                            txtPriceofchecout.Text = "0"; // Default to 0 if CheckInDate is null
                        }
                    }
                    else
                    {
                        txtcromnum.Text = ""; // RoomID is empty
                        txtPriceofchecout.Text = "0";  // Default total price to 0
                    }
                }
            }
        }


        private decimal GetPricePerNight(int roomId)
        {
            decimal pricePerNight = 0;

            // Update the connection string as needed
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\LoginData.mdf;Integrated Security=True;Connect Timeout=30";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT PricePerNight FROM Rooms WHERE RoomID = @RoomID";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@RoomID", roomId);
                    connection.Open();

                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        pricePerNight = Convert.ToDecimal(result);
                    }
                }
            }

            return pricePerNight;
        }


        private void UC_ResCHECKOUT_Load(object sender, EventArgs e)
        {
            String query = "select * from Residents";
            DataSet ds = fn.getData(query);
            guna2DataGridViewrescheckout.DataSource = ds.Tables[0];
        }
        public void clearAll()
        {
            txtcnamerescheck.Clear();
            txtcromnum.Clear();
            txtPriceofchecout.Clear();
            guna2TextBoxidmail.Clear();
          
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            if (txtcnamerescheck.Text != "") // Check if a customer is selected
            {
                if (MessageBox.Show("Are You Sure?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    try
                    {
                        string checkoutDate = txtDateCheckOut.Text;
                        int roomId = string.IsNullOrEmpty(txtcromnum.Text) ? 0 : Convert.ToInt32(txtcromnum.Text);

                        // Update the Residents and Rooms tables
                        string query = @"
                    UPDATE Residents 
                    SET CheckOutDate = @CheckOutDate 
                    WHERE Email = @Email;

                    UPDATE Rooms 
                    SET Status = 'Available', 
                        Email = NULL 
                    WHERE RoomID = @RoomID;
                ";

                        using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\LoginData.mdf;Integrated Security=True;Connect Timeout=30"))
                        {
                            conn.Open();

                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                // Parameters for the Residents table
                                cmd.Parameters.AddWithValue("@CheckOutDate", checkoutDate);
                                cmd.Parameters.AddWithValue("@Email", guna2TextBoxidmail.Text);

                                // Parameters for the Rooms table
                                cmd.Parameters.AddWithValue("@RoomID", roomId);

                                cmd.ExecuteNonQuery();
                            }
                        }

                        MessageBox.Show("Check Out Successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Refresh the data and clear all input fields
                        UC_ResCHECKOUT_Load(this, null);
                      // clearAll();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("No Customer Selected.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void UC_ResCHECKOUT_Leave(object sender, EventArgs e)
        {
            clearAll();
        }

        private void txtPriceofchecout_TextChanged(object sender, EventArgs e)
        {
            txtPriceofchecout.ReadOnly = true;
        }
    }
}
