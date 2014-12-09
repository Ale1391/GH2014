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
    public partial class ReservaBusqueda : Form
    {
        public string hotel_id;

        private List<int> lista_codigos_regimenes = new List<int>();
        private List<string> lista_nombres_regimenes = new List<string>();

        private int cantidad_dias_reserva;

        private List<int> lista_codigos_tipo_habitacion = new List<int>();
        private List<string> lista_nombres_tipo_habitacion = new List<string>();

        System.Data.SqlClient.SqlConnection connection;
        private SqlCommand command;
        private SqlDataAdapter adapter;
        private DataTable dataTable;

        public ReservaBusqueda()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Reserva reserva_inicio = new Reserva();
            reserva_inicio.StartPosition = FormStartPosition.CenterScreen;
            reserva_inicio.Show();
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

        private void ReservaBusqueda_Load(object sender, EventArgs e)
        {
            iniciarConexion();
            llenarComboTipoHabitacion();
            llenarComboRegimenes();
        }

        private void llenarComboTipoHabitacion()
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
                lista_codigos_tipo_habitacion.Add(Convert.ToInt32(row["codigo"]));
                lista_nombres_tipo_habitacion.Add(row["descripcion"].ToString());
            }
        }

        private void llenarComboRegimenes()
        {
            string query = "select * from GITAR_HEROES.Regimen inner join GITAR_HEROES.RegimenHotel on GITAR_HEROES.RegimenHotel.codigo_regimen = GITAR_HEROES.Regimen.codigo where GITAR_HEROES.RegimenHotel.codigo_hotel = "+hotel_id;
            command = new SqlCommand(query);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            foreach (DataRow row in dataTable.Rows)
            {

                comboBoxTipoRegimen.Items.Add(row["descripcion"].ToString());
                lista_codigos_regimenes.Add(Convert.ToInt32(row["codigo"]));
                lista_nombres_regimenes.Add(row["descripcion"].ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(Variables.connectionStr))
            {
                using (SqlCommand cmd = new SqlCommand("GITAR_HEROES.verificar_disponibilidad", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    string string_fechaDesde = textBoxFechaDesde.Text;
                    DateTime dt_desde = DateTime.Parse(string_fechaDesde);
                    cmd.Parameters.Add("@fechaInicioNuevaReserva", System.Data.SqlDbType.DateTime).Value = dt_desde;

                    string string_fechaHasta = textBoxFechaHasta.Text;
                    DateTime dt_hasta = DateTime.Parse(string_fechaHasta);
                    cmd.Parameters.Add("@fechaFinNuevaReserva", System.Data.SqlDbType.DateTime).Value = dt_hasta;

                    cantidad_dias_reserva = (int)(dt_hasta - dt_desde).TotalDays;

                    cmd.Parameters.Add("@hotelid", SqlDbType.Int).Value = Convert.ToInt32(hotel_id);
                    cmd.Parameters.Add("@tipo_hab", SqlDbType.Int).Value = Convert.ToInt32(lista_codigos_tipo_habitacion[comboBoxTipoHabitacion.SelectedIndex]);

                    var returnParameter = cmd.Parameters.Add("@num_hab", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();

                    int numero_habitacion_disponible = (int)returnParameter.Value;

                    if (numero_habitacion_disponible == -1)
                    {
                        MessageBox.Show("No hay ninguna habitación disponible de ese tipo en el rango de fechas especificadas.");
                    }
                    else
                    {
                        string query = "select GITAR_HEROES.Regimen.descripcion, GITAR_HEROES.precioHabitacion(GITAR_HEROES.Regimen.codigo,GITAR_HEROES.RegimenHotel.codigo_hotel," + lista_codigos_tipo_habitacion[comboBoxTipoHabitacion.SelectedIndex] + ") as Precio_por_dia, GITAR_HEROES.precioHabitacion(GITAR_HEROES.Regimen.codigo,GITAR_HEROES.RegimenHotel.codigo_hotel," + lista_codigos_tipo_habitacion[comboBoxTipoHabitacion.SelectedIndex] + ")*" + cantidad_dias_reserva.ToString() + " as Precio_Estadia, 'Haz Doble Click' as Seleccionar from GITAR_HEROES.Regimen inner join GITAR_HEROES.RegimenHotel on GITAR_HEROES.RegimenHotel.codigo_regimen = GITAR_HEROES.Regimen.codigo where GITAR_HEROES.RegimenHotel.codigo_hotel = " + hotel_id + (comboBoxTipoRegimen.Text.Length > 0 ? "and GITAR_HEROES.Regimen.codigo = " + lista_codigos_regimenes[comboBoxTipoRegimen.SelectedIndex].ToString() : "");
                        command = new SqlCommand(query);
                        command.Connection = connection;
                        adapter = new SqlDataAdapter(command);
                        dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridViewRegimenes.DataSource = dataTable;
                    }
                }
            }
        }

        private void dataGridViewRegimenes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                this.Hide();
                ReservaCliente reserva_cliente = new ReservaCliente();
                reserva_cliente.fecha_fin = textBoxFechaHasta.Text;
                reserva_cliente.fecha_inicio = textBoxFechaDesde.Text;
                reserva_cliente.codigo_hotel = hotel_id;
                reserva_cliente.codigo_tipo_habitacion = lista_codigos_tipo_habitacion[comboBoxTipoHabitacion.SelectedIndex].ToString();
                reserva_cliente.codigo_regimen = e.RowIndex.ToString();
                reserva_cliente.StartPosition = FormStartPosition.CenterScreen;
                reserva_cliente.Show();
            }
        }
    }
}
