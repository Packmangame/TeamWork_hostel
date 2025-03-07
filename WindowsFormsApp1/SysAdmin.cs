using Checks;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class SysAdmin :Form
    {
        private DataGridViewRow selectedRow; // Для хранения выбранной строки
        public MySqlConnection connection;
        private string connectionString = "server=127.0.0.1;user=root;password=;database=Hostel";
        Sql_Requests_SysAdmin req = new Sql_Requests_SysAdmin( );
        public SysAdmin ()
        {
            InitializeComponent( );
            contextMenuStrip1.ItemClicked += ContextMenuStrip1_ItemClicked;
        }

        private void ContextMenuStrip1_ItemClicked (object sender, ToolStripItemClickedEventArgs e)
        {
            if (selectedRow == null) return;

            if (e.ClickedItem.Text == "Изменить запись")
            {
                ShowEditForm( );
            }
            else if (e.ClickedItem.Text == "Удалить запись")
            {
                ShowDeleteConfirmation( );
            }
        }

        private void editToolStripMenuItem_Click (object sender, EventArgs e)
        {
            if (selectedRow == null) return;

            var editForm = new Form2( );

            // Установка значений полей через свойства
            editForm.Login = selectedRow.Cells [ "Login" ].Value?.ToString( );
            editForm.Password = selectedRow.Cells [ "Password" ].Value?.ToString( );
            editForm.FIO = selectedRow.Cells [ "FIO" ].Value?.ToString( );
            editForm.Role = selectedRow.Cells [ "RoleName" ].Value?.ToString( );

            if (editForm.ShowDialog( ) == DialogResult.OK)
            {
                // Обновление данных в таблице
                UpdateWorkerInDatabase(editForm.Login, editForm.Password, editForm.FIO, editForm.Role);

                // Обновление DataGridView
                RefreshDataGridView( );
            }
        }

        private void deleteToolStripMenuItem_Click (object sender, EventArgs e)
        {
            if (selectedRow == null) return;

            if (MessageBox.Show("Вы уверены, что хотите удалить запись?", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string loginToDelete = selectedRow.Cells [ "Login" ].Value?.ToString( );
                req.DeleteWorkerFromDatabase(loginToDelete);
                RefreshDataGridView( );
            }
        }

        // Метод для получения выбранной строки
        private void dataGridView1_CellMouseDown (object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dataGridView1.ClearSelection( );
                dataGridView1.Rows [ e.RowIndex ].Selected = true;
                selectedRow = dataGridView1.Rows [ e.RowIndex ];
                contextMenuStrip1.Show(dataGridView1, dataGridView1.PointToClient(Cursor.Position));
            }
        }

        private void SysAdmin_Load (object sender, EventArgs e)
        {
            DataTable workersData = req.GetWorkers( );
            if (workersData != null && workersData.Rows.Count > 0)
            {
                dataGridView1.DataSource = workersData;

                // Настройка заголовков столбцов
                dataGridView1.Columns [ "RoleName" ].HeaderText = "Роль";
                dataGridView1.Columns [ "Login" ].HeaderText = "Логин";
                dataGridView1.Columns [ "Password" ].HeaderText = "Пароль";
                dataGridView1.Columns [ "FIO" ].HeaderText = "ФИО";

                // Добавление обработчика события для контекстного меню
                dataGridView1.CellMouseDown += dataGridView1_CellMouseDown;
            }
            else
            {
                MessageBox.Show("Нет данных о работниках.");
            }
        }

        private void DisplayWorkersInPanel (Panel panel, DataTable workersData)
        {
            // Проверяем, существует ли уже DataGridView в панели
            DataGridView dataGridView = panel.Controls.OfType<DataGridView>( ).FirstOrDefault( );

            if (dataGridView == null)
            {
                // Создаем новый DataGridView
                dataGridView = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    DataSource = workersData
                };

                // Настройка заголовков (опционально)
                dataGridView.Columns [ "Login" ].HeaderText = "Логин";
                dataGridView.Columns [ "Password" ].HeaderText = "Пароль";
                dataGridView.Columns [ "RoleName" ].HeaderText = "Роль";
                dataGridView.Columns [ "FIO" ].HeaderText = "ФИО";

                panel.Controls.Add(dataGridView);
            }
            else
            {
                // Обновляем существующий DataGridView
                dataGridView.DataSource = workersData;
            }
        }

        private void dataGridView1_CellContentClick (object sender, DataGridViewCellEventArgs e)
        {

        }

        public int GetFilledRowsCountLINQ (DataGridView dataGridView)
        {
            return dataGridView.Rows.Cast<DataGridViewRow>( )
                .Count(row => row.Cells.Cast<DataGridViewCell>( )
                    .Any(cell => cell.Value != null && !string.IsNullOrWhiteSpace(cell.Value.ToString( ))));
        }

        private void ShowEditForm ()
        {
            if (selectedRow == null) return;

            var editForm = new Form2( );

            // Установка значений полей
            editForm.Login = selectedRow.Cells [ "Login" ].Value?.ToString( );
            editForm.Password = selectedRow.Cells [ "Password" ].Value?.ToString( );
            editForm.FIO = selectedRow.Cells [ "FIO" ].Value?.ToString( );

            // Получение роли как числовое значение
            string role = selectedRow.Cells [ "RoleName" ].Value?.ToString( ); // Используйте столбец с числом, например, "Role"
            editForm.Role = role;

            if (editForm.ShowDialog( ) == DialogResult.OK)
            {
                // Обновление данных в базе
                req.UpdateWorker(
                    oldLogin: selectedRow.Cells [ "Login" ].Value?.ToString( ),
                    newLogin: editForm.Login,
                    newPassword: editForm.Password,
                    newFIO: editForm.FIO,
                    newRoleName: editForm.Role
                );

                RefreshDataGridView( );
            }
        }

        private void RefreshDataGridView ()
        {
            DataTable workersData = req.GetWorkers( );
            if (workersData != null && workersData.Rows.Count > 0)
            {
                dataGridView1.DataSource = null; // Очистка текущих данных
                dataGridView1.DataSource = workersData;
            }
        }

        private void ShowDeleteConfirmation ()
        {
            if (selectedRow == null) return;

            if (MessageBox.Show("Вы уверены, что хотите удалить запись?", "Подтверждение", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                string loginToDelete = selectedRow.Cells [ "Login" ].Value?.ToString( );
                req.DeleteWorkerFromDatabase(loginToDelete);
                RefreshDataGridView( );
            }
        }

        private void UpdateWorkerInDatabase (string login, string password, string fio, string roleName)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open( );
                string query = @"
                UPDATE Workers 
                SET FIO = @FIO 
                WHERE IDWorker = (
                    SELECT WorkerID 
                    FROM WorkersLogs 
                    WHERE Login = @OldLogin
                );

                UPDATE WorkersLogs 
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
                            ELSE @RoleName -- Если роль не найдена, можно вернуть ошибку или оставить как есть
                        END
                    ) 
                WHERE Login = @OldLogin;";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OldLogin", selectedRow.Cells [ "Login" ].Value);
                    command.Parameters.AddWithValue("@Login", login);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@RoleName", roleName); // Теперь передаем название роли
                    command.Parameters.AddWithValue("@FIO", fio);

                    command.ExecuteNonQuery( );
                }
            }
        }

        private void AddNew_Click (object sender, EventArgs e)
        {
            if (LogAdd.Text != "" && PassAdd.Text != "" && FIOAdd.Text != "" && ComboAdd.SelectedIndex != 0)
            {
                int count = GetFilledRowsCountLINQ(dataGridView1);
                count++;
                string login = LogAdd.Text;
                string password = PassAdd.Text;
                string fio = FIOAdd.Text;
                int role = ComboAdd.SelectedIndex;
                req.AddNewWorker(login, password, fio, role, count);
            }
            else
            {
                MessageBox.Show("Потом будет подсвечивать неверное");
            }
        }
    }
}
