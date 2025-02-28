using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

      
        private async void Send_password(object sender, EventArgs e)
        {
            try
            {
                await HelpRegister.SendSecretKeyToEmailAsync("al.ploskikh@list.ru"); 

                MessageBox.Show("Секретный ключ успешно отправлен на почту.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SmtpException ex)
            {
                // Обработка ошибок SMTP
                MessageBox.Show($"Ошибка SMTP: {ex.Message}. Код состояния: {ex.StatusCode}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Обработка остальных ошибок
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
