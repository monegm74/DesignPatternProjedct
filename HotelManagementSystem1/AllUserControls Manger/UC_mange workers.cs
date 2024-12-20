using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace HotelManagementSystem1.AllUserControls
{
    public partial class UC_mange_workers : UserControl
    {

        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\LoginData.mdf;Integrated Security=True;Connect Timeout=30");
        DPFunctions fn = new DPFunctions();
        public UC_mange_workers()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {

        }

        private void btnRegister_Click_1(object sender, EventArgs e)
        {
            // Check if any field is empty
            if (txtNameworker.Text != "" && txtMobileWorker.Text != "" && txtGenderWorker.Text != "" && txtEmailIdWorker.Text != "" && txtSalaryWorker.Text != "" && txtJObTitaleW.Text != "")
            {
                // Get input data from the form fields
                String name = txtNameworker.Text;
                Int64 mobile = Int64.Parse(txtMobileWorker.Text);
                String gender = txtGenderWorker.Text;
                String email = txtEmailIdWorker.Text;
                Int64 salary = Int64.Parse(txtSalaryWorker.Text);
                String jobTitle = txtJObTitaleW.Text;

                // Create the SQL connection
                SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\LoginData.mdf;Integrated Security=True;Connect Timeout=30");

                try
                {
                    // Open the connection
                    connect.Open();

                    // First, check if the email already exists
                    string checkEmailQuery = "SELECT COUNT(*) FROM Workers WHERE Email = @Email";
                    using (SqlCommand checkCmd = new SqlCommand(checkEmailQuery, connect))
                    {
                        // Add the email parameter for checking
                        checkCmd.Parameters.AddWithValue("@Email", email);

                        // Execute the query to check if email exists
                        int emailCount = (int)checkCmd.ExecuteScalar();

                        // If email already exists, show an error message
                        if (emailCount > 0)
                        {
                            MessageBox.Show("Error: The email address is already in use. Please use a unique email.", "Duplicate Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return; // Exit the method if email is duplicate
                        }
                    }

                    // SQL INSERT query to add a new worker
                    String query = "INSERT INTO Workers (Email, Name, MobileNo, Gender, Salary, JobTitle) " +
                                    "VALUES (@Email, @Name, @MobileNo, @Gender, @Salary, @JobTitle)";

                    // Use SqlCommand to execute the query with parameters
                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        // Add parameters and assign values from the form
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@MobileNo", mobile);
                        cmd.Parameters.AddWithValue("@Gender", gender);
                        cmd.Parameters.AddWithValue("@Salary", salary);
                        cmd.Parameters.AddWithValue("@JobTitle", jobTitle);

                        // Execute the query
                        cmd.ExecuteNonQuery();

                        // Show success message
                        MessageBox.Show("Worker Added.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    // Show error message if there's an issue with the database
                    MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    // Close the connection
                    connect.Close();
                }
            }
            else
            {
                // If any field is empty, show a warning message
                MessageBox.Show("Fill all Fields.", "Warning...!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }





        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                SetWorker(guna2DataGridView1);
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                SetWorker(guna2DataGridView2);
            }
            else if (tabControl1.SelectedIndex == 3)
            {
                SetWorker(guna2DataGridView3);
            }
        }

        public void SetWorker(DataGridView dg)
        {
          String query = "select * from Workers";
            DataSet ds = fn.getData(query);
            dg.DataSource = ds.Tables[0];
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (txtEmailW.Text != "")
            {
                if (MessageBox.Show("Are You Sure?", "Confirmation...", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        // Define the query
                        String query = "DELETE FROM Workers WHERE Email = @Email";

                        // Create and open the connection
                        using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\LoginData.mdf;Integrated Security=True;Connect Timeout=30"))
                        {
                            connection.Open();  // Ensure the connection is open

                            // Create the SQL command
                            using (SqlCommand cmd = new SqlCommand(query, connection))
                            {
                                // Add the parameter
                                cmd.Parameters.AddWithValue("@Email", txtEmailW.Text);

                                // Execute the query
                                cmd.ExecuteNonQuery();

                                MessageBox.Show("Record Deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                tabControl1_SelectedIndexChanged(this, null);  // Refresh the tab or do any other required operation
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle errors and show a message box
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void txtEmailW_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string query;

                // Check if the textbox is empty
                if (string.IsNullOrEmpty(txtEmailW.Text))
                {
                    // Query to fetch all workers when the TextBox is empty
                    query = "SELECT * FROM Workers";
                }
                else
                {
                    // Query to filter workers based on entered email
                    query = "SELECT * FROM Workers WHERE Email LIKE '" + txtEmailW.Text + "%'";
                }

                // Fetch data using the existing method
                DataSet ds = fn.getData(query);

                // Bind the result to the DataGridView
                guna2DataGridView2.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            // Ensure a row is selected in the DataGridView
            if (guna2DataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    // Get the current Email (Primary Key) from the selected row
                    string oldEmail = guna2DataGridView1.SelectedRows[0].Cells["Email"].Value.ToString();
                    string newEmail = guna2TextBoxEmailId.Text;

                    // SQL query to check if the new email already exists in the database (excluding the current record)
                    string checkEmailQuery = "SELECT COUNT(*) FROM Workers WHERE Email = @NewEmail AND Email != @OldEmail";

                    // SQL query to update the record
                    string updateQuery = "UPDATE Workers SET Name = @Name, MobileNo = @MobileNo, Salary = @Salary, " +
                                         "JobTitle = @JobTitle, Email = @NewEmail WHERE Email = @OldEmail";

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

                        // Step 2: Update the record if the email is unique
                        using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                        {
                            updateCmd.Parameters.AddWithValue("@OldEmail", oldEmail); // Old Email to identify the record
                            updateCmd.Parameters.AddWithValue("@NewEmail", newEmail); // New Email to update
                            updateCmd.Parameters.AddWithValue("@Name", guna2TextBoxName.Text);
                            updateCmd.Parameters.AddWithValue("@MobileNo", guna2TextBoxMobileNo.Text);
                            updateCmd.Parameters.AddWithValue("@Salary", Convert.ToDecimal(guna2TextBoxSalary.Text));
                            updateCmd.Parameters.AddWithValue("@JobTitle", guna2TextBoxJobTitle.Text);

                            int rowsAffected = updateCmd.ExecuteNonQuery();

                            // Show a success message if the update was successful
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Record updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Refresh the DataGridView to reflect updated data
                                RefreshDataGridView();
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
                    guna2TextBoxEmailId.Text = row.Cells["Email"].Value.ToString();      // Email column
                    guna2TextBoxName.Text = row.Cells["Name"].Value.ToString();        // Name column
                    guna2TextBoxMobileNo.Text = row.Cells["MobileNo"].Value.ToString();  // MobileNo column
                                                                                      
                    guna2TextBoxSalary.Text = row.Cells["Salary"].Value.ToString();    // Salary column
                    guna2TextBoxJobTitle.Text = row.Cells["JobTitle"].Value.ToString(); // JobTitle column
                }
            }
        }
        private void RefreshDataGridView()
        {
            try
            {
                // SQL query to fetch all records from the Workers table
                string query = "SELECT * FROM Workers";

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

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string query;

                // Check if the textbox is empty
                if (string.IsNullOrEmpty(guna2TextBox1.Text))
                {
                    // Query to fetch all workers when the TextBox is empty
                    query = "SELECT * FROM Workers";
                }
                else
                {
                    // Query to filter workers based on entered email
                    query = "SELECT * FROM Workers WHERE Email LIKE '" + guna2TextBox1.Text + "%'";
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

        private void guna2TextBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure the click is valid (not on the header row or invalid index)
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Ensure the clicked cell has a value
                if (guna2DataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    // Fetch the entire row where the click occurred
                    DataGridViewRow row = guna2DataGridView2.Rows[e.RowIndex];

                    // Get the Email value from the clicked row and set it to the TextBox
                    txtEmailW.Text = row.Cells["Email"].Value.ToString();  // Assuming "Email" is the column name
                }
            }
        }

        private void guna2DataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void UC_mange_workers_Load(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
    }
}
