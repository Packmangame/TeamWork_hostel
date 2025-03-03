using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Checks;
using MySql.Data.MySqlClient;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private MySqlConnection connection;

        private string secretcode = "";
        public Form1()
        {
            InitializeComponent();
            InitializeDatabaseConnection();
        }


        private void InitializeDatabaseConnection()
        {
            string connectionString = "server=127.0.0.1;user=root;password=1234;database=Hostel";
            connection = new MySqlConnection(connectionString);
        }


        private void Form1_Load(object sender, EventArgs e)
        {

           
            panel1.Location = new System.Drawing.Point(
                (this.ClientSize.Width - panel1.Width) / 2,
                (this.ClientSize.Height - panel1.Height) / 2
            );

            textBox1.Location = new System.Drawing.Point(
               (panel1.Width - textBox1.Width) / 2,
               (panel1.Height - textBox1.Height) / 2
            );
            textBox2.Location = new System.Drawing.Point(
              (panel1.Width - textBox2.Width) / 2,
              (panel1.Height - textBox2.Height) / 2 + 30
            );
            button1.Location = new System.Drawing.Point(
              (panel1.Width - button1.Width) / 2,
              (panel1.Height - button1.Height) / 2 + 80
            );

            Panel panel2 = new Panel
            {
                Location = new Point((panel1.Width - textBox1.Width) / 2 - 1,
                (panel1.Height - textBox1.Height) / 2 - 1),
                Name = "panel 2",
                Size = new Size(
                    (textBox1.Width + 2),
                    (textBox1.Height + 2)
                ),
                BackColor = Color.White
            };

            Panel panel3 = new Panel
            {
                Location = new Point((panel1.Width - textBox2.Width) / 2 - 1,
            (panel1.Height - textBox2.Height) / 2 - 1 + 30),
                Name = "panel3",
                Size = new Size(
            (textBox2.Width + 2),
            (textBox2.Height + 2)
        ),
                BackColor = Color.White
            };

            Label labelss = new Label
            {
                Location = new Point((panel1.Width - textBox2.Width) / 2,
                    (panel1.Height - button1.Height) / 2 + 65),
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
            string error = Checks.Checks.IsValidPassword(textBox2.Text); 

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
            }

            Admin adminForm = new Admin();
            adminForm.Show();
            /*this.Hide();*/


        }

        
    }

}

