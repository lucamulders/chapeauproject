using ChapeauProject.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace ChapeauProject.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        private readonly string _connectionString;

        public MenuRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ChapeauProject");
        }

        public List<MenuItem> GetFiltered(string cardFilter, string courseFilter)
        {
            List<MenuItem> items = new List<MenuItem>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                // We use LEFT JOIN to ensure items show up even if Stock or Course data is missing
                string query = @"SELECT MI.MenuItemID, MI.ItemName, MI.Price, 
                                       ISNULL(S.Quantity, 0) AS Quantity, 
                                       ISNULL(C.CourseName, 'N/A') AS CourseName, 
                                       MI.MenuCard 
                                FROM MenuItems MI
                                LEFT JOIN Stock S ON MI.MenuItemID = S.MenuItemID
                                LEFT JOIN Courses C ON MI.CourseID = C.CourseID
                                WHERE MI.MenuCard = @MenuCard";

                if (courseFilter != "All")
                {
                    query += " AND C.CourseName = @CourseName";
                }

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MenuCard", cardFilter);
                    if (courseFilter != "All")
                    {
                        command.Parameters.AddWithValue("@CourseName", courseFilter);
                    }

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new MenuItem(
                                (int)reader["MenuItemID"],
                                (string)reader["ItemName"],
                                (decimal)reader["Price"],
                                (int)reader["Quantity"],
                                (string)reader["CourseName"],
                                (string)reader["MenuCard"]
                            ));
                        }
                    }
                }
            }
            return items;
        }
    }
}