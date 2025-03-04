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


namespace WindowsFormsApp1
{
    public partial class Admin : Form
    {
        private Timer timer;
        private int targetX; 
        private const int animationStep = 10;
        private bool isPanel3Hovered = false;
        Sql_Request sqlRequest = new Sql_Request();
        private Dictionary<string, int> conditionMapping = new Dictionary<string, int>
        {
            { "Убрать", 1 },
            { "Ремонт", 2 },
            { "Готово", 3 },
            { "Ремонт+Уборка", 4 }
        };

        public Admin()
        {
            InitializeComponent();
            InitializeAnimation();
        }
        
        //Методы для анимации
        private void InitializeAnimation()
        {
            timer = new Timer();
            timer.Interval = 10; 
            timer.Tick += Timer_Tick;

            panel1.Location = new System.Drawing.Point(-panel1.Width, panel2.Height);
            targetX = 0; 
        }
        
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (panel1.Location.X < targetX)
            {
                panel1.Left += animationStep;
                if (panel1.Left > targetX)
                    panel1.Left = targetX; 
            }
            else if (panel1.Location.X > targetX)
            {
                panel1.Left -= animationStep;
                if (panel1.Left < targetX)
                    panel1.Left = targetX;
            }
            else
            {
                timer.Stop();
                if (targetX == -panel1.Width)
                {
                    filter_panel.Visible = true;
                }
                else
                {
                    filter_panel.Visible = false;
                }
            }
        }

