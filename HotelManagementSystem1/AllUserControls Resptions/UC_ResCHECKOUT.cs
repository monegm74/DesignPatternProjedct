/*using Guna.UI2.WinForms;
using HotelManagementSystem1.Repositories;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;


namespace HotelManagementSystem1.AllUserControls_Resptions
{
    public partial class UC_ResCHECKOUT : UserControl
    {
      
        private readonly ResidentRepository _residentRepository = new ResidentRepository();
        private readonly RoomRepository _roomRepository = new RoomRepository();

        public UC_ResCHECKOUT()
        {
            InitializeComponent();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string query;

                // Check if the textbox is empty
                if (string.IsNullOrEmpty(txtemailres.Text))
                {
                    query = "SELECT * FROM Residents";
                }
                else
                {
                    query = "SELECT * FROM Residents WHERE Email LIKE '" + txtemailres.Text + "%'";
                }

                DataTable dataTable = _residentRepository.GetResidents(query);
                guna2DataGridViewrescheckout.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (guna2DataGridViewrescheckout.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    DataGridViewRow row = guna2DataGridViewrescheckout.Rows[e.RowIndex];

                    guna2TextBoxidmail.Text = row.Cells["Email"].Value?.ToString();
                    txtcnamerescheck.Text = row.Cells["Name"].Value?.ToString();

                    if (row.Cells["CheckOutDate"].Value != DBNull.Value && row.Cells["CheckOutDate"].Value != null)
                    {
                        txtDateCheckOut.Value = Convert.ToDateTime(row.Cells["CheckOutDate"].Value);
                    }
                    else
                    {
                        txtDateCheckOut.Value = DateTime.Now;
                    }

                    if (row.Cells["RoomID"].Value != DBNull.Value && row.Cells["RoomID"].Value != null)
                    {
                        txtcromnum.Text = row.Cells["RoomID"].Value?.ToString();

                        int roomId = Convert.ToInt32(row.Cells["RoomID"].Value);
                        decimal pricePerNight = _roomRepository.GetRoomPrice(roomId.ToString());

                        if (row.Cells["CheckInDate"].Value != DBNull.Value && row.Cells["CheckInDate"].Value != null)
                        {
                            DateTime checkInDate = Convert.ToDateTime(row.Cells["CheckInDate"].Value);
                            DateTime checkOutDate = txtDateCheckOut.Value;

                            int totalDays = (checkOutDate - checkInDate).Days;
                            if (totalDays < 0) totalDays = 0;

                            decimal totalPrice = totalDays * pricePerNight;
                            txtPriceofchecout.Text = totalPrice.ToString("C");
                        }
                        else
                        {
                            txtPriceofchecout.Text = "0";
                        }
                    }
                    else
                    {
                        txtcromnum.Text = "";
                        txtPriceofchecout.Text = "0";
                    }
                }
            }
        }

        private void UC_ResCHECKOUT_Load(object sender, EventArgs e)
        {
            DataTable dataTable = _residentRepository.GetAllResidents();
            guna2DataGridViewrescheckout.DataSource = dataTable;
        }

        public void clearAll()
        {
            txtcnamerescheck.Clear();
            txtcromnum.Clear();
            txtPriceofchecout.Clear();
            guna2TextBoxidmail.Clear();
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
           
            if (!string.IsNullOrEmpty(txtcnamerescheck.Text))
            {
                if (MessageBox.Show("Are You Sure?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    try
                    {
                       // string checkoutDate = txtDateCheckOut.Text;
                        string roomId = txtcromnum.Text;
                        string Email = guna2TextBoxidmail.Text;

                        DateTime checkoutDate = DateTime.Parse(txtDateCheckOut.Text);
                        _residentRepository.UpdateCheckoutDate(guna2TextBoxidmail.Text, checkoutDate);
                        _roomRepository.MarkRoomAsAvailable(roomId); 
                        _roomRepository.EmpytRoomID(Email);

                        MessageBox.Show("Check Out Successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                     
                        UC_ResCHECKOUT_Load(this, null);
                      
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("No Customer Selected.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void UC_ResCHECKOUT_Leave(object sender, EventArgs e)
        {
            clearAll();
        }

        private void txtPriceofchecout_TextChanged(object sender, EventArgs e)
        {
            txtPriceofchecout.ReadOnly = true;
        }
    }
}
*/


