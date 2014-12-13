using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaHotel.Generar_Modificar_Reserva
{
    public partial class EditarHabitaciones : Form
    {
        public string codigo_reserva;
        public string hotel_id;

        private List<int> lista_numeros_habitaciones = new List<int>();

        System.Data.SqlClient.SqlConnection connection;
        private SqlCommand command;
        private SqlDataAdapter adapter;
        private DataTable dataTable;

        public EditarHabitaciones()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            EditarReserva reserva_inicio = new EditarReserva();
            reserva_inicio.hotel_id = hotel_id;
            reserva_inicio.codigo_reserva = codigo_reserva;
            reserva_inicio.StartPosition = FormStartPosition.CenterScreen;
            reserva_inicio.Show();
        }

        private void EditarHabitaciones_Load(object sender, EventArgs e)
        {

            comboBoxOpcion.Items.Add("Agregar habitacion");
            comboBoxOpcion.Items.Add("Editar habitacion");
            comboBoxHabitacion.Enabled = false;

            iniciarConexion();
            llenarComboBoxHabitaciones();
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

        private void llenarComboBoxHabitaciones()
        {
            string query = "select * from GITAR_HEROES.ReservaHabitacion where codigo_reserva = "+codigo_reserva;
            command = new SqlCommand(query);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            foreach (DataRow row in dataTable.Rows)
            {
                comboBoxHabitacion.Items.Add(row["numero_habitacion"].ToString());
                lista_numeros_habitaciones.Add(Convert.ToInt32(row["numero_habitacion"]));
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (comboBoxOpcion.Text == "")
            {
                MessageBox.Show("Error: Primero elije una opción.");
            }
            else if (comboBoxOpcion.Text == "Editar habitacion" && comboBoxHabitacion.Text == "")
            {
                MessageBox.Show("Error: Primero elije una habitación.");
            }
            else if (comboBoxOpcion.Text == "Agregar habitacion")
            {
                //CREAR HABITACION
                this.Hide();
                EditarHabitacionForm reserva_inicio = new EditarHabitacionForm();
                reserva_inicio.numero_habitacion = "";
                reserva_inicio.hotel_id = hotel_id;
                reserva_inicio.codigo_reserva = codigo_reserva;
                reserva_inicio.StartPosition = FormStartPosition.CenterScreen;
                reserva_inicio.Show();

            }
            else
            {
                //EDITAR HABITACION
                this.Hide();
                EditarHabitacionForm reserva_inicio = new EditarHabitacionForm();
                reserva_inicio.numero_habitacion = comboBoxHabitacion.Text;
                reserva_inicio.codigo_reserva = codigo_reserva;
                reserva_inicio.hotel_id = hotel_id;
                reserva_inicio.StartPosition = FormStartPosition.CenterScreen;
                reserva_inicio.Show();
            }
        }

        private void comboBoxOpcion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxOpcion.Text == "Agregar habitacion")
            {
                comboBoxHabitacion.Enabled = false;
                comboBoxHabitacion.Text = "";
            }
            else
            {
                comboBoxHabitacion.Enabled = true;
            }
        }
    }
}
