using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;
using WebAPIDemo.Models;

namespace WebAPIDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CakeController : ControllerBase
    {
        private string connectionString = "Data Source=YOUR-SERVER-NAME-HERE;Initial Catalog=StatusCakeDemo;Integrated Security=True";

        [HttpGet]
        public List<Cake> Get()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;
            connection.Open();

            var query = "SELECT * FROM StatusCakeDemo";
            var result = new List<Cake>();

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.CommandType = System.Data.CommandType.Text;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string CakeType = reader[0].ToString();
                        int Cost = (int)reader[1];
                        string Description = reader[2].ToString();
                        int Weight = (int)reader[3];
                        int StoreAvalibility = (int)reader[4];

                        result.Add(new Cake(CakeType, Cost, Description, Weight, StoreAvalibility));
                    }
                }
            }
            return result;
        }

        [HttpPut]
        public void Put(string cakeType, int cost, string description, int weight, int storeAvalibility)
        {
            SqlConnection connection = new SqlConnection();

            connection.ConnectionString = connectionString;
            connection.Open();

            string query = $"INSERT INTO StatusCakeDemo (CakeType,Cost,Description,Weight,StoreAvalibility) " +
                                   $"VALUES ('{cakeType}',{cost},'{description}',{weight},{storeAvalibility});";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.CommandType = System.Data.CommandType.Text;
                command.ExecuteNonQuery();
            }
        }

        [HttpDelete]
        public void Delete(string cakeType)
        {
            SqlConnection connection = new SqlConnection();

            connection.ConnectionString = connectionString;
            connection.Open();

            string query = $"DELETE FROM StatusCakeDemo WHERE CakeType = '{cakeType}'";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.CommandType = System.Data.CommandType.Text;
                command.ExecuteNonQuery();
            }
        }
    }
}
