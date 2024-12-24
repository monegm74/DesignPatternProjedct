/*using Guna.UI2.WinForms;
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

namespace HotelManagementSystem1.AllUserControls_Resptions
{
    public partial class UC_ManageResidents : UserControl
    {
        private readonly IResidentService _residentService;
        private DPFunctions fn = DPFunctions.Instance;

        public UC_ManageResidents()
        {
            InitializeComponent();
            _residentService = new ResidentServiceProxy();
        }

        public void Setres(DataGridView dg)
        {
            dg.DataSource = _residentService.GetResidents().Tables[0];
        }

        private void RefreshDataGridView()
        {
            try
            {
                guna2DataGridView1.DataSource = _residentService.GetResidents().Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error refreshing DataGridView: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddW_Click(object sender, EventArgs e)
        {
            try
            {
                _residentService.AddResident(
                    txtNameResident.Text,
                    txtMobileRes.Text,
                    txtGenderRes.Text,
                    txtEmailIdres.Text,
                    txtcheckinRes.Value,
                    txtCheckoutRes.Value,
                    txtBoardingRes.Text
                );

                MessageBox.Show("Resident added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2ButtonUPdateWorker_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row in the table.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string oldEmail = guna2DataGridView1.SelectedRows[0].Cells["Email"].Value.ToString();
                int? roomId = null;
                if (int.TryParse(guna2TextBoxRoomNo.Text, out int parsedRoomId))
                {
                    roomId = parsedRoomId;
                }

                _residentService.UpdateResident(
                    oldEmail,
                    guna2TextBoxEmailId.Text,
                    guna2TextBoxName.Text,
                    guna2TextBoxMobileNo.Text,
                    guna2DateTimePickercheckin.Value,
                    guna2DateTimePickercheckout.Value,
                    guna2ComboBoxboarding.Text,
                    roomId
                );

                MessageBox.Show("Record updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshDataGridView();
                ClearAll();
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
                if (guna2DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    DataGridViewRow row = guna2DataGridView1.Rows[e.RowIndex];

                    guna2TextBoxEmailId.Text = row.Cells["Email"].Value?.ToString();
                    guna2TextBoxName.Text = row.Cells["Name"].Value?.ToString();
                    guna2TextBoxMobileNo.Text = row.Cells["ContactInfo"].Value?.ToString();
                    guna2DateTimePickercheckin.Value = Convert.ToDateTime(row.Cells["CheckInDate"].Value);

                    if (row.Cells["CheckOutDate"].Value != DBNull.Value && row.Cells["CheckOutDate"].Value != null)
                    {
                        guna2DateTimePickercheckout.Value = Convert.ToDateTime(row.Cells["CheckOutDate"].Value);
                    }
                    else
                    {
                        guna2DateTimePickercheckout.Value = DateTime.Now;
                    }

                    guna2ComboBoxboarding.Text = row.Cells["BoardingType"].Value?.ToString();

                    if (row.Cells["RoomID"].Value != DBNull.Value && row.Cells["RoomID"].Value != null)
                    {
                        guna2TextBoxRoomNo.Text = row.Cells["RoomID"].Value?.ToString();
                    }
                    else
                    {
                        guna2TextBoxRoomNo.Text = "";
                    }
                }
            }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                guna2DataGridView1.DataSource = _residentService.GetResidents(guna2TextBoxemailres.Text).Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tabControl1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                Setres(guna2DataGridView1);
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                Setres(guna2DataGridView2res);
            }
        }

        private void guna2ComboBoxboarding_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Event handler for boarding type selection
        }

        private void txtEmailW_TextChanged(object sender, EventArgs e)
        {
            try
            {
                guna2DataGridView2res.DataSource = _residentService.GetResidents(txtEmailres.Text).Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmailres.Text))
            {
                MessageBox.Show("Please enter a valid email address.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Are You Sure?", "Confirmation...", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    _residentService.DeleteResident(txtEmailres.Text);
                    MessageBox.Show("Record Deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tabControl1_SelectedIndexChanged_1(this, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void guna2DataGridView2res_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (guna2DataGridView2res.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    DataGridViewRow row = guna2DataGridView2res.Rows[e.RowIndex];
                    txtEmailres.Text = row.Cells["Email"].Value.ToString();
                }
            }
        }

        private void UC_ManageResidents_Load(object sender, EventArgs e)
        {
            // Initialize any required data or UI elements when the control loads
        }

        public void ClearAll()
        {
            txtNameResident.Clear();
            txtMobileRes.Clear();
            txtBoardingRes.SelectedIndex = -1;
            txtGenderRes.SelectedIndex = -1;
            txtEmailIdres.Clear();
            guna2TextBoxName.Clear();
            guna2TextBoxRoomNo.Clear();
            guna2TextBoxEmailId.Clear();
            guna2TextBoxMobileNo.Clear();
        }

        private void UC_ManageResidents_Leave(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // Event handler for label click
        }

        private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            // Event handler for date time picker value change
        }
        // Interface defining resident management operations
        public interface IResidentService
    {
        void AddResident(string name, string contactInfo, string gender, string email, DateTime checkInDate, DateTime? checkOutDate, string boardingType);
        void UpdateResident(string oldEmail, string newEmail, string name, string contactInfo, DateTime checkInDate, DateTime? checkOutDate, string boardingType, int? roomId);
        void DeleteResident(string email);
        DataSet GetResidents(string emailFilter = "");
    }

    // Real resident service implementation
    public class RealResidentService : IResidentService
    {
        private readonly string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\LoginData.mdf;Integrated Security=True;Connect Timeout=30";
        private readonly DPFunctions fn = DPFunctions.Instance;

        public void AddResident(string name, string contactInfo, string gender, string email, DateTime checkInDate, DateTime? checkOutDate, string boardingType)
        {
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();

                // Check if email already exists
                string checkEmailQuery = "SELECT COUNT(*) FROM Residents WHERE Email = @Email";
                using (SqlCommand checkCmd = new SqlCommand(checkEmailQuery, connect))
                {
                    checkCmd.Parameters.AddWithValue("@Email", email);
                    if ((int)checkCmd.ExecuteScalar() > 0)
                    {
                        throw new Exception("The email address is already in use.");
                    }
                }

                // Insert resident details
                string query = "INSERT INTO Residents (Email, Name, ContactInfo, Gender, CheckInDate, CheckOutDate, BoardingType) " +
                             "VALUES (@Email, @Name, @ContactInfo, @Gender, @CheckInDate, @CheckOutDate, @BoardingType)";
                using (SqlCommand cmd = new SqlCommand(query, connect))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@ContactInfo", contactInfo);
                    cmd.Parameters.AddWithValue("@Gender", gender);
                    cmd.Parameters.AddWithValue("@CheckInDate", checkInDate);
                    cmd.Parameters.AddWithValue("@CheckOutDate", checkOutDate.HasValue ? (object)checkOutDate.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@BoardingType", boardingType);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateResident(string oldEmail, string newEmail, string name, string contactInfo, DateTime checkInDate, DateTime? checkOutDate, string boardingType, int? roomId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Check if new email exists (if changed)
                if (oldEmail != newEmail)
                {
                    string checkEmailQuery = "SELECT COUNT(*) FROM Residents WHERE Email = @NewEmail AND Email != @OldEmail";
                    using (SqlCommand checkCmd = new SqlCommand(checkEmailQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@NewEmail", newEmail);
                        checkCmd.Parameters.AddWithValue("@OldEmail", oldEmail);
                        if (Convert.ToInt32(checkCmd.ExecuteScalar()) > 0)
                        {
                            throw new Exception("The new email is already in use by another resident.");
                        }
                    }
                }

                // Check room availability if room is specified
                if (roomId.HasValue)
                {
                    string checkRoomQuery = "SELECT COUNT(*) FROM Residents WHERE RoomID = @RoomID AND Email != @OldEmail";
                    using (SqlCommand checkRoomCmd = new SqlCommand(checkRoomQuery, conn))
                    {
                        checkRoomCmd.Parameters.AddWithValue("@RoomID", roomId.Value);
                        checkRoomCmd.Parameters.AddWithValue("@OldEmail", oldEmail);
                        if (Convert.ToInt32(checkRoomCmd.ExecuteScalar()) > 0)
                        {
                            throw new Exception("The selected room is already assigned to another resident.");
                        }
                    }
                }

                // Update resident
                string updateQuery = @"UPDATE Residents 
                                     SET Name = @Name, 
                                         ContactInfo = @ContactInfo, 
                                         CheckInDate = @CheckInDate,
                                         CheckOutDate = @CheckOutDate, 
                                         BoardingType = @BoardingType, 
                                         RoomID = @RoomID,
                                         Email = @NewEmail 
                                     WHERE Email = @OldEmail";

                using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@OldEmail", oldEmail);
                    cmd.Parameters.AddWithValue("@NewEmail", newEmail);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@ContactInfo", contactInfo);
                    cmd.Parameters.AddWithValue("@CheckInDate", checkInDate);
                    cmd.Parameters.AddWithValue("@CheckOutDate", (object)checkOutDate ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@BoardingType", boardingType);
                    cmd.Parameters.AddWithValue("@RoomID", (object)roomId ?? DBNull.Value);

                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        throw new Exception("No resident found with the specified email.");
                    }
                }
            }
        }

        public void DeleteResident(string email)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Delete related records in Income table
                        string deleteIncomeQuery = "DELETE FROM Income WHERE ResidentEmail = @Email";
                        using (SqlCommand cmdIncome = new SqlCommand(deleteIncomeQuery, connection, transaction))
                        {
                            cmdIncome.Parameters.AddWithValue("@Email", email);
                            cmdIncome.ExecuteNonQuery();
                        }

                        // Delete related records in Rooms table
                        string deleteRoomsQuery = "DELETE FROM Rooms WHERE Email = @Email";
                        using (SqlCommand cmdRooms = new SqlCommand(deleteRoomsQuery, connection, transaction))
                        {
                            cmdRooms.Parameters.AddWithValue("@Email", email);
                            cmdRooms.ExecuteNonQuery();
                        }

                        // Delete resident
                        string deleteResidentQuery = "DELETE FROM Residents WHERE Email = @Email";
                        using (SqlCommand cmdResident = new SqlCommand(deleteResidentQuery, connection, transaction))
                        {
                            cmdResident.Parameters.AddWithValue("@Email", email);
                            if (cmdResident.ExecuteNonQuery() == 0)
                            {
                                throw new Exception("No resident found with the specified email.");
                            }
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public DataSet GetResidents(string emailFilter = "")
        {
            string query = string.IsNullOrEmpty(emailFilter)
                ? "SELECT * FROM Residents"
                : $"SELECT * FROM Residents WHERE Email LIKE '{emailFilter}%'";
            return fn.getData(query);
        }
    }

    // Proxy class for resident service
    public class ResidentServiceProxy : IResidentService
    {
        private readonly RealResidentService _realService;
        private readonly HashSet<string> _validBoardingTypes = new HashSet<string> { "Full Board", "Half Board", "Bed and Breakfast" };

        public ResidentServiceProxy()
        {
            _realService = new RealResidentService();
        }

        public void AddResident(string name, string contactInfo, string gender, string email, DateTime checkInDate, DateTime? checkOutDate, string boardingType)
        {
            // Validate inputs
            ValidateResidentData(name, contactInfo, gender, email, boardingType);

            _realService.AddResident(name, contactInfo, gender, email, checkInDate, checkOutDate, boardingType);
        }

        public void UpdateResident(string oldEmail, string newEmail, string name, string contactInfo, DateTime checkInDate, DateTime? checkOutDate, string boardingType, int? roomId)
        {
            // Validate inputs
            if (string.IsNullOrEmpty(oldEmail))
                throw new ArgumentException("Old email cannot be empty.");
            ValidateResidentData(name, contactInfo, null, newEmail, boardingType);

            _realService.UpdateResident(oldEmail, newEmail, name, contactInfo, checkInDate, checkOutDate, boardingType, roomId);
        }

        public void DeleteResident(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("Email cannot be empty.");

            _realService.DeleteResident(email);
        }

        public DataSet GetResidents(string emailFilter = "")
        {
            return _realService.GetResidents(emailFilter);
        }

        private void ValidateResidentData(string name, string contactInfo, string gender, string email, string boardingType)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name cannot be empty.");
            if (string.IsNullOrEmpty(contactInfo))
                throw new ArgumentException("Contact information cannot be empty.");
            if (gender != null && string.IsNullOrEmpty(gender))
                throw new ArgumentException("Gender cannot be empty.");
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("Email cannot be empty.");
            if (!_validBoardingTypes.Contains(boardingType))
                throw new ArgumentException("Invalid boarding type. Valid types are: Full Board, Half Board, Bed and Breakfast");
        }
    }

    
    }
}
*/




