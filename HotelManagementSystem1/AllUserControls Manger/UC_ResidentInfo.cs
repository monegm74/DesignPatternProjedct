
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;


namespace HotelManagementSystem1.AllUserControls
{
    public partial class UC_ResidentInfo : UserControl
    {
       
        DBHelpers fn = DBHelpers.Instance;
        public UC_ResidentInfo()
        {
            InitializeComponent();
        }

        private void txtserchBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtserchBy.SelectedIndex == 0)
            {
                // Query to fetch all residents with their room details
                string query = "SELECT residents.Email, residents.Name, residents.ContactInfo, residents.CheckInDate, " +
                               "residents.CheckOutDate, residents.BoardingType "+
                               "FROM residents "
                               ;
                GetRecord(query);
            }
            else if (txtserchBy.SelectedIndex == 1)
            {
                // Query to fetch residents who have not checked out (CheckOutDate is NULL)
                string query = "SELECT residents.Email, residents.Name, residents.ContactInfo, residents.CheckInDate, " +
                               "residents.CheckOutDate, residents.BoardingType, rooms.RoomID, rooms.RoomType, rooms.PricePerNight, " +
                               "rooms.Status " +
                               "FROM residents " +
                               "INNER JOIN rooms ON residents.RoomID = rooms.RoomID " +
                               "WHERE rooms.Status = 'Occupied'";
                GetRecord(query);
            }
            else if (txtserchBy.SelectedIndex == 2)
            {
                // Query to fetch residents who have checked out (CheckOutDate is NOT NULL)
                string query = "SELECT residents.Email, residents.Name, residents.ContactInfo, residents.CheckInDate, " +
                               "residents.CheckOutDate, residents.BoardingType " +
                               "FROM residents " +
                               "WHERE residents.RoomID IS NULL";


                GetRecord(query);
            }
        }

        public void GetRecord(String query)
        {
            DataSet ds = fn.getData(query);
            guna2DataGridView1.DataSource = ds.Tables[0];

            //disable resizing of header row
            guna2DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            // Add a margin or padding to allow the header to show
            guna2DataGridView1.RowHeadersVisible = false;
            guna2DataGridView1.ColumnHeadersHeight = 25;
            guna2DataGridView1.Padding = new Padding(0, 25, 0, 0);

            // Visual adjustments for Guna2DataGridView
            guna2DataGridView1.BackgroundColor = Color.White;
            guna2DataGridView1.GridColor = Color.LightGray;
            guna2DataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSteelBlue;
            guna2DataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            guna2DataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
            guna2DataGridView1.EnableHeadersVisualStyles = false; // important to use custom styles
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void UC_ResidentInfo_Load(object sender, EventArgs e)
        {

        }
    }
}