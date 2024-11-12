using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using VartotojuValdymoSistema.Core.Models;

namespace VartotojuValdymoSistema.Core.Repositories
{
    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<User> ListUsers()
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    return connection.Query<User>("SELECT * FROM Users").ToList();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Klaida gaunant vartotojus: {ex.Message}");
                return new List<User>();
            }
        }

        public void AddUser(User user)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    connection.Execute("INSERT INTO Users (Username, Password, IsActive, Role) VALUES (@Username, @Password, @IsActive, @Role)", user);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Klaida pridedant vartotoją: {ex.Message}");
            }
        }

        public User GetUserById(int id)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    return connection.Query<User>("SELECT * FROM Users WHERE Id = @Id", new { Id = id }).FirstOrDefault();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Klaida gaunant vartotoją pagal ID: {ex.Message}");
                return null;
            }
        }

        public void UpdateUser(User user)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    connection.Execute("UPDATE Users SET Username = @Username, Password = @Password, IsActive = @IsActive, Role = @Role WHERE Id = @Id", user);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Klaida atnaujinant vartotoją: {ex.Message}");
            }
        }

        public void DeleteUser(int id)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    connection.Execute("DELETE FROM Users WHERE Id = @Id", new { Id = id });
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Klaida ištrinant vartotoją: {ex.Message}");
            }
        }

        public void ChangePassword(int id, string newPassword)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    connection.Execute("UPDATE Users SET Password = @Password WHERE Id = @Id", new { Password = newPassword, Id = id });
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Klaida keičiant slaptažodį: {ex.Message}");
            }
        }

        public void SetUserStatus(int id, bool isActive)
        {
            try
            {
                using (IDbConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    connection.Execute("UPDATE Users SET IsActive = @IsActive WHERE Id = @Id", new { IsActive = isActive, Id = id });
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Klaida nustatant vartotojo statusą: {ex.Message}");
            }
        }
    }
}