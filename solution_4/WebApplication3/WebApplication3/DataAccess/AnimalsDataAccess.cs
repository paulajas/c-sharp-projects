using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using WebApplication3.Models;

namespace WebApplication3.DataAccess
{
    public class AnimalsDataAccess : IAnimalsDataAccess
    {
        private readonly IConfiguration _configuration;
        public AnimalsDataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private bool IsExecuted(int rows)
        {
            if (rows >= 1)
                return true;
            else return false;
        }

        public IEnumerable<Animal> GetAnimal(string OrderBy)
        {
            List<Animal> list = new List<Animal>();
            using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultDbConnection"));
            var command = new SqlCommand();
            command.Connection = connection;
            if (OrderBy == null)
            {
                command.CommandText = "SELECT * FROM Animal ORDER BY Name ASC";
                return null;
            }
            Type type = typeof(Animal);
            var names = type.GetProperties()
                            .Select(p => p.Name);
            foreach (var item in names)
            {
                if (OrderBy == item)
                {
                    command.CommandText = $"SELECT * FROM Animal ORDER BY @val ASC";
                    command.Parameters.AddWithValue("@val", OrderBy);
                }
                else
                {
                    continue;
                }
            }
            connection.Open();
            SqlDataReader rd = command.ExecuteReader();
            while (rd.Read())
                list.Add(new Animal
                {
                    IdAnimal = int.Parse(rd["IdAnimal"].ToString()),
                    Name = rd["Name"].ToString(),
                    Description = rd["Description"].ToString(),
                    Category = rd["Category"].ToString(),
                    Area = rd["Area"].ToString()
                });
            connection.Close();
            
            return list;
        }
        public void DeleteAnimal(int idAnimal)
        {
            using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultDbConnection"));
            var command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "DELETE FROM Animal WHERE idAnimal = @idAnimal";
            command.Parameters.AddWithValue("idAnimal", idAnimal);
            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
/*            if (!IsExecuted(rowsAffected)) throw new NoExecutedQueryException();*/
            connection.Close();
        }

        public void CreateAnimal(Animal animal)
        {
            using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultDbConnection"));
            var command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = $"INSERT INTO Animal(Name, Description, Category, Area) VALUES(@name, @description, @category, @area)";
            command.Parameters.AddWithValue("name", animal.Name);
            command.Parameters.AddWithValue("description", animal.Description);
            command.Parameters.AddWithValue("category", animal.Category);
            command.Parameters.AddWithValue("area", animal.Area);
            connection.Open();
            int rowsInserted = command.ExecuteNonQuery();
/*                    if (!IsExecuted(rowsInserted)) throw new NoExecutedQueryException();*/
            connection.Close();
        }

        public void ChangeAnimal(int idAnimal, Animal animal)
        {
            using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultDbConnection"));
            var command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE Animal SET Name = @name, Description = @description, Category = @category, Area = @area WHERE idAnimal = @idAnimal";
            command.Parameters.AddWithValue("name", animal.Name);
            command.Parameters.AddWithValue("description", animal.Description);
            command.Parameters.AddWithValue("category", animal.Category);
            command.Parameters.AddWithValue("area", animal.Area);
            command.Parameters.AddWithValue("idAnimal", idAnimal);
            connection.Open();
            int rowsChanged = command.ExecuteNonQuery();
/*                    if (!IsExecuted(rowsChanged)) throw new NoExecutedQueryException();*/
            connection.Close();

        }
    }
}