        //Постановка элементов формы
        private void Admin_Load(object sender, EventArgs e)
        {
            //header
            panel2.Width = this.Width;
            panel2.Height = this.Height - ((90 * this.Height) / 100);
            panel2.Location= new System.Drawing.Point(0,0);
            //search
            textBox1.Width = this.Width - ((20 * this.Width) / 100);
            textBox1.Height = panel2.Height - ((70 * panel2.Height) / 100);
            textBox1.Location = new System.Drawing.Point(
              (panel2.Width - textBox1.Width) / 2, (panel2.Height - textBox1.Height) / 2);

            //button_side_menu
            pictureBox1.Location = new System.Drawing.Point(
                (panel2.Width - textBox1.Width)/3 , (panel2.Height - textBox1.Height) / 2);
            pictureBox1.Height = panel2.Height - ((70 * panel2.Height) / 100);
            pictureBox1.Width = pictureBox1.Height;

            //side menu
            panel1.Width = (this.Width - textBox1.Width)/2;
            panel1.Height = this.Height-panel2.Height;
            panel1.Location = new System.Drawing.Point(-panel1.Width, panel2.Height);
            panel1.BackColor = System.Drawing.Color.Red;
            
            //main place
            panel3.Location= new System.Drawing.Point(
                (this.Width - textBox1.Width) / 2,this.Height - ((90 * this.Height) / 100));
            panel3.Width=this.Width-panel1.Width*2;
            panel3.Height=this.Height-panel2.Height;
            panel3.BackColor = System.Drawing.Color.Pink;

            //Filter panel
            filter_panel.Size = panel1.Size;
            filter_panel.Location = new System.Drawing.Point(0,panel2.Height);
            //Filter_rooms
            foreach(Control control in filter_rooms.Controls)
            {
                control.Width=(int)(panel1.Width*0.8);
                control.Height = 20;
                control.Top += 10;
                
                if (control is Button)
                {
                    control.Top += 40;
                    control.Height = 25;
                }
                if(control is CheckedListBox)
                {
                    control.Height = 50;
                    
                }
            }
            //filter_users
            foreach (Control control in filter_users.Controls)
            {
                control.Width = (int)(panel1.Width * 0.8);
                control.Height = 20;
                control.Top += 10;
                if (control is Button)
                {
                    control.Top += 40;
                    control.Height = 25;
                }
            }
            





            panel1.MouseEnter += Panel3_MouseEnter;
            panel1.MouseLeave += Panel3_MouseLeave;
                        
            AddMouseEventsToChildren(panel1);

        }
        //Методы для открытия и сокрытия боковой панели задач
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (panel1.Location.X < 0)
            {
                targetX = 0;
                panel3.BackColor = Color.FromArgb(173, 183, 185);
                this.BackColor= Color.FromArgb(173, 183, 185);
            }
            else
            {
                targetX = -panel1.Width;
                panel3.BackColor = Color.FromArgb(243, 253, 255);
                this.BackColor = Color.FromArgb(243, 253, 255);
            }
            timer.Start();
            
        }

        private void AddMouseEventsToChildren(Control parent)
        {
            foreach (Control child in parent.Controls)
            {
                child.MouseEnter += Panel3_MouseEnter;
                child.MouseLeave += Panel3_MouseLeave;

                if (child.HasChildren)
                {
                    AddMouseEventsToChildren(child);
                }
            }
        }

        private void Panel3_MouseEnter(object sender, EventArgs e)
        {
            isPanel3Hovered = true;
        }

        private void Panel3_MouseLeave(object sender, EventArgs e)
        {
            isPanel3Hovered = false; 
            Task.Delay(200).ContinueWith(t =>
            {
                if (!isPanel3Hovered) 
                {
                    this.Invoke((Action)(() =>
                    {
                        HidePanel3();
                    }));
                }
            });
        }

        private void HidePanel3()
        {
            targetX = -panel1.Width;
            panel3.BackColor = Color.FromArgb(243, 253, 255);
            this.BackColor = Color.FromArgb(243, 253, 255);
            timer.Start();
        }

        //Выбор функции
        private void Select_Mode(object sender, EventArgs e)
        {
            Button bt = sender as Button;
            foreach (Control control in panel3.Controls)
            {
                if(control is Panel panel && panel.Tag.ToString() != bt.Tag.ToString())
                {
                    panel.Visible = false;
                }
                else if(control is Panel panel1 && panel1.Tag.ToString() == bt.Tag.ToString())
                {
                    panel1.Visible = true;
                    panel1.Dock = DockStyle.Fill;
                    panel1.Size=panel3.Size;
                    foreach (Control control1 in filter_panel.Controls)
                    {
                        if (control1 is Panel p1 && p1.Tag.ToString() == bt.Tag.ToString())
                        {
                            p1.Visible = true;
                            p1.Dock = DockStyle.Fill;
                        }
                        else control1.Visible = false;
                    }
                    HidePanel3();
                    switch (panel1.Tag.ToString())
                    {
                        case "list_of_rooms":
                            LoadRoomPanels();
                            break;
                        case "list_of_peoples":
                            LoadListOfPeoples();
                            break;
                        case "populate":
                            Add_Populate();
                            break;
                    }
                    break;
                }
            }
        }

        private void Add_Populate()
        {
            dataGridView3.DataSource = sqlRequest.ExecuteQuery("Rooms");
            dataGridView2.DataSource = sqlRequest.ExecuteQuery("Users");
            panel7.Dock = DockStyle.Fill;
            panel7.Size=panel3.Size ;
            dataGridView2.Width = (int)(panel7.Width * 0.8);
            dataGridView2.Height = (int)(panel7.Height * 0.3);
            dataGridView3.Size = dataGridView2.Size;
            //Add_panel
            label13.Location = new Point((panel7.Width - label13.Width) / 2, 10);
            dataGridView2.Location = new Point((panel7.Width - dataGridView2.Width) / 2, label13.Bottom + 10);
            label14.Location = new Point(dataGridView2.Left, dataGridView2.Bottom + 5);
            label15.Location = new Point((panel7.Width - label15.Width) / 3, label14.Bottom + 10);
            date_entry.Location= new Point((label15.Left - label15.Width/2), label15.Bottom + 10);
            date_entry.Width = label15.Width * 2;
            label16.Location= new Point(label15.Right*((panel7.Width - label15.Width) / 3), label14.Bottom + 10);



        }





        //Загрузка карточек с комнатами
        private void LoadRoomPanels()
        {
            List<Panel> roomPanels = sqlRequest.GetRoomPanels(panel5.Width, panel5.Height);

            if (roomPanels != null && roomPanels.Any())
            {
                int verticalSpacing = 10; // Отступ между панелями
                int currentY = 10; // Начальная позиция по Y

                foreach (var panel in roomPanels)
                {
                    panel.Location = new Point(10, currentY); // Устанавливаем позицию
                    panel5.Controls.Add(panel); // Добавляем панель на panel5
                    currentY += panel.Height + verticalSpacing; // Обновляем позицию для следующей панели
                }
            }
            else
            {
                MessageBox.Show("Нет данных о комнатах.");
            }
        }

        //Загрузка списка пользователей
        private void LoadListOfPeoples()
        {
            dataGridView1.DataSource = sqlRequest.ExecuteQuery("Users");
            dataGridView1.Dock = DockStyle.Fill;
        }

        private void Find_Children(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int idu;
                if (int.TryParse(dataGridView1.SelectedRows[0].Cells["IDU"].Value?.ToString(), out idu))
                {
                    // Вызываем метод из библиотеки классов
                    Sql_Request sqlRequest = new Sql_Request();
                    DataTable details = sqlRequest.LoadChildren(idu); 

                    if (details != null && details.Rows.Count > 0)
                    {
                        // Отображаем детали (например, в новой форме или MessageBox)
                        dataGridView1.DataSource= details;
                    }
                    else
                    {
                        MessageBox.Show("Детали не найдены.");
                    }
                }
                else
                {
                    MessageBox.Show("Неверный формат IDU.");
                }
            }
            else
            {
                MessageBox.Show("Выберите строку в таблице.");
            }
        }
        
        //Фильтрация комнат
        private void Filter_click(object sender, EventArgs e)
        {
            var (bedsMin, bedsMax) = GetBedsFilter();
            List<int> conditions = GetConditionIdsFilter();
            List<bool?> statuses = GetStatusesFilter();

            panel5.Controls.Clear();

            List<Panel> roomPanels = sqlRequest.GetRoomPanels(panel5.Width, panel5.Height, bedsMin, bedsMax, conditions, statuses);

            if (roomPanels.Any())
            {
                int verticalSpacing = 10;
                int currentY = 10;

                foreach (var panel in roomPanels)
                {
                    panel.Location = new Point(10, currentY);
                    panel5.Controls.Add(panel);
                    currentY += panel.Height + verticalSpacing;
                }
            }
            else
            {
                MessageBox.Show("Нет комнат, соответствующих выбранным фильтрам.");
            }
        }
        //Получение+перевод данных по кроватке
        private (int? bedsMin, int? bedsMax) GetBedsFilter()
        {
            int minBeds = trackBar1.Minimum;
            int maxBeds = trackBar1.Value;

            return (minBeds == trackBar1.Minimum ? (int?)null : minBeds,
                    maxBeds == trackBar1.Maximum ? (int?)null : maxBeds);
        }
        //Получение+перевод состояние комнаты
        private List<int> GetConditionIdsFilter()
        {
            List<int> selectedConditionIds = new List<int>();
            foreach (var item in checkedListBox2.CheckedItems)
            {
                if (conditionMapping.TryGetValue(item.ToString(), out int conditionId))
                {
                    selectedConditionIds.Add(conditionId);
                }
            }
            return selectedConditionIds;
        }
        //Получение+перевод статуса комнаты
        private List<bool?> GetStatusesFilter()
        {
            List<bool?> selectedStatuses = new List<bool?>();
            foreach (var item in checkedListBox1.CheckedItems)
            {
                if (bool.TryParse(item.ToString(), out bool status))
                {
                    selectedStatuses.Add(status);
                }
                else if (item.ToString().Equals("null", StringComparison.OrdinalIgnoreCase))
                {
                    selectedStatuses.Add(null);
                }
            }
            return selectedStatuses;
        }
        //Фильтрация пользователей
        private void Filter_users(object sender, EventArgs e)
        {

        }

        //Поиск места проживания
        private void Find_room_living(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int idu;
                if (int.TryParse(dataGridView1.SelectedRows[0].Cells["IDU"].Value?.ToString(), out idu))
                {
                    // Вызываем метод из библиотеки классов
                    Sql_Request sqlRequest = new Sql_Request();
                    DataTable details = sqlRequest.GetCurrentResidence(idu);

                    if (details != null && details.Rows.Count > 0)
                    {
                        // Отображаем детали (например, в новой форме или MessageBox)
                        dataGridView1.DataSource = details;
                    }
                    else
                    {
                        MessageBox.Show("Детали не найдены.");
                    }
                }
                else
                {
                    MessageBox.Show("Неверный формат IDU.");
                }
            }
            else
            {
                MessageBox.Show("Выберите строку в таблице.");
            }
        }

        //Поиск будующих резерваций
        private void Find_reservation(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int idu;
                if (int.TryParse(dataGridView1.SelectedRows[0].Cells["IDU"].Value?.ToString(), out idu))
                {
                    // Вызываем метод из библиотеки классов
                    Sql_Request sqlRequest = new Sql_Request();
                    DataTable details = sqlRequest.GetFutureReservations(idu);

                    if (details != null && details.Rows.Count > 0)
                    {
                        // Отображаем детали (например, в новой форме или MessageBox)
                        dataGridView1.DataSource = details;
                    }
                    else
                    {
                        MessageBox.Show("Детали не найдены.");
                    }
                }
                else
                {
                    MessageBox.Show("Неверный формат IDU.");
                }
            }
            else
            {
                MessageBox.Show("Выберите строку в таблице.");
            }
        }

        private void Change_of_living(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                if (row.Cells["IDU"] == null || row.Cells["IDU"].Value == null)
                {
                    MessageBox.Show("Ошибка: отсутствует ID пользователя.");
                    return;
                }
                int idUser = Convert.ToInt32(row.Cells["IDU"].Value);
                if (dataGridView1.Columns[e.ColumnIndex].Name == "Reservation")
                {
                    bool reservationValue;
                    if (bool.TryParse(row.Cells["Reservation"].Value?.ToString(), out reservationValue))
                    {
                        sqlRequest.UpdateUserReservation(idUser, reservationValue);
                    }
                    else
                    {
                        MessageBox.Show("Неверное значение для поля Reservation.");
                    }
                }
                if (dataGridView1.Columns[e.ColumnIndex].Name == "Lives_in")
                {
                    // Проверяем, что событие было вызвано для существующей строки и столбца
                    if (e.RowIndex < 0 || e.ColumnIndex < 0 || dataGridView1.Rows.Count == 0)
                    {
                        MessageBox.Show("Выберите строку в таблице.");
                        return;
                    }

                    // Получаем текущую строку
                    DataGridViewRow row1 = dataGridView1.Rows[e.RowIndex];

                    // Проверяем, что строка содержит данные
                    if (row1.Cells["IDU"] == null || row1.Cells["IDU"].Value == null)
                    {
                        MessageBox.Show("Ошибка: отсутствует ID пользователя.");
                        return;
                    }

                    // Получаем ID пользователя
                    int idUser1;
                    if (!int.TryParse(row1.Cells["IDU"].Value?.ToString(), out idUser1))
                    {
                        MessageBox.Show("Неверный формат ID пользователя.");
                        return;
                    }

                    // Проверяем, что изменено поле "Lives_in"
                    if (dataGridView1.Columns[e.ColumnIndex].Name != "Lives_in")
                    {
                        return; // Игнорируем изменения в других полях
                    }

                    // Получаем новое значение поля "Lives_in"
                    bool livesInValue;
                    if (!bool.TryParse(row.Cells["Lives_in"].Value?.ToString(), out livesInValue))
                    {
                        MessageBox.Show("Неверное значение для поля Lives_in.");
                        return;
                    }

                    // Обновляем статус проживания в базе данных
                    sqlRequest.UpdateLivingAndRoomStatus(idUser, livesInValue);
                }
            }
        }

       


    }
}