using Guna.UI2.WinForms;
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

namespace HotelManagementSystem1.AllUserControls_Resptions
{
    public partial class UC_ManageResidents : UserControl
    {
        private readonly IResidentService _residentService;
        private DPFunctions fn = DPFunctions.Instance;

        public UC_ManageResidents()
        {
            InitializeComponent();
            _residentService = new ResidentServiceProxy();
        }


        public void Setres(DataGridView dg)
        {
            dg.DataSource = _residentService.GetResidents().Tables[0];
            //disable resizing of header row
            dg.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // Add a margin or padding to allow the header to show
            if (dg is Guna.UI2.WinForms.Guna2DataGridView guna2DataGridView)
            {
                guna2DataGridView.Location = new Point(guna2DataGridView.Location.X, guna2DataGridView.Location.Y + 25);
                guna2DataGridView.RowHeadersVisible = false;
                guna2DataGridView.ColumnHeadersHeight = 25;
                guna2DataGridView.Padding = new Padding(0, 20, 0, 0);

                // Visual adjustments for Guna2DataGridView
                guna2DataGridView.BackgroundColor = Color.White;
                guna2DataGridView.GridColor = Color.LightGray;
                guna2DataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSteelBlue;
                guna2DataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                guna2DataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
                guna2DataGridView.EnableHeadersVisualStyles = false; // important to use custom styles
            }
            else
            {
                dg.Location = new Point(dg.Location.X, dg.Location.Y + 25);
                dg.RowHeadersVisible = false;
                dg.ColumnHeadersHeight = 25;
                dg.Padding = new Padding(0, 20, 0, 0);
                // Visual adjustments for regular DataGridView
                dg.BackgroundColor = Color.White;
                dg.GridColor = Color.LightGray;
                dg.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSteelBlue;
                dg.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                dg.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
                dg.EnableHeadersVisualStyles = false; // important to use custom styles

            }
        }

