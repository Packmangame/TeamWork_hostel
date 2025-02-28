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


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        public Form1()
        {
           /* MessageBox.Show("Hello World!");*/
            InitializeComponent();
        }

        public string secretcode="";



        private void Form1_Load(object sender, EventArgs e)
        {
           
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
                string secretKey = HelpRegister.GeneratePassword(10,true,true);
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
    }
}
