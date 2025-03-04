using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Checks
{
    public class Sql_Requests_Enter
    {
        public MySqlConnection connection;
        private string connectionString = "server=127.0.0.1;user=root;password=;database=Hostel";


        public Sql_Requests_Enter ()
        {
            connection = new MySqlConnection(connectionString);
        }

        public string Enter (string login, string password)
        {
            // Инициализация соединения внутри метода
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = @"
                SELECT 
                    Role 
                FROM Workers_logs
                WHERE Login = @Login AND Password = @Password";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Login", login);
                    command.Parameters.AddWithValue("@Password", password);

                    // Открываем соединение
                    connection.Open( );

                    object result = command.ExecuteScalar( );

                    if (result != null)
                    {
                        return result.ToString( );
                    }
                    else
                    {
                        return "Пользователь не найден";
                    }
                }
            }
        }
    }
}
