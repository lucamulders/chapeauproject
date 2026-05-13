using ChapeauProject.Models;
using Microsoft.Data.SqlClient;

namespace ChapeauProject.Repositories
{
    public class TableRepository : ITableRepository
    {
        private readonly string? _connectionString;

        public TableRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ChapeauProject");
        }

        public List<Table> GetAll()
        {
            var tables = new List<Table>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT TableNumber, Seats, IsOccupied FROM Tables";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tables.Add(ReadTable(reader));
                        }
                    }
                }
            }
            return tables;
        }

        public Table? GetByTableNumber(int tableNumber)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT TableNumber, Seats, IsOccupied FROM Tables WHERE TableNumber = @TableNumber";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TableNumber", tableNumber);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            return ReadTable(reader);
                    }
                }
            }
            return null;
        }

        private Table ReadTable(SqlDataReader reader)
        {
            int tableNumber = reader.GetInt32(reader.GetOrdinal("TableNumber"));
            int seats = reader.GetInt32(reader.GetOrdinal("Seats"));
            bool isOccupied = reader.GetBoolean(reader.GetOrdinal("IsOccupied"));
            return new Table(tableNumber, seats, isOccupied);
        }
        public void ToggleOccupied(int tableNumber)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Tables SET IsOccupied = ~IsOccupied WHERE TableNumber = @TableNumber";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TableNumber", tableNumber);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}