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

namespace HotelManagementSystem1.AllUserControls
{
    public partial class UC_ResidentInfo : UserControl
    {
        DPFunctions fn = new DPFunctions();
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
                               "residents.CheckOutDate, residents.BoardingType, rooms.RoomID, rooms.RoomType, rooms.PricePerNight, " +
                               "rooms.Status " +
                               "FROM residents " +
                               "INNER JOIN rooms ON residents.RoomID = rooms.RoomID";
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
                               "WHERE residents.CheckOutDate IS NULL";
                GetRecord(query);
            }
            else if (txtserchBy.SelectedIndex == 2)
            {
                // Query to fetch residents who have checked out (CheckOutDate is NOT NULL)
                string query = "SELECT residents.Email, residents.Name, residents.ContactInfo, residents.CheckInDate, " +
                               "residents.CheckOutDate, residents.BoardingType, rooms.RoomID, rooms.RoomType, rooms.PricePerNight, " +
                               "rooms.Status " +
                               "FROM residents " +
                               "INNER JOIN rooms ON residents.RoomID = rooms.RoomID " +
                               "WHERE residents.CheckOutDate IS NOT NULL";
                GetRecord(query);
            }
        }



        public void GetRecord(String query)
        {
            DataSet ds = fn.getData(query);
            guna2DataGridView1.DataSource = ds.Tables[0];

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