using Guna.UI2.WinForms;
using HotelManagementSystem1.AllUserControls;
using HotelManagementSystem1.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace HotelManagementSystem1.AllUserControls_Resptions
{
    public partial class UC_ResCHECKOUT : UserControl
    {

        private readonly ResidentRepository _residentRepository = new ResidentRepository();
        private readonly RoomRepository _roomRepository = new RoomRepository();

        public UC_ResCHECKOUT()
        {
            InitializeComponent();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string query;

                // Check if the textbox is empty
                if (string.IsNullOrEmpty(txtemailres.Text))
                {
                    query = "SELECT * FROM Residents";
                }
                else
                {
                    query = "SELECT * FROM Residents WHERE Email LIKE '" + txtemailres.Text + "%'";
                }

                DataTable dataTable = _residentRepository.GetResidents(query);
                guna2DataGridViewrescheckout.DataSource = dataTable;

                //disable resizing of header row
                guna2DataGridViewrescheckout.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                // Add a margin or padding to allow the header to show
                guna2DataGridViewrescheckout.RowHeadersVisible = false;
                guna2DataGridViewrescheckout.ColumnHeadersHeight = 25;
                guna2DataGridViewrescheckout.Padding = new Padding(0, 25, 0, 0); // Adjust padding


                // Visual adjustments for Guna2DataGridView
                guna2DataGridViewrescheckout.BackgroundColor = Color.White;
                guna2DataGridViewrescheckout.GridColor = Color.LightGray;
                guna2DataGridViewrescheckout.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSteelBlue;
                guna2DataGridViewrescheckout.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                guna2DataGridViewrescheckout.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
                guna2DataGridViewrescheckout.EnableHeadersVisualStyles = false; // important to use custom styles

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (guna2DataGridViewrescheckout.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    DataGridViewRow row = guna2DataGridViewrescheckout.Rows[e.RowIndex];

                    guna2TextBoxidmail.Text = row.Cells["Email"].Value?.ToString();
                    txtcnamerescheck.Text = row.Cells["Name"].Value?.ToString();

                    if (row.Cells["CheckOutDate"].Value != DBNull.Value && row.Cells["CheckOutDate"].Value != null)
                    {
                        txtDateCheckOut.Value = Convert.ToDateTime(row.Cells["CheckOutDate"].Value);
                    }
                    else
                    {
                        txtDateCheckOut.Value = DateTime.Now;
                    }

                    if (row.Cells["RoomID"].Value != DBNull.Value && row.Cells["RoomID"].Value != null)
                    {
                        txtcromnum.Text = row.Cells["RoomID"].Value?.ToString();

                        int roomId = Convert.ToInt32(row.Cells["RoomID"].Value);
                        decimal pricePerNight = _roomRepository.GetRoomPrice(roomId.ToString());

                        if (row.Cells["CheckInDate"].Value != DBNull.Value && row.Cells["CheckInDate"].Value != null)
                        {
                            DateTime checkInDate = Convert.ToDateTime(row.Cells["CheckInDate"].Value);
                            DateTime checkOutDate = txtDateCheckOut.Value;

                            int totalDays = (checkOutDate - checkInDate).Days;
                            if (totalDays < 0) totalDays = 0;

                            decimal totalPrice = totalDays * pricePerNight;
                            //txtPriceofchecout.Text = totalPrice.ToString("C");
                            txtPriceofchecout.Text = totalPrice.ToString("F2");
                        }
                        else
                        {
                            txtPriceofchecout.Text = "0";
                        }
                    }
                    else
                    {
                        txtcromnum.Text = "";
                        txtPriceofchecout.Text = "0";
                    }
                    txtemailres.Text = row.Cells["Email"].Value.ToString();
                }
            }
        }

        private void UC_ResCHECKOUT_Load(object sender, EventArgs e)
        {
            DataTable dataTable = _residentRepository.GetAllResidents();
            guna2DataGridViewrescheckout.DataSource = dataTable;

            //disable resizing of header row
            guna2DataGridViewrescheckout.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            // Add a margin or padding to allow the header to show
            guna2DataGridViewrescheckout.RowHeadersVisible = false;
            guna2DataGridViewrescheckout.ColumnHeadersHeight = 25;
            guna2DataGridViewrescheckout.Padding = new Padding(0, 25, 0, 0); // Adjust padding

            // Visual adjustments for Guna2DataGridView
            guna2DataGridViewrescheckout.BackgroundColor = Color.White;
            guna2DataGridViewrescheckout.GridColor = Color.LightGray;
            guna2DataGridViewrescheckout.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSteelBlue;
            guna2DataGridViewrescheckout.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            guna2DataGridViewrescheckout.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
            guna2DataGridViewrescheckout.EnableHeadersVisualStyles = false; // important to use custom styles
        }

        public void clearAll()
        {
            txtcnamerescheck.Clear();
            txtcromnum.Clear();
            txtPriceofchecout.Clear();
            guna2TextBoxidmail.Clear();
        }


        // Method to insert income into the database





        /*        private void btnCheckOut_Click(object sender, EventArgs e)
                {

                    if (!string.IsNullOrEmpty(txtcnamerescheck.Text))
                    {
                        if (MessageBox.Show("Are You Sure?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                        {
                            try
                            {
                                // string checkoutDate = txtDateCheckOut.Text;
                                string roomId = txtcromnum.Text;
                                string Email = guna2TextBoxidmail.Text;


                                DateTime checkoutDate = DateTime.Parse(txtDateCheckOut.Text);
                                _residentRepository.UpdateCheckoutDate(guna2TextBoxidmail.Text, checkoutDate);
                                _roomRepository.MarkRoomAsAvailable(roomId);
                                _roomRepository.EmpytRoomID(Email);

                                MessageBox.Show("Check Out Successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                UC_ResCHECKOUT_Load(this, null);

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("No Customer Selected.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }*/

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtcnamerescheck.Text))
            {
                if (MessageBox.Show("Are You Sure?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    try
                    {
                        string roomId = txtcromnum.Text;
                        string email = guna2TextBoxidmail.Text;

                        // Parse checkout date
                        DateTime checkoutDate = DateTime.Parse(txtDateCheckOut.Text);

                        // Parse the price of checkout from txtPriceofchecout
                        string priceText = txtPriceofchecout.Text.Replace(CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol, "").Trim();
                        if (!decimal.TryParse(priceText, NumberStyles.Currency, CultureInfo.CurrentCulture, out decimal price))
                        {
                            MessageBox.Show("Invalid price format. Please ensure the value is numeric.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Update checkout date for resident
                        _residentRepository.UpdateCheckoutDate(email, checkoutDate);

                        // Update room availability
                        _roomRepository.MarkRoomAsAvailable(roomId);
                        _roomRepository.EmpytRoomID(email);

                        // Insert income into the Income table
                        InsertIncome(email, price, checkoutDate);

                        MessageBox.Show("Check Out Successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Reload UI
                        UC_ResCHECKOUT_Load(this, null);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("No Customer Selected.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }




        public void InsertIncome(string residentEmail, decimal amount, DateTime date)
        {
            using (var connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=E:\\LoginData.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=false"))
            {
                connection.Open();
                var command = new SqlCommand("INSERT INTO Income (ResidentEmail, Amount, Date) VALUES (@Email, @Amount, @Date)", connection);
                command.Parameters.AddWithValue("@Email", residentEmail);
                command.Parameters.AddWithValue("@Amount", amount);
                command.Parameters.AddWithValue("@Date", date);

                command.ExecuteNonQuery(); // IncomeID will be auto-generated  
            }
        }

        private void UC_ResCHECKOUT_Leave(object sender, EventArgs e)
        {
            clearAll();
        }

        private void txtPriceofchecout_TextChanged(object sender, EventArgs e)
        {
            txtPriceofchecout.ReadOnly = true;
        }
    }
}