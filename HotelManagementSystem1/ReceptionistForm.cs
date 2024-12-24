using HotelManagementSystem1.AllUserControls;
using HotelManagementSystem1.AllUserControls_Resptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelManagementSystem1
{
    public partial class ReceptionistForm : Form
    {
        public ReceptionistForm()
        {
            InitializeComponent();
        }

        private void ReceptionistForm_Load(object sender, EventArgs e)
        {
            // btnAddRoom.Visible = false;
            uC_RoomAssign1.Visible = false;
            btnManageRes.PerformClick();
        }

        private void btnExist2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            // Confirmation dialog
            DialogResult result = MessageBox.Show("Are you sure you want to logout?",
                                                  "Logout Confirmation",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Go back to the login form
                Form1 loginForm = new Form1(); // Replace with the actual name of your login form
                loginForm.Show();

                this.Close(); // Close the current form
            }
        }

        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            MovingPanal.Left = btnManageRes.Left + 18;
            uC_ManageResidents1.Visible = true;
            uC_ManageResidents1.BringToFront();
        }

        private void BTN_ROOMMonitor_Click(object sender, EventArgs e)
        {
            MovingPanal.Left = BTN_ROOMASS.Left + 18;
            uC_RoomAssign1.Visible = true;
            uC_RoomAssign1.BringToFront();


        }

        private void btnTrackIncome_Click(object sender, EventArgs e)
        {
            MovingPanal.Left = btnCheckout.Left + 18;
            uC_ResCHECKOUT1.Visible = true;
            uC_ResCHECKOUT1.BringToFront();


        }

        private void BTN_ROOMASS_Leave(object sender, EventArgs e)
        {
           
        }

        private void btnCheckout_Leave(object sender, EventArgs e)
        {
    
        }
    }
}
