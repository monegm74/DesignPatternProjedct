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

namespace HotelManagementSystem1
{
    public partial class Signup : Form
    {
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\LoginData.mdf;Integrated Security=True;Connect Timeout=30");
        public Signup()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void SignUP_loginhere_Click(object sender, EventArgs e)
        {
        Form1 FormI= new Form1();
            FormI.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (SignUP_txtEmailAdd.Text == "" || SignUP_txtusername.Text == "" || SignUP_Pass.Text == "" || Signup_ApprovalId.Text == "")
            {
                MessageBox.Show("Please fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (connect.State != ConnectionState.Open)
                {
                    try
                    {
                        connect.Open();

                        // Check if the ApprovalID exists in AllowedUsers table
                        string checkApprovalID = "SELECT * FROM AllowedUsers WHERE ApprovalID = @approvalID";

                        using (SqlCommand checkApprovalCmd = new SqlCommand(checkApprovalID, connect))
                        {
                            checkApprovalCmd.Parameters.AddWithValue("@approvalID", Signup_ApprovalId.Text.Trim());
                            SqlDataAdapter approvalAdapter = new SqlDataAdapter(checkApprovalCmd);
                            DataTable approvalTable = new DataTable();
                            approvalAdapter.Fill(approvalTable);

                            if (approvalTable.Rows.Count == 0)
                            {
                                MessageBox.Show("Invalid Approval ID. Registration is not allowed.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        // Check if the username already exists in Users table
                        string checkUsername = "SELECT * FROM Users WHERE Username = @username";

                        using (SqlCommand checkUserCmd = new SqlCommand(checkUsername, connect))
                        {
                            checkUserCmd.Parameters.AddWithValue("@username", SignUP_txtusername.Text.Trim());
                            SqlDataAdapter userAdapter = new SqlDataAdapter(checkUserCmd);
                            DataTable userTable = new DataTable();
                            userAdapter.Fill(userTable);

                            if (userTable.Rows.Count >= 1)
                            {
                                MessageBox.Show(SignUP_txtusername.Text + " already exists", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                // If role is Manager, check if a Manager already exists in the Users table
                                if (signup_role.Text.Trim().ToLower() == "manager")
                                {
                                    string checkManager = "SELECT * FROM Users WHERE Role = 'Manager'";

                                    using (SqlCommand checkManagerCmd = new SqlCommand(checkManager, connect))
                                    {
                                        SqlDataAdapter managerAdapter = new SqlDataAdapter(checkManagerCmd);
                                        DataTable managerTable = new DataTable();
                                        managerAdapter.Fill(managerTable);

                                        if (managerTable.Rows.Count >= 1)
                                        {
                                            MessageBox.Show("A Manager already exists. Only one Manager can be registered.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            return;
                                        }
                                    }
                                }

                                // Insert new user into the Users table
                                string insertData = "INSERT INTO Users (ApprovalID, Email, Username, PasswordHash, Role) " +
                                                    "VALUES (@ApprovalID, @Email, @Username, @PasswordHash, @Role)";

                                using (SqlCommand insertCmd = new SqlCommand(insertData, connect))
                                {
                                    string hashedPassword = HashPassword(SignUP_Pass.Text.Trim()); // Hash the password

                                    insertCmd.Parameters.AddWithValue("@ApprovalID", Signup_ApprovalId.Text.Trim());
                                    insertCmd.Parameters.AddWithValue("@Email", SignUP_txtEmailAdd.Text.Trim());
                                    insertCmd.Parameters.AddWithValue("@Username", SignUP_txtusername.Text.Trim());
                                    insertCmd.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                                    insertCmd.Parameters.AddWithValue("@Role", signup_role.Text.Trim());

                                    insertCmd.ExecuteNonQuery();

                                    MessageBox.Show("Registered successfully", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    // Switch to login form
                                    Form1 loginForm = new Form1();
                                    loginForm.Show();
                                    this.Hide();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error connecting to the database: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connect.Close();
                    }
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void Signup_ApprovalId_TextChanged(object sender, EventArgs e)
        {

        }

        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void SignUP_txtEmailAdd_TextChanged(object sender, EventArgs e)
        {

        }

        private void SignUP_showpass_CheckedChanged(object sender, EventArgs e)
        {
            if (SignUP_showpass.Checked)
            {
                // Show password  
                SignUP_Pass.PasswordChar = '\0'; // or just "" to make it visible  
            }
            else
            {
                // Hide password  
                SignUP_Pass.PasswordChar = '*'; // or your preferred mask character  
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}