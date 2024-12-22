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

namespace HotelManagementSystem1.AllUserControls_Resptions
{
    public partial class UC_RoomAssign : UserControl
    {

        private readonly RoomRepository _roomRepository;
        private readonly ResidentRepository _residentRepository;

/*        public UC_RoomAssign(RoomRepository roomRepository, ResidentRepository residentRepository)
        {
            InitializeComponent();
            _roomRepository = roomRepository;
            _residentRepository = residentRepository;
        }*/
          DPFunctions fn = DPFunctions.Instance;   
     //   private readonly DPFunctions fn;
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

        /*        private void UC_RoomAssign_Load(object sender, EventArgs e)
                {



                       String query = "select * from Rooms";
                              DataSet ds = fn.getData(query);
                              DataGridView1.DataSource = ds.Tables[0];
                }*/

        private void UC_RoomAssign_Load(object sender, EventArgs e)
        {
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


/*namespace HotelManagementSystem1.AllUserControls_Resptions
{
    public partial class UC_RoomAssign : UserControl
    {
        private readonly RoomRepository _roomRepository;
        private readonly ResidentRepository _residentRepository;

        public UC_RoomAssign(RoomRepository roomRepository, ResidentRepository residentRepository)
        {
            InitializeComponent();
            _roomRepository = roomRepository;
            _residentRepository = residentRepository;
        }

        private void UC_RoomAssign_Load(object sender, EventArgs e)
        {
            DataGridView1.DataSource = _roomRepository.GetAllRooms();
        }

        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            if (txtEmailres.Text != "" && txtRoNumber.Text != "" && guna2Comboxroomtype.Text != "" && guna2ComboBox2VIP.Text != "")
            {
                string email = txtEmailres.Text;
                string roomno = txtRoNumber.Text;
                string type = guna2Comboxroomtype.Text;
                string vipPackage = guna2ComboBox2VIP.Text;

                if (!_residentRepository.IsEmailExists(email))
                {
                    MessageBox.Show("The entered email does not exist in the Residents table.", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!_roomRepository.IsRoomAvailable(roomno))
                {
                    MessageBox.Show("The room is either occupied or does not exist.", "Invalid Room", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int rowsAffected = _roomRepository.AssignRoom(roomno, type, vipPackage, email);
                if (rowsAffected > 0)
                {
                    _residentRepository.AssignRoomToResident(email, roomno);
                    MessageBox.Show("Room successfully booked and assigned to the resident!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("The room status was changed before booking. Please try again.", "Room Status Changed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

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

        public void clearAll()
        {
            txtEmailres.Clear();
            txtRoNumber.Clear();
        }
    }
}*/

