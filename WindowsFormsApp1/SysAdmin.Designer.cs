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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.AddNewPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Right;
            this.dataGridView1.Location = new System.Drawing.Point(305, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(495, 450);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // LogAdd
            // 
            this.LogAdd.Location = new System.Drawing.Point(33, 26);
            this.LogAdd.Name = "LogAdd";
            this.LogAdd.Size = new System.Drawing.Size(100, 20);
            this.LogAdd.TabIndex = 1;
            // 
            // PassAdd
            // 
            this.PassAdd.Location = new System.Drawing.Point(33, 78);
            this.PassAdd.Name = "PassAdd";
            this.PassAdd.Size = new System.Drawing.Size(100, 20);
            this.PassAdd.TabIndex = 2;
            // 
            // FIOAdd
            // 
            this.FIOAdd.Location = new System.Drawing.Point(33, 130);
            this.FIOAdd.Name = "FIOAdd";
            this.FIOAdd.Size = new System.Drawing.Size(100, 20);
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
            this.ComboAdd.Location = new System.Drawing.Point(33, 184);
            this.ComboAdd.Name = "ComboAdd";
            this.ComboAdd.Size = new System.Drawing.Size(100, 21);
            this.ComboAdd.TabIndex = 4;
            // 
            // AddNew
            // 
            this.AddNew.Location = new System.Drawing.Point(47, 242);
            this.AddNew.Name = "AddNew";
            this.AddNew.Size = new System.Drawing.Size(75, 23);
            this.AddNew.TabIndex = 5;
            this.AddNew.Text = "Добавить";
            this.AddNew.UseVisualStyleBackColor = true;
            this.AddNew.Click += new System.EventHandler(this.AddNew_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Логин";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Пароль";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "ФИО";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
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
            this.AddNewPanel.Location = new System.Drawing.Point(12, 12);
            this.AddNewPanel.Name = "AddNewPanel";
            this.AddNewPanel.Size = new System.Drawing.Size(275, 426);
            this.AddNewPanel.TabIndex = 10;
            // 
            // SysAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(253)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.AddNewPanel);
            this.Controls.Add(this.dataGridView1);
            this.Name = "SysAdmin";
            this.Text = "SysAdmin";
            this.Load += new System.EventHandler(this.SysAdmin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.AddNewPanel.ResumeLayout(false);
            this.AddNewPanel.PerformLayout();
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
    }
}