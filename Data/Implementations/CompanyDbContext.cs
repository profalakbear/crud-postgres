using map360.Dto;
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
    public class CompanyDbContext : ICompanyDbContext
    {
        // Fields
        private string connectionString = "Host=localhost;Username=postgres;Password=postgres;Database=test";


        // Standart methods
        public List<Company> GetAll() { throw new NotImplementedException(); }
        public Company GetById(int id) { throw new NotImplementedException(); }
        public void Add(Company entity) { throw new NotImplementedException(); }
        public void Update(Company entity) { throw new NotImplementedException(); }
        public void Delete(Company entity) { throw new NotImplementedException(); }


        // Hybrid methods
        public List<Company> GetAllHybrid()
        {
            List<Company> result = new List<Company>();

            using (NpgsqlConnection connection = new NpgsqlConnection())
            {
                NpgsqlCommand command = new NpgsqlCommand();

                try
                {
                    string query = "Select * from \"Companies\" ";
                    var company = new Company();

                    connection.ConnectionString = connectionString;
                    connection.Open();

                    command.Connection = connection;
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;
                    NpgsqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        string company_json = (string)dataReader[1];
                        company = JsonSerializer.Deserialize<Company>(company_json);
                        company.Id = (long)dataReader[0];
                        result.Add(company);
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

        public Company GetByIdHybrid(int id)
        {
            Company company = new Company();

            using (NpgsqlConnection connection = new NpgsqlConnection())
            {
                NpgsqlCommand command = new NpgsqlCommand();

                try
                {
                    string query = "Select \"Details\" from \"Companies\" where \"Id\" = @CompanyId";

                    connection.ConnectionString = connectionString;
                    connection.Open();

                    command.Connection = connection;
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@CompanyId", id);
                    NpgsqlDataReader dataReader = command.ExecuteReader();
                    var dr = dataReader.Read();
                    string user_json = (string)dataReader[0];
                    company = JsonSerializer.Deserialize<Company>(user_json);
                    company.Id = id;
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
            return company;
        }

        public Company AddHybrid(Company company)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection())
            {
                var affectedRows = 0;

                CompanyForPostDto companyForPost = new CompanyForPostDto();
                companyForPost.Name = company.Name;
                companyForPost.Phone = company.Phone;
                companyForPost.TaxNo = company.TaxNo;
                companyForPost.Address = company.Address;

                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(companyForPost, options);

                NpgsqlCommand command = new NpgsqlCommand();
                NpgsqlCommand command2 = new NpgsqlCommand();

                try
                {
                    string query = "Insert into \"Companies\" (\"Details\") values( (@Details) :: json)";
                    //INSERT INTO cars(name, price) VALUES('Audi',52642)
                    string select_last = "Select * from \"Companies\" order by \"Id\" desc limit 1";

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
                        if (dataReader.Read())
                        {
                            //var res = dataReader[0];
                            company.Id = (long)dataReader[0];
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
                return affectedRows > 0 ? company : new Company();
            }
        }

        public Company UpdateHybrid(Company company)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection())
            {
                var affectedRows = 0;

                CompanyForPostDto companyForPost = new CompanyForPostDto();
                companyForPost.Name = company.Name;
                companyForPost.Phone = company.Phone;
                companyForPost.TaxNo = company.TaxNo;
                companyForPost.Address = company.Address;

                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(companyForPost, options);

                NpgsqlCommand command = new NpgsqlCommand();

                try
                {
                    string query = "Update \"Companies\" set \"Details\" = (@Details) :: json where \"Id\" = @CompanyId";

                    connection.ConnectionString = connectionString;
                    connection.Open();

                    command.Connection = connection;
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@CompanyId", company.Id);
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
                return affectedRows > 0 ? company : new Company();
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
                    string query = "Delete from \"Companies\" where \"Id\" = @CompanyId";

                    connection.ConnectionString = connectionString;
                    connection.Open();

                    command.Connection = connection;
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@CompanyId", id);
                    var affectedRows = command.ExecuteNonQuery();

                    if (affectedRows > 0)
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
    }
}