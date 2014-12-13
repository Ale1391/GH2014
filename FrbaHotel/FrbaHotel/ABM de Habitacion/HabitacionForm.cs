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
using System.Globalization;
using System.Security.Cryptography;

namespace FrbaHotel.ABM_de_Habitacion
{
    public partial class HabitacionForm : Form
    {
        public List<int> lista_codigos_habitaciones = new List<int>();
        public List<string> lista_nombres_habitaciones = new List<string>();
        public string numero_habitacion;

        System.Data.SqlClient.SqlConnection connection;
        private SqlCommand command;
        private SqlDataAdapter adapter;
        private DataTable dataTable;

        public HabitacionForm()
        {
            InitializeComponent();
        }

        private void cargarFormulario()
        {
            string query = "select GITAR_HEROES.Habitacion.numero,GITAR_HEROES.Habitacion.piso,GITAR_HEROES.Habitacion.ubicacion,GITAR_HEROES.Habitacion.descripcion_comodidades,GITAR_HEROES.Habitacion.estado,GITAR_HEROES.TipoHabitacion.descripcion as tipo_habitacion_descripcion from GITAR_HEROES.Habitacion inner join GITAR_HEROES.TipoHabitacion on GITAR_HEROES.TipoHabitacion.codigo = GITAR_HEROES.Habitacion.tipo where numero = " + numero_habitacion + " and codigo_hotel = " + Variables.hotel_id;
            command = new SqlCommand(query);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            if (dataTable.Rows.Count > 0)
            {
                textBoxNumeroHabitacion.Text = dataTable.Rows[0]["numero"].ToString();
                textBoxPiso.Text = dataTable.Rows[0]["piso"].ToString();
                comboBoxTipoHabitacion.Text = dataTable.Rows[0]["tipo_habitacion_descripcion"].ToString();
                textBoxUbicacion.Text = dataTable.Rows[0]["ubicacion"].ToString();
                textBoxDescripcion.Text = dataTable.Rows[0]["descripcion_comodidades"].ToString();
                checkBoxHabitacionActiva.Checked = (dataTable.Rows[0]["estado"].ToString() == "1") ? true : false;
            }
            comboBoxTipoHabitacion.Enabled = false;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Habitacion habitacionForm = new Habitacion();
            habitacionForm.StartPosition = FormStartPosition.CenterScreen;
            habitacionForm.Show();
        }

        private void llenarCombosTipoHabitacion()
        {
            string query = "select * from GITAR_HEROES.TipoHabitacion";
            command = new SqlCommand(query);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            foreach (DataRow row in dataTable.Rows)
            {
                comboBoxTipoHabitacion.Items.Add(row["descripcion"].ToString());
                lista_codigos_habitaciones.Add(Convert.ToInt32(row["codigo"]));
                lista_nombres_habitaciones.Add(row["descripcion"].ToString());
            }
        }

        private int validarCampos()
        {
            if (textBoxNumeroHabitacion.Text.Length == 0 || textBoxPiso.Text.Length == 0 ||
                textBoxUbicacion.Text.Length == 0 || comboBoxTipoHabitacion.Text.Length == 0 ||
                textBoxDescripcion.Text.Length == 0)
            {
                MessageBox.Show("Error, campos que faltan completar: "
                    + (textBoxNumeroHabitacion.Text.Length == 0 ? " Numero de habitacion" : "")
                    + (textBoxPiso.Text.Length == 0 ? ", Piso" : "")
                    + (textBoxUbicacion.Text.Length == 0 ? ", Ubicacion" : "")
                    + (comboBoxTipoHabitacion.Text.Length == 0 ? ", Tipo de habitacion" : "")
                    + (textBoxDescripcion.Text.Length == 0 ? ", Descripcion" : "")
                );
                return 1;
            }
            return 0;
        }

        private void buttonFinalizar_Click(object sender, EventArgs e)
        {
            int resultado = validarCampos();
            if (resultado == 0)
            {
                if (numero_habitacion.Length > 0)
                {
                    try
                    {
                        editarHabitacion();
                        MessageBox.Show("Habitación editada exitosamente");
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Error: " + exc);
                    }
                }
                else
                {
                    try
                    {
                        crearHabitacion();
                        MessageBox.Show("Habitación creada exitosamente");
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Error: " + exc);
                    }
                }
            }
        }

        private void crearHabitacion()
        {
            string query = "INSERT INTO GITAR_HEROES.Habitacion (codigo_hotel, numero, piso,tipo,ubicacion,descripcion_comodidades,estado) VALUES ("+Variables.hotel_id+","+textBoxNumeroHabitacion.Text+","+textBoxPiso.Text+","+lista_codigos_habitaciones[comboBoxTipoHabitacion.SelectedIndex]+",'"+textBoxUbicacion.Text+"','"+textBoxDescripcion.Text+"',1)";
            command = new SqlCommand(query);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            if (dataTable.HasErrors)
            {
                MessageBox.Show("Error al crear la habitación");
            }
        }

        private void editarHabitacion()
        {
            string query = "UPDATE GITAR_HEROES.Habitacion SET numero = "+textBoxNumeroHabitacion.Text+", piso = "+textBoxPiso.Text+", ubicacion = '"+textBoxUbicacion.Text+"', descripcion_comodidades = '"+textBoxDescripcion.Text+"', estado = "+(checkBoxHabitacionActiva.Checked ? "1":"0")+" WHERE codigo_hotel = "+Variables.hotel_id+" and numero = "+numero_habitacion;
            command = new SqlCommand(query);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            if (dataTable.HasErrors)
            {
                MessageBox.Show("Error al editar la habitación");
            }
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void HabitacionForm_Load_1(object sender, EventArgs e)
        {
            iniciarConexion();
            llenarCombosTipoHabitacion();
            if (numero_habitacion.Length > 0)
            {
                cargarFormulario();
            }
            else
            {
                checkBoxHabitacionActiva.Checked = true;
                checkBoxHabitacionActiva.Enabled = false;
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
    }
}
