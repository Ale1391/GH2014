using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrbaHotel.Listado_Funcionalidades;

namespace FrbaHotel.ABM_de_Cliente
{
    public partial class Cliente : Form
    {

        public Cliente()
        {
            InitializeComponent();
            comboBoxCliente.Items.Add("Editar Cliente Existente");
            comboBoxCliente.Items.Add("Crear Cliente Nuevo");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Funcionalidades funcionalidadesForm = new Funcionalidades();
            funcionalidadesForm.StartPosition = FormStartPosition.CenterScreen;
            funcionalidadesForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBoxCliente.Text.Length == 0)
            {
                MessageBox.Show("Primero debes elegir una opción.");
            }
            else if (comboBoxCliente.Text == "Crear Cliente Nuevo")
            {
                this.Hide();
                ClienteForm cliente_form = new ClienteForm();
                cliente_form.mail = "";
                cliente_form.StartPosition = FormStartPosition.CenterScreen;
                cliente_form.ShowDialog();
            }
            else if (comboBoxCliente.Text == "Editar Cliente Existente")
            {
                this.Hide();
                ClienteBusqueda busqueda_rol = new ClienteBusqueda();
                busqueda_rol.StartPosition = FormStartPosition.CenterScreen;
                busqueda_rol.ShowDialog();
            }
        }

        private void Rol_Load(object sender, EventArgs e)
        {
            
        }
    }
}
