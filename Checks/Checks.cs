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

        //Проверка на правильность вводимого пароля
        static public string IsValidPassword (string password)
        {
            string SpecialSymbols = @"@$#!%^&*()-_=+[]{}\|:;',.<>/?";
            bool hasSpec = false;

            //long less 8
            if (password.Length < 8)
                return "long";

            //have special symbols
            foreach (char let in password)
            {
                if (SpecialSymbols.Contains(let))
                {
                    hasSpec = true;
                    break;
                }
            }
            if (hasSpec == false)
            {
                return "symb";
            }

            //have number
            if (!password.Any(char.IsDigit))
                return "num";

            //have Upper
            if (!password.Any(char.IsUpper))
                return "up";

            //have Lower
            if (!password.Any(char.IsLower))
                return "low";

            string [ ] commonPasswords = { "password", "123456", "qwerty", "abc123", "admin" };
            if (commonPasswords.Any(common => password.IndexOf(common, StringComparison.OrdinalIgnoreCase) >= 0))
                return "com";

            //all is good
            return "";

        }
    }

    

}
