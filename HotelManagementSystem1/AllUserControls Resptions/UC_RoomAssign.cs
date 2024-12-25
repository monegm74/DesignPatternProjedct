/*using HotelManagementSystem1.Repositories;
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

        private readonly RoomRepository _roomRepository;
        private readonly ResidentRepository _residentRepository;
          DPFunctions fn = DPFunctions.Instance;   
    
        public UC_RoomAssign()
        {
            InitializeComponent();

            // Initialize the repositories
            _residentRepository = new ResidentRepository();
            _roomRepository = new RoomRepository();
        }

        private void guna2ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        private void UC_RoomAssign_Load(object sender, EventArgs e)
        {


       *//*     if (DataGridView1.Rows.Count > 0)
            {
                DataGridView1.Rows[0].Frozen = true;
                DataGridView1.Rows[0].DefaultCellStyle.BackColor = Color.LightGray;
                DataGridView1.Rows[0].DefaultCellStyle.ForeColor = Color.Black;
            }*//*
            try
            {
                if (DPFunctions.Instance == null)
                {
                    MessageBox.Show("DPFunctions.Instance is null. Ensure the singleton is correctly initialized.");
                    return;
                }

                String query = "SELECT * FROM Rooms";
                DataSet ds = DPFunctions.Instance.getData(query);

                if (ds == null || ds.Tables.Count == 0)
                {
                    MessageBox.Show("Dataset is null or has no tables. Check your query or database.");
                    return;
                }

                DataGridView1.DataSource = ds.Tables[0];

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during UC_RoomAssign_Load: " + ex.Message);
            }
        }

        public void RefreshGridView()
        {
            DataGridView1.DataSource = _roomRepository.GetAllRooms(); // Assuming this fetches the updated list
            DataGridView1.Refresh();
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
           
            // Check if repositories are initialized
            if (_residentRepository == null || _roomRepository == null)
            {
                MessageBox.Show("Repositories are not initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if controls are initialized
            if (txtEmailres == null || txtRoNumber == null || guna2Comboxroomtype == null || guna2ComboBox2VIP == null)
            {
                MessageBox.Show("One or more controls are not initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if the required fields are filled
            if (txtEmailres.Text != "" && txtRoNumber.Text != "" && guna2Comboxroomtype.Text != "" && guna2ComboBox2VIP.Text != "")
            {
                string email = txtEmailres.Text;
                string roomno = txtRoNumber.Text;
                string type = guna2Comboxroomtype.Text;
                string vipPackage = guna2ComboBox2VIP.Text;

                // Check if the email exists in the residents table
                if (!_residentRepository.IsEmailExists(email))
                {
                    MessageBox.Show("The entered email does not exist in the Residents table.", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Check if the room is available
                if (!_roomRepository.IsRoomAvailable(roomno))
                {
                    MessageBox.Show("The room is either occupied or does not exist.", "Invalid Room", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Assign the room to the resident
                int rowsAffected = _roomRepository.AssignRoom(roomno, type, vipPackage, email);
                if (rowsAffected > 0)
                {
                    // Assign room to resident in the repository
                    _residentRepository.AssignRoomToResident(email, roomno);
                    MessageBox.Show("Room successfully booked and assigned to the resident!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Reload or refresh the grid view (assuming a DataGridView named 'gridViewRooms')
                    DataGridView1.DataSource = _roomRepository.GetAllRooms(); // Assuming this fetches the updated list
                    DataGridView1.Refresh(); // Force refresh
                }
                else
                {
                    MessageBox.Show("The room status was changed before booking. Please try again.", "Room Status Changed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // Reload the room assignment UI
                UC_RoomAssign_Load(this, null);
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




*/


using HotelManagementSystem1.Repositories;
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
using Guna.UI2.WinForms;

namespace HotelManagementSystem1.AllUserControls_Resptions
{
    public partial class UC_RoomAssign : UserControl
    {

        private readonly RoomRepository _roomRepository;
        private readonly ResidentRepository _residentRepository;
        DBHelpers fn = DBHelpers.Instance;

        public UC_RoomAssign()
        {
            InitializeComponent();

            // Initialize the repositories
            _residentRepository = new ResidentRepository();
            _roomRepository = new RoomRepository();
        }

        private void guna2ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        private void UC_RoomAssign_Load(object sender, EventArgs e)
        {
            try
            {
                if (DBHelpers.Instance == null)
                {
                    MessageBox.Show("DPFunctions.Instance is null. Ensure the singleton is correctly initialized.");
                    return;
                }

                String query = "SELECT * FROM Rooms";
                DataSet ds = DBHelpers.Instance.getData(query);

                if (ds == null || ds.Tables.Count == 0)
                {
                    MessageBox.Show("Dataset is null or has no tables. Check your query or database.");
                    return;
                }

                DataGridView1.DataSource = ds.Tables[0];
                //disable resizing of header row
                DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

                // Add a margin or padding to allow the header to show
                DataGridView1.Location = new Point(DataGridView1.Location.X, DataGridView1.Location.Y + 25);
                DataGridView1.RowHeadersVisible = false;
                DataGridView1.ColumnHeadersHeight = 25;
                DataGridView1.Padding = new Padding(0, 20, 0, 0);

                // Visual adjustments for regular DataGridView
                DataGridView1.BackgroundColor = Color.White;
                DataGridView1.GridColor = Color.LightGray;
                DataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSteelBlue;
                DataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
                DataGridView1.EnableHeadersVisualStyles = false; // important to use custom styles
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during UC_RoomAssign_Load: " + ex.Message);
            }
        }

        public void RefreshGridView()
        {
            DataGridView1.DataSource = _roomRepository.GetAllRooms(); // Assuming this fetches the updated list
            DataGridView1.Refresh();
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

            // Check if repositories are initialized
            if (_residentRepository == null || _roomRepository == null)
            {
                MessageBox.Show("Repositories are not initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if controls are initialized
            if (txtEmailres == null || txtRoNumber == null || guna2Comboxroomtype == null || guna2ComboBox2VIP == null)
            {
                MessageBox.Show("One or more controls are not initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if the required fields are filled
            if (txtEmailres.Text != "" && txtRoNumber.Text != "" && guna2Comboxroomtype.Text != "" && guna2ComboBox2VIP.Text != "")
            {
                string email = txtEmailres.Text;
                string roomno = txtRoNumber.Text;
                string type = guna2Comboxroomtype.Text;
                string vipPackage = guna2ComboBox2VIP.Text;

                // Check if the email exists in the residents table
                if (!_residentRepository.IsEmailExists(email))
                {
                    MessageBox.Show("The entered email does not exist in the Residents table.", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Check if the room is available
                if (!_roomRepository.IsRoomAvailable(roomno))
                {
                    MessageBox.Show("The room is either occupied or does not exist.", "Invalid Room", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Assign the room to the resident
                int rowsAffected = _roomRepository.AssignRoom(roomno, type, vipPackage, email);
                if (rowsAffected > 0)
                {
                    // Assign room to resident in the repository
                    _residentRepository.AssignRoomToResident(email, roomno);
                    MessageBox.Show("Room successfully booked and assigned to the resident!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Reload or refresh the grid view (assuming a DataGridView named 'gridViewRooms')
                    DataGridView1.DataSource = _roomRepository.GetAllRooms(); // Assuming this fetches the updated list
                    DataGridView1.Refresh(); // Force refresh
                }
                else
                {
                    MessageBox.Show("The room status was changed before booking. Please try again.", "Room Status Changed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // Reload the room assignment UI
                UC_RoomAssign_Load(this, null);
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