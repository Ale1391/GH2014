using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrbaHotel.Listado_Funcionalidades;
using FrbaHotel.Login;

namespace FrbaHotel
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //OMITIR
            Variables.tipo_usuario = "Guest";
            this.Hide();
            Funcionalidades func = new Funcionalidades();
            func.StartPosition = FormStartPosition.CenterScreen;
            func.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //USUARIO
            this.Hide();
            LoginForm login = new LoginForm();
            login.StartPosition = FormStartPosition.CenterScreen;
            login.Show();
        }
    }
}
