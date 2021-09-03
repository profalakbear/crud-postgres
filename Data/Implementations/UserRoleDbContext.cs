using map360.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;
using System.Web;

namespace map360.Data.Implementations
{
    public class UserRoleDbContext : IUserRoleDbContext
    {
        private string connectionString = "Host=localhost;Username=postgres;Password=postgres;Database=test";

        public void Add(UserRole entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(UserRole entity)
        {
            throw new NotImplementedException();
        }

        public List<UserRole> GetAll()
        {
            List<UserRole> role_list = new List<UserRole>();

            using (NpgsqlConnection connection = new NpgsqlConnection())
            {
                NpgsqlCommand command = new NpgsqlCommand();

                try
                {
                    string query = "Select * from \"UserRoles\" ";
                    //var usRole = new UserRole();

                    connection.ConnectionString = connectionString;
                    connection.Open();

                    command.Connection = connection;
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;
                    NpgsqlDataReader dataReader = command.ExecuteReader();


                    while (dataReader.Read())
                    {
                        var usRole = new UserRole();
                        //string role_list_json = (string)dataReader[1];
                        //role_list = JsonSerializer.Deserialize<UserRole>(role_list_json);
                        usRole.Id = (long)dataReader[0];
                        usRole.Name = (string)dataReader[1];
                        role_list.Add(usRole);
                        usRole = null;
                    }
                }

                catch (NpgsqlException ex)
                {
                    throw ex;
                }

                finally
                {
                    command.Dispose();
                    connection.Close();
                }
                return role_list;
            }
        }

        public UserRole GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(UserRole entity)
        {
            throw new NotImplementedException();
        }
    }
}