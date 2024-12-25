using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem1.Repositories
{
    public class ResidentRepository
    {
        private readonly DBHelpers _dbFunctions = DBHelpers.Instance;

        public bool IsEmailExists(string email)
        {
            string query = "SELECT COUNT(*) FROM Residents WHERE Email = @Email";
            using (SqlConnection conn = _dbFunctions.getConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                conn.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        public void AssignRoomToResident(string email, string roomId)
        {
            string query = "UPDATE Residents SET RoomID = @RoomID WHERE Email = @Email";
            using (SqlConnection conn = _dbFunctions.getConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@RoomID", roomId);
                cmd.Parameters.AddWithValue("@Email", email);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<string> GetResidentsEmailsByRoomId(string roomId)
        {
            string query = "SELECT Email FROM Residents WHERE RoomID = @RoomID";
            var emails = new List<string>();

            using (SqlConnection conn = _dbFunctions.getConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@RoomID", roomId);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        emails.Add(reader["Email"].ToString());
                    }
                }
            }

            return emails;
        }

        public void UpdateCheckoutDate(string email, DateTime checkoutDate)
        {
            string query = "UPDATE Residents SET CheckOutDate = @CheckOutDate WHERE Email = @Email";
            using (SqlConnection conn = _dbFunctions.getConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CheckOutDate", checkoutDate);
                cmd.Parameters.AddWithValue("@Email", email);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void RemoveResidentFromRoom(string email)
        {
            string query = "UPDATE Residents SET RoomID = NULL WHERE Email = @Email";
            using (SqlConnection conn = _dbFunctions.getConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable GetAllResidents()
        {
            string query = "SELECT * FROM Residents";
            DataTable dataTable = new DataTable();

            using (SqlConnection conn = _dbFunctions.getConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataTable);
            }

            return dataTable;
        }

        public DataTable GetResidents(string query)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection conn = _dbFunctions.getConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataTable);
            }

            return dataTable;
        }
    }
}


