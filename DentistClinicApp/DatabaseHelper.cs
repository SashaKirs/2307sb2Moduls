using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentistClinicApp
{
    public static class DatabaseHelper
    {
        private static string connectionString = @"Data Source=DBSrv\yar2024 Catalog=KR2DB;Integrated Security=True";

       
        public static List<User> GetUsers()
        {
            var users = new List<User>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Users", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(new User
                    {
                        Id = (int)reader["Id"],
                        Login = (string)reader["Login"],
                        Password = (string)reader["Password"],
                        RegistrationDate = (DateTime)reader["RegistrationDate"],
                        FullName = (string)reader["FullName"],
                        Phone = (string)reader["Phone"]
                    });
                }
            }
            return users;
        }


        public static List<Request> GetRequests()
        {
            var requests = new List<Request>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Requests", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    requests.Add(new Request
                    {
                        Id = (int)reader["Id"],
                        Articul = (string)reader["Articul"],
                        Description = (string)reader["Description"],
                        Type = (string)reader["Type"],
                        FullDescription = (string)reader["FullDescription"],
                        RegistrationDate = (DateTime)reader["RegistrationDate"],
                        Status = (string)reader["Status"],
                        UserId = (int)reader["UserId"]
                    });
                }
            }
            return requests;
        }

  
        public static void AddRequest(Request request)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"INSERT INTO Requests
                (Articul, Description, Type, FullDescription, RegistrationDate, Status, UserId)
                VALUES (@Articul, @Description, @Type, @FullDescription, @RegistrationDate, @Status, @UserId)", conn);
                cmd.Parameters.AddWithValue("@Articul", request.Articul);
                cmd.Parameters.AddWithValue("@Description", request.Description);
                cmd.Parameters.AddWithValue("@Type", request.Type);
                cmd.Parameters.AddWithValue("@FullDescription", request.FullDescription);
                cmd.Parameters.AddWithValue("@RegistrationDate", request.RegistrationDate);
                cmd.Parameters.AddWithValue("@Status", request.Status);
                cmd.Parameters.AddWithValue("@UserId", request.UserId);
                cmd.ExecuteNonQuery();
            }
        }

        public static void UpdateRequest(Request request)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"UPDATE Requests SET
                Articul=@Articul, Description=@Description, Type=@Type, FullDescription=@FullDescription,
                RegistrationDate=@RegistrationDate, Status=@Status, UserId=@UserId WHERE Id=@Id", conn);
                cmd.Parameters.AddWithValue("@Articul", request.Articul);
                cmd.Parameters.AddWithValue("@Description", request.Description);
                cmd.Parameters.AddWithValue("@Type", request.Type);
                cmd.Parameters.AddWithValue("@FullDescription", request.FullDescription);
                cmd.Parameters.AddWithValue("@RegistrationDate", request.RegistrationDate);
                cmd.Parameters.AddWithValue("@Status", request.Status);
                cmd.Parameters.AddWithValue("@UserId", request.UserId);
                cmd.Parameters.AddWithValue("@Id", request.Id);
                cmd.ExecuteNonQuery();
            }
        }


        public static void DeleteRequest(int requestId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Requests WHERE Id=@Id", conn);
                cmd.Parameters.AddWithValue("@Id", requestId);
                cmd.ExecuteNonQuery();
            }
        }

        public static bool RegisterUser(string login, string password, string fullName, string phone)
        {
         н
            var existingUsers = GetUsers();
            if (existingUsers.Exists(u => u.Login == login))
                return false; 

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"INSERT INTO Users (Login, Password, RegistrationDate, FullName, Phone)
                                              VALUES (@Login, @Password, @RegistrationDate, @FullName, @Phone)", conn);
                cmd.Parameters.AddWithValue("@Login", login);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@RegistrationDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@FullName", fullName);
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.ExecuteNonQuery();
                return true;
            }
        }

        public static User AuthenticateUser(string login, string password)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE Login=@Login AND Password=@Password", conn);
                cmd.Parameters.AddWithValue("@Login", login);
                cmd.Parameters.AddWithValue("@Password", password);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new User
                    {
                        Id = (int)reader["Id"],
                        Login = (string)reader["Login"],
                        Password = (string)reader["Password"],
                        RegistrationDate = (DateTime)reader["RegistrationDate"],
                        FullName = (string)reader["FullName"],
                        Phone = (string)reader["Phone"]
                    };
                }
                else
                {
                    return null; 
                }
            }
        }