using MySql.Data.MySqlClient;
using System.Data;

namespace LSP_AqillaShahbaniM
{
    public static class DatabaseHelper
    {
        private static readonly string ConnectionString = "server=127.0.0.1;uid=root;pwd=root;database=subsift8_lsp3";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public static void ExecuteNonQuery(string query)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public static DataTable ExecuteQuery(string query)
        {
            DataTable dataTable = new DataTable();

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                using (var adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }
            }

            return dataTable;
        }
    }
}