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
    public partial class ReservaClienteForm : Form
    {
        private List<int> lista_codigos_tipo_documento = new List<int>();
        private List<string> lista_nombres_tipo_documento = new List<string>();

        System.Data.SqlClient.SqlConnection connection;
        private SqlCommand command;
        private SqlDataAdapter adapter;
        private DataTable dataTable;

        public string fecha_inicio;
        public string fecha_fin;
        public string codigo_hotel;
        public string codigo_regimen;
        public string codigo_tipo_habitacion;
        public string costo_habitacion;

        public ReservaClienteForm()
        {
            InitializeComponent();
        }

        private void ReservaClienteForm_Load(object sender, EventArgs e)
        {
            iniciarConexion();
            llenarComboBoxTipoDocumento();
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

        private void llenarComboBoxTipoDocumento()
        {
            string query = "select * from GITAR_HEROES.TipoDocumento";
            command = new SqlCommand(query);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            foreach (DataRow row in dataTable.Rows)
            {
                comboBoxTipoDocumento.Items.Add(row["descripcion"].ToString());
                lista_codigos_tipo_documento.Add(Convert.ToInt32(row["codigo"]));
                lista_nombres_tipo_documento.Add(row["descripcion"].ToString());
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            ReservaCliente reserva_cliente = new ReservaCliente();
            reserva_cliente.fecha_fin = fecha_fin;
            reserva_cliente.fecha_inicio = fecha_inicio;
            reserva_cliente.codigo_hotel = codigo_hotel;
            reserva_cliente.codigo_tipo_habitacion = codigo_tipo_habitacion;
            reserva_cliente.codigo_regimen = codigo_regimen;
            reserva_cliente.StartPosition = FormStartPosition.CenterScreen;
            reserva_cliente.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                insertarCliente();
                generarReserva();
                MessageBox.Show("Cliente creado exitosamente.");
            }
            catch (Exception exc)
            {
                MessageBox.Show("No se pudo crear el nuevo cliente. Error: " + exc);
            }
        }

        private void insertarCliente()
        {
            string query = "INSERT INTO GITAR_HEROES.Cliente (nombre,apellido,tipo_doc,nro_doc,mail,telefono,domicilio_calle,localidad,pais_origen,estado) VALUES ('" + textBoxNombre.Text + "','" + textBoxApellido.Text + "'," + lista_codigos_tipo_documento[comboBoxTipoDocumento.SelectedIndex].ToString() + "," + textBoxDocumento.Text + ",'" + textBoxMail.Text + "'," + textBoxTelefono.Text + ",'" + textBoxDireccion.Text + "','" + textBoxLocalidad.Text + "','" + textBoxPais.Text + "',1)";
            command = new SqlCommand(query);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            if (dataTable.HasErrors)
            {
                MessageBox.Show("Error al realizar el insert del cliente.");
            }
        }

        private void generarReserva()
        {
            string query = "";//INSERT INTO GITAR_HEROES.Reserva (codigo_hotel, numero, piso,tipo,ubicacion,descripcion_comodidades,estado) VALUES (" + codigo_hotel + "," + textBoxNumeroHabitacion.Text + "," + textBoxPiso.Text + "," + lista_codigos_habitaciones[comboBoxTipoHabitacion.SelectedIndex] + ",'" + textBoxUbicacion.Text + "','" + textBoxDescripcion.Text + "',1)";
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
    }
}
