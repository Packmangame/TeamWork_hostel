using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Checks
{
    public class Checks
    {
        //Проверка email
        static public bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            // Регулярное выражение для проверки формата email
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }
    }

    //Методы Регистрации
    public class HelpRegister
    {
        //Создание пароля за место пользователя
        static public string GeneratePassword(int length, bool useDigits, bool useSymbols)
        {
            const string lower = "abcdefghijklmnopqrstuvwxyz";
            const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numbers = "0123456789";
            const string symbols = "!@#$%^&*()_+[]{}|;:,.<>?";

            string charPool = lower + upper;

            if (useDigits) charPool += numbers;
            if (useSymbols) charPool += symbols;

            var random = new Random();
            return new string(Enumerable.Repeat(charPool, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        //2факторная аутентификация
        static public async Task SendSecretKeyToEmailAsync(string email)
        {
            
            try
            {
                string smtpServer = "smtp.mail.ru";
                int smtpPort = 587; 
                string senderEmail = "a.ploskikh@list.ru"; 
                string senderPassword = "dRusv8QTzTAkxHWLZBMp";
                string secretKey = GeneratePassword(6,true,true);
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
