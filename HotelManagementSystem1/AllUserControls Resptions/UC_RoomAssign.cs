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

namespace HotelManagementSystem1.AllUserControls_Resptions
{
    public partial class UC_RoomAssign : UserControl
    {
      DPFunctions fn = new DPFunctions();   
        public UC_RoomAssign()
        {
            InitializeComponent();
        }

        private void guna2ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void UC_RoomAssign_Load(object sender, EventArgs e)
        {
           String query = "select * from Rooms";
            DataSet ds = fn.getData(query);
            DataGridView1.DataSource = ds.Tables[0];
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void clearAll()
        {
            txtEmailres.Clear();
            txtRoNumber.Clear();
        }




        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            // Ensure all required fields are filled
            if (txtEmailres.Text != "" && txtRoNumber.Text != "" && guna2Comboxroomtype.Text != "" && guna2ComboBox2VIP.Text != "")
            {
                // Extract values from input fields
                String email = txtEmailres.Text;
                String roomno = txtRoNumber.Text;
                String type = guna2Comboxroomtype.Text;
                String vipPackage = guna2ComboBox2VIP.Text;

                // Validate VIPPackage value
                if (vipPackage.ToUpper() != "YES" && vipPackage.ToUpper() != "NO")
                {
                    MessageBox.Show("VIP Package must be either 'YES' or 'NO'.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\LoginData.mdf;Integrated Security=True;Connect Timeout=30"))
                {
                    conn.Open();

                    // Step 1: Validate that the email exists in the Residents table
                    string emailCheckQuery = "SELECT COUNT(*) FROM Residents WHERE Email = @Email";
                    using (SqlCommand cmd = new SqlCommand(emailCheckQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);

                        int emailExists = Convert.ToInt32(cmd.ExecuteScalar());
                        if (emailExists == 0)
                        {
                            MessageBox.Show("The entered email does not exist in the Residents table.", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Step 2: Check if the room exists and its status
                    string roomStatusQuery = "SELECT Status FROM Rooms WHERE RoomID = @RoomID";
                    using (SqlCommand cmd = new SqlCommand(roomStatusQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@RoomID", roomno);
                        object statusObj = cmd.ExecuteScalar();

                        if (statusObj == null)
                        {
                            MessageBox.Show("The room number does not exist. Please enter a valid room number.", "Invalid Room", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        string currentStatus = statusObj.ToString();
                        if (currentStatus == "Occupied")
                        {
                            MessageBox.Show("The selected room is already occupied.", "Room Occupied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Step 3: Update the room's status to 'Occupied' and associate it with the resident's email
                    string updateRoomQuery = "UPDATE Rooms SET Status = 'Occupied', RoomType = @RoomType, VIPPackage = @VIPPackage, Email = @Email " +
                                             "WHERE RoomID = @RoomID AND Status = 'Available'";
                    using (SqlCommand cmd = new SqlCommand(updateRoomQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@RoomID", roomno);
                        cmd.Parameters.AddWithValue("@RoomType", type);
                        cmd.Parameters.AddWithValue("@VIPPackage", vipPackage.ToUpper());
                        cmd.Parameters.AddWithValue("@Email", email);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Step 4: Update the Residents table with the assigned RoomID
                            string updateResidentQuery = "UPDATE Residents SET RoomID = @RoomID WHERE Email = @Email";
                            using (SqlCommand residentCmd = new SqlCommand(updateResidentQuery, conn))
                            {
                                residentCmd.Parameters.AddWithValue("@RoomID", roomno);
                                residentCmd.Parameters.AddWithValue("@Email", email);

                                residentCmd.ExecuteNonQuery();
                            }

                            MessageBox.Show("Room successfully booked and assigned to the resident!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("The room status was changed before booking. Please try again.", "Room Status Changed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }

                // Refresh form and clear fields
                UC_RoomAssign_Load(this, null);
                // clearAll(); // Uncomment if you want to clear all fields
            }
            else
            {
                MessageBox.Show("Fill All Fields.", "Warning !!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UC_RoomAssign_Leave(object sender, EventArgs e)
        {
            clearAll();
        }







    }
}
