using DAL_B3.DomainClass;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_B3.Repositories
{
    public class UserStoredRepository: IUserStoredRepository
    {
        private string _connectionString;

        public UserStoredRepository()
        {
            _connectionString = "Data Source=.;Initial Catalog=baiTap3;User Id=sa;Password=Hung2001;";
        }

        public void Add(User group)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("CreateUsers", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserName", group.UserName);
                command.Parameters.AddWithValue("@Password ", group.Password);
                command.Parameters.AddWithValue("@RoleId", group.RoleId);
                command.Parameters.AddWithValue("@Status", group.Status);
                command.Parameters.AddWithValue("@CreatedPerson", group.CreatedPerson);
                command.Parameters.AddWithValue("@Description", group.Description);
                command.Parameters.AddWithValue("@Phone", group.Phone);
                command.ExecuteNonQuery();
            }
        }

        public void Delete(Guid id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("DeleteUsers", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserId", id);
                command.ExecuteNonQuery();
            }
        }

        public List<User> GetAll()
        {
            var group = new List<User>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("GetUser", connection);
                command.CommandType = CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var result = new User()
                            {
                                UserId = reader.GetGuid(0),
                                UserName = reader.GetString(1),
                                Description = reader.GetString(2),
                                Phone = (string)reader["Phone"],
                                CreatedPerson = reader["CreatedPerson"].ToString(),
                                Status = (bool)reader["status"],
                            };
                            group.Add(result);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Not found!");
                    }
                }
            }
            return group;
        }

        public User GetById(Guid id)
        {
            User user = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("GetDataUserById", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserId", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new User()
                        {
                            UserId = reader.GetGuid(0),
                            UserName = reader.GetString(1),
                            Password = reader.GetString(2),
                            RoleId = (Guid)reader["RoleId"],
                            Status = (bool)reader["Status"],
                            CreatedPerson = (string)reader["CreatedPerson"],
                            Description = ((string)reader["Description"]),
                            Phone = ((string)reader["Phone"]),

                        };
                    }
                }
            }
            return user;
        }

        public void Update(Guid? id, string userName, string password, Guid? roleID, bool status, string createPerson,string description,string phone)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("UpdateUsers", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserId", id);
                command.Parameters.AddWithValue("@UserName", userName);
                command.Parameters.AddWithValue("@Password", password);
                command.Parameters.AddWithValue("@RoleId", roleID);
                command.Parameters.AddWithValue("@Status", status);
                command.Parameters.AddWithValue("@CreatedPerson", createPerson);
                command.Parameters.AddWithValue("@Description", description);
                command.Parameters.AddWithValue("@Phone", phone);
                command.ExecuteNonQuery();
            }
        }
    }
}
