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
        private const string connectionString = "Data Source=DESKTOP-8DRN3BN;Initial Catalog=StatusCakeDemo;Integrated Security=True";

        [HttpGet]
        public List<Cake> Get()
        {
            const string query = "SELECT * FROM StatusCakeDemo";
            var result = new List<Cake>();

            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                using SqlCommand command = new SqlCommand(query, connection);
                command.CommandType = System.Data.CommandType.Text;

                using SqlDataReader reader = command.ExecuteReader();
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
            return result;
        }

        [HttpPut]
        public void Put(string cakeType, int cost, string description, int weight, int storeAvalibility)
        {
            const string query = "INSERT INTO StatusCakeDemo ([CakeType],[Cost],[Description],[Weight],[StoreAvalibility]) " +
                            "VALUES (@CakeType,@Cost,@Description,@Weight,@StoreAvalibility);";

            using SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;
            connection.Open();

            using SqlCommand command = new SqlCommand(query, connection);
            command.CommandType = System.Data.CommandType.Text;
            command.Parameters.Add(new SqlParameter("@CakeType", cakeType));
            command.Parameters.Add(new SqlParameter("@Cost", cost));
            command.Parameters.Add(new SqlParameter("@Description", description));
            command.Parameters.Add(new SqlParameter("@Weight", weight));
            command.Parameters.Add(new SqlParameter("@StoreAvalibility", storeAvalibility));
            command.ExecuteNonQuery();
        }

        [HttpDelete]
        public void Delete(string cakeType)
        {
            const string query = "DELETE FROM StatusCakeDemo WHERE [CakeType] = @CakeType";

            using SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;
            connection.Open();

            using SqlCommand command = new SqlCommand(query, connection);
            command.CommandType = System.Data.CommandType.Text;
            command.Parameters.Add(new SqlParameter("@CakeType", cakeType));
            command.ExecuteNonQuery();
        }
    }
}
