namespace WindowsFormsApp1
{
    partial class Admin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.просмотретьДетейToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel5 = new System.Windows.Forms.Panel();
            this.filter_panel = new System.Windows.Forms.Panel();
            this.panel13 = new System.Windows.Forms.Panel();
            this.panel12 = new System.Windows.Forms.Panel();
            this.panel11 = new System.Windows.Forms.Panel();
            this.filter_rooms = new System.Windows.Forms.Panel();
            this.button6 = new System.Windows.Forms.Button();
            this.checkedListBox2 = new System.Windows.Forms.CheckedListBox();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.mySqlCommand1 = new MySql.Data.MySqlClient.MySqlCommand();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.filter_panel.SuspendLayout();
            this.filter_rooms.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(69, 47);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(132, 24);
            this.textBox1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Location = new System.Drawing.Point(4, 15);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(267, 194);
            this.panel1.TabIndex = 1;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(13, 149);
            this.button5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(247, 28);
            this.button5.TabIndex = 4;
            this.button5.Tag = "list_of_reservation";
            this.button5.Text = "Список бронирований";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.Select_Mode);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(11, 112);
            this.button4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(249, 28);
            this.button4.TabIndex = 3;
            this.button4.Tag = "add_user";
            this.button4.Text = "Добавить человека";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.Select_Mode);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 75);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(249, 28);
            this.button3.TabIndex = 2;
            this.button3.Tag = "populate";
            this.button3.Text = "Заселить";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Select_Mode);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(11, 4);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(249, 28);
            this.button2.TabIndex = 1;
            this.button2.Tag = "list_of_rooms";
            this.button2.Text = "Список комнат";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Select_Mode);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(11, 39);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(249, 28);
            this.button1.TabIndex = 0;
            this.button1.Tag = "list_of_peoples";
            this.button1.Text = "Список людей";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Select_Mode);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Tan;
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Location = new System.Drawing.Point(279, 15);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(267, 197);
            this.panel2.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Tan;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(4, 47);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 28);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.panel8);
            this.panel3.Controls.Add(this.panel7);
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Location = new System.Drawing.Point(555, 15);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(903, 780);
            this.panel3.TabIndex = 3;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.panel4.Controls.Add(this.dateTimePicker2);
            this.panel4.Controls.Add(this.textBox7);
            this.panel4.Controls.Add(this.checkBox1);
            this.panel4.Controls.Add(this.dateTimePicker1);
            this.panel4.Controls.Add(this.textBox6);
            this.panel4.Controls.Add(this.textBox5);
            this.panel4.Controls.Add(this.textBox4);
            this.panel4.Controls.Add(this.textBox3);
            this.panel4.Controls.Add(this.textBox2);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Location = new System.Drawing.Point(516, 4);
            this.panel4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(143, 758);
            this.panel4.TabIndex = 2;
            this.panel4.Tag = "add_user";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(5, 427);
            this.dateTimePicker2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(132, 22);
            this.dateTimePicker2.TabIndex = 16;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(5, 395);
            this.textBox7.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(132, 22);
            this.textBox7.TabIndex = 15;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(5, 366);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(95, 20);
            this.checkBox1.TabIndex = 14;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(5, 332);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(132, 22);
            this.dateTimePicker1.TabIndex = 13;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(5, 299);
            this.textBox6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(132, 22);
            this.textBox6.TabIndex = 12;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(5, 266);
            this.textBox5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(132, 22);
            this.textBox5.TabIndex = 11;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(5, 233);
            this.textBox4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(132, 22);
            this.textBox4.TabIndex = 10;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(5, 199);
            this.textBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(132, 22);
            this.textBox3.TabIndex = 9;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(5, 166);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(132, 22);
            this.textBox2.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 145);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(87, 16);
            this.label8.TabIndex = 7;
            this.label8.Text = "Резервация";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(48, 124);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 16);
            this.label7.TabIndex = 6;
            this.label7.Text = "Дети";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 101);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 16);
            this.label6.TabIndex = 5;
            this.label6.Text = "Дата рождения";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 82);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "Предпочтения";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(45, 62);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Почта";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 41);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Телефон";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "ФИО";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 25);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Паспорт";
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.panel8.Location = new System.Drawing.Point(667, 4);
            this.panel8.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(205, 758);
            this.panel8.TabIndex = 6;
            this.panel8.Tag = "list_of_reservation";
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.panel7.Location = new System.Drawing.Point(337, 4);
            this.panel7.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(171, 758);
            this.panel7.TabIndex = 5;
            this.panel7.Tag = "populate";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panel6.Controls.Add(this.dataGridView1);
            this.panel6.Location = new System.Drawing.Point(168, 4);
            this.panel6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(161, 758);
            this.panel6.TabIndex = 4;
            this.panel6.Tag = "list_of_peoples";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Location = new System.Drawing.Point(5, 9);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(152, 185);
            this.dataGridView1.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.просмотретьДетейToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(215, 28);
            // 
            // просмотретьДетейToolStripMenuItem
            // 
            this.просмотретьДетейToolStripMenuItem.Name = "просмотретьДетейToolStripMenuItem";
            this.просмотретьДетейToolStripMenuItem.Size = new System.Drawing.Size(214, 24);
            this.просмотретьДетейToolStripMenuItem.Text = "Просмотреть детей";
            this.просмотретьДетейToolStripMenuItem.Click += new System.EventHandler(this.Find_Children);
            // 
            // panel5
            // 
            this.panel5.AutoScroll = true;
            this.panel5.BackColor = System.Drawing.Color.Gray;
            this.panel5.Location = new System.Drawing.Point(4, 4);
            this.panel5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(156, 758);
            this.panel5.TabIndex = 3;
            this.panel5.Tag = "list_of_rooms";
            // 
            // filter_panel
            // 
            this.filter_panel.Controls.Add(this.panel13);
            this.filter_panel.Controls.Add(this.panel12);
            this.filter_panel.Controls.Add(this.panel11);
            this.filter_panel.Controls.Add(this.filter_rooms);
            this.filter_panel.Location = new System.Drawing.Point(4, 219);
            this.filter_panel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.filter_panel.Name = "filter_panel";
            this.filter_panel.Size = new System.Drawing.Size(541, 576);
            this.filter_panel.TabIndex = 5;
            this.filter_panel.Tag = "filter";
            // 
            // panel13
            // 
            this.panel13.Location = new System.Drawing.Point(269, 265);
            this.panel13.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(263, 293);
            this.panel13.TabIndex = 2;
            // 
            // panel12
            // 
            this.panel12.Location = new System.Drawing.Point(13, 287);
            this.panel12.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(248, 271);
            this.panel12.TabIndex = 1;
            // 
            // panel11
            // 
            this.panel11.Location = new System.Drawing.Point(275, 5);
            this.panel11.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(263, 252);
            this.panel11.TabIndex = 1;
            // 
            // filter_rooms
            // 
            this.filter_rooms.Controls.Add(this.button6);
            this.filter_rooms.Controls.Add(this.checkedListBox2);
            this.filter_rooms.Controls.Add(this.checkedListBox1);
            this.filter_rooms.Controls.Add(this.label11);
            this.filter_rooms.Controls.Add(this.label10);
            this.filter_rooms.Controls.Add(this.label9);
            this.filter_rooms.Controls.Add(this.trackBar1);
            this.filter_rooms.Location = new System.Drawing.Point(13, 5);
            this.filter_rooms.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.filter_rooms.Name = "filter_rooms";
            this.filter_rooms.Size = new System.Drawing.Size(248, 274);
            this.filter_rooms.TabIndex = 0;
            this.filter_rooms.Tag = "list_of_rooms";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(11, 250);
            this.button6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(233, 21);
            this.button6.TabIndex = 7;
            this.button6.Text = "button6";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // checkedListBox2
            // 
            this.checkedListBox2.FormattingEnabled = true;
            this.checkedListBox2.Items.AddRange(new object[] {
            "Убрать",
            "Ремонт",
            "Готово",
            "Ремонт+Уборка"});
            this.checkedListBox2.Location = new System.Drawing.Point(11, 98);
            this.checkedListBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkedListBox2.Name = "checkedListBox2";
            this.checkedListBox2.Size = new System.Drawing.Size(159, 72);
            this.checkedListBox2.TabIndex = 6;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "Проживают",
            "Не проживают"});
            this.checkedListBox1.Location = new System.Drawing.Point(8, 201);
            this.checkedListBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(159, 38);
            this.checkedListBox1.TabIndex = 5;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 181);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(152, 16);
            this.label11.TabIndex = 4;
            this.label11.Text = "Проживают в комнате";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(4, 79);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(202, 16);
            this.label10.TabIndex = 3;
            this.label10.Text = "Выберите состояние комнаты";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 2);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(168, 16);
            this.label9.TabIndex = 1;
            this.label9.Text = "Выберите кол-во комнат";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(11, 22);
            this.trackBar1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.trackBar1.Maximum = 4;
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(43, 56);
            this.trackBar1.TabIndex = 0;
            this.trackBar1.Value = 1;
            // 
            // mySqlCommand1
            // 
            this.mySqlCommand1.CacheAge = 0;
            this.mySqlCommand1.Connection = null;
            this.mySqlCommand1.EnableCaching = false;
            this.mySqlCommand1.Transaction = null;
            // 
            // Admin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(253)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1473, 810);
            this.Controls.Add(this.filter_panel);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Admin";
            this.Tag = "";
            this.Text = "Admin";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Admin_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.filter_panel.ResumeLayout(false);
            this.filter_rooms.ResumeLayout(false);
            this.filter_rooms.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem просмотретьДетейToolStripMenuItem;
        private System.Windows.Forms.Panel filter_panel;
        private System.Windows.Forms.Panel filter_rooms;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private MySql.Data.MySqlClient.MySqlCommand mySqlCommand1;
        private System.Windows.Forms.CheckedListBox checkedListBox2;
        private System.Windows.Forms.Button button6;
    }
}