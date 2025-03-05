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
        private bool isPanel4Open = false;
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

        private void UpdateDataGrid()
        {
            dataGridView1.DataSource = sqlRequest.ExecuteQuery("Users");
            dataGridView2.DataSource = sqlRequest.ExecuteQuery("Users");
            dataGridView3.DataSource = sqlRequest.ExecuteQuery("Rooms where Status=0");
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
                        case "list_of_reservation":
                            LoadReservation();
                            break;
                    }
                    break;
                }
            }
        }

        private void LoadReservation()
        {
            dataGridView5.DataSource = sqlRequest.ExecuteQuery("Reservation");
            dataGridView5.Dock = DockStyle.Fill;
        }
        //Размещение элементов для заселения
        private void Add_Populate()
        {
            dataGridView3.DataSource = sqlRequest.ExecuteQuery("Rooms Where Status=0");
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

            panel4.Size = new Size((int)(panel7.Width *0.8), 400); 
            panel4.Location = new Point((panel7.Width - panel4.Width) / 2, label14.Bottom + 5);
            panel4.Visible = false;


            UpdateLayout();

        }

        int verticalPadding = 10; // Отступ между элементами
        int sidePadding = 10;     // Отступ от краев панели
        int currentY = 10; // Текущая вертикальная позиция

        private void Placement_Elements_Add_People()
        {
            int verticalPadding = 10; // Отступ между элементами
            int sidePadding = 10;     // Отступ от краев панели
            int currentY = sidePadding; // Текущая вертикальная позиция

            // Очищаем панель перед размещением элементов
            panel4.Controls.Clear();

            // Создаем основные элементы
            Label label1 = new Label { Text = "Имя", AutoSize = true };
            TextBox textBox2 = new TextBox();
            Label label2 = new Label { Text = "Фамилия", AutoSize = true };
            TextBox textBox3 = new TextBox();
            Label label3 = new Label { Text = "Email", AutoSize = true };
            TextBox textBox4 = new TextBox();
            Label label4 = new Label { Text = "Телефон", AutoSize = true };
            TextBox textBox5 = new TextBox();
            Label label5 = new Label { Text = "Дата рождения", AutoSize = true };
            DateTimePicker dateEntry = new DateTimePicker();

            // CheckBox для выбора количества панелей
            CheckBox checkBoxAddPanels = new CheckBox { Text = "Добавить детей", AutoSize = true };

            // NumericUpdown для указания количества панелей
            NumericUpDown numericUpDownPanels = new NumericUpDown
            {
                Minimum = 1, // Минимальное значение равно 1
                Value = 1,   // Начальное значение равно 1
                Width = 100,
                Visible = false
            };

            // Кнопка button9
            Button button9 = new Button
            {
                Text = "Добавить пользователя",
                Width = 150,
                Height = 30,
                Visible = true
            };

            // Добавляем основные элементы на panel4
            AddControlToPanel(label1);
            AddControlToPanel(textBox2);
            AddControlToPanel(label2);
            AddControlToPanel(textBox3);
            AddControlToPanel(label3);
            AddControlToPanel(textBox4);
            AddControlToPanel(label4);
            AddControlToPanel(textBox5);
            AddControlToPanel(label5);
            AddControlToPanel(dateEntry);

            // Добавляем CheckBox
            AddControlToPanel(checkBoxAddPanels);

            // Добавляем NumericUpdown (скрытый по умолчанию)
            AddControlToPanel(numericUpDownPanels);

            // Сохраняем текущую позицию после numericUpDownPanels
            int dynamicPanelStartY = currentY;

            // Добавляем кнопку button9 (пока не добавляем на панель)
            button9.Location = new Point(sidePadding, currentY); // Запоминаем позицию кнопки
            currentY += button9.Height + verticalPadding;

            // Инициализируем обработчики событий
            InitializeEventHandlers(checkBoxAddPanels, numericUpDownPanels, button9);

            // Устанавливаем начальную высоту panel4
            UpdatePanel4Height();
        }

        // Метод для инициализации обработчиков событий
        void InitializeEventHandlers(CheckBox checkBoxAddPanels, NumericUpDown numericUpDownPanels, Button button9)
        {
            // Обработчик события для CheckBox
            checkBoxAddPanels.CheckedChanged += (s, e) =>
            {
                numericUpDownPanels.Visible = checkBoxAddPanels.Checked;

                if (checkBoxAddPanels.Checked)
                {
                    // Если выбрано, добавляем динамические панели
                    AddDynamicPanels((int)numericUpDownPanels.Value);
                }
                else
                {
                    // Если не выбрано, очищаем добавленные панели
                    ClearAddedPanels();
                }

                // Перерасполагаем кнопку button9
                button9.Location = new Point(
                    sidePadding,
                    GetButtonPositionY()
                );

                // Обновляем высоту panel4
                UpdatePanel4Height();
            };

            // Обработчик изменения значения в NumericUpdown
            numericUpDownPanels.ValueChanged += (s, e) =>
            {
                if (checkBoxAddPanels.Checked)
                {
                    // Обновляем количество динамических панелей
                    AddDynamicPanels((int)numericUpDownPanels.Value);
                }

                // Перерасполагаем кнопку button9
                button9.Location = new Point(
                    sidePadding,
                    GetButtonPositionY()
                );

                // Обновляем высоту panel4
                UpdatePanel4Height();
            };

            // Добавляем кнопку button9 на панель
            panel4.Controls.Add(button9);
        }

        // Метод для добавления контролов на panel4
        void AddControlToPanel(Control control)
        {
            control.Width = panel4.Width - 2 * sidePadding;
            if (control is Button || control is Label)
            {
                control.Height = 30;
            }
            control.Location = new Point(sidePadding, currentY);
            panel4.Controls.Add(control);
            currentY += control.Height + verticalPadding;
        }

        // Метод для получения Y-позиции кнопки button9
        int GetButtonPositionY()
        {
            return currentY; // Текущая позиция после всех динамических панелей
        }

        // Метод для обновления высоты panel4
        void UpdatePanel4Height()
        {
            panel4.Height = currentY + sidePadding;

            // Обновляем расположение элементов ниже panel4
            UpdateLayout();
        }

        // Метод для очистки добавленных панелей
        void ClearAddedPanels()
        {
            // Преобразуем Controls в IEnumerable<Control> с помощью Cast<Control>()
            foreach (Control control in panel4.Controls.Cast<Control>().ToList())
            {
                if (control is Panel panel && panel.Tag != null && panel.Tag.ToString() == "dynamic")
                {
                    panel4.Controls.Remove(control);
                }
            }

            // Сбрасываем текущую позицию для корректного перерасчета
            currentY = sidePadding + panel4.Controls.OfType<Control>().Sum(c => c.Height + verticalPadding) - verticalPadding;
        }

        // Метод для добавления динамических панелей
        void AddDynamicPanels(int count)
        {
            ClearAddedPanels(); // Очищаем предыдущие панели

            for (int i = 0; i < count; i++)
            {
                Panel dynamicPanel = new Panel
                {
                    Tag = "dynamic", // Метка для идентификации
                    Size = new Size(panel4.Width - 2 * sidePadding, 50),
                    BackColor = Color.LightGray,
                    Location = new Point(sidePadding, currentY)
                };

                // Добавляем текстовое поле на панель
                TextBox childName = new TextBox
                {
                    Location = new Point(10, 10),
                    Width = dynamicPanel.Width - 20
                };
                dynamicPanel.Controls.Add(childName);

                panel4.Controls.Add(dynamicPanel);
                currentY += dynamicPanel.Height + verticalPadding;
            }
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

        //Смена статуса проживания
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

        //Добавление резервации
        private void Add_population(object sender, EventArgs e)
        {
            int idu=0;
            int idr = 0;
            DateTime entryDate;
            DateTime departureDate;
            if (dataGridView2.SelectedRows.Count > 0)
            {
                
                if (!int.TryParse(dataGridView2.SelectedRows[0].Cells["IDU"].Value?.ToString(), out idu))
                {
                    MessageBox.Show("Неверный формат IDU.");
                }
            }
            else
            {
                MessageBox.Show("Выберите строку в таблице Users.");
                return;
            }
            if (dataGridView3.SelectedRows.Count > 0)
            {

                if (!int.TryParse(dataGridView3.SelectedRows[0].Cells["IDR"].Value?.ToString(), out idr))
                {
                    MessageBox.Show("Неверный формат IDR.");
                }
            }
            else
            {
                MessageBox.Show("Выберите строку в таблице Rooms.");
                return;
            }
            
            if (date_entry.Checked && dep_date.Checked)
            {
                entryDate = date_entry.Value.Date;
                departureDate = dep_date.Value.Date;

                sqlRequest.AddReservation(idu, idr, entryDate, departureDate, Convert.ToInt32(numericUpDown2.Value));
                UpdateDataGrid();
            }
            else
            {
                MessageBox.Show("Выберите даты заезда и выезда!");
                return;
            }

            



        }

        private void Open_add_user(object sender, EventArgs e)
        {
            isPanel4Open = !isPanel4Open; 
            panel4.Visible = isPanel4Open;
            Placement_Elements_Add_People();
            UpdateLayout();
        }

        private void UpdateLayout()
        {
            int yOffset = isPanel4Open ? panel4.Height + 10 : 0; // Смещение для учета высоты панели

            // Расположение элементов после панели
            label15.Location = new Point((panel7.Width - label15.Width) / 3, label14.Bottom + 5 + yOffset);
            date_entry.Location = new Point((label15.Left - label15.Width / 2), label15.Bottom + 10);
            date_entry.Width = label15.Width * 2;

            label16.Location = new Point(label15.Left + ((panel7.Width - label15.Width) / 3), label14.Bottom + 10 + yOffset);
            dep_date.Location = new Point((label16.Left - label16.Width / 2), label16.Bottom + 10);
            dep_date.Width = date_entry.Width;

            label17.Location = new Point((panel7.Width - label17.Width) / 2, dep_date.Bottom + 10);
            dataGridView3.Location = new Point((panel7.Width - dataGridView3.Width) / 2, label17.Bottom + 10);

            label19.Location = new Point((panel7.Width - label19.Width) / 2, dataGridView3.Bottom + 10);
            numericUpDown2.Location = new Point((panel7.Width - numericUpDown2.Width) / 2, label19.Bottom + 10);
            button8.Location = new Point((panel7.Width - button8.Width) / 2, numericUpDown2.Bottom + 10);


        }

        private void изменитьДатуЗаездаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView5.SelectedRows.Count == 0 || dataGridView5.SelectedRows[0].Cells["DateOfEntry"] == null)
            {
                MessageBox.Show("Выберите строку с датой для изменения!");
                return;
            }
            int roomNumber = Convert.ToInt32(dataGridView5.SelectedRows[0].Cells["RoomNumber"].Value);
            DateTime currentEntryDate = Convert.ToDateTime(dataGridView5.SelectedRows[0].Cells["DateOfEntry"].Value);
            DateTime currentDepartureDate = Convert.ToDateTime(dataGridView5.SelectedRows[0].Cells["DepartureDate"].Value);
            Form dateForm = new Form
            {
                Width = 250,
                Height = 150,
                Text = "Изменение даты",
                StartPosition = FormStartPosition.CenterParent
            };

            DateTimePicker datePicker = new DateTimePicker
            {
                Location = new Point(50, 30),
                Width = 150,
                Value = currentEntryDate 
            };
            Button okButton = new Button
            {
                Text = "OK",
                Location = new Point(85, 70),
                Width = 80,
                Height = 30
            };
            okButton.Click += async (s, args) =>
            {
                DateTime newDate = datePicker.Value;
                if (newDate >= currentDepartureDate)
                {
                    MessageBox.Show("Дата заезда не может быть позже или равна дате выезда!");
                    return;
                }
                bool isConflict = await sqlRequest.CheckReservationConflictAsync(roomNumber, newDate, currentDepartureDate);

                if (isConflict)
                {
                    MessageBox.Show("На выбранную дату уже существует резервация для этого номера!");
                    return;
                }
                MessageBox.Show($"Новое значение даты: {newDate.ToShortDateString()}");
                sqlRequest.UpdateReservationEntryDate(roomNumber, newDate, currentEntryDate);
                UpdateDataGrid();
                dateForm.Close();
            };
            dateForm.Controls.Add(datePicker);
            dateForm.Controls.Add(okButton);
            dateForm.ShowDialog();
        }
    }
}
