using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form1 form1 = new Form1();
            Application.Run(form1);

           /* if (form1.Visible == false)
            {
                foreach (Form form in Application.OpenForms)
                {
                    if (form is Admin adminForm)
                    {
                        Application.Run(adminForm);
                        form1.Close();
                        break;
                    }
                }
            }*/
        }


    }
}
