
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
        private readonly IWorkerService _workerService;
      //  SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\LoginData.mdf;Integrated Security=True;Connect Timeout=30");

        DBHelpers fn = DBHelpers.Instance;
        public UC_mange_workers()
        {
            InitializeComponent();
            _workerService = new WorkerServiceProxy();
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
                string mobile = txtMobileWorker.Text;
                String gender = txtGenderWorker.Text;
                String email = txtEmailIdWorker.Text;
                decimal salary = decimal.Parse(txtSalaryWorker.Text);
                String jobTitle = txtJObTitaleW.Text;

                try
                {
                    _workerService.AddWorker(name, email, mobile, gender, salary, jobTitle);

                    MessageBox.Show("Worker Added.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show($"Validation Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                catch (Exception ex)
                {
                    // Show error message if there's an issue with the database
                    MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            dg.DataSource = _workerService.GetWorkers().Tables[0];

            //disable resizing of header row
            dg.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // Add a margin or padding to allow the header to show
            if (dg is Guna.UI2.WinForms.Guna2DataGridView guna2DataGridView)
            {
                guna2DataGridView.Location = new Point(guna2DataGridView.Location.X, guna2DataGridView.Location.Y + 25);
                guna2DataGridView.RowHeadersVisible = false;
                guna2DataGridView.ColumnHeadersHeight = 25;
                guna2DataGridView.Padding = new Padding(0, 20, 0, 0);

                // Visual adjustments for Guna2DataGridView
                guna2DataGridView.BackgroundColor = Color.White;
                guna2DataGridView.GridColor = Color.LightGray;
                guna2DataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSteelBlue;
                guna2DataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                guna2DataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
                guna2DataGridView.EnableHeadersVisualStyles = false; // important to use custom styles
            }
            else
            {
                dg.Location = new Point(dg.Location.X, dg.Location.Y + 25);
                dg.RowHeadersVisible = false;
                dg.ColumnHeadersHeight = 25;
                dg.Padding = new Padding(0, 20, 0, 0);
                // Visual adjustments for regular DataGridView
                dg.BackgroundColor = Color.White;
                dg.GridColor = Color.LightGray;
                dg.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSteelBlue;
                dg.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                dg.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
                dg.EnableHeadersVisualStyles = false; // important to use custom styles

            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (txtEmailW.Text != "")
            {
                if (MessageBox.Show("Are You Sure?", "Confirmation...", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        _workerService.DeleteWorker(txtEmailW.Text);
                        MessageBox.Show("Record Deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tabControl1_SelectedIndexChanged(this, null);
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
                guna2DataGridView2.DataSource = _workerService.GetWorkers(txtEmailW.Text).Tables[0];
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
                    string name = guna2TextBoxName.Text;
                    string mobileNo = guna2TextBoxMobileNo.Text;
                    decimal salary = Convert.ToDecimal(guna2TextBoxSalary.Text);
                    string jobTitle = guna2TextBoxJobTitle.Text;


                    _workerService.UpdateWorker(oldEmail, newEmail, name, mobileNo, salary, jobTitle);


                    MessageBox.Show("Record updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Refresh the DataGridView to reflect updated data
                    RefreshDataGridView();
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show($"Validation Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    guna2TextBox1.Text = row.Cells["Email"].Value.ToString();  // Assuming "Email" is the column name
                }
            }
        }
        private void RefreshDataGridView()
        {
            try
            {

                // Fetch data using the existing method or another method you may have
                guna2DataGridView1.DataSource = _workerService.GetWorkers().Tables[0];
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

                guna2DataGridView1.DataSource = _workerService.GetWorkers(guna2TextBox1.Text).Tables[0];
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

    public interface IWorkerService
    {
        void AddWorker(string name, string email, string mobileNo, string gender, decimal salary, string jobTitle);
        void UpdateWorker(string oldEmail, string newEmail, string name, string mobileNo, decimal salary, string jobTitle);
        void DeleteWorker(string email);
        DataSet GetWorkers(string emailFilter = "");
    }

    public class RealWorkerService : IWorkerService
    {
        private readonly string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\LoginData.mdf;Integrated Security=True;Connect Timeout=30";
        private readonly DBHelpers fn = DBHelpers.Instance;

        public void AddWorker(string name, string email, string mobileNo, string gender, decimal salary, string jobTitle)
        {
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();

                string checkEmailQuery = "SELECT COUNT(*) FROM Workers WHERE Email = @Email";
                using (SqlCommand checkCmd = new SqlCommand(checkEmailQuery, connect))
                {
                    checkCmd.Parameters.AddWithValue("@Email", email);
                    if ((int)checkCmd.ExecuteScalar() > 0)
                    {
                        throw new Exception("The email address is already in use.");
                    }
                }

                string query = "INSERT INTO Workers (Email, Name, MobileNo, Gender, Salary, JobTitle) " +
                             "VALUES (@Email, @Name, @MobileNo, @Gender, @Salary, @JobTitle)";
                using (SqlCommand cmd = new SqlCommand(query, connect))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@MobileNo", mobileNo);
                    cmd.Parameters.AddWithValue("@Gender", gender);
                    cmd.Parameters.AddWithValue("@Salary", salary);
                    cmd.Parameters.AddWithValue("@JobTitle", jobTitle);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void UpdateWorker(string oldEmail, string newEmail, string name, string mobileNo, decimal salary, string jobTitle)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Check if new email exists (if changed)
                if (oldEmail != newEmail)
                {
                    string checkEmailQuery = "SELECT COUNT(*) FROM Workers WHERE Email = @NewEmail AND Email != @OldEmail";
                    using (SqlCommand checkCmd = new SqlCommand(checkEmailQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@NewEmail", newEmail);
                        checkCmd.Parameters.AddWithValue("@OldEmail", oldEmail);
                        if (Convert.ToInt32(checkCmd.ExecuteScalar()) > 0)
                        {
                            throw new Exception("The new email is already in use by another worker.");
                        }
                    }
                }
                // Update worker
                string updateQuery = "UPDATE Workers SET Name = @Name, MobileNo = @MobileNo, Salary = @Salary, " +
                                         "JobTitle = @JobTitle, Email = @NewEmail WHERE Email = @OldEmail";

                using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                {
                    updateCmd.Parameters.AddWithValue("@OldEmail", oldEmail);
                    updateCmd.Parameters.AddWithValue("@NewEmail", newEmail);
                    updateCmd.Parameters.AddWithValue("@Name", name);
                    updateCmd.Parameters.AddWithValue("@MobileNo", mobileNo);
                    updateCmd.Parameters.AddWithValue("@Salary", salary);
                    updateCmd.Parameters.AddWithValue("@JobTitle", jobTitle);

                    if (updateCmd.ExecuteNonQuery() == 0)
                    {
                        throw new Exception("No worker found with the specified email.");
                    }
                }
            }
        }


        public void DeleteWorker(string email)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Workers WHERE Email = @Email";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        throw new Exception("No worker found with the specified email.");
                    }
                }
            }
        }


        public DataSet GetWorkers(string emailFilter = "")
        {
            string query = string.IsNullOrEmpty(emailFilter)
                ? "SELECT * FROM Workers"
                : $"SELECT * FROM Workers WHERE Email LIKE '{emailFilter}%'";
            return fn.getData(query);
        }
    }

    public class WorkerServiceProxy : IWorkerService
    {
        private readonly RealWorkerService _realService;

        public WorkerServiceProxy()
        {
            _realService = new RealWorkerService();
        }

        public void AddWorker(string name, string email, string mobileNo, string gender, decimal salary, string jobTitle)
        {
            ValidateWorkerData(name, email, mobileNo, gender, salary, jobTitle);
            _realService.AddWorker(name, email, mobileNo, gender, salary, jobTitle);
        }

        public void UpdateWorker(string oldEmail, string newEmail, string name, string mobileNo, decimal salary, string jobTitle)
        {
            if (string.IsNullOrEmpty(oldEmail))
                throw new ArgumentException("Old email cannot be empty.");
            ValidateWorkerData(name, newEmail, mobileNo, null, salary, jobTitle);
            _realService.UpdateWorker(oldEmail, newEmail, name, mobileNo, salary, jobTitle);
        }


        public void DeleteWorker(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("Email cannot be empty.");
            _realService.DeleteWorker(email);
        }

        public DataSet GetWorkers(string emailFilter = "")
        {
            return _realService.GetWorkers(emailFilter);
        }

        private void ValidateWorkerData(string name, string email, string mobileNo, string gender, decimal salary, string jobTitle)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name cannot be empty.");
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("Email cannot be empty.");
            if (string.IsNullOrEmpty(mobileNo))
                throw new ArgumentException("Mobile number cannot be empty.");
            if (gender != null && (string.IsNullOrEmpty(gender) || (gender != "Male" && gender != "Female")))
                throw new ArgumentException("Gender must be either 'Male' or 'Female'.");
            if (salary <= 0)
                throw new ArgumentException("Salary must be a positive value.");
            if (string.IsNullOrEmpty(jobTitle))
                throw new ArgumentException("Job Title cannot be empty.");
        }
    }
}