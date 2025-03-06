using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Checks;
using System.Drawing.Drawing2D;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {




        private MySqlConnection connection;

        Sql_Requests_Enter enter = new Sql_Requests_Enter( );

        Admin admin = new Admin( );
        Builder builder = new Builder( );
        Caretaker caretaker = new Caretaker( );
        Storekeeper storekeeper = new Storekeeper( );
        SysAdmin sysAdmin = new SysAdmin( );
        Washer washer = new Washer( );

        private string secretcode = "";
        public Form1()
        {
            InitializeComponent();
            InitializeDatabaseConnection();
        }


        private void InitializeDatabaseConnection()
        {
            string connectionString = "server=0.0.0.0;user=root;password=;database=Hostel";
            connection = new MySqlConnection(connectionString);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
           
            panel1.Location = new System.Drawing.Point(
                (this.ClientSize.Width - panel1.Width) / 2,
                (this.ClientSize.Height - panel1.Height) / 2
            );
            LoginLine.Location = new System.Drawing.Point(
               (panel1.Width - LoginLine.Width) / 2,
               (panel1.Height - LoginLine.Height) / 2
            );
            PassLine.Location = new System.Drawing.Point(
              (panel1.Width - PassLine.Width) / 2,
              (panel1.Height - PassLine.Height) / 2 + 30
            );
            Enter.Location = new System.Drawing.Point(
              (panel1.Width - Enter.Width) / 2,
              (panel1.Height - Enter.Height) / 2 + 80
            );

            Panel panel2 = new Panel
            {
                Location = new Point((panel1.Width - LoginLine.Width) / 2 - 1,
                (panel1.Height - LoginLine.Height) / 2 - 1),
                Name = "panel 2",
                Size = new Size(
                    (LoginLine.Width + 2),
                    (LoginLine.Height + 2)
                ),
                BackColor = Color.White
            };

            Panel panel3 = new Panel
            {
                Location = new Point((panel1.Width - PassLine.Width) / 2 - 1,
            (panel1.Height - PassLine.Height) / 2 - 1 + 30),
                Name = "panel3",
                Size = new Size(
            (PassLine.Width + 2),
            (PassLine.Height + 2)
               ),
                BackColor = Color.White
            };

            Label labelss = new Label
            {
                Location = new Point((panel1.Width - PassLine.Width) / 2,
                    (panel1.Height - Enter.Height) / 2 + 65),
                Name = "labelss",
                Size = new Size(
                    (panel3.Width),
                    (panel3.Height)
                ),

                Text = ""
            };
            panel1.Controls.Add(labelss);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(panel3);

            Enter.BackColor= Color.FromArgb(0xA3, 0xD6, 0xB8);


        }



        private async void Send_password(object sender, EventArgs e)
        {
            try
            {
                await SendSecretKeyToEmailAsync("al.ploskikh@list.ru");
                MessageBox.Show("Секретный ключ успешно отправлен на почту.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SmtpException ex)
            {
                MessageBox.Show($"Ошибка SMTP: {ex.Message}. Код состояния: {ex.StatusCode}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Он должен быть тут потому тчо если его засунуть в библиотеку то он перестает работать 
        public async Task SendSecretKeyToEmailAsync(string email)
        {

            try
            {
                string smtpServer = "smtp.mail.ru";
                int smtpPort = 587;
                string senderEmail = "a.ploskikh@list.ru";
                string senderPassword = "dRusv8QTzTAkxHWLZBMp";
                string secretKey = HelpRegister.GeneratePassword(10, true, true);
                secretcode = secretKey;
                // Создание сообщения
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(senderEmail);
                mail.To.Add(email);
                mail.Subject = "Секретный ключ для двухфакторной аутентификации";
                mail.Body = $"Ваш секретный ключ: {secretKey}\n" +
                            $"Вы можете импортировать его в приложение для аутентификации.";

                // Настройка SMTP-клиента
                SmtpClient smtpClient = new SmtpClient(smtpServer)
                {
                    Port = smtpPort,
                    Credentials = new NetworkCredential(senderEmail, senderPassword),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Timeout = 10000
                };

                // Асинхронная отправка письма
                await smtpClient.SendMailAsync(mail);
            }
            catch (SmtpException ex)
            {

                Console.WriteLine($"Ошибка SMTP: {ex.Message}. Код состояния: {ex.StatusCode}");
                throw; // Переброс исключения дальше
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
                throw; // Переброс исключения дальше
            }
        }

        private void Text_Fading(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && textBox.Text == textBox.Tag?.ToString())
            {
                if (textBox.Tag == null)
                {
                    textBox.Tag = textBox.Text;
                }
                textBox.Text = string.Empty;
                textBox.ForeColor = Color.Black;
            }
        }

        private void Text_Return(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text) && textBox.Tag != null)
            {
                textBox.Text = textBox.Tag.ToString();
                textBox.ForeColor = Color.Black;
            }
        }

        private void Entrance(object sender, EventArgs e)
        {
            //Код для сиса
            /* string error = Checks.Checks.IsValidPassword(textBox2.Text); 


             if (!string.IsNullOrEmpty(error)) 
             {
                 foreach (Control control in panel1.Controls)
                 {
                     if (control is Panel panel && panel.Name == "panel3") 
                     {
                         panel.BackColor = Color.Red; 
                         break;
                     }
                 }

                 foreach (Control control in panel1.Controls)
                 {
                     if (control is Label label && label.Name == "labelss") 
                     {
                         label.Text = error;
                         button1.Top += (label.Height + 10);
                         label.ForeColor = Color.Red; 
                         break;
                     }
                 }
             }
             else
             {
                 foreach (Control control in panel1.Controls)
                 {
                     if (control is Panel panel && panel.Name == "panel3") 
                     {
                         panel.BackColor = Color.White; 
                         break;
                     }
                 }

                 foreach (Control control in panel1.Controls)
                 {
                     if (control is Label label && label.Name == "labelss") 
                     {
                         label.Text = ""; 
                         break;
                     }
                 }
             }*/

            if (LoginLine.Text != "Введите Логин" && PassLine.Text != "Введите Пароль")
            {
                switch (enter.Enter(LoginLine.Text, PassLine.Text))
                {
                    case "1":
                    {
                        admin.Show( );
                        break;
                    }
                    case "2":
                    {
                        caretaker.Show( );
                        break;
                    }
                    case "3":
                    {
                        storekeeper.Show( );
                        break;
                    }
                    case "4":
                    {
                        sysAdmin.Show( );
                        break;
                    }
                    case "5":
                    {
                        washer.Show( );
                        break;
                    }
                    case "6":
                    {
                        builder.Show( );
                        break;
                    }
                    default:
                    {
                        MessageBox.Show("Пользователь не найден, проверьте данные");
                        break;
                    }
                }

            }
            else
            {
                MessageBox.Show("Имеются пустые поля");
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click (object sender, EventArgs e)
        {
            Caretaker ct = new Caretaker( );
            ct.Show( );
        }

        private void button1_Click (object sender, EventArgs e)
        {
            Admin ct = new Admin( );
            ct.Show( );
        }

        private void button3_Click (object sender, EventArgs e)
        {
            SysAdmin s = new SysAdmin( );
            s.Show( );
        }
    }

}

