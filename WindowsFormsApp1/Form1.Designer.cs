namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.LogIn = new System.Windows.Forms.Label();
            this.Enter = new System.Windows.Forms.Button();
            this.PassLine = new System.Windows.Forms.TextBox();
            this.LoginLine = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Tan;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.LogIn);
            this.panel1.Controls.Add(this.Enter);
            this.panel1.Controls.Add(this.PassLine);
            this.panel1.Controls.Add(this.LoginLine);
            this.panel1.Location = new System.Drawing.Point(177, 68);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 261);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // LogIn
            // 
            this.LogIn.AutoSize = true;
            this.LogIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LogIn.Location = new System.Drawing.Point(74, 21);
            this.LogIn.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LogIn.Name = "LogIn";
            this.LogIn.Size = new System.Drawing.Size(63, 26);
            this.LogIn.TabIndex = 3;
            this.LogIn.Text = "Вход";
            // 
            // Enter
            // 
            this.Enter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Enter.Location = new System.Drawing.Point(73, 187);
            this.Enter.Name = "Enter";
            this.Enter.Size = new System.Drawing.Size(95, 30);
            this.Enter.TabIndex = 2;
            this.Enter.Text = "Войти";
            this.Enter.UseVisualStyleBackColor = true;
            this.Enter.Click += new System.EventHandler(this.Entrance);
            // 
            // PassLine
            // 
            this.PassLine.BackColor = System.Drawing.Color.WhiteSmoke;
            this.PassLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PassLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PassLine.Location = new System.Drawing.Point(13, 127);
            this.PassLine.Name = "PassLine";
            this.PassLine.Size = new System.Drawing.Size(217, 23);
            this.PassLine.TabIndex = 1;
            this.PassLine.Tag = "Введите Пароль";
            this.PassLine.Text = "Введите Пароль";
            this.PassLine.MouseEnter += new System.EventHandler(this.Text_Fading);
            this.PassLine.MouseLeave += new System.EventHandler(this.Text_Return);
            // 
            // LoginLine
            // 
            this.LoginLine.BackColor = System.Drawing.Color.WhiteSmoke;
            this.LoginLine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LoginLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LoginLine.Location = new System.Drawing.Point(13, 74);
            this.LoginLine.Name = "LoginLine";
            this.LoginLine.Size = new System.Drawing.Size(217, 23);
            this.LoginLine.TabIndex = 0;
            this.LoginLine.Tag = "Введите Логин";
            this.LoginLine.Text = "Введите Логин";
            this.LoginLine.MouseEnter += new System.EventHandler(this.Text_Fading);
            this.LoginLine.MouseLeave += new System.EventHandler(this.Text_Return);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(498, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Caretaker";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(498, 52);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Admin";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Moccasin;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Путь в Рай";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox LoginLine;
        private System.Windows.Forms.TextBox PassLine;
        private System.Windows.Forms.Button Enter;
        private System.Windows.Forms.Label LogIn;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}

