using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 :Form
    {
        public string Login { get => txtLogin.Text; set => txtLogin.Text = value; }
        public string Password { get => txtPassword.Text; set => txtPassword.Text = value; }
        public string FIO { get => txtFIO.Text; set => txtFIO.Text = value; }
        public string Role { get => comboBoxRole.SelectedItem.ToString( ); set => comboBoxRole.SelectedText = value; }

        public Form2 ()
        {
            InitializeComponent( );
        }

        private void btnSave_Click (object sender, EventArgs e)
        {
            Login = txtLogin.Text;
            Password = txtPassword.Text;
            FIO = txtFIO.Text;
            //Role = comboBoxRole.SelectedIndex + 1; // Индекс ComboBox

            this.DialogResult = DialogResult.OK;
            Close( );
        }
    }
}
