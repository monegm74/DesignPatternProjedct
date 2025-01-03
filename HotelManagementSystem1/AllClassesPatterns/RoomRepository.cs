﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem1.Repositories
{
    public class RoomRepository
    {
        private readonly DBHelpers _dbFunctions = DBHelpers.Instance;

        public DataTable GetAllRooms()
        {
            string query = "SELECT * FROM Rooms";
            DataSet ds = _dbFunctions.getData(query);
            return ds.Tables[0];
        }

        public bool IsRoomAvailable(string roomId)
        {
            string query = "SELECT Status FROM Rooms WHERE RoomID = @RoomID";
            using (SqlConnection conn = _dbFunctions.getConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@RoomID", roomId);
                conn.Open();
                object statusObj = cmd.ExecuteScalar();
                return statusObj != null && statusObj.ToString() == "Available";
            }
        }

        public int AssignRoom(string roomId, string type, string vipPackage, string email)
        {
            string query = "UPDATE Rooms SET Status = 'Occupied', RoomType = @RoomType, VIPPackage = @VIPPackage, Email = @Email " +
                           "WHERE RoomID = @RoomID AND Status = 'Available'";
            using (SqlConnection conn = _dbFunctions.getConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@RoomID", roomId);
                cmd.Parameters.AddWithValue("@RoomType", type);
                cmd.Parameters.AddWithValue("@VIPPackage", vipPackage.ToUpper());
                cmd.Parameters.AddWithValue("@Email", email);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public decimal GetRoomPrice(string roomId)
        {
            string query = "SELECT PricePerNight FROM Rooms WHERE RoomID = @RoomID";
            using (SqlConnection conn = _dbFunctions.getConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@RoomID", roomId);
                conn.Open();
                object result = cmd.ExecuteScalar();
                return result != null && result != DBNull.Value ? Convert.ToDecimal(result) : 0;
            }
        }

        public void MarkRoomAsAvailable(string roomId)
        {
            string query = "UPDATE Rooms SET Status = 'Available', Email = NULL , VIPPackage = NULL WHERE RoomID = @RoomID";
            using (SqlConnection conn = _dbFunctions.getConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@RoomID", roomId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public void EmpytRoomID(string Email)
        {
            string query = "UPDATE Residents SET RoomID = NULL WHERE Email = @Email";
            using (SqlConnection conn = _dbFunctions.getConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", Email);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }



        public void MarkRoomAsUnderMaintenance(string roomId)
        {
            string query = "UPDATE Rooms SET Status = 'Under Maintenance' WHERE RoomID = @RoomID";
            using (SqlConnection conn = _dbFunctions.getConnection())
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@RoomID", roomId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}


