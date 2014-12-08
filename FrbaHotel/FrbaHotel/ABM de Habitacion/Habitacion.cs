using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrbaHotel.Listado_Funcionalidades;
using System.Data.SqlClient;

namespace FrbaHotel.ABM_de_Habitacion
{
    public partial class Habitacion : Form
    {
        System.Data.SqlClient.SqlConnection connection;
        private SqlCommand command;
        private SqlDataAdapter adapter;
        private DataTable dataTable;

        public Habitacion()
        {
            InitializeComponent();
            comboBoxHabitacion.Items.Add("Editar Habitacion Existente");
            comboBoxHabitacion.Items.Add("Crear Habitacion Nuevo");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Funcionalidades funcionalidadesForm = new Funcionalidades();
            funcionalidadesForm.StartPosition = FormStartPosition.CenterScreen;
            funcionalidadesForm.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxHabitacion.Text == "Editar Habitacion Existente")
            {
                textBoxNumeroHabitacion.Enabled = true;
            }
            else if (comboBoxHabitacion.Text == "Crear Habitacion Nuevo")
            {
                textBoxNumeroHabitacion.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBoxHabitacion.Text.Length == 0)
            {
                MessageBox.Show("Primero debes elegir una opción.");
            }
            else if (comboBoxHabitacion.Text == "Editar Habitacion Existente" && textBoxNumeroHabitacion.Text.Length == 0)
            {
                MessageBox.Show("Primero debes ingresar una habitación valida.");
            }
            else if (comboBoxHabitacion.Text == "Crear Habitacion Nuevo")
            {
                this.Hide();
                HabitacionForm abm_habitacion = new HabitacionForm();
                abm_habitacion.numero_habitacion = "";
                abm_habitacion.StartPosition = FormStartPosition.CenterScreen;
                abm_habitacion.ShowDialog();
            }
            else
            {
                try
                {
                    string query = "select * from GITAR_HEROES.Habitacion where numero = "+textBoxNumeroHabitacion.Text+" and codigo_hotel = "+Variables.hotel_id;
                    command = new SqlCommand(query);
                    command.Connection = connection;
                    adapter = new SqlDataAdapter(command);
                    dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        this.Hide();
                        HabitacionForm abm_habitacion = new HabitacionForm();
                        abm_habitacion.numero_habitacion = textBoxNumeroHabitacion.Text;
                        abm_habitacion.StartPosition = FormStartPosition.CenterScreen;
                        abm_habitacion.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Error: No existe ninguna habitacion con ese número en el hotel.");
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Error: " + exc);
                }

                
            }
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

        private void Usuario_Load(object sender, EventArgs e)
        {
            iniciarConexion();
        }
    }
}
