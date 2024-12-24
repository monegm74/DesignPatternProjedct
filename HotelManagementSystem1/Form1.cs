using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelManagementSystem1
{

    public partial class Form1 : Form
    {
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\LoginData.mdf;Integrated Security=True;Connect Timeout=30");

        public Form1()
        {
            InitializeComponent();
            label5.Visible = false; // Hide the label when the form/control is initialized
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Login_RegisterHere_Click(object sender, EventArgs e)
        {
            Signup sForm = new Signup();
            sForm.Show();
            this.Hide();
        }

        private void Login_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /*        private void btnLogin_Click(object sender, EventArgs e)
                {


                    if (Login_txtUsername.Text == "" || Login_txtPassword.Text == "")
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

                                // Query to check username and password
                                string selectData = "SELECT Role FROM Users WHERE Username = @username AND PasswordHash = @pass";
                                using (SqlCommand cmd = new SqlCommand(selectData, connect))
                                {
                                    // Hash the entered password
                                    string hashedPassword = HashPassword(Login_txtPassword.Text.Trim());

                                    cmd.Parameters.AddWithValue("@username", Login_txtUsername.Text.Trim());
                                    cmd.Parameters.AddWithValue("@pass", hashedPassword);

                                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                                    DataTable table = new DataTable();
                                    adapter.Fill(table);

                                    if (table.Rows.Count == 1)
                                    {
                                        // Retrieve the role of the user
                                        string role = table.Rows[0]["Role"].ToString();

                                        // Redirect based on the role
                                        if (role == "Manager")
                                        {
                                            MessageBox.Show("Logged in as Manager successfully", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            ManagerForm managerForm = new ManagerForm(); // Replace with your Manager Form class
                                            managerForm.Show();
                                            this.Hide();
                                        }
                                        else if (role == "Receptionist")
                                        {
                                            MessageBox.Show("Logged in as Receptionist successfully", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            ReceptionistForm receptionistForm = new ReceptionistForm(); // Replace with your Receptionist Form class
                                            receptionistForm.Show();
                                            this.Hide();
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Incorrect Username/Password", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error Connecting: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            finally
                            {
                                connect.Close();
                            }
                        }
                    }


                }*/



        /*        private void btnLogin_Click(object sender, EventArgs e)
                {
                    if (Login_txtUsername.Text == "" || Login_txtPassword.Text == "")
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

                                // Query to check username and password
                                string selectData = "SELECT Role FROM Users WHERE Username = @username AND PasswordHash = @pass";
                                using (SqlCommand cmd = new SqlCommand(selectData, connect))
                                {
                                    // Hash the entered password
                                    string hashedPassword = HashPassword(Login_txtPassword.Text.Trim());

                                    cmd.Parameters.AddWithValue("@username", Login_txtUsername.Text.Trim());
                                    cmd.Parameters.AddWithValue("@pass", hashedPassword);

                                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                                    DataTable table = new DataTable();
                                    adapter.Fill(table);

                                    if (table.Rows.Count == 1)
                                    {
                                        // Retrieve the role of the user
                                        string role = table.Rows[0]["Role"].ToString();

                                        // Use the factory to create the appropriate role object
                                        IRole userRole = RoleFactory.CreateRole(role);
                                        MessageBox.Show($"Logged in as {role} successfully", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        // Display the appropriate dashboard
                                        userRole.DisplayDashboard();
                                        this.Hide();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Incorrect Username/Password", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error Connecting: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            finally
                            {
                                connect.Close();
                            }
                        }
                    }
                }*/



        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (Login_txtUsername.Text == "" || Login_txtPassword.Text == "")
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

                        // Query to check username and password
                        string selectData = "SELECT Role FROM Users WHERE Username = @username AND PasswordHash = @pass";
                        using (SqlCommand cmd = new SqlCommand(selectData, connect))
                        {
                            // Hash the entered password
                            string hashedPassword = HashPassword(Login_txtPassword.Text.Trim());

                            cmd.Parameters.AddWithValue("@username", Login_txtUsername.Text.Trim());
                            cmd.Parameters.AddWithValue("@pass", hashedPassword);

                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            DataTable table = new DataTable();
                            adapter.Fill(table);

                            if (table.Rows.Count == 1)
                            {
                                // Retrieve the role of the user
                                string role = table.Rows[0]["Role"].ToString();

                                // Use the factory to create the appropriate role object
                                IRole userRole = RoleFactory.CreateRole(role);
                                MessageBox.Show($"Logged in as {role} successfully", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Display the appropriate dashboard
                                userRole.DisplayDashboard();
                                this.Hide();
                            }
                            else
                            {
                                // Show incorrect password message and display the label
                                MessageBox.Show("Incorrect Username/Password", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                label5.Visible = true; // Ensure label5 is visible
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Connecting: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connect.Close();
                    }
                }
            }
        }

        private void Login_showpass_CheckedChanged(object sender, EventArgs e)
        {
            if (Login_showpass.Checked)
            {
                // Show password  
                Login_txtPassword.PasswordChar = '\0'; // or just "" to make it visible  
            }
            else
            {
                // Hide password  
                Login_txtPassword.PasswordChar = '*'; // or your preferred mask character  
            }
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

        private void label5_Click(object sender, EventArgs e)
        {
           
            ForgetPass ssForm = new ForgetPass();
            ssForm.Show();
            this.Hide();
        }
    }

    public interface IRole
    {
        void DisplayDashboard();
    }

    // Manager.cs
    public class Manager : IRole
    {
        public void DisplayDashboard()
        {
            ManagerForm managerForm = new ManagerForm(); // Your Manager Form
            managerForm.Show();
        }
    }

    // Receptionist.cs
    public class Receptionist : IRole
    {
        public void DisplayDashboard()
        {
            ReceptionistForm receptionistForm = new ReceptionistForm(); // Your Receptionist Form
            receptionistForm.Show();
        }
    }

    // RoleFactory.cs
    public static class RoleFactory
    {
        public static IRole CreateRole(string role)
        {
            if (role == "Manager")
            {
                return new Manager();
            }
            else if (role == "Receptionist")
            {
                return new Receptionist();
            }
            else
            {
                throw new ArgumentException("Invalid role type");
            }
        }
    }
}
