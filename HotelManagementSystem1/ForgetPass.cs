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
using System.Security.Cryptography;
using System.IO;
using Guna.UI2.WinForms;
using System.Web.UI.WebControls;

namespace HotelManagementSystem1
{
    public partial class ForgetPass : Form
    {

        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\LoginData.mdf;Integrated Security=True;Connect Timeout=30");

        public ForgetPass()
        {
            InitializeComponent();
            // Set initial values for the text boxes and labels
            label1.Text = "Welcome back!";
            Login_txtUsername.PlaceholderText = "Username";
            Login_id.PlaceholderText = "Enter The ID";
            guna2TextBox1.PlaceholderText = "Enter the New Pass";
            guna2TextBox2.PlaceholderText = "Confirm The Pass";

            btnLogin.Text = "Confirm";
        }

        private void Login_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            // guna2TextBox1.ReadOnly = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (Login_txtUsername.Text == "" || Login_id.Text == "" || guna2TextBox1.Text == "" || guna2TextBox2.Text == "")
            {
                MessageBox.Show("Please Fill All The Fields");
            }
            else if (guna2TextBox1.Text != guna2TextBox2.Text)
            {
                MessageBox.Show("Password Not Matched");
            }
            else
            {
                try
                {
                    connect.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE Username = @Username AND ApprovalID = @ApprovalID", connect);
                    cmd.Parameters.AddWithValue("@Username", Login_txtUsername.Text);
                    cmd.Parameters.AddWithValue("@ApprovalID", Login_id.Text);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        string newPassword = guna2TextBox1.Text;

                        // Hash the new password
                        string hashedPassword = HashPassword(newPassword);

                        // Update the password
                        SqlCommand updateCmd = new SqlCommand("UPDATE Users SET PasswordHash = @PasswordHash WHERE Username = @Username AND ApprovalID = @ApprovalID", connect);
                        updateCmd.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                        updateCmd.Parameters.AddWithValue("@Username", Login_txtUsername.Text);
                        updateCmd.Parameters.AddWithValue("@ApprovalID", Login_id.Text);

                        updateCmd.ExecuteNonQuery();
                        MessageBox.Show("Password Reset Successfully");
                        this.Hide();
                        Form1 login = new Form1();
                        login.Show();

                    }
                    else
                    {
                        MessageBox.Show("Wrong Username Or Email");
                    }

                    connect.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }

            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
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
    }
}