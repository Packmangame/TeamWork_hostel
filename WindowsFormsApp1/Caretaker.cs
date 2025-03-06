using Checks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Checks;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public partial class Caretaker :Form
    {
        Sql_Requests_Caretaker sqlRequest = new Sql_Requests_Caretaker( );

        public Caretaker ()
        {
            InitializeComponent( );
            sqlRequest.ConditionSelected += OnConditionSelected;
        }

        private void OnConditionSelected (object sender, Sql_Requests_Caretaker.ConditionSelectedEventArgs e)
        {
            int selectedConditionIndex = e.SelectedCondition;

            // Скрыть все панели
            panelWashers.Visible = false;
            panelBuilders.Visible = false;

            switch (selectedConditionIndex)
            {
                case 0: // Уборщики
                    panelWashers.Visible = true;
                    DisplayWorkersInPanel(panelWashers, sqlRequest.GetWorkersByRole(5), "Уборщики"); // Роль 5 - Уборщик
                    break;

                case 1: // Строители
                    panelBuilders.Visible = true;
                    DisplayWorkersInPanel(panelBuilders, sqlRequest.GetWorkersByRole(6), "Строители"); // Роль 6 - Строитель
                    break;

                case 2: // Нет выбора
                    break;

                case 3: // Обе роли
                    panelWashers.Visible = true;
                    panelBuilders.Visible = true;
                    DisplayWorkersInPanel(panelWashers, sqlRequest.GetWorkersByRole(5), "Уборщики"); // Роль 5 - Уборщик
                    DisplayWorkersInPanel(panelBuilders, sqlRequest.GetWorkersByRole(6), "Строители"); // Роль 6 - Строитель
                    break;
            }
        }

        private void DisplayWorkersInPanel (Panel panel, DataTable workersData, string roleText)
        {
            // Проверяем, существует ли уже DataGridView в панели
            DataGridView dataGridView = panel.Controls.OfType<DataGridView>( ).FirstOrDefault( );

            if (dataGridView == null)
            {
                // Создаем новый DataGridView
                dataGridView = new DataGridView
                {
                    Dock = DockStyle.None,
                    Location = new Point(0, 30), // Оставляем место для текста роли
                    Size = new Size(panel.Width, panel.Height - 50), // Устанавливаем размеры
                    DataSource = workersData,
                    BackgroundColor= Color.FromArgb(0xA3, 0xD6, 0xB8)
                };
                panel.Controls.Add(dataGridView);

                // Добавляем Label для отображения текста роли
                Label roleLabel = new Label
                {
                    Text = roleText,
                    Font = new Font("Arial", 10),
                    Location = new Point(10, 5),
                    AutoSize = true
                };
                panel.Controls.Add(roleLabel);
            }
            else
            {
                // Обновляем существующий DataGridView
                dataGridView.DataSource = workersData;
            }

            // Настройка заголовков столбцов
            dataGridView.Columns [ "FIO" ].HeaderText = "ФИО";
            dataGridView.Columns [ "Role" ].HeaderText = "Роль";

            // Обработка события нажатия на запись
            dataGridView.CellClick += DataGridView_CellClick;
        }

        // Обработчик события нажатия на запись в DataGridView
        private void DataGridView_CellClick (object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = ((DataGridView) sender).Rows [ e.RowIndex ];
                string fio = row.Cells [ "FIO" ].Value.ToString( );
                string role = row.Cells [ "Role" ].Value.ToString( ); // Убедитесь, что имя столбца правильное

                // Вызов метода для назначения выбранного человека на работу
                AssignWorkerToTask(fio, role);
            }
        }

        // Метод для назначения выбранного человека на работу
        private void AssignWorkerToTask (string fio, string role)
        {
            MessageBox.Show($"Назначение работника {fio} с ролью {role} на работу.");
            sqlRequest.UpdateWorkerStatus(fio);
            if (role == "Уборщик")
            {
                DisplayWorkersInPanel(panelWashers, sqlRequest.GetWorkersByRole(5), "Уборщики"); // Роль 5 - Уборщик
            }
            else
            { 
                DisplayWorkersInPanel(panelBuilders, sqlRequest.GetWorkersByRole(6), "Строители"); // Роль 6 - Строитель
            }
        }

        private void Caretaker_Load (object sender, EventArgs e)
        {
            List<Panel> roomPanels = sqlRequest.GetRoomPanelsForCaretaker(CardsPanel.Width, CardsPanel.Height);

            if (roomPanels != null && roomPanels.Any( ))
            {
                int verticalSpacing = 10;
                int currentY = 10;

                foreach (var panel in roomPanels)
                {
                    panel.Location = new Point(10, currentY);
                    CardsPanel.Controls.Add(panel);
                    currentY += panel.Height + verticalSpacing;
                }
            }
            else
            {
                MessageBox.Show("Нет данных о комнатах.");
            }
        }
    }
}
