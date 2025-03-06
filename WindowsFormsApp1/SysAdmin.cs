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

namespace WindowsFormsApp1
{
    public partial class SysAdmin : Form
    {
        private Timer timer;
        private int targetX;
        Sql_Requests_SysAdmin req = new Sql_Requests_SysAdmin( );
        public SysAdmin()
        {
            InitializeComponent();
        }

        private void SysAdmin_Load (object sender, EventArgs e)
        {
            DataTable workersData = req.GetWorkers( );
            if (workersData != null && workersData.Rows.Count > 0)
            {
                dataGridView1.DataSource = workersData;
            }
            else
            {
                MessageBox.Show("Нет данных о работниках.");
            }

            // Дополнительные настройки столбцов (опционально)
            dataGridView1.Columns [ "RoleName" ].HeaderText = "Роль";
            dataGridView1.Columns [ "Login" ].HeaderText = "Логин";
            dataGridView1.Columns [ "Password" ].HeaderText = "Пароль";
            dataGridView1.Columns [ "FIO" ].HeaderText = "ФИО";
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
                dataGridView.Columns [ "Role" ].HeaderText = "Роль";
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

        private void AddNew_Click (object sender, EventArgs e)
        {
            if(LogAdd.Text!="" && PassAdd.Text!="" && FIOAdd.Text!="" && ComboAdd.SelectedIndex!=0)
            {
                int count = GetFilledRowsCountLINQ(dataGridView1);
                count++;
                string login = LogAdd.Text;
                string password = PassAdd.Text;
                string fio=FIOAdd.Text;
                int role = ComboAdd.SelectedIndex;
                req.AddNewWorker(login,password,fio,role,count);
            }
            else
            {
                MessageBox.Show("Потом будет подсвечивать неверное");
            }
        }
    }
}
