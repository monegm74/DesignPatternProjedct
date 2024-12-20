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
    public partial class UC_ManageResidents : UserControl
    {
        DPFunctions fn = new DPFunctions();
        public UC_ManageResidents()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }



        public void Setres(DataGridView dg)
        {
            String query = "select * from Residents";
            DataSet ds = fn.getData(query);
            dg.DataSource = ds.Tables[0];
        }

        private void RefreshDataGridView()
        {
            try
            {
                // SQL query to fetch all records from the Workers table
                string query = "SELECT * FROM Residents";

                // Fetch data using the existing method or another method you may have
                DataSet ds = fn.getData(query);

                // Bind the data to the DataGridView
                guna2DataGridView1.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error refreshing DataGridView: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddW_Click(object sender, EventArgs e)
        {
            // Check if all required fields are filled
            if (!string.IsNullOrEmpty(txtNameResident.Text) &&
                !string.IsNullOrEmpty(txtMobileRes.Text) &&
                !string.IsNullOrEmpty(txtGenderRes.Text) &&
                !string.IsNullOrEmpty(txtEmailIdres.Text) &&
                !string.IsNullOrEmpty(txtcheckinRes.Text) &&
                !string.IsNullOrEmpty(txtCheckoutRes.Text) &&
                !string.IsNullOrEmpty(txtBoardingRes.Text))
            {
                try
                {
                    // Get input values from the form
                    string name = txtNameResident.Text;
                    string contactInfo = txtMobileRes.Text;
                    string gender = txtGenderRes.Text;
                    string email = txtEmailIdres.Text;
                    DateTime checkInDate = txtcheckinRes.Value;
                    DateTime? checkOutDate = string.IsNullOrEmpty(txtCheckoutRes.Text) ? (DateTime?)null : txtCheckoutRes.Value;
                    string boardingType = txtBoardingRes.Text;

                    // Validate boarding type
                    string[] validBoardingTypes = { "Full Board", "Half Board", "Bed and Breakfast" };
                    if (!validBoardingTypes.Contains(boardingType))
                    {
                        MessageBox.Show("Invalid Boarding Type. Please select a valid option: Full Board, Half Board, or Bed and Breakfast.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Create SQL connection
                    using (SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\LoginData.mdf;Integrated Security=True;Connect Timeout=30"))
                    {
                        connect.Open();

                        // Check if email already exists
                        string checkEmailQuery = "SELECT COUNT(*) FROM Residents WHERE Email = @Email";
                        using (SqlCommand checkCmd = new SqlCommand(checkEmailQuery, connect))
                        {
                            checkCmd.Parameters.AddWithValue("@Email", email);
                            int emailCount = (int)checkCmd.ExecuteScalar();

                            if (emailCount > 0)
                            {
                                MessageBox.Show("Error: The email address is already in use. Please use a unique email.", "Duplicate Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        // Insert resident details
                        string query = "INSERT INTO Residents (Email, Name, ContactInfo, Gender, CheckInDate, CheckOutDate, BoardingType) " +
                                       "VALUES (@Email, @Name, @ContactInfo, @Gender, @CheckInDate, @CheckOutDate, @BoardingType)";
                        using (SqlCommand cmd = new SqlCommand(query, connect))
                        {
                            cmd.Parameters.AddWithValue("@Email", email);
                            cmd.Parameters.AddWithValue("@Name", name);
                            cmd.Parameters.AddWithValue("@ContactInfo", contactInfo);
                            cmd.Parameters.AddWithValue("@Gender", gender);
                            cmd.Parameters.AddWithValue("@CheckInDate", checkInDate);
                            cmd.Parameters.AddWithValue("@CheckOutDate", checkOutDate.HasValue ? (object)checkOutDate.Value : DBNull.Value);
                            cmd.Parameters.AddWithValue("@BoardingType", boardingType);

                            cmd.ExecuteNonQuery();

           
                            ClearAll();

                            MessageBox.Show("Resident added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please fill all the required fields.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure the click is valid (not on the header row or invalid index)
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Check that the clicked cell has a value
                if (guna2DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    // Fetch the entire row where the click occurred
                    DataGridViewRow row = guna2DataGridView1.Rows[e.RowIndex];

                    // Populate the TextBoxes with respective column values
                    guna2TextBoxEmailId.Text = row.Cells["Email"].Value?.ToString();       // Email column
                    guna2TextBoxName.Text = row.Cells["Name"].Value?.ToString();          // Name column
                    guna2TextBoxMobileNo.Text = row.Cells["ContactInfo"].Value?.ToString(); // ContactInfo column
                    guna2DateTimePickercheckin.Value = Convert.ToDateTime(row.Cells["CheckInDate"].Value); // CheckInDate column

                    // Check if CheckOutDate is not null before setting it
                    if (row.Cells["CheckOutDate"].Value != DBNull.Value && row.Cells["CheckOutDate"].Value != null)
                    {
                        guna2DateTimePickercheckout.Value = Convert.ToDateTime(row.Cells["CheckOutDate"].Value); // CheckOutDate column
                    }
                    else
                    {
                        guna2DateTimePickercheckout.Value = DateTime.Now; // Default to today's date if null
                    }

                    guna2ComboBoxboarding.Text = row.Cells["BoardingType"].Value?.ToString(); // BoardingType column

                    // Check if RoomID is not null before setting it
                    if (row.Cells["RoomID"].Value != DBNull.Value && row.Cells["RoomID"].Value != null)
                    {
                        guna2TextBoxRoomNo.Text = row.Cells["RoomID"].Value?.ToString();  // RoomID column
                    }
                    else
                    {
                        guna2TextBoxRoomNo.Text = ""; // Leave RoomID empty if null
                    }
                }
            }

        }

        private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void guna2ButtonUPdateWorker_Click(object sender, EventArgs e)
        {
            // Ensure a row is selected in the DataGridView
            if (guna2DataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    // Get the current Email (Primary Key) from the selected row
                    string oldEmail = guna2DataGridView1.SelectedRows[0].Cells["Email"].Value.ToString();
                    string newEmail = guna2TextBoxEmailId.Text;

                    // SQL queries
                    string checkEmailQuery = "SELECT COUNT(*) FROM Residents WHERE Email = @NewEmail AND Email != @OldEmail";
                    string checkRoomQuery = "SELECT COUNT(*) FROM Residents WHERE RoomID = @RoomID AND Email != @OldEmail";
                    string updateResidentQuery = "UPDATE Residents SET Name = @Name, ContactInfo = @ContactInfo, CheckInDate = @CheckInDate, " +
                                                 "CheckOutDate = @CheckOutDate, BoardingType = @BoardingType, RoomID = @RoomID, Email = @NewEmail " +
                                                 "WHERE Email = @OldEmail";

                    using (SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\LoginData.mdf;Integrated Security=True;Connect Timeout=30"))
                    {
                        conn.Open();

                        // Step 1: Check if the new email already exists
                        using (SqlCommand checkCmd = new SqlCommand(checkEmailQuery, conn))
                        {
                            checkCmd.Parameters.AddWithValue("@NewEmail", newEmail);
                            checkCmd.Parameters.AddWithValue("@OldEmail", oldEmail);

                            int emailCount = Convert.ToInt32(checkCmd.ExecuteScalar());
                            if (emailCount > 0)
                            {
                                MessageBox.Show("The new email is already in use by another record.", "Duplicate Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return; // Stop further execution
                            }
                        }

                        // Step 2: Check if the RoomID is already assigned to another resident
                        if (int.TryParse(guna2TextBoxRoomNo.Text, out int roomId))
                        {
                            using (SqlCommand checkRoomCmd = new SqlCommand(checkRoomQuery, conn))
                            {
                                checkRoomCmd.Parameters.AddWithValue("@RoomID", roomId);
                                checkRoomCmd.Parameters.AddWithValue("@OldEmail", oldEmail);

                                int roomCount = Convert.ToInt32(checkRoomCmd.ExecuteScalar());
                                if (roomCount > 0)
                                {
                                    MessageBox.Show("The selected RoomID is already assigned to another resident.", "Duplicate RoomID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return; // Stop further execution
                                }
                            }
                        }

                        // Step 3: Update the record in the Residents table
                        using (SqlCommand updateResidentCmd = new SqlCommand(updateResidentQuery, conn))
                        {
                            updateResidentCmd.Parameters.AddWithValue("@OldEmail", oldEmail); // Old Email to identify the record
                            updateResidentCmd.Parameters.AddWithValue("@NewEmail", newEmail); // New Email to update
                            updateResidentCmd.Parameters.AddWithValue("@Name", guna2TextBoxName.Text);
                            updateResidentCmd.Parameters.AddWithValue("@ContactInfo", guna2TextBoxMobileNo.Text);
                            updateResidentCmd.Parameters.AddWithValue("@CheckInDate", guna2DateTimePickercheckin.Value.Date);

                            // Check if CheckOutDate is set
                            if (guna2DateTimePickercheckout.Value != DateTime.MinValue)
                            {
                                updateResidentCmd.Parameters.AddWithValue("@CheckOutDate", guna2DateTimePickercheckout.Value.Date);
                            }
                            else
                            {
                                updateResidentCmd.Parameters.AddWithValue("@CheckOutDate", DBNull.Value);
                            }

                            updateResidentCmd.Parameters.AddWithValue("@BoardingType", guna2ComboBoxboarding.Text);

                            // Add RoomID
                            if (int.TryParse(guna2TextBoxRoomNo.Text, out roomId))
                            {
                                updateResidentCmd.Parameters.AddWithValue("@RoomID", roomId);
                            }
                            else
                            {
                                updateResidentCmd.Parameters.AddWithValue("@RoomID", DBNull.Value);
                            }

                            int rowsAffected = updateResidentCmd.ExecuteNonQuery();

                            // Show a success message if the update was successful
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Record updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Refresh the DataGridView to reflect updated data
                                RefreshDataGridView();

                                // Clear all fields after successful update
                                ClearAll();
                            }
                            else
                            {
                                MessageBox.Show("No record found with the specified Email.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Show error message if something goes wrong
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Show a warning message if no row is selected
                MessageBox.Show("Please select a row in the table.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }




        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string query;

                // Check if the textbox is empty
                if (string.IsNullOrEmpty(guna2TextBoxemailres.Text))
                {
                    // Query to fetch all workers when the TextBox is empty
                    query = "SELECT * FROM Residents";
                }
                else
                {
                    // Query to filter workers based on entered email
                    query = "SELECT * FROM Residents WHERE Email LIKE '" + guna2TextBoxemailres.Text + "%'";
                }

                // Fetch data using the existing method
                DataSet ds = fn.getData(query);

                // Bind the result to the DataGridView
                guna2DataGridView1.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tabControl1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                Setres(guna2DataGridView1);
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                Setres(guna2DataGridView2res);
            }
        }

        private void guna2ComboBoxboarding_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtEmailW_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string query;

                // Check if the textbox is empty
                if (string.IsNullOrEmpty(txtEmailres.Text))
                {
                    // Query to fetch all workers when the TextBox is empty
                    query = "SELECT * FROM Residents";
                }
                else
                {
                    // Query to filter workers based on entered email
                    query = "SELECT * FROM Residents WHERE Email LIKE '" + txtEmailres.Text + "%'";
                }

                // Fetch data using the existing method
                DataSet ds = fn.getData(query);

                // Bind the result to the DataGridView
                guna2DataGridView2res.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (txtEmailres.Text != "")
            {
                if (MessageBox.Show("Are You Sure?", "Confirmation...", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        // Create and open the connection
                        using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\LoginData.mdf;Integrated Security=True;Connect Timeout=30"))
                        {
                            connection.Open();

                            // Step 1: Delete rows in the Income table that reference this resident
                            string deleteIncomeQuery = "DELETE FROM Income WHERE ResidentEmail = @Email";
                            using (SqlCommand cmdIncome = new SqlCommand(deleteIncomeQuery, connection))
                            {
                                cmdIncome.Parameters.AddWithValue("@Email", txtEmailres.Text);
                                cmdIncome.ExecuteNonQuery();
                            }

                            // Step 2: Delete rows in the Rooms table that reference this resident
                            string deleteRoomsQuery = "DELETE FROM Rooms WHERE Email = @Email";
                            using (SqlCommand cmdRooms = new SqlCommand(deleteRoomsQuery, connection))
                            {
                                cmdRooms.Parameters.AddWithValue("@Email", txtEmailres.Text);
                                cmdRooms.ExecuteNonQuery();
                            }

                            // Step 3: Delete the resident from the Residents table
                            string deleteResidentQuery = "DELETE FROM Residents WHERE Email = @Email";
                            using (SqlCommand cmdResident = new SqlCommand(deleteResidentQuery, connection))
                            {
                                cmdResident.Parameters.AddWithValue("@Email", txtEmailres.Text);
                                cmdResident.ExecuteNonQuery();
                            }

                            MessageBox.Show("Record Deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Refresh the tab or perform any necessary UI updates
                            tabControl1_SelectedIndexChanged_1(this, null);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle errors and show a message box
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid email address.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private void guna2DataGridView2res_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure the click is valid (not on the header row or invalid index)
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Ensure the clicked cell has a value
                if (guna2DataGridView2res.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    // Fetch the entire row where the click occurred
                    DataGridViewRow row = guna2DataGridView2res.Rows[e.RowIndex];

                    // Get the Email value from the clicked row and set it to the TextBox
                    txtEmailres.Text = row.Cells["Email"].Value.ToString();  // Assuming "Email" is the column name
                }
            }
        }

        private void UC_ManageResidents_Load(object sender, EventArgs e)
        {

        }

        public void ClearAll()
        {
            txtNameResident.Clear();
            txtMobileRes.Clear();
            txtBoardingRes.SelectedIndex = -1;
            txtGenderRes.SelectedIndex = -1;
            txtEmailIdres.Clear();
            guna2TextBoxName.Clear();
            guna2TextBoxRoomNo.Clear();
            guna2TextBoxEmailId.Clear();
            guna2TextBoxMobileNo.Clear();
        }

        private void UC_ManageResidents_Leave(object sender, EventArgs e)
        {
            ClearAll();
        }
    }
}
