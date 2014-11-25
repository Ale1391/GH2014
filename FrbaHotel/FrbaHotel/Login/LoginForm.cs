using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrbaHotel.Login
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            this.Location = new Point(100, 100);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Contraseña incorrecta");
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Principal prinForm = new Principal();
            prinForm.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
