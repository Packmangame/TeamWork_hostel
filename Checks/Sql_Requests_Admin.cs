using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Checks
{
    public class RoomCard
    {
        public string RoomNum { get; set; }
        public int Beds { get; set; }
        public string Extras { get; set; }
        public string ImagePath { get; set; }
        public string Cond { get; set; }
        public bool IsOcuped { get; set; }

        public RoomCard(string roomNum, int beds, string extras, string imagePath, string cond, bool isOcuped)
        {
            RoomNum = roomNum;
            Beds = beds;
            Extras = extras;
            ImagePath = imagePath;
            Cond = cond;
            IsOcuped = isOcuped;
        }
    }


    public class Sql_Request
    {
        // Приватное соединение с базой данных
        private MySqlConnection connection;

        // Строка подключения
        private string connectionString = "server=127.0.0.1;user=root;password=;database=Hostel";

        // Конструктор для инициализации соединения
        public Sql_Request()
        {
            connection = new MySqlConnection(connectionString);
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        // Метод для закрытия соединения
        public void Disconnect_DB()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        // Метод для выполнения SQL-запросов (например, INSERT, UPDATE, DELETE)
        public int ExecuteNonQuery(string query)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    return command.ExecuteNonQuery(); 
                }
            }
            catch (Exception ex)
            {
                return -1; 
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        // Метод для выполнения SELECT-запросов и получения данных
        public DataTable ExecuteQuery(string tablename)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                string query = $"SELECT * FROM {tablename}";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable); 
                        return dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                return null; // Возвращаем null при ошибке
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        //Вывод детей пользователя
        public DataTable LoadChildren(int id)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                string query = $"SELECT Childrens.IDChild,Childrens.FIO,Childrens.Birthday,Users.ФИО FROM Childrens INNER JOIN Users ON Childrens.IDPer = Users.IDU WHERE Childrens.IDPer = {id};";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении деталей: {ex.Message}");
                return null;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        //Вывод комнаты проживания
        public DataTable GetCurrentResidence(int idUser)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                string query = @"
            SELECT 
                Rooms.RoomNum, 
                Rooms.Beds, 
                Rooms.Extras, 
                Reservation.DateOfEntry, 
                Reservation.DepartureDate
            FROM 
                Reservation
            INNER JOIN 
                Rooms ON Reservation.RoomNumber = Rooms.IDR
            WHERE 
                Reservation.IDUser = @IDUser
                AND CURDATE() BETWEEN Reservation.DateOfEntry AND Reservation.DepartureDate;";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IDUser", idUser);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении текущего проживания: {ex.Message}");
                return null;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        //Вывод резервации пользователя
        public DataTable GetFutureReservations(int idUser)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                string query = @"
            SELECT 
                Rooms.RoomNum, 
                Rooms.Beds, 
                Rooms.Extras, 
                Reservation.DateOfEntry, 
                Reservation.DepartureDate
            FROM 
                Reservation
            INNER JOIN 
                Rooms ON Reservation.RoomNumber = Rooms.IDR
            WHERE 
                Reservation.IDUser = @IDUser
                AND Reservation.DateOfEntry > CURDATE();";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IDUser", idUser);

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении будущих резерваций: {ex.Message}");
                return null;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        // Метод для создания панелей с карточками комнат
        public List<Panel> GetRoomPanels(int panelWidth, int panelHeight, int? bedsMin = null, int? bedsMax = null, List<int> conditionIds = null, List<bool?> statuses = null)
        {
            List<Panel> roomPanels = new List<Panel>();
            string query = "SELECT RoomNum, Beds, Extras, Image, Conditions, Status FROM Rooms ";

            // Добавляем условия для фильтрации
            if (bedsMin.HasValue || bedsMax.HasValue)
            {
                query += $" AND Beds BETWEEN {bedsMin ?? 0} AND {bedsMax ?? int.MaxValue}";
            }

            if (conditionIds != null && conditionIds.Count > 0)
            {
                query += $" AND Conditions IN ({string.Join(",", conditionIds)})";
            }

            if (statuses != null && statuses.Count > 0)
            {
                // Преобразуем bool в int (0 или 1)
                List<int> statusValues = statuses.Select(status => status == true ? 1 : 0).ToList();
                query += $" AND Status IN ({string.Join(",", statusValues)})";
            }
            int count = 0;
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable(); // Создаем таблицу для хранения данных
                        adapter.Fill(dataTable); // Заполняем таблицу данными из базы
                        foreach (DataRow row in dataTable.Rows) // Итерируемся по строкам DataTable
                        {
                            string roomNum = row["RoomNum"].ToString();
                            int beds = Convert.ToInt32(row["Beds"]);
                            string extras = row["Extras"].ToString();
                            string imagePath = row["Image"].ToString();
                            string conditionsValue = row["Conditions"]?.ToString() ?? "3";
                            string cond = GetConditionText(conditionsValue);
                            bool isOccupied = row["Status"] != null && Convert.ToBoolean(row["Status"]);

                            Panel cardPanel = CreateRoomCardPanel(roomNum, beds, extras, imagePath, panelWidth, panelHeight, cond, isOccupied);
                            roomPanels.Add(cardPanel);
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при создании панелей: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return roomPanels;
        }

        private string GetConditionText(string conditionIndex)
        {
            if (string.IsNullOrEmpty(conditionIndex)) return "Не указано"; 

            switch (conditionIndex)
            {
                case "1":
                    return "Убрать";
                case "2":
                    return "Ремонт";
                case "3":
                    return "Готово";
                case "4":
                    return "Ремонт+Уборка";
                default:
                    return "Неизвестно";
            }
        }

        // Метод для создания одной панели с карточкой комнаты
        private Panel CreateRoomCardPanel(string roomNum, int beds, string extras, string imagePath, int panelWidth, int panelHeight, string cond, bool isOccupied)
        {
            Panel cardPanel = new Panel
            {
                Size = new Size(panelWidth - 20, 140), // Высота панели 140
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            // Добавляем PictureBox для изображения
            PictureBox roomImage = new PictureBox
            {
                Size = new Size((int)(cardPanel.Width * 0.2), (int)(cardPanel.Height * 0.8)),
                Location = new Point(10, (cardPanel.Height - (int)(cardPanel.Height * 0.8)) / 2),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.LightGray // Для отладки
            };
            if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
            {
                // roomImage.Image = System.Drawing.Image.FromFile(imagePath); // Закомментировано для отладки
            }
            cardPanel.Controls.Add(roomImage);

            // Добавляем Label для номера комнаты
            Label roomNumberLabel = new Label
            {
                Text = $"Номер: {roomNum}",
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(roomImage.Right + 10, 10),
                AutoSize = true,
                BackColor = Color.LightBlue // Для отладки
            };
            cardPanel.Controls.Add(roomNumberLabel);

            // Добавляем Label для количества кроватей
            Label bedsLabel = new Label
            {
                Text = $"Кровати: {beds}",
                Font = new Font("Arial", 10),
                Location = new Point(roomImage.Right + 10, roomNumberLabel.Bottom + 5),
                AutoSize = true,
                BackColor = Color.LightGreen // Для отладки
            };
            cardPanel.Controls.Add(bedsLabel);

            // Добавляем Label для дополнительных услуг
            Label extrasLabel = new Label
            {
                Text = $"Услуги: {extras}",
                Font = new Font("Arial", 10),
                Location = new Point(roomImage.Right + 10, bedsLabel.Bottom + 5),
                AutoSize = true,
                MaximumSize = new Size(cardPanel.Width - roomImage.Width - 20, 0),
                BackColor = Color.LightYellow // Для отладки
            };
            cardPanel.Controls.Add(extrasLabel);

            // Добавляем Label для состояния комнаты
            Label condLabel = new Label
            {
                Text = $"Состояние комнаты: {cond}",
                Font = new Font("Arial", 10),
                Location = new Point(roomImage.Right + 10, extrasLabel.Bottom + 5),
                AutoSize = true,
                MaximumSize = new Size(cardPanel.Width - roomImage.Width - 20, 0),
                BackColor = Color.LightPink // Для отладки
            };
            cardPanel.Controls.Add(condLabel);

            // Добавляем CheckBox для статуса проживания
            CheckBox statusCheckBox = new CheckBox
            {
                Checked = isOccupied,
                Location = new Point(roomImage.Right + 10, condLabel.Bottom + 5),
                AutoSize = true,
                Text = isOccupied ? "Заселено" : "Не живут",
                BackColor = Color.LightCyan // Для отладки
            };

            // Обработчик события изменения состояния CheckBox
            statusCheckBox.CheckedChanged += (sender, e) =>
            {
                bool isChecked = statusCheckBox.Checked;
                UpdateRoomStatus(roomNum, isChecked);
                UpdateCheckBoxText(statusCheckBox, isChecked);
            };

            cardPanel.Controls.Add(statusCheckBox);
            return cardPanel;
        }

        //Смена текста
        public void UpdateCheckBoxText(CheckBox checkBox, bool isOccupied)
        {
            checkBox.Text = isOccupied ? "Заселено" : "Не живут";
        }


        //Обновление проживания в комнате
        public void UpdateRoomStatus(string roomNum, bool isOccupied)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                string updateQuery = "UPDATE Rooms SET Status = @Status WHERE RoomNum = @RoomNum";
                using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Status", isOccupied ? 1 : 0); 
                    command.Parameters.AddWithValue("@RoomNum", roomNum);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении статуса комнаты: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        public void UpdateLivingAndRoomStatus(int idUser, bool livesInValue)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                // Шаг 1: Обновление поля Lives_in в таблице Users
                string updateUserQuery = "UPDATE Users SET Lives_in = @Lives_in WHERE IDU = @IDU";
                using (MySqlCommand command = new MySqlCommand(updateUserQuery, connection))
                {
                    command.Parameters.AddWithValue("@Lives_in", livesInValue ? 1 : 0);
                    command.Parameters.AddWithValue("@IDU", idUser);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        MessageBox.Show($"Пользователь с ID {idUser} не найден.");
                        return;
                    }
                }

                // Шаг 2: Получение номера комнаты из таблицы Reservation
                string getRoomIdQuery = @"
                    SELECT RoomNumber 
                    FROM Reservation 
                     WHERE IDUser = @IDUser AND CURDATE() BETWEEN DateOfEntry AND DepartureDate";

                using (MySqlCommand command = new MySqlCommand(getRoomIdQuery, connection))
                {
                    command.Parameters.AddWithValue("@IDUser", idUser);

                    object roomIdResult = command.ExecuteScalar();

                    if (roomIdResult == null)
                    {
                        MessageBox.Show($"Для пользователя с ID {idUser} не найдена комната в текущий момент.");
                        return;
                    }

                    int roomId = Convert.ToInt32(roomIdResult);

                    // Шаг 3: Обновление статуса комнаты в таблице Rooms
                    string updateRoomQuery = "UPDATE Rooms SET Status = @Status WHERE IDR = @IDR";
                    using (MySqlCommand updateCommand = new MySqlCommand(updateRoomQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@Status", livesInValue ? 1 : 0);
                        updateCommand.Parameters.AddWithValue("@IDR", roomId);

                        int roomsUpdated = updateCommand.ExecuteNonQuery();
                        if (roomsUpdated > 0)
                        {
                            MessageBox.Show($"Статус комнаты с IDR={roomId} успешно обновлен.");
                        }
                        else
                        {
                            MessageBox.Show($"Не удалось обновить статус комнаты с IDR={roomId}.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении данных: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        //Смена значения резервации
        public void UpdateUserReservation(int idUser, bool reservationValue)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                string query = "UPDATE Users SET Reservation = @Reservation WHERE IDU = @IDU";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Reservation", reservationValue ? 1 : 0);
                    command.Parameters.AddWithValue("@IDU", idUser);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show($"Запись с IDU={idUser} успешно обновлена.");
                    }
                    else
                    {
                        MessageBox.Show($"Не удалось обновить запись с IDU={idUser}.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении записи в Users: {ex.Message}");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

    }
}
