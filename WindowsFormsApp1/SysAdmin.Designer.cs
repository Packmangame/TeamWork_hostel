namespace WindowsFormsApp1
{
    partial class SysAdmin
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.LogAdd = new System.Windows.Forms.TextBox();
            this.PassAdd = new System.Windows.Forms.TextBox();
            this.FIOAdd = new System.Windows.Forms.TextBox();
            this.ComboAdd = new System.Windows.Forms.ComboBox();
            this.AddNew = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.AddNewPanel = new System.Windows.Forms.Panel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.изменитьЗаписьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьЗаписьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.AddNewPanel.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(214)))), ((int)(((byte)(184)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Right;
            this.dataGridView1.Location = new System.Drawing.Point(407, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(660, 554);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // LogAdd
            // 
            this.LogAdd.Location = new System.Drawing.Point(44, 32);
            this.LogAdd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.LogAdd.Name = "LogAdd";
            this.LogAdd.Size = new System.Drawing.Size(132, 22);
            this.LogAdd.TabIndex = 1;
            // 
            // PassAdd
            // 
            this.PassAdd.Location = new System.Drawing.Point(44, 96);
            this.PassAdd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PassAdd.Name = "PassAdd";
            this.PassAdd.Size = new System.Drawing.Size(132, 22);
            this.PassAdd.TabIndex = 2;
            // 
            // FIOAdd
            // 
            this.FIOAdd.Location = new System.Drawing.Point(44, 160);
            this.FIOAdd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FIOAdd.Name = "FIOAdd";
            this.FIOAdd.Size = new System.Drawing.Size(132, 22);
            this.FIOAdd.TabIndex = 3;
            // 
            // ComboAdd
            // 
            this.ComboAdd.DisplayMember = "Роль";
            this.ComboAdd.FormattingEnabled = true;
            this.ComboAdd.Items.AddRange(new object[] {
            "Администратор",
            "Завхоз",
            "Кладовщик",
            "Сис. Администратор",
            "Уборщик",
            "Строитель"});
            this.ComboAdd.Location = new System.Drawing.Point(44, 226);
            this.ComboAdd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ComboAdd.Name = "ComboAdd";
            this.ComboAdd.Size = new System.Drawing.Size(132, 24);
            this.ComboAdd.TabIndex = 4;
            // 
            // AddNew
            // 
            this.AddNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(214)))), ((int)(((byte)(184)))));
            this.AddNew.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(208)))), ((int)(((byte)(152)))));
            this.AddNew.FlatAppearance.BorderSize = 2;
            this.AddNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddNew.Location = new System.Drawing.Point(63, 298);
            this.AddNew.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.AddNew.Name = "AddNew";
            this.AddNew.Size = new System.Drawing.Size(100, 28);
            this.AddNew.TabIndex = 5;
            this.AddNew.Text = "Добавить";
            this.AddNew.UseVisualStyleBackColor = false;
            this.AddNew.Click += new System.EventHandler(this.AddNew_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Логин";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 76);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Пароль";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 140);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "ФИО";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 207);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "Роль";
            // 
            // AddNewPanel
            // 
            this.AddNewPanel.Controls.Add(this.LogAdd);
            this.AddNewPanel.Controls.Add(this.label4);
            this.AddNewPanel.Controls.Add(this.PassAdd);
            this.AddNewPanel.Controls.Add(this.label3);
            this.AddNewPanel.Controls.Add(this.FIOAdd);
            this.AddNewPanel.Controls.Add(this.label2);
            this.AddNewPanel.Controls.Add(this.ComboAdd);
            this.AddNewPanel.Controls.Add(this.label1);
            this.AddNewPanel.Controls.Add(this.AddNew);
            this.AddNewPanel.Location = new System.Drawing.Point(16, 15);
            this.AddNewPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.AddNewPanel.Name = "AddNewPanel";
            this.AddNewPanel.Size = new System.Drawing.Size(367, 524);
            this.AddNewPanel.TabIndex = 10;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.изменитьЗаписьToolStripMenuItem,
            this.удалитьЗаписьToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(200, 52);
            // 
            // изменитьЗаписьToolStripMenuItem
            // 
            this.изменитьЗаписьToolStripMenuItem.Name = "изменитьЗаписьToolStripMenuItem";
            this.изменитьЗаписьToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.изменитьЗаписьToolStripMenuItem.Text = "Изменить запись";
            // 
            // удалитьЗаписьToolStripMenuItem
            // 
            this.удалитьЗаписьToolStripMenuItem.Name = "удалитьЗаписьToolStripMenuItem";
            this.удалитьЗаписьToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.удалитьЗаписьToolStripMenuItem.Text = "Удалить запись";
            // 
            // SysAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(253)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.AddNewPanel);
            this.Controls.Add(this.dataGridView1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "SysAdmin";
            this.Text = "SysAdmin";
            this.Load += new System.EventHandler(this.SysAdmin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.AddNewPanel.ResumeLayout(false);
            this.AddNewPanel.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox LogAdd;
        private System.Windows.Forms.TextBox PassAdd;
        private System.Windows.Forms.TextBox FIOAdd;
        private System.Windows.Forms.ComboBox ComboAdd;
        private System.Windows.Forms.Button AddNew;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel AddNewPanel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem изменитьЗаписьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьЗаписьToolStripMenuItem;
    }
}