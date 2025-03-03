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

namespace Checks
{
    public class RoomCard
    {
        public string RoomNum { get; set; } 
        public int Beds { get; set; }       
        public string Extras { get; set; }  
        public string ImagePath { get; set; } 

        public RoomCard(string roomNum, int beds, string extras, string imagePath)
        {
            RoomNum = roomNum;
            Beds = beds;
            Extras = extras;
            ImagePath = imagePath;
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



        // Метод для создания панелей с карточками комнат
        public List<Panel> GetRoomPanels(int panelWidth, int panelHeight)
        {
            List<Panel> roomPanels = new List<Panel>();

            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                string query = "SELECT RoomNum, Beds, Extras, Image, Conditions, Status FROM Rooms"; 
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string roomNum = reader["RoomNum"].ToString();
                            int beds = Convert.ToInt32(reader["Beds"]);
                            string extras = reader["Extras"].ToString();
                            string imagePath = reader["Image"].ToString();
                            string conditions = reader["Conditions"]?.ToString() ?? "3"; 
                            string cond = GetConditionText(conditions);
                            bool isOccupied = reader["Status"] != null && Convert.ToBoolean(reader["Status"]);
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
        private Panel CreateRoomCardPanel(string roomNum, int beds, string extras, string imagePath, int panelWidth, int panelHeight,string cond,bool isOccupied)
        {
            Panel cardPanel = new Panel
            {
                Size = new Size(panelWidth - 20, (int)(panelHeight * 0.2)), 
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            // Добавляем PictureBox для изображения
            PictureBox roomImage = new PictureBox
            {
                Size = new Size((int)(cardPanel.Width * 0.2), (int)(cardPanel.Height * 0.8)),
                Location = new Point(10, (cardPanel.Height - (int)(cardPanel.Height * 0.8)) / 2),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
            {
                roomImage.Image = System.Drawing.Image.FromFile(imagePath);
            }
            else
            {
            }
            cardPanel.Controls.Add(roomImage);

            // Добавляем Label для номера комнаты
            Label roomNumberLabel = new Label
            {
                Text = $"Номер: {roomNum}",
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(roomImage.Right + 10, 10),
                AutoSize = true
            };
            cardPanel.Controls.Add(roomNumberLabel);

            // Добавляем Label для количества кроватей
            Label bedsLabel = new Label
            {
                Text = $"Кровати: {beds}",
                Font = new Font("Arial", 10),
                Location = new Point(roomImage.Right + 10, roomNumberLabel.Bottom + 5),
                AutoSize = true
            };
            cardPanel.Controls.Add(bedsLabel);

            // Добавляем Label для дополнительных услуг
            Label extrasLabel = new Label
            {
                Text = $"Услуги: {extras}",
                Font = new Font("Arial", 10),
                Location = new Point(roomImage.Right + 10, bedsLabel.Bottom + 5),
                AutoSize = true,
                MaximumSize = new Size(cardPanel.Width - roomImage.Width - 20, 0) 
            };
            cardPanel.Controls.Add(extrasLabel);

            //Добавляем Label для состояния комнаты
            Label condLabel = new Label
            {
                Text = $"Состояние комнаты: {cond}",
                Font = new Font("Arial", 10),
                Location = new Point(roomImage.Right + 10, extrasLabel.Bottom + 5),
                AutoSize = true,
                MaximumSize = new Size(cardPanel.Width - roomImage.Width - 20, 0)
            };
            cardPanel.Controls.Add(condLabel);

            CheckBox statusCheckBox = new CheckBox
            {
                Text = "Заселено",
                Checked = isOccupied,
                Location = new Point(roomImage.Right + 10, condLabel.Bottom + 5),
                AutoSize = true
            };
            statusCheckBox.CheckedChanged += (sender, e) => UpdateRoomStatus(roomNum, statusCheckBox.Checked); 
            cardPanel.Controls.Add(statusCheckBox);


            return cardPanel;
        }

        //Обновление проживания в комнате
        private void UpdateRoomStatus(string roomNum, bool isOccupied)
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

    }
}
