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
        private bool _isUpdating = false;
        private Label labelFIO;
        private TextBox textBoxFIO;
        private Label labelPassport;
        private TextBox textBoxPassport;
        private Label labelEmail;
        private TextBox textBoxEmail;
        private Label labelPhone;
        private TextBox textBoxPhone;
        private Label labelBirthDate;
        private DateTimePicker dateTimePickerBirth;
        private Label labelPreferences;
        private TextBox textBoxPreferences;
        private Label labelChildren;
        private CheckBox checkBoxChildren;
        private Label labelReservation;
        private CheckBox checkBoxReservation;
        private Label labelLivesIn;
        private CheckBox checkBoxLivesIn;
        private Button buttonAdd;
        private ListBox dynamicListBox;
        private int? selectedRoomId; 
        private int? selectedUserId; 
        private int? selectedReservationId;

        private string mode = "list_of_rooms";
        public Admin()
        {
            InitializeComponent();
            InitializeAnimation();
            System.Windows.Forms.ToolTip toolTip1 = new System.Windows.Forms.ToolTip();
            trackBar1.Scroll += TrackBar_Scroll1;
            trackBar2.Scroll += TrackBar_Scroll2;
            textBox4.SelectionStart = 3;
        }

        private void UpdateDataGrid()
        {
            dataGridView1.DataSource = sqlRequest.ExecuteQuery("Users");
            dataGridView2.DataSource = sqlRequest.ExecuteQuery("Users");
            dataGridView3.DataSource = sqlRequest.ExecuteQuery("Rooms where Status=0");
            dataGridView5.DataSource = sqlRequest.ExecuteQuery("Reservation");
        }

        private void TrackBar_Scroll1(object sender, EventArgs e)
        {
            label9.Text= $"Кол-во комнат: {trackBar1.Minimum}-{trackBar1.Value}";
        }
        private void TrackBar_Scroll2(object sender, EventArgs e)
        {
            label12.Text = $"Год рождения: {trackBar2.Value}-{trackBar2.Maximum}";
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
            textBox1.Width = this.Width - ((25 * this.Width) / 100);
            textBox1.Height = panel2.Height - ((70 * panel2.Height) / 100);
            textBox1.Location = new System.Drawing.Point(
              (panel2.Width - textBox1.Width) / 2, (panel2.Height - textBox1.Height) / 2);

            //button_side_menu
            pictureBox1.Location = new System.Drawing.Point(
                (panel2.Width - textBox1.Width)/3 , (panel2.Height - textBox1.Height) / 2);
            pictureBox1.Height = panel2.Height - ((70 * panel2.Height) / 100);
            pictureBox1.Width = pictureBox1.Height;

            //Логотип
            pictureBox2.Location = new System.Drawing.Point(textBox1.Right + 10, (int)(panel2.Height * 0.1));
            pictureBox2.Image= System.Drawing.Image.FromFile("C:\\Users\\ПК\\Desktop\\tw\\TeamWork_hostel\\WindowsFormsApp1\\Resources\\лого.png");
            pictureBox2.Size = new Size((int)((this.Width - textBox1.Width) / 2 * 0.7), (int)(panel2.Height * 0.7));
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;


            //side menu
            panel1.Width = (this.Width - textBox1.Width)/2;
            panel1.Height = this.Height-panel2.Height;
            panel1.Location = new System.Drawing.Point(-panel1.Width, panel2.Height);
          /*  panel1.BackColor = System.Drawing.Color.Red;*/
            
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



            Select_Mode("list_of_rooms");


            panel1.MouseEnter += Panel3_MouseEnter;
            panel1.MouseLeave += Panel3_MouseLeave;
                        
            AddMouseEventsToChildren(panel1);

        }


        private void Select_Mode(string tag)
        {
            // Ищем кнопку с указанным тегом в panel1
            Button targetButton = panel1.Controls.OfType<Button>()
                .FirstOrDefault(btn => btn.Tag?.ToString() == tag);

            if (targetButton != null)
            {
                // Имитируем нажатие кнопки
                Select_Mode(targetButton, EventArgs.Empty);
            }
            else
            {
                MessageBox.Show($"Кнопка с тегом '{tag}' не найдена!");
            }
        }



        //Методы для открытия и сокрытия боковой панели задач
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (panel1.Location.X < 0)
            {
                targetX = 0;
            }
            else
            {
                targetX = -panel1.Width;
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
                            mode = "list_of_rooms";
                            LoadRoomPanels();
                            break;
                        case "list_of_peoples":
                            mode = "list_of_peoples";
                            LoadListOfPeoples();
                            break;
                        case "populate":
                            mode = "populate";
                            Add_Populate();
                            break;
                        case "list_of_reservation":
                            mode = "list_of_reservation";
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

        const int verticalPadding = 10; // Отступ между элементами
        const int sidePadding = 10;     // Отступ от краев панели
        int currentY = 10; // Текущая вертикальная позиция

        private void Placement_Elements_Add_People()
        {
            currentY = 10;
            panel4.Controls.Clear();

            // Создаем элементы для всех полей:
            this.labelFIO = new Label { Text = "ФИО", AutoSize = true };
            this.textBoxFIO = new TextBox();

            this.labelPassport = new Label { Text = "Паспорт", AutoSize = true };
            this.textBoxPassport = new TextBox();

            this.labelEmail = new Label { Text = "Email", AutoSize = true };
            this.textBoxEmail = new TextBox();

            this.labelPhone = new Label { Text = "Телефон", AutoSize = true };
            this.textBoxPhone = new TextBox();
            this.textBoxPhone.Text = "+7";
            this.textBoxPhone.SelectionStart = 3;
            this.textBoxPhone.MaxLength = 16;
            this.textBoxPhone.TextChanged += Checked_Phonenumber;

            this.labelBirthDate = new Label { Text = "Дата рождения", AutoSize = true };
            this.dateTimePickerBirth = new DateTimePicker();

            this.labelPreferences = new Label { Text = "Предпочтения", AutoSize = true };
            this.textBoxPreferences = new TextBox();

            this.labelChildren = new Label { Text = "Есть дети", AutoSize = true };
            this.checkBoxChildren = new CheckBox();

            

            this.buttonAdd = new Button
            {
                Text = "Добавить пользователя",
                Width = 150,
                Height = 30,
                Visible = true,
                BackColor= Color.FromArgb(0xA3, 0xD6, 0xB8),
                FlatStyle = FlatStyle.Flat,
                FlatAppearance =
                {
                    BorderColor = Color.FromArgb(113, 208, 152), 
                    BorderSize = 2                              
                }
            };


            // Добавляем элементы на панель:
            AddControlToPanel(labelFIO);
            AddControlToPanel(textBoxFIO);

            AddControlToPanel(labelPassport);
            AddControlToPanel(textBoxPassport);

            AddControlToPanel(labelEmail);
            AddControlToPanel(textBoxEmail);

            AddControlToPanel(labelPhone);
            AddControlToPanel(textBoxPhone);

            AddControlToPanel(labelBirthDate);
            AddControlToPanel(dateTimePickerBirth);

            AddControlToPanel(labelPreferences);
            AddControlToPanel(textBoxPreferences);

            AddControlToPanel(labelChildren);
            AddControlToPanel(checkBoxChildren);
            AddControlToPanel(buttonAdd);

            // Инициализация обработчиков:
            InitializeEventHandlers(buttonAdd);

            UpdatePanel4Height();
        }

        // Метод для инициализации обработчиков событий
        void InitializeEventHandlers(Button buttonAdd)
        {
            // Обработчик события для CheckBox
            buttonAdd.Click += (s, e) =>
            {
                AddUser();
            };
        }

        private void AddUser()
        {
            // Получаем данные из полей:
            string fio = textBoxFIO.Text.Trim();
            string passport = textBoxPassport.Text.Trim();
            string email = textBoxEmail.Text.Trim();
            string phone = textBoxPhone.Text.Trim();
            DateTime birthDate = dateTimePickerBirth.Value;
            string preferences = textBoxPreferences.Text.Trim();
            bool children = checkBoxChildren.Checked;
            // Проверка заполнения полей:
            if (string.IsNullOrEmpty(fio) ||
                string.IsNullOrEmpty(passport) ||
                string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Заполните все обязательные поля!");
                return;
            }

            try
            {
                // Добавляем пользователя в БД:
                int newUserId = sqlRequest.AddUser(
                    fio,
                    passport,
                    phone,
                    email,
                    birthDate,
                    children,
                    preferences
                );

                // Обновляем DataGridView и выделяем новую строку:
                UpdateDataGrid();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["IDU"]?.Value != null &&
                        Convert.ToInt32(row.Cells["IDU"].Value) == newUserId)
                    {
                        row.Selected = true;
                        break;
                    }
                }

                // Очищаем поля:
                textBoxFIO.Clear();
                textBoxPassport.Clear();
                textBoxEmail.Clear();
                textBoxPhone.Text = "+7";
                textBoxPhone.SelectionStart = 3;
                textBoxPreferences.Clear();
                checkBoxChildren.Checked = false;
                dateTimePickerBirth.Value = DateTime.Now;
                panel4.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        // Метод для добавления контролов на panel4
        void AddControlToPanel(Control control)
        {
            control.Width = panel4.Width - 2 * sidePadding;
            if (control is Button || control is Label)
            {
                control.Height = 30;
            }
            else if (control is DateTimePicker)
            {
                control.Height = 25;
            }
            else if (control is CheckBox)
            {
                control.Height = 20;
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
        private void UpdatePanel4Height()
        {
            // Очистка текущего значения currentY
            currentY = sidePadding;

            // Расчет высоты всех элементов
            foreach (Control control in panel4.Controls)
            {
                if (control is Panel dynamicPanel && dynamicPanel.Tag?.ToString() == "dynamic")
                {
                    currentY += dynamicPanel.Height + verticalPadding;
                }
                else
                {
                    currentY += control.Height + verticalPadding;
                }
            }

            // Установка финальной высоты панели
            panel4.Height = currentY + verticalPadding;
            panel4.Refresh();
        }

        // Метод для очистки добавленных панелей
      /*  void ClearAddedPanels()
        {
            // Удаление динамических панелей
            var dynamicPanels = panel4.Controls.OfType<Panel>()
                .Where(p => p.Tag?.ToString() == "dynamic")
                .ToList();

            foreach (var panel in dynamicPanels)
            {
                panel4.Controls.Remove(panel);
            }

            // Сброс currentY до начальной позиции
            currentY = sidePadding;

            // Обновление позиции кнопки "Добавить пользователя"
            button9.Location = new Point(
                sidePadding,
                currentY
            );

            // Обновление высоты и позиций
            UpdatePanel4Height();
            UpdateLayout();
        }

        // Метод для добавления динамических панелей
        void AddDynamicPanels(int count)
        {
            *//*ClearAddedPanels(); // Очистка предыдущих панелей*//*

            for (int i = 0; i < count; i++)
            {
                Panel dynamicPanel = new Panel
                {
                    Tag = "dynamic",
                    Size = new Size(panel4.Width - 2 * sidePadding, 50),
                    BackColor = Color.LightGray,
                    Location = new Point(sidePadding, currentY)
                };

                // Добавление текстового поля
                TextBox childName = new TextBox
                {
                    Location = new Point(10, 10),
                    Width = dynamicPanel.Width - 20
                };
                dynamicPanel.Controls.Add(childName);

                panel4.Controls.Add(dynamicPanel);
                currentY += dynamicPanel.Height + verticalPadding;
            }

            // Позиционирование кнопки после всех элементов
            button9.Location = new Point(
                sidePadding,
                currentY
            );
            currentY += button9.Height + verticalPadding;

            // Обновление высоты и позиций
            UpdatePanel4Height();
            UpdateLayout();
        }*/
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
            List<bool> statuses = GetStatusesFilter();

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
        private List<bool> GetStatusesFilter()
        {
            List<bool> selectedStatuses = new List<bool>();
            foreach (var item in checkedListBox1.CheckedItems)
            {
                string text = item.ToString();

                if (text == "Проживают")
                {
                    selectedStatuses.Add(true); // Проживают = true
                }
                else if (text == "Не проживают")
                {
                    selectedStatuses.Add(false); // Не проживают = false
                }
            }
            return selectedStatuses;
        }
        //Фильтрация пользователей
        private void Filter_users(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            var (minYear, maxYear) = GetBirthYearFilter();
            List<bool> children = GetChildrenFilter();
            List<bool> reservations = GetReservationFilter();
            List<bool> livesIn = GetLivesInFilter();
            DataTable usersTable = sqlRequest.GetUsersForGrid(minYear, maxYear, children, reservations, livesIn);           
            if (usersTable.Rows.Count > 0)
            {
                dataGridView1.DataSource = usersTable;
            }
            else
            {
                MessageBox.Show("Нет данных, соответствующих фильтрам.");
            }
        }

        private (int minYear, int maxYear) GetBirthYearFilter()
        {
            int minYear = trackBar2.Value; 
            int maxYear = trackBar2.Maximum; 
            return (minYear, maxYear);
        }

        private List<bool> GetReservationFilter()
        {
            List<bool> reservations = new List<bool>();
            if (checkBox3.Checked)
            {
                reservations.Add(true);
            }
            else if (checkBox3.Checked)
            {
                reservations.Add(false);
            }
            return reservations;
        }
        private List<bool> GetChildrenFilter()
        {
            List<bool> children = new List<bool>();
            if (checkBox2.Checked)
            {
                children.Add(true); 
            }
            else if (checkBox2.Checked)
            {
                children.Add(false); 
            }
            return children;
        }
        private List<bool> GetLivesInFilter()
        {
            List<bool> livesIn = new List<bool>();
            if (checkBox4.Checked)
            {
                livesIn.Add(true);
            }
            else if (checkBox4.Checked)
            {
                livesIn.Add(false);
            }
            return livesIn;
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

            label19.Location = new Point((panel7.Width - label19.Width) / 4, dataGridView3.Bottom + 10);
            numericUpDown2.Location = new Point((label19.Right + 5), dataGridView3.Bottom + 10);
            button8.Location = new Point(label19.Left+ ((panel7.Width - numericUpDown2.Width) / 4)*2, dataGridView3.Bottom + 10);
            label8.Location = new Point(1, button8.Bottom + 10);
            panel7.Height = panel7.Height + 40;

        }

        private void изменитьДатуЗаездаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView5.SelectedRows.Count == 0 ||
       dataGridView5.SelectedRows[0].Cells["DateOfEntry"] == null)
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

                // Передаем старые даты для исключения текущей записи
                bool isConflict = await sqlRequest.CheckReservationConflictAsync(
                    roomNumber,
                    newDate,
                    currentDepartureDate,
                    currentEntryDate,
                    currentDepartureDate);

                if (isConflict)
                {
                    MessageBox.Show("На выбранную дату уже существует резервация для этого номера!");
                    return;
                }

                // Обновляем запись с проверкой старых дат
                sqlRequest.UpdateReservationEntryDate(
                    roomNumber,
                    newDate,
                    currentDepartureDate,
                    currentEntryDate,
                    currentDepartureDate);

                UpdateDataGrid();
                dateForm.Close();
            };

            dateForm.Controls.Add(datePicker);
            dateForm.Controls.Add(okButton);
            dateForm.ShowDialog();
            UpdateDataGrid();
        }

        private void изменитьДатуВыездаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView5.SelectedRows.Count == 0 ||
        dataGridView5.SelectedRows[0].Cells["DepartureDate"] == null)
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
                Text = "Изменение даты выезда",
                StartPosition = FormStartPosition.CenterParent
            };

            DateTimePicker datePicker = new DateTimePicker
            {
                Location = new Point(50, 30),
                Width = 150,
                Value = currentDepartureDate
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
                DateTime newDepartureDate = datePicker.Value;

                if (newDepartureDate <= currentEntryDate)
                {
                    MessageBox.Show("Дата выезда не может быть раньше даты заезда!");
                    return;
                }

                // Проверка конфликта с учетом старых дат
                bool isConflict = await sqlRequest.CheckDepartureConflictAsync(
                    roomNumber,
                    newDepartureDate,
                    currentEntryDate,
                    currentDepartureDate);

                if (isConflict)
                {
                    MessageBox.Show("На выбранную дату уже существует резервация для этого номера!");
                    return;
                }

                // Обновление записи
                sqlRequest.UpdateReservationDepartureDate(
                    roomNumber,
                    newDepartureDate,
                    currentEntryDate,
                    currentDepartureDate);

                UpdateDataGrid();
                dateForm.Close();
            };

            dateForm.Controls.Add(datePicker);
            dateForm.Controls.Add(okButton);
            dateForm.ShowDialog();
            UpdateDataGrid();
        }

        private void Delete_filter(object sender, EventArgs e)
        {
            panel5.Controls.Clear();
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

        private void button11_Click(object sender, EventArgs e)
        {
            UpdateDataGrid();
        }

        private void Checked_Phonenumber(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox == null) return;

            string text = textBox.Text;

            // Удаляем лишние символы
            text = new string(text.Where(c => char.IsDigit(c)).ToArray());

            // Добавляем префикс
            if (!text.StartsWith("7") && !text.StartsWith("8") && text.Length > 0)
            {
                text = "7" + text;
            }

            // Форматируем
            if (text.Length >= 11)
            {
                text = "+7 (" + text.Substring(1, 3) + ") " +
                       text.Substring(4, 3) + "-" +
                       text.Substring(7, 2) + "-" +
                       text.Substring(9, 2);
            }

            // Обновляем текст
            if (textBox.Text != text)
            {
                textBox.Text = text;
                textBox.SelectionStart = text.Length;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(searchText))
            {
                dynamicListBox?.Hide();
                return;
            }
            if (dynamicListBox == null)
            {
                dynamicListBox = new ListBox
                {
                    Width = textBox1.Width,
                    Height = 100,
                    Location = new Point(textBox1.Left, textBox1.Bottom),
                    Visible = false
                };
                this.Controls.Add(dynamicListBox);
                dynamicListBox.BringToFront();
                dynamicListBox.SelectedIndexChanged += ListBox_SelectedIndexChanged;
            }
            dynamicListBox.Items.Clear();
            mode = panel3.Controls.OfType<Panel>()
                .FirstOrDefault(p => p.Visible)?.Tag?.ToString() ?? "default";
            switch (mode)
            {
                case "list_of_rooms":
                    /*FilterRooms(searchText);*/
                    FilterPanelsByTag(searchText);
                    break;
                case "list_of_peoples":
                    FilterPeoples(searchText);
                    break;
                case "list_of_reservation":
                    FilterReservations(searchText);
                    break;
                case "populate":
                    FilterRooms(searchText);
                    FilterPeoples(searchText,'s');
                    break;
                default:
                    FilterSideMenuFunctions(searchText);
                    break;
            }
            dynamicListBox.Visible = dynamicListBox.Items.Count > 0;
        }
        private void FilterPeoples(string searchText)
        {
            var filteredData = dataGridView1.Rows
         .Cast<DataGridViewRow>()
         .Where(row => row.Cells["ФИО"].Value?.ToString().IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
         .Select(row => row.Cells["ФИО"].Value?.ToString())
         .Distinct()
         .ToList();
            dynamicListBox.Items.Clear();
            dynamicListBox.Items.AddRange(filteredData.ToArray());
            dynamicListBox.Visible = dynamicListBox.Items.Count > 0;
        }

        private void FilterPeoples(string searchText,char c)
        {
            var filteredData = dataGridView2.Rows
         .Cast<DataGridViewRow>()
         .Where(row => row.Cells["ФИО"].Value?.ToString().IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
         .Select(row => row.Cells["ФИО"].Value?.ToString())
         .Distinct()
         .ToList();
            dynamicListBox.Items.AddRange(filteredData.ToArray());
            dynamicListBox.Visible = dynamicListBox.Items.Count > 0;
        }

        private void FilterRooms(string searchText)
        {
            dynamicListBox.Items.Clear();
            var filteredData = dataGridView3.Rows
                .Cast<DataGridViewRow>()
                .Where(row => row.Cells["RoomNum"].Value?.ToString().IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                .Select(row => row.Cells["RoomNum"].Value?.ToString())
                .Distinct()
                .ToList();
            dynamicListBox.Items.AddRange(filteredData.ToArray());
        }


        private void FilterReservations(string searchText)
        {
            var reservations = sqlRequest.ExecuteQuery("Reservation").AsEnumerable()
         .Where(row => row["IDReser"].ToString().IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
         .Select(row => new
         {
             Id = Convert.ToInt32(row["IDReser"]),
             Name = row["IDReser"].ToString()
         })
         .ToList();

            dynamicListBox.Items.Clear();
            dynamicListBox.Items.AddRange(reservations.Select(r => r.Name).ToArray());

            for (int i = 0; i < reservations.Count; i++)
            {
                dynamicListBox.Items[i] = new ListBoxItem
                {
                    Text = reservations[i].Name,
                    Id = reservations[i].Id
                };
            }
        }

        private void FilterSideMenuFunctions(string searchText)
        {
            // Пример фильтрации функций бокового меню
            var functions = panel1.Controls.OfType<Button>()
        .Where(btn => btn.Text.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
        .Select(btn => btn.Text)
        .ToList();

            dynamicListBox.Items.AddRange(functions.ToArray());
        }

        private void ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dynamicListBox.SelectedItem == null) return;
           
            
            
            switch (mode)
            {
                case "list_of_rooms":
                    var item = dynamicListBox.SelectedItem as ListBoxItem;
                    if (item == null) return;
                    dynamicListBox.Visible = false;
                    selectedRoomId = item.Id;
                    ShowOnlySelectedPanel(item.Id);
                    break;
                case "list_of_peoples":
                    var selectedValue = dynamicListBox.SelectedItem.ToString();
                    dynamicListBox.Visible = false;
                    var row = dataGridView1.Rows
                        .Cast<DataGridViewRow>()
                        .FirstOrDefault(r => r.Cells["ФИО"].Value?.ToString() == selectedValue);
                    if (row != null)
                    {
                        
                        // Выделяем строку
                        dataGridView1.ClearSelection();
                        row.Selected = true;

                        // Прокручиваем к строке
                        dataGridView1.FirstDisplayedScrollingRowIndex = row.Index;
                    }
                    break;
                case "list_of_reservation":
                    break;
                case "populate":
                    var selectedValue1 = dynamicListBox.SelectedItem.ToString();
                    dynamicListBox.Visible = false;
                    var userRow = dataGridView2.Rows
                        .Cast<DataGridViewRow>()
                        .FirstOrDefault(r => r.Cells["ФИО"].Value?.ToString() == selectedValue1);
                    if (userRow != null)
                    {
                        dataGridView2.ClearSelection();
                        userRow.Selected = true;
                        dataGridView2.FirstDisplayedScrollingRowIndex = userRow.Index;
                    }
                    var roomRow = dataGridView3.Rows
                        .Cast<DataGridViewRow>()
                        .FirstOrDefault(r => r.Cells["RoomNum"].Value?.ToString() == selectedValue1);

                    if (roomRow != null)
                    {
                        dataGridView3.ClearSelection();
                        roomRow.Selected = true;
                        dataGridView3.FirstDisplayedScrollingRowIndex = roomRow.Index;
                    }
                    break;
            }
        }
        private void ShowOnlySelectedPanel(int roomId)
        {
            // Находим панель с выбранной комнатой
            var selectedPanel = panel5.Controls.OfType<Panel>()
                .FirstOrDefault(p => p.Tag != null && Convert.ToInt32(p.Tag) == roomId);

            if (selectedPanel == null)
            {
                MessageBox.Show("Комната не найдена.");
                return;
            }
            Form roomDetailsForm = new Form
            {
                Width = 400, // Ширина формы
                Height = 200, // Высота формы
                Text = "Информация о комнате", // Заголовок формы
                StartPosition = FormStartPosition.CenterParent // Позиция формы по центру родительского окна
            };
            PictureBox roomImage = new PictureBox
            {
                Size = new Size(80, 100), 
                Location = new Point(10, 10), // Позиция изображения
                SizeMode = PictureBoxSizeMode.StretchImage, // Режим отображения изображения
                BackColor = Color.LightGray 
            };
            Label roomNumberLabel = new Label
            {
                Text = $"Номер: {selectedPanel.Tag}", // Номер комнаты из Tag панели
                Font = new Font("Arial", 12, FontStyle.Bold), // Шрифт и стиль
                Location = new Point(roomImage.Right + 10, 10), // Позиция Label
                AutoSize = true // Автоматический размер
            };
            Label bedsLabel = new Label
            {
                Text = $"Кровати: {GetControlText(selectedPanel, "bedsLabel")}", // Текст из Label на панели
                Font = new Font("Arial", 10), // Шрифт
                Location = new Point(roomImage.Right + 10, roomNumberLabel.Bottom + 5), // Позиция
                AutoSize = true // Автоматический размер
            };
            Label extrasLabel = new Label
            {
                Text = $"Услуги: {GetControlText(selectedPanel, "extrasLabel")}", 
                Font = new Font("Arial", 10), 
                Location = new Point(roomImage.Right + 10, bedsLabel.Bottom + 5), 
                AutoSize = true, 
                MaximumSize = new Size(roomDetailsForm.Width - roomImage.Width - 30, 0) 
            };
            
            roomDetailsForm.Controls.Add(roomImage);
            roomDetailsForm.Controls.Add(roomNumberLabel);
            roomDetailsForm.Controls.Add(bedsLabel);
            roomDetailsForm.Controls.Add(extrasLabel);
            roomDetailsForm.ShowDialog();
        }

        private void FilterRoomsById(int roomId)
        {
            panel5.Controls.Clear();

            // Загружаем только выбранную комнату
            List<Panel> filteredRooms = sqlRequest.GetRoomPanels(panel5.Width, panel5.Height)
                .Where(p => Convert.ToInt32(p.Tag) == roomId)
                .ToList();

            if (filteredRooms.Any())
            {
                int verticalSpacing = 10;
                int currentY = 10;

                foreach (var panel in filteredRooms)
                {
                    panel.Location = new Point(10, currentY);
                    panel5.Controls.Add(panel);
                    currentY += panel.Height + verticalSpacing;
                }
            }
            else
            {
                MessageBox.Show("Комната не найдена.");
            }
        }

        private void FilterPanelsByTag(string searchText)
        {
            
            var filteredPanels = panel5.Controls.OfType<Panel>()
                .Where(p => p.Tag != null && p.Tag.ToString().IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();

            if (filteredPanels.Any())
            {
                dynamicListBox.Items.Clear();
                foreach (var panel in filteredPanels)
                {
                    dynamicListBox.Items.Add(new ListBoxItem
                    {
                        Text = panel.Tag.ToString(),
                        Id = Convert.ToInt32(panel.Tag)
                    });
                }
            }
        }

        private string GetControlText(Panel cardPanel, string controlName)
        {
            var control = cardPanel.Controls.OfType<Label>().FirstOrDefault(c => c.Name == controlName);
            return control?.Text ?? "N/A";
        }
        

    }
    public class ListBoxItem
    {
        public string Text { get; set; }
        public int Id { get; set; }

        public override string ToString() => Text;
    }
}
