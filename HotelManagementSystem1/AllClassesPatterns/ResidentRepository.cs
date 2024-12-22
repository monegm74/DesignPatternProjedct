using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem1.Repositories
{
    public class ResidentRepository
    {
        private readonly DPFunctions _dbFunctions = DPFunctions.Instance;

        public bool IsEmailExists(string email)
        {
            string query = "SELECT COUNT(*) FROM Residents WHERE Email = @Email";
            using (SqlConnection conn = _dbFunctions.getConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                conn.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
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
                conn.Close();
            }
        }
    }
}

