using DAL_B3.DomainClass;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_B3.Repositories
{
    public class GroupStoredRepository: IGroupStoredRepository
    {
        private string _connectionString;

        public GroupStoredRepository()
        {
            _connectionString = "Data Source=.;Initial Catalog=baiTap3;User Id=sa;Password=Hung2001;";
        }

        public void Add(Group group)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("CreateGroup", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@GroupCode", group.GroupCode);
                command.Parameters.AddWithValue("@Description", group.Description);
                command.Parameters.AddWithValue("@Status", group.Status);
                command.Parameters.AddWithValue("@CreatedPerson", group.CreatedPerson);
                command.Parameters.AddWithValue("@FromDate", group.FromDate);
                command.Parameters.AddWithValue("@ToDate", group.ToDate);
                command.ExecuteNonQuery();
            }
        }

        public void Delete(Guid id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("DeleteGroup", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@GroupId", id);
                command.ExecuteNonQuery();
            }
        }

        public List<Group> GetAll()
        {
            var group = new List<Group>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("GetGroup", connection);
                command.CommandType = CommandType.StoredProcedure;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var result = new Group()
                            {
                                GroupId = reader.GetGuid(0),
                                GroupCode = reader.GetString(1),
                                Description = reader.GetString(2),
                                Status = (bool)reader["Status"],
                                CreatedPerson = reader["CreatedPerson"].ToString(),
                                FromDate = (DateTime)reader["FromDate"],
                                ToDate = ((DateTime)reader["ToDate"]),
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

        public Group GetById(Guid id)
        {
            Group group = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("GetDataById", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        group = new Group()
                        {
                            GroupId = reader.GetGuid(0),
                            GroupCode = reader.GetString(1),
                            Description = reader.GetString(2),
                            Status = (bool)reader["Status"],
                            CreatedPerson = reader["CreatedPerson"].ToString(),
                            FromDate = (DateTime)reader["FromDate"],
                            ToDate = ((DateTime)reader["ToDate"]),
                        };
                    }
                }
            }
            return group;
        }

        public void Update(Guid id,string groupCode, string description,bool status,DateTime fromDate,DateTime toDate)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("UpdateGroup", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@GroupId", id);
                command.Parameters.AddWithValue("@GroupCode", groupCode);
                command.Parameters.AddWithValue("@Description", description);
                command.Parameters.AddWithValue("@Status", status);
                command.Parameters.AddWithValue("@FromDate", fromDate);
                command.Parameters.AddWithValue("@ToDate", toDate);
                command.ExecuteNonQuery();
            }
        }
    }
}
