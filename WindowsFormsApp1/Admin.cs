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
                    }
                    break;
                }
            }
        }

        //Загрузка карточек с комнатами
        private void LoadRoomPanels()
        {
            
            List<Panel> roomPanels = sqlRequest.GetRoomPanels(panel5.Width , panel5.Height);

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
                    DataTable details = sqlRequest.LoadChildren(idu); // Предполагается, что такой метод существует

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
        
        //Фильтрация 
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

        private (int? bedsMin, int? bedsMax) GetBedsFilter()
        {
            int minBeds = trackBar1.Minimum;
            int maxBeds = trackBar1.Value;

            return (minBeds == trackBar1.Minimum ? (int?)null : minBeds,
                    maxBeds == trackBar1.Maximum ? (int?)null : maxBeds);
        }

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
    }
}
