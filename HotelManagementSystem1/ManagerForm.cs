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
    public partial class ManagerForm : Form
    {
        public ManagerForm()
        {
            InitializeComponent();
        }

        private void btnExist2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnAddRoom_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {

        }

        private void btnAddRoom_Click_1(object sender, EventArgs e)
        {
            MovingPanal.Left = btnAddRoom.Left + 18;
            uC_mange_workers1.Visible = true;
            uC_mange_workers1.BringToFront();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            MovingPanal.Left = BtnResidentInfo.Left + 18;
            uC_ResidentInfo1.Visible = true;
            uC_ResidentInfo1.BringToFront();
        }

        private void MovingPanal_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ManagerForm_Load(object sender, EventArgs e)
        {
           // btnAddRoom.Visible = false;
            uC_ResidentInfo1.Visible = false;
            btnAddRoom.PerformClick();
        }

        private void btnCusDetils_Click(object sender, EventArgs e)
        {

            MovingPanal.Left = btnTrackIncome.Left + 18;
            uC_TrackIncome1.Visible = true;
            uC_TrackIncome1.BringToFront();

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

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            MovingPanal.Left = BTN_ROOMMonitor.Left + 18;
            uC_MonitoringRoom1.Visible = true;
            uC_MonitoringRoom1.BringToFront();

        }
    }
}
