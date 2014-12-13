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
    public partial class ReservaClienteBusqueda : Form
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

        public ReservaClienteBusqueda()
        {
            //
            InitializeComponent();
        }

        private void ReservaClienteBusqueda_Load(object sender, EventArgs e)
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
            reserva_cliente.nro_habitacion = nro_habitacion;
            reserva_cliente.StartPosition = FormStartPosition.CenterScreen;
            reserva_cliente.Show();
        }

        private void buttonBuscar_Click(object sender, EventArgs e)
        {
            string query;
            if (comboBoxTipoDocumento.Text.Length > 0)
            {
                query = "select GITAR_HEROES.Cliente.nombre,GITAR_HEROES.Cliente.apellido,GITAR_HEROES.TipoDocumento.codigo as tipo_documento,GITAR_HEROES.TipoDocumento.descripcion as tipo_documento,GITAR_HEROES.Cliente.nro_doc,GITAR_HEROES.Cliente.mail,GITAR_HEROES.Cliente.pais_origen,'Doble Click' as Seleccionar from GITAR_HEROES.Cliente inner join GITAR_HEROES.TipoDocumento on GITAR_HEROES.TipoDocumento.codigo = GITAR_HEROES.Cliente.tipo_doc where GITAR_HEROES.Cliente.tipo_doc = " + lista_codigos_tipo_documento[comboBoxTipoDocumento.SelectedIndex] + (textBoxNumeroDocumento.Text.Length > 0 ? " and GITAR_HEROES.Cliente.nro_doc = " + textBoxNumeroDocumento.Text : "") + (textBoxMail.Text.Length > 0 ? "and GITAR_HEROES.Cliente.mail = '" + textBoxMail.Text + "'" : "");
            }
            else if (textBoxNumeroDocumento.Text.Length > 0)
            {
                query = "select GITAR_HEROES.Cliente.nombre,GITAR_HEROES.Cliente.apellido,GITAR_HEROES.TipoDocumento.codigo as tipo_documento,GITAR_HEROES.TipoDocumento.descripcion as tipo_documento,GITAR_HEROES.Cliente.nro_doc,GITAR_HEROES.Cliente.mail,GITAR_HEROES.Cliente.pais_origen,'Doble Click' as Seleccionar from GITAR_HEROES.Cliente inner join GITAR_HEROES.TipoDocumento on GITAR_HEROES.TipoDocumento.codigo = GITAR_HEROES.Cliente.tipo_doc where GITAR_HEROES.Cliente.nro_doc = " + textBoxNumeroDocumento.Text + (textBoxMail.Text.Length > 0 ? " and GITAR_HEROES.Cliente.mail = '" + textBoxMail.Text + "'" : "");
            }
            else if (textBoxMail.Text.Length > 0)
            {
                query = "select GITAR_HEROES.Cliente.nombre,GITAR_HEROES.Cliente.apellido,GITAR_HEROES.TipoDocumento.codigo as tipo_documento,GITAR_HEROES.TipoDocumento.descripcion as tipo_documento,GITAR_HEROES.Cliente.nro_doc,GITAR_HEROES.Cliente.mail,GITAR_HEROES.Cliente.pais_origen,'Doble Click' as Seleccionar from GITAR_HEROES.Cliente inner join GITAR_HEROES.TipoDocumento on GITAR_HEROES.TipoDocumento.codigo = GITAR_HEROES.Cliente.tipo_doc where GITAR_HEROES.Cliente.mail = '" + textBoxMail.Text + "'";
            }
            else
            {
                query = "select GITAR_HEROES.Cliente.nombre,GITAR_HEROES.Cliente.apellido,GITAR_HEROES.TipoDocumento.codigo as tipo_documento,GITAR_HEROES.TipoDocumento.descripcion as tipo_documento,GITAR_HEROES.Cliente.nro_doc,GITAR_HEROES.Cliente.mail,GITAR_HEROES.Cliente.pais_origen,'Doble Click' as Seleccionar from GITAR_HEROES.Cliente inner join GITAR_HEROES.TipoDocumento on GITAR_HEROES.TipoDocumento.codigo = GITAR_HEROES.Cliente.tipo_doc";
            }
            command = new SqlCommand(query);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            dataGridViewClientes.DataSource = dataTable;
        }

        private void buttonLimpiar_Click(object sender, EventArgs e)
        {
            comboBoxTipoDocumento.Text = "";
            comboBoxTipoDocumento.SelectedIndex = -1;
            textBoxMail.Text = "";
            textBoxNumeroDocumento.Text = "";
        }

        private void dataGridViewClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7)
            {
                try
                {
                    generarReserva(e.RowIndex);
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Error en el proceso de generar el cliente y reserva. Error: " + exc);
                }
            }
        }

        private void generarReserva(int row)
        {
            string numero_documento = dataTable.Rows[row]["nro_doc"].ToString();
            string tipo_documento = dataTable.Rows[row]["tipo_documento"].ToString();

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

            string query = "INSERT INTO GITAR_HEROES.Reserva (codigo, fecha_reserva, fecha_inicio,fecha_fin,codigo_hotel,codigo_regimen,tipo_doc_cliente,nro_doc_cliente,costo_base,codigo_estado) VALUES ("+id_codigo+",'"+Variables.fecha_sistema+" 00:00:00','" + fecha_inicio + " 00:00:00','" + fecha_fin + " 00:00:00'," + codigo_hotel + "," + codigo_regimen + "," + tipo_documento + "," + numero_documento + ",GITAR_HEROES.precioHabitacion(" + codigo_regimen + "," + codigo_hotel + "," + codigo_tipo_habitacion + ")*" + cant_dias.ToString() + ",1)";
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
                query2 = "insert into GITAR_HEROES.UsuarioReserva (codigo_reserva,username,descripción) values (" + id_codigo + ",'" + Variables.usuario + "','Generar reserva')";
                command = new SqlCommand(query2);
                command.Connection = connection;
                adapter = new SqlDataAdapter(command);
                dataTable = new DataTable();
                adapter.Fill(dataTable);

                if (!dataTable.HasErrors)
                {
                    string query3;
                    query3 = "insert into GITAR_HEROES.ReservaHabitacion (codigo_reserva,codigo_hotel,numero_habitacion) values (" + id_codigo + "," + codigo_hotel + "," + nro_habitacion + ")";
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