        private void RefreshDataGridView()
        {
            try
            {
                guna2DataGridView1.DataSource = _residentService.GetResidents().Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error refreshing DataGridView: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddW_Click(object sender, EventArgs e)
        {
            try
            {
                _residentService.AddResident(
                    txtNameResident.Text,
                    txtMobileRes.Text,
                    txtGenderRes.Text,
                    txtEmailIdres.Text,
                    txtcheckinRes.Value,
                    txtCheckoutRes.Value,
                    txtBoardingRes.Text
                );

                MessageBox.Show("Resident added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2ButtonUPdateWorker_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row in the table.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string oldEmail = guna2DataGridView1.SelectedRows[0].Cells["Email"].Value.ToString();
                int? roomId = null;
                if (int.TryParse(guna2TextBoxRoomNo.Text, out int parsedRoomId))
                {
                    roomId = parsedRoomId;
                }

                _residentService.UpdateResident(
                    oldEmail,
                    guna2TextBoxEmailId.Text,
                    guna2TextBoxName.Text,
                    guna2TextBoxMobileNo.Text,
                    guna2DateTimePickercheckin.Value,
                    guna2DateTimePickercheckout.Value,
                    guna2ComboBoxboarding.Text,
                    roomId
                );

                MessageBox.Show("Record updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshDataGridView();
                ClearAll();
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
                if (guna2DataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    DataGridViewRow row = guna2DataGridView1.Rows[e.RowIndex];

                    guna2TextBoxEmailId.Text = row.Cells["Email"].Value?.ToString();
                    guna2TextBoxName.Text = row.Cells["Name"].Value?.ToString();
                    guna2TextBoxMobileNo.Text = row.Cells["ContactInfo"].Value?.ToString();
                    guna2DateTimePickercheckin.Value = Convert.ToDateTime(row.Cells["CheckInDate"].Value);
                  

                    if (row.Cells["CheckOutDate"].Value != DBNull.Value && row.Cells["CheckOutDate"].Value != null)
                    {
                        guna2DateTimePickercheckout.Value = Convert.ToDateTime(row.Cells["CheckOutDate"].Value);
                    }
                    else
                    {
                        guna2DateTimePickercheckout.Value = DateTime.Now;
                    }

                    guna2ComboBoxboarding.Text = row.Cells["BoardingType"].Value?.ToString();

                    if (row.Cells["RoomID"].Value != DBNull.Value && row.Cells["RoomID"].Value != null)
                    {
                        guna2TextBoxRoomNo.Text = row.Cells["RoomID"].Value?.ToString();
                    }
                    else
                    {
                        guna2TextBoxRoomNo.Text = "";
                    }
                    guna2TextBoxemailres.Text = row.Cells["Email"].Value.ToString();
                }
            }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                guna2DataGridView1.DataSource = _residentService.GetResidents(guna2TextBoxemailres.Text).Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tabControl1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                Setres(guna2DataGridView1);
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                Setres(guna2DataGridView2res);
            }
        }

        private void guna2ComboBoxboarding_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Event handler for boarding type selection
        }

        private void txtEmailW_TextChanged(object sender, EventArgs e)
        {
            try
            {
                guna2DataGridView2res.DataSource = _residentService.GetResidents(txtEmailres.Text).Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmailres.Text))
            {
                MessageBox.Show("Please enter a valid email address.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Are You Sure?", "Confirmation...", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    _residentService.DeleteResident(txtEmailres.Text);
                    MessageBox.Show("Record Deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tabControl1_SelectedIndexChanged_1(this, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void guna2DataGridView2res_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (guna2DataGridView2res.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    DataGridViewRow row = guna2DataGridView2res.Rows[e.RowIndex];
                    txtEmailres.Text = row.Cells["Email"].Value.ToString();
                }
            }
        }

        private void UC_ManageResidents_Load(object sender, EventArgs e)
        {
            // Initialize any required data or UI elements when the control loads
        }

        public void ClearAll()
        {
            txtNameResident.Clear();
            txtMobileRes.Clear();
            txtBoardingRes.SelectedIndex = -1;
            txtGenderRes.SelectedIndex = -1;
            txtEmailIdres.Clear();
            guna2TextBoxName.Clear();
            guna2TextBoxRoomNo.Clear();
            guna2TextBoxEmailId.Clear();
            guna2TextBoxMobileNo.Clear();
        }

        private void UC_ManageResidents_Leave(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // Event handler for label click
        }

        private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            // Event handler for date time picker value change
        }


        // Interface defining resident management operations
        public interface IResidentService
    {
        void AddResident(string name, string contactInfo, string gender, string email, DateTime checkInDate, DateTime? checkOutDate, string boardingType);
        void UpdateResident(string oldEmail, string newEmail, string name, string contactInfo, DateTime checkInDate, DateTime? checkOutDate, string boardingType, int? roomId);
        void DeleteResident(string email);
        DataSet GetResidents(string emailFilter = "");
    }

    // Real resident service implementation
    public class RealResidentService : IResidentService
    {
        private readonly string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\LoginData.mdf;Integrated Security=True;Connect Timeout=30";
        private readonly DPFunctions fn = DPFunctions.Instance;

        public void AddResident(string name, string contactInfo, string gender, string email, DateTime checkInDate, DateTime? checkOutDate, string boardingType)
        {
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();

                // Check if email already exists
                string checkEmailQuery = "SELECT COUNT(*) FROM Residents WHERE Email = @Email";
                using (SqlCommand checkCmd = new SqlCommand(checkEmailQuery, connect))
                {
                    checkCmd.Parameters.AddWithValue("@Email", email);
                    if ((int)checkCmd.ExecuteScalar() > 0)
                    {
                        throw new Exception("The email address is already in use.");
                    }
                }

                // Insert resident details
                string query = "INSERT INTO Residents (Email, Name, ContactInfo, Gender, CheckInDate, CheckOutDate, BoardingType) " +
                             "VALUES (@Email, @Name, @ContactInfo, @Gender, @CheckInDate, @CheckOutDate, @BoardingType)";
                using (SqlCommand cmd = new SqlCommand(query, connect))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@ContactInfo", contactInfo);
                    cmd.Parameters.AddWithValue("@Gender", gender);
                    cmd.Parameters.AddWithValue("@CheckInDate", checkInDate);
                    cmd.Parameters.AddWithValue("@CheckOutDate", checkOutDate.HasValue ? (object)checkOutDate.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@BoardingType", boardingType);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateResident(string oldEmail, string newEmail, string name, string contactInfo, DateTime checkInDate, DateTime? checkOutDate, string boardingType, int? roomId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Check if new email exists (if changed)
                if (oldEmail != newEmail)
                {
                    string checkEmailQuery = "SELECT COUNT(*) FROM Residents WHERE Email = @NewEmail AND Email != @OldEmail";
                    using (SqlCommand checkCmd = new SqlCommand(checkEmailQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@NewEmail", newEmail);
                        checkCmd.Parameters.AddWithValue("@OldEmail", oldEmail);
                        if (Convert.ToInt32(checkCmd.ExecuteScalar()) > 0)
                        {
                            throw new Exception("The new email is already in use by another resident.");
                        }
                    }
                }

                // Check room availability if room is specified
                if (roomId.HasValue)
                {
                    string checkRoomQuery = "SELECT COUNT(*) FROM Residents WHERE RoomID = @RoomID AND Email != @OldEmail";
                    using (SqlCommand checkRoomCmd = new SqlCommand(checkRoomQuery, conn))
                    {
                        checkRoomCmd.Parameters.AddWithValue("@RoomID", roomId.Value);
                        checkRoomCmd.Parameters.AddWithValue("@OldEmail", oldEmail);
                        if (Convert.ToInt32(checkRoomCmd.ExecuteScalar()) > 0)
                        {
                            throw new Exception("The selected room is already assigned to another resident.");
                        }
                    }
                }

                // Update resident
                string updateQuery = @"UPDATE Residents 
                                     SET Name = @Name, 
                                         ContactInfo = @ContactInfo, 
                                         CheckInDate = @CheckInDate,
                                         CheckOutDate = @CheckOutDate, 
                                         BoardingType = @BoardingType, 
                                         RoomID = @RoomID,
                                         Email = @NewEmail 
                                     WHERE Email = @OldEmail";

                using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@OldEmail", oldEmail);
                    cmd.Parameters.AddWithValue("@NewEmail", newEmail);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@ContactInfo", contactInfo);
                    cmd.Parameters.AddWithValue("@CheckInDate", checkInDate);
                    cmd.Parameters.AddWithValue("@CheckOutDate", (object)checkOutDate ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@BoardingType", boardingType);
                    cmd.Parameters.AddWithValue("@RoomID", (object)roomId ?? DBNull.Value);

                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        throw new Exception("No resident found with the specified email.");
                    }
                }
            }
        }

        public void DeleteResident(string email)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Delete related records in Income table
                        string deleteIncomeQuery = "DELETE FROM Income WHERE ResidentEmail = @Email";
                        using (SqlCommand cmdIncome = new SqlCommand(deleteIncomeQuery, connection, transaction))
                        {
                            cmdIncome.Parameters.AddWithValue("@Email", email);
                            cmdIncome.ExecuteNonQuery();
                        }

                        // Delete related records in Rooms table
                        string deleteRoomsQuery = "DELETE FROM Rooms WHERE Email = @Email";
                        using (SqlCommand cmdRooms = new SqlCommand(deleteRoomsQuery, connection, transaction))
                        {
                            cmdRooms.Parameters.AddWithValue("@Email", email);
                            cmdRooms.ExecuteNonQuery();
                        }

                        // Delete resident
                        string deleteResidentQuery = "DELETE FROM Residents WHERE Email = @Email";
                        using (SqlCommand cmdResident = new SqlCommand(deleteResidentQuery, connection, transaction))
                        {
                            cmdResident.Parameters.AddWithValue("@Email", email);
                            if (cmdResident.ExecuteNonQuery() == 0)
                            {
                                throw new Exception("No resident found with the specified email.");
                            }
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public DataSet GetResidents(string emailFilter = "")
        {
            string query = string.IsNullOrEmpty(emailFilter)
                ? "SELECT * FROM Residents"
                : $"SELECT * FROM Residents WHERE Email LIKE '{emailFilter}%'";
            return fn.getData(query);
        }
    }

    // Proxy class for resident service
    public class ResidentServiceProxy : IResidentService
    {
        private readonly RealResidentService _realService;
        private readonly HashSet<string> _validBoardingTypes = new HashSet<string> { "Full Board", "Half Board", "Bed and Breakfast" };

        public ResidentServiceProxy()
        {
            _realService = new RealResidentService();
        }

        public void AddResident(string name, string contactInfo, string gender, string email, DateTime checkInDate, DateTime? checkOutDate, string boardingType)
        {
            // Validate inputs
            ValidateResidentData(name, contactInfo, gender, email, boardingType);

            _realService.AddResident(name, contactInfo, gender, email, checkInDate, checkOutDate, boardingType);
        }

        public void UpdateResident(string oldEmail, string newEmail, string name, string contactInfo, DateTime checkInDate, DateTime? checkOutDate, string boardingType, int? roomId)
        {
            // Validate inputs
            if (string.IsNullOrEmpty(oldEmail))
                throw new ArgumentException("Old email cannot be empty.");
            ValidateResidentData(name, contactInfo, null, newEmail, boardingType);

            _realService.UpdateResident(oldEmail, newEmail, name, contactInfo, checkInDate, checkOutDate, boardingType, roomId);
        }

        public void DeleteResident(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("Email cannot be empty.");

            _realService.DeleteResident(email);
        }

        public DataSet GetResidents(string emailFilter = "")
        {
            return _realService.GetResidents(emailFilter);
        }

        private void ValidateResidentData(string name, string contactInfo, string gender, string email, string boardingType)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name cannot be empty.");
            if (string.IsNullOrEmpty(contactInfo))
                throw new ArgumentException("Contact information cannot be empty.");
            if (gender != null && string.IsNullOrEmpty(gender))
                throw new ArgumentException("Gender cannot be empty.");
            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("Email cannot be empty.");
            if (!_validBoardingTypes.Contains(boardingType))
                throw new ArgumentException("Invalid boarding type. Valid types are: Full Board, Half Board, Bed and Breakfast");
        }
    }

   
    }
}