using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using FrbaHotel.Listado_Funcionalidades;

namespace FrbaHotel.Facturar_Estadia
{
    public partial class FacturarEstadia : Form
    {
        // Genera conexion con la base de datos
        System.Data.SqlClient.SqlConnection connection;
        private SqlCommand command;
        private SqlDataAdapter adapter;
        private DataTable dataTable;

        
        public FacturarEstadia()
        {
            InitializeComponent();

            comboBoxFormaPago.Items.Add("Efectivo");
            comboBoxFormaPago.Items.Add("Tarjera de Credito");

        }

        private void FacturarEstadia_Load(object sender, EventArgs e)
        {
            iniciarConexion();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Funcionalidades funcionalidadesForm = new Funcionalidades();
            funcionalidadesForm.StartPosition = FormStartPosition.CenterScreen;
            funcionalidadesForm.Show();
        }
        private void iniciarConexion()
        {
            connection = new System.Data.SqlClient.SqlConnection();
            try
            {
                connection.ConnectionString = Variables.connectionStr;
                connection.Open();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc);
            }
        }

    }
}
