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

namespace FrbaHotel.Generar_Modificar_Reserva
{
    public partial class Reserva : Form
    {
        System.Data.SqlClient.SqlConnection connection;
        private SqlCommand command;
        private SqlDataAdapter adapter;
        private DataTable dataTable;

        public List<int> lista_codigos_hoteles = new List<int>();
        public List<string> lista_nombres_hoteles = new List<string>();

        public Reserva()
        {
            InitializeComponent();
            comboBoxReserva.Items.Add("Modificar Reserva");
            comboBoxReserva.Items.Add("Generar Reserva");
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
            if (comboBoxReserva.Text.Length == 0)
            {
                MessageBox.Show("Primero debes elegir una opción.");
            }
            else if (comboBoxReserva.Text == "Modificar Reserva" && textBoxCodigoReserva.Text.Length == 0)
            {
                MessageBox.Show("Primero debes ingresar un código de reserva valido.");
            }
            else if (comboBoxReserva.Text == "Generar Reserva" && comboBoxHotel.Text.Length == 0 && Variables.tipo_usuario == "Guest")
            {
                MessageBox.Show("Primero debes seleccionar un hotel.");
            }
            else if (comboBoxReserva.Text == "Generar Reserva")
            {
                this.Hide();
                ReservaBusqueda busqueda_reserva = new ReservaBusqueda();
                if (Variables.tipo_usuario == "Guest")
                {
                    busqueda_reserva.hotel_id = lista_codigos_hoteles[comboBoxHotel.SelectedIndex].ToString();
                }
                else
                {
                    busqueda_reserva.hotel_id = Variables.hotel_id.ToString();
                }
                busqueda_reserva.StartPosition = FormStartPosition.CenterScreen;
                busqueda_reserva.ShowDialog();
            }
            else
            {
                try
                {
                    string query = "select * from GITAR_HEROES.Reserva where codigo = " + textBoxCodigoReserva.Text;
                    command = new SqlCommand(query);
                    command.Connection = connection;
                    adapter = new SqlDataAdapter(command);
                    dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        /*this.Hide();
                        HabitacionForm abm_habitacion = new HabitacionForm();
                        abm_habitacion.numero_habitacion = textBoxCodigoReserva.Text;
                        abm_habitacion.StartPosition = FormStartPosition.CenterScreen;
                        abm_habitacion.ShowDialog();*/
                    }
                    else
                    {
                        MessageBox.Show("Error: No existe ningun código de reserva con ese número.");
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
            textBoxCodigoReserva.Enabled = false;
            comboBoxHotel.Enabled = false;
            if (Variables.tipo_usuario == "Guest")
            {
                cargarComboHotel();
            }
        }

        private void cargarComboHotel()
        {
            string query = "select * from GITAR_HEROES.Hotel";
            command = new SqlCommand(query);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            foreach (DataRow row in dataTable.Rows)
            {
                comboBoxHotel.Items.Add(row["domicilio_calle"].ToString());
                lista_codigos_hoteles.Add(Convert.ToInt32(row["codigo"]));
                lista_nombres_hoteles.Add(row["domicilio_calle"].ToString());
            }
        }

        private void comboBoxReserva_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxReserva.Text == "Modificar Reserva")
            {
                textBoxCodigoReserva.Enabled = true;
                comboBoxHotel.Enabled = false;
            }
            else if (comboBoxReserva.Text == "Generar Reserva")
            {
                textBoxCodigoReserva.Enabled = false;
                if (Variables.tipo_usuario == "Guest")
                {
                    comboBoxHotel.Enabled = true;
                }
            }
        }
    }
}
