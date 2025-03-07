using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
                W.FIO,
                CASE WL.Role
                    WHEN 1 THEN 'Админ'
                    WHEN 2 THEN 'Завхоз'
                    WHEN 3 THEN 'Кладовщик'
                    WHEN 4 THEN 'Сис.Администратор'
                    WHEN 5 THEN 'Уборщик'
                    WHEN 6 THEN 'Строитель'
                    ELSE 'Неизвестно'
                END AS RoleName
            FROM 
                Workers AS W
            INNER JOIN 
                Workers_logs AS WL 
            ON 
                W.IDWorker = WL.WorkerID;"; // Убедитесь в правильности названия таблицы (WorkersLogs или Workers_logs)

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

        public void AddNewWorker (string log, string pass, string fio, int role, int id)
        {
            try
            {
                // Открываем соединение
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open( );

                    // Начинаем транзакцию
                    using (MySqlTransaction transaction = connection.BeginTransaction( ))
                    {
                        try
                        {
                            // 1. Вставка в WorkersLogs
                            string insertLogsQuery = "INSERT INTO Workers_logs (WorkerID,Login, Password, Role) VALUES (@WorkerID, @Login, @Password, @Role)";
                            using (MySqlCommand commandLogs = new MySqlCommand(insertLogsQuery, connection, transaction))
                            {
                                commandLogs.Parameters.AddWithValue("@Login", log);
                                commandLogs.Parameters.AddWithValue("@Password", pass);
                                commandLogs.Parameters.AddWithValue("@Role", role);
                                commandLogs.Parameters.AddWithValue("@WorkerID", id);
                                commandLogs.ExecuteNonQuery( );

                                // Получаем сгенерированный ID
                                int workerId = Convert.ToInt32(commandLogs.LastInsertedId);
                            }

                            // 2. Вставка в Workers
                            string insertWorkersQuery = "INSERT INTO Workers (IDWorker, FIO, Busy, RoomNum) VALUES (@IDWorker, @FIO, 0, NULL)";
                            using (MySqlCommand commandWorkers = new MySqlCommand(insertWorkersQuery, connection, transaction))
                            {
                                // Получаем ID из WorkersLogs (предполагается, что он уже вставлен)
                                // Если нужно явно получить ID, выполните SELECT LAST_INSERT_ID() перед этим шагом
                                int workerId = GetLastWorkerId(connection, transaction);

                                commandWorkers.Parameters.AddWithValue("@IDWorker", id);
                                commandWorkers.Parameters.AddWithValue("@FIO", fio);
                                commandWorkers.ExecuteNonQuery( );
                            }

                            // Подтверждаем транзакцию
                            transaction.Commit( );
                        }
                        catch (Exception ex)
                        {
                            // Откатываем транзакцию при ошибке
                            transaction.Rollback( );
                            throw new Exception("Ошибка при добавлении работника: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Общая ошибка: " + ex.Message);

                throw;
            }
        }

        // Вспомогательный метод для получения последнего WorkerID
        private int GetLastWorkerId (MySqlConnection connection, MySqlTransaction transaction)
        {
            string query = "SELECT LAST_INSERT_ID()";
            using (MySqlCommand command = new MySqlCommand(query, connection, transaction))
            {
                return Convert.ToInt32(command.ExecuteScalar( ));
            }
        }


        public void DeleteWorkerFromDatabase (string login)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open( );
                string query = @"
                DELETE FROM Workers_logs WHERE Login = @Login;
                DELETE FROM Workers WHERE IDWorker = (
                    SELECT WorkerID FROM Workers_logs WHERE Login = @Login
                );";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Login", login);
                    command.ExecuteNonQuery( );
                }
            }
        }

        public void UpdateWorker (string oldLogin, string newLogin, string newPassword, string newFIO, string newRoleName)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open( );

                // Используем CASE для преобразования названия роли в индекс
                string query = @"
                UPDATE Workers 
                SET FIO = @FIO 
                WHERE IDWorker = (
                    SELECT WorkerID 
                    FROM Workers_logs 
                    WHERE Login = @OldLogin
                );

                UPDATE Workers_logs 
                SET 
                    Login = @Login, 
                    Password = @Password, 
                    Role = (
                        CASE @RoleName
                            WHEN 'Админ' THEN 1
                            WHEN 'Завхоз' THEN 2
                            WHEN 'Кладовщик' THEN 3
                            WHEN 'Сис.Администратор' THEN 4
                            WHEN 'Уборщик' THEN 5
                            WHEN 'Строитель' THEN 6
                            ELSE @RoleName -- Если роль не найдена, можно вернуть ошибку
                        END
                    ) 
                WHERE Login = @OldLogin;";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OldLogin", oldLogin);
                    command.Parameters.AddWithValue("@Login", newLogin);
                    command.Parameters.AddWithValue("@Password", newPassword);
                    command.Parameters.AddWithValue("@RoleName", newRoleName); // Передаем название роли
                    command.Parameters.AddWithValue("@FIO", newFIO);

                    int rowsAffected = command.ExecuteNonQuery( );

                    if (rowsAffected == 0)
                        throw new Exception("Работник не найден");
                }
            }
        }
    }
}
