using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace AtividadeIA
{
    public static class Database
    {
        // Conforme as especificações de database.md
        private static readonly string connectionString = "Server=127.0.0.1;Port=3307;Database=bd_escola;Uid=root;Pwd=;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
