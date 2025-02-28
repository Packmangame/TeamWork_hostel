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

        //Проверка пароля на валидность
        static public string IsValidPassword(string password)
        {
            string SpecialSymbols = @"@$#!%^&*()-_=+[]{}\|:;',.<>/?";
            bool hasSpec = false;

            //long less 8
            if (password.Length < 8)
                return "Ваш пароль должен быть больше 8 символов";

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
                return "Ваш пароль должен содержать специальные сиволы";
            }

            //have number
            if (!password.Any(char.IsDigit))
                return "Ваш пароль должег содержать цифры";
            if (password == "Введите пароль")
                return "Введиете пароль";

            //have Upper
            if (!password.Any(char.IsUpper))
                return "Ваш пароль должен содердать сиволы с верхним регистром";

            //have Lower
            if (!password.Any(char.IsLower))
                return "Ваш пароль должен содердать сиволы с нижним регистром";

            string[] commonPasswords = { "password", "123456", "qwerty", "abc123", "admin" };
            if (commonPasswords.Any(common => password.IndexOf(common, StringComparison.OrdinalIgnoreCase) >= 0))
                return "Ваш пароль слишком легкий";

            //all is good
            return "";

        }

        //Проверка совпадения пароля
        static public bool Matching_passwords(string password)
        {
            return true;
        }

        //Проверка совпадения Логина
        static public bool Matching_login(string password)
        {
            return true;
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
        
    }

    

}
