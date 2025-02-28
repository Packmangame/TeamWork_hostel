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
        //Проверкак кода аутентификации с почты
        static public bool IsValidSecretpassword(string code,string usercode)
        { 
            if (string.IsNullOrEmpty(code))
                return false;

            return code.Equals(usercode, StringComparison.Ordinal);
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
    }

}
