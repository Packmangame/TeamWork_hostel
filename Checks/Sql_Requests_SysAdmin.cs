using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checks
{
    public class Sql_Requests_SysAdmin
    {
        public MySqlConnection connection;
        private string connectionString = "server=127.0.0.1;user=root;password=;database=Hostel";

        public Sql_Requests_SysAdmin ()
        {
            connection = new MySqlConnection(connectionString);
        }

        //Заполнение датагрид
        public DataTable GetWorkers ()
        {
            string query = @"
                   SELECT 
                    WL.Login,
                    WL.Password,
                    WL.Role,
                    W.FIO
                FROM 
                    Workers AS W
                INNER JOIN 
                    Workers_logs AS WL 
                ON 
                    W.IDWorker = WL.WorkerID;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    connection.Open( );
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable( );
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }
    }
}
