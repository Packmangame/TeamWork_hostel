using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Checks
{
    public class Sql_Requests_Caretaker
    {
        public MySqlConnection connection;
        private string connectionString = "server=127.0.0.1;user=root;password=;database=Hostel";

        public Sql_Requests_Caretaker ()
        {
            connection = new MySqlConnection(connectionString);
        }

        public delegate void ConditionSelectedEventHandler (object sender, ConditionSelectedEventArgs e);

        public event ConditionSelectedEventHandler ConditionSelected;

        public class ConditionSelectedEventArgs :EventArgs
        {
            public int SelectedCondition { get; }

            public ConditionSelectedEventArgs (int selectedCondition)
            {
                SelectedCondition = selectedCondition;
            }
        }


        // Метод для создания панелей с карточками комнат для завхоза
        public List<Panel> GetRoomPanelsForCaretaker (int panelWidth, int panelHeight)
        {
            List<Panel> roomPanels = new List<Panel>( );

            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open( );
                }

                string query = "SELECT RoomNum, Beds, Extras, Image, Conditions, Status FROM Rooms WHERE Conditions IN (1, 2, 4);";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader( ))
                    {
                        while (reader.Read( ))
                        {
                            string roomNum = reader [ "RoomNum" ].ToString( );
                            int beds = Convert.ToInt32(reader [ "Beds" ]);
                            string extras = reader [ "Extras" ].ToString( );
                            string imagePath = reader [ "Image" ].ToString( );
                            string conditions = reader [ "Conditions" ]?.ToString( ) ?? "3";
                            string cond = GetConditionText(conditions);
                            bool isOccupied = reader [ "Status" ] != null && Convert.ToBoolean(reader [ "Status" ]);
                            Panel cardPanel = CreateRoomCardPanelForCaretaker(roomNum, beds, extras, imagePath, panelWidth, panelHeight, cond, isOccupied);
                            roomPanels.Add(cardPanel);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при создании панелей: " + ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close( );
                }
            }

            return roomPanels;
        }

        private Panel CreateRoomCardPanelForCaretaker (string roomNum, int beds, string extras, string imagePath, int panelWidth, int panelHeight, string cond, bool isOccupied)
        {
            Panel cardPanel = new Panel
            {
                Size = new Size(panelWidth - 20, (int) (panelHeight * 0.2)),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            // Добавляем PictureBox для изображения
            PictureBox roomImage = new PictureBox
            {
                Size = new Size((int) (cardPanel.Width * 0.2), (int) (cardPanel.Height * 0.8)),
                Location = new Point(10, (cardPanel.Height - (int) (cardPanel.Height * 0.8)) / 2),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
            {
                roomImage.Image = System.Drawing.Image.FromFile(imagePath);
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

            // Создание Label "Состояние комнаты:"
            Label condLabel = new Label
            {
                Text = "Состояние комнаты:",
                Font = new Font("Arial", 10),
                Location = new Point(roomImage.Right + 10, roomNumberLabel.Bottom + 5)
            };
            cardPanel.Controls.Add(condLabel);

            // Создание ComboBox
            ComboBox condComb = new ComboBox
            {
                Font = new Font("Arial", 10),
                Location = new Point(condLabel.Right + 5, condLabel.Top),
                AutoSize = true,
                MaximumSize = new Size(cardPanel.Width - roomImage.Width - condLabel.Width - 25, 0)
            };
            cardPanel.Controls.Add(condComb);

            // Добавление значений в ComboBox
            condComb.Items.Add("Убрать");
            condComb.Items.Add("Ремонт");
            condComb.Items.Add("Готово");
            condComb.Items.Add("Ремонт+Уборка");

            // Установка выбранного значения (если необходимо)
            if (!string.IsNullOrEmpty(cond))
            {
                if (condComb.Items.Contains(cond))
                {
                    condComb.SelectedItem = cond;
                }
            }

            // Создание Label для статуса заселения
            Label statusCheckBox = new Label
            {
                Text = "",
                Location = new Point(roomImage.Right + 10, condComb.Bottom + 5),
                AutoSize = true
            };

            if (isOccupied == true)
            {
                statusCheckBox.Text = "Заселено";
            }
            else
            {
                statusCheckBox.Text = "Не заселено";
            }
            cardPanel.Controls.Add(statusCheckBox);

            // Создание кнопки
            Button workerBut = new Button
            {
                Text = "Назначить",
                Font = new Font("Arial", 8),
                Location = new Point(cardPanel.Width - 80, cardPanel.Height - 40),
                Size = new Size(70, 35)
            };
            cardPanel.Controls.Add(workerBut);

            // Обработчик нажатия на кнопку
            workerBut.Click += (sender, e) =>
            {
                if (condComb.SelectedIndex != -1)
                {
                    // Вызов события с выбранным индексом
                    ConditionSelected?.Invoke(this, new ConditionSelectedEventArgs(condComb.SelectedIndex));
                }
            };

            return cardPanel;
        }

        //Выбор по роли
        public DataTable GetWorkersByRole (int roleIndex)
        {
            string query = @"
            SELECT 
                W.FIO, 
                CASE WL.Role
                    WHEN 5 THEN 'Уборщик'
                    WHEN 6 THEN 'Строитель'
                    ELSE 'Неизвестная роль'
                END AS Role
            FROM Workers AS W
            JOIN Workers_logs AS WL ON W.IDWorker = WL.WorkerID
            WHERE WL.Role = @Role AND W.Busy!=TRUE";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Role", roleIndex); // Индексы начинаются с 0, а роли с 1
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

        public void UpdateWorkerStatus (string fio)
        {
            string query = "UPDATE Workers SET Busy = 1 WHERE FIO = @FIO";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FIO", fio);
                    connection.Open( );
                    int rowsAffected = command.ExecuteNonQuery( );

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Статус занятости успешно обновлен.");
                    }
                    else
                    {
                        Console.WriteLine("Не удалось обновить статус занятости. Работник не найден.");
                    }
                }
            }
        }

        private string GetConditionText (string conditionIndex)
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
    }
}