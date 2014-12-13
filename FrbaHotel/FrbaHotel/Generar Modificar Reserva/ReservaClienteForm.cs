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

        public string nro_habitacion;
        public string fecha_inicio;
        public string fecha_fin;
        public string codigo_hotel;
        public string codigo_regimen;
        public string codigo_tipo_habitacion;

        public ReservaClienteForm()
        {
            InitializeComponent();
        }

        private void ReservaClienteForm_Load(object sender, EventArgs e)
        {
            iniciarConexion();
            llenarComboBoxTipoDocumento();
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
            reserva_cliente.nro_habitacion = nro_habitacion;
            reserva_cliente.StartPosition = FormStartPosition.CenterScreen;
            reserva_cliente.Show();
        }

        private int validarCampos()
        {
            if (textBoxApellido.Text.Length == 0 || textBoxNombre.Text.Length == 0 ||
                comboBoxTipoDocumento.Text.Length == 0 || textBoxMail.Text.Length == 0 ||
                textBoxDocumento.Text.Length == 0 || textBoxTelefono.Text.Length == 0 ||
                textBoxDireccion.Text.Length == 0 || textBoxLocalidad.Text.Length == 0 ||
                textBoxPais.Text.Length == 0)
            {
                MessageBox.Show("Error, campos que faltan completar: "
                    + (textBoxApellido.Text.Length == 0 ? " Apellido" : "")
                    + (textBoxNombre.Text.Length == 0 ? ", Nombre" : "")
                    + (comboBoxTipoDocumento.Text.Length == 0 ? ", Tipo de Documento" : "")
                    + (textBoxMail.Text.Length == 0 ? ", Email" : "")
                    + (textBoxDocumento.Text.Length == 0 ? ", Numero de Documento" : "")
                    + (textBoxTelefono.Text.Length == 0 ? ", Telefono" : "")
                    + (textBoxDireccion.Text.Length == 0 ? ", Direccion" : "")
                    + (textBoxLocalidad.Text.Length == 0 ? ", Localidad" : "")
                    + (textBoxPais.Text.Length == 0 ? ", Pais" : "")
                );
                return 1;
            }
            return 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int resultado = validarCampos();
            if (resultado == 0)
            {
                try
                {
                    insertarCliente();
                    generarReserva();
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Error en el proceso de generar el cliente y reserva. Error: " + exc);
                }
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
            string string_fechaDesde = fecha_inicio;
            DateTime dt_desde = DateTime.Parse(string_fechaDesde);
            
            string string_fechaHasta = fecha_fin;
            DateTime dt_hasta = DateTime.Parse(string_fechaHasta);
            
            int cant_dias = (int)(dt_hasta - dt_desde).TotalDays;

            string queryId = "select GITAR_HEROES.obtenerSiguienteReserva() as id";
            command = new SqlCommand(queryId);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            string id_codigo = "";
            if (!dataTable.HasErrors)
            {
                id_codigo = dataTable.Rows[0]["id"].ToString();
            }

            string query = "INSERT INTO GITAR_HEROES.Reserva (codigo, fecha_reserva, fecha_inicio,fecha_fin,codigo_hotel,codigo_regimen,tipo_doc_cliente,nro_doc_cliente,costo_base,codigo_estado) VALUES (" + id_codigo + ",'"+Variables.fecha_sistema+" 00:00:00','" + fecha_inicio + " 00:00:00','" + fecha_fin + " 00:00:00'," + codigo_hotel + "," + codigo_regimen + "," + lista_codigos_tipo_documento[comboBoxTipoDocumento.SelectedIndex] + "," + textBoxDocumento.Text + ",GITAR_HEROES.precioHabitacion(" + codigo_regimen + "," + codigo_hotel + "," + codigo_tipo_habitacion + ")*" + cant_dias.ToString() + ",1)";
            command = new SqlCommand(query);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            if (dataTable.HasErrors)
            {
                MessageBox.Show("Error al generar la reserva.");
            }
            else
            {
                string query2;
                query2 = "insert into GITAR_HEROES.UsuarioReserva (codigo_reserva,username,descripción) values (" + id_codigo + ",'"+Variables.usuario+"','Generar reserva')";
                command = new SqlCommand(query2);
                command.Connection = connection;
                adapter = new SqlDataAdapter(command);
                dataTable = new DataTable();
                adapter.Fill(dataTable);

                if (!dataTable.HasErrors)
                {
                    string query3;
                    query3 = "insert into GITAR_HEROES.ReservaHabitacion (codigo_reserva,codigo_hotel,numero_habitacion) values ("+id_codigo+","+codigo_hotel+","+nro_habitacion+")";
                    command = new SqlCommand(query3);
                    command.Connection = connection;
                    adapter = new SqlDataAdapter(command);
                    dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (!dataTable.HasErrors)
                    {
                        MessageBox.Show("Cliente y reserva generada exitosamente. Su código de reserva es " + id_codigo);
                    }
                }
                
            }
        }
    }
}
