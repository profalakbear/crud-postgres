using map360.Dto;
using map360.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;
using System.Web;
using System.Web.Mvc;

namespace map360.Data.Implementations
{
    public class UserDbContext : IUserDbContext
    {
        // Fields
        private string connectionString = "Host=localhost;Username=postgres;Password=postgres;Database=test";

      
        // Standart methods
        public List<User> GetAll() { throw new NotImplementedException(); }
        public User GetById(int id) { throw new NotImplementedException(); }
        public void Add(User entity) { throw new NotImplementedException(); }
        public void Update(User entity) { throw new NotImplementedException(); }
        public void Delete(User entity) { throw new NotImplementedException(); }


        // Hybrid methods
        public List<User> GetAllHybrid()
        {
            List<User> result = new List<User>();

            using (NpgsqlConnection connection = new NpgsqlConnection())
            {
                NpgsqlCommand command = new NpgsqlCommand();

                try
                {
                    string query = "Select * from \"Users\" ";
                    var user = new User();

                    connection.ConnectionString = connectionString;
                    connection.Open();

                    command.Connection = connection;
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;
                    NpgsqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        string user_json = (string)dataReader[1];
                        user = JsonSerializer.Deserialize<User>(user_json);
                        user.Id = (long)dataReader[0];
                        result.Add(user);
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
            }
            return result;
        }

        public User GetByIdHybrid(int id)
        {
            User user = new User();

            using (NpgsqlConnection connection = new NpgsqlConnection())
            {
                NpgsqlCommand command = new NpgsqlCommand();

                try
                {
                    string query = "Select \"Details\" from \"Users\" where \"Id\" = @UserId";

                    connection.ConnectionString = connectionString;
                    connection.Open();

                    command.Connection = connection;
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@UserId", id);
                    NpgsqlDataReader dataReader = command.ExecuteReader();
                    var dr = dataReader.Read();
                    string user_json = (string)dataReader[0];
                    user = JsonSerializer.Deserialize<User>(user_json);
                    user.Id = id;
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
            }
            return user;
        }

        public User AddHybrid(User user)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection())
            {
                var affectedRows = 0;

                UserForPostDto userForPost = new UserForPostDto();
                userForPost.NameAndSurname = user.NameAndSurname;
                userForPost.Email = user.Email;
                userForPost.RoleId = user.RoleId;
                userForPost.CompanyId = user.CompanyId;

                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(userForPost, options);

                NpgsqlCommand command = new NpgsqlCommand();
                NpgsqlCommand command2 = new NpgsqlCommand();

                try
                {
                    string query = "Insert into \"Users\" (\"Details\") values( (@Details) :: json)";
                    //INSERT INTO cars(name, price) VALUES('Audi',52642)
                    string select_last = "Select * from \"Users\" order by \"Id\" desc limit 1";

                    connection.ConnectionString = connectionString;
                    connection.Open();

                    command.Connection = connection;
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;
                    //command.Parameters.AddWithValue("@UserId", user.Id);
                    command.Parameters.AddWithValue("@Details", jsonString);
                    affectedRows = command.ExecuteNonQuery();

                    

                    if (affectedRows > 0)
                    {

                        command2.Connection = connection;
                        command2.CommandText = select_last;
                        command2.CommandType = CommandType.Text;
                        NpgsqlDataReader dataReader = command2.ExecuteReader();
                        if(dataReader.Read())
                        {
                            //var res = dataReader[0];
                            user.Id = (long)dataReader[0];
                        }

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
                return affectedRows > 0 ? user : new User();
            }
        }

        public User UpdateHybrid(User user)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection())
            {
                var affectedRows = 0;

                //  user.UserForPostDro
                UserForPostDto userForPost = new UserForPostDto();

                // asagidakilari sil
                userForPost.NameAndSurname = user.NameAndSurname;
                userForPost.Email = user.Email;
                userForPost.RoleId = user.RoleId;
                userForPost.CompanyId = user.CompanyId;

                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(userForPost, options);

                NpgsqlCommand command = new NpgsqlCommand();

                try
                {
                    string query = "Update \"Users\" set \"Details\" = (@Details) :: json where \"Id\" = @UserId";

                    connection.ConnectionString = connectionString;
                    connection.Open();

                    command.Connection = connection;
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@UserId", user.Id);
                    command.Parameters.AddWithValue("@Details", jsonString);
                    affectedRows = command.ExecuteNonQuery();
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
                return affectedRows > 0 ? user : new User() ;
            }
        }

        public bool DeleteHybrid(int id)
        {
            bool result = false;

            using (NpgsqlConnection connection = new NpgsqlConnection())
            {
                NpgsqlCommand command = new NpgsqlCommand();

                try
                {
                    string query = "Delete from \"Users\" where \"Id\" = @UserId";

                    connection.ConnectionString = connectionString;
                    connection.Open();

                    command.Connection = connection;
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@UserId", id);
                    var affectedRows = command.ExecuteNonQuery();

                    if(affectedRows > 0)
                    {
                        // Successfully deleted
                        result = true;
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
                return result;
            }
        }

        //public UserToReturn SelectWithJoinById(int id)
        //{

        //    UserToReturn userToReturn = new UserToReturn();

        //    using (NpgsqlConnection connection = new NpgsqlConnection())
        //    {
        //        NpgsqlCommand command = new NpgsqlCommand();

        //        try
        //        {
        //            string query = "Select \"u.Id\", \"u.Details\", \"c.Id\", \"c.Details\", \"r.Name\" " +
        //                "           from \"Users\" \"u\" " +
        //                "           inner join \"Companies\" \"c\" on \"u.Company_id\" == @CompanyId" +
        //                "           inner join \"UserRoles\" \"r\" on \"u.Role_id\" == @RoleId";


        //            // select t1.name, t2.image_id, t3.path
        //            // from table1 t1
        //            // inner join table2 t2 on t1.person_id = t2.person_id
        //            // inner join table3 t3 on t2.image_id = t3.image_id

        //            connection.ConnectionString = connectionString;
        //            connection.Open();

        //            command.Connection = connection;
        //            command.CommandText = query;
        //            command.CommandType = CommandType.Text;
        //            command.Parameters.AddWithValue("@UserId", id);
        //            command.Parameters.AddWithValue("@UserId", id);
        //            NpgsqlDataReader dataReader = command.ExecuteReader();
        //            var dr = dataReader.Read();
        //            string user_json = (string)dataReader[0];
        //            userToReturn = JsonSerializer.Deserialize<UserToReturn>(user_json);
        //            userToReturn.Id = id;
        //        }

        //        catch (NpgsqlException ex)
        //        {
        //            throw ex;
        //        }

        //        finally
        //        {
        //            command.Dispose();
        //            connection.Close();
        //        }
        //    }
        //    return userToReturn;
        //}
    }
}