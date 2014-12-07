using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrbaHotel.ABM_de_Cliente
{
    public partial class ClienteBusqueda : Form
    {
        public ClienteBusqueda()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Cliente pantalla_cliente = new Cliente();
            pantalla_cliente.StartPosition = FormStartPosition.CenterScreen;
            pantalla_cliente.Show();
        }

        private void ClienteBusqueda_Load(object sender, EventArgs e)
        {

        }
    }
}
