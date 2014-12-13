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
    public partial class EditarHabitacionForm : Form
    {
        private List<int> lista_codigos_tipo_habitacion = new List<int>();
        private List<string> lista_nombres_tipo_habitacion = new List<string>();

        System.Data.SqlClient.SqlConnection connection;
        private SqlCommand command;
        private SqlDataAdapter adapter;
        private DataTable dataTable;

        public string numero_habitacion;
        public string codigo_reserva;
        public string hotel_id;

        public EditarHabitacionForm()
        {
            InitializeComponent();
        }

        private void EditarHabitacionForm_Load(object sender, EventArgs e)
        {
            iniciarConexion();
            llenarComboTipoHabitacion();
            if (numero_habitacion.Length > 0)
            {
                llenarComboBoxTipoHabitacion();
            }
        }

        private void llenarComboBoxTipoHabitacion()
        {
            string query = "select th.descripcion from GITAR_HEROES.Habitacion h inner join GITAR_HEROES.TipoHabitacion th on th.codigo = h.tipo where codigo_hotel = "+hotel_id+" and numero = "+numero_habitacion;
            command = new SqlCommand(query);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            if (dataTable.Rows.Count > 0)
            {
                comboBoxTipoHabitacion.Text = dataTable.Rows[0]["descripcion"].ToString();
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            EditarHabitaciones habitaciones = new EditarHabitaciones();
            habitaciones.codigo_reserva = codigo_reserva;
            habitaciones.hotel_id = hotel_id;
            habitaciones.StartPosition = FormStartPosition.CenterScreen;
            habitaciones.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(Variables.connectionStr))
            {
                using (SqlCommand cmd = new SqlCommand("GITAR_HEROES.finalizarReservasPerdidas", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    string currentDate = DateTime.Now.ToString("dd-MM-yyyy");
                    DateTime dt = DateTime.Parse(currentDate);
                    cmd.Parameters.Add("@fecha", System.Data.SqlDbType.DateTime).Value = dt;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            string query = "select * from GITAR_HEROES.Reserva where codigo = "+codigo_reserva;
            command = new SqlCommand(query);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            string string_fechaDesde = "";
            string string_fechaHasta = "";
            

            if (dataTable.Rows.Count > 0)
            {
                string fecha_inicio = dataTable.Rows[0]["fecha_inicio"].ToString();
                if (fecha_inicio.Length > 0)
                {
                    DateTime dt = DateTime.Parse(fecha_inicio);
                    string_fechaDesde = (dt.Day.ToString().Length == 1 ? "0" : "") + dt.Day.ToString() + "-" + (dt.Month.ToString().Length == 1 ? "0" : "") + dt.Month.ToString() + "-" + dt.Year.ToString();
                }

                string fecha_fin = dataTable.Rows[0]["fecha_fin"].ToString();
                if (fecha_fin.Length > 0)
                {
                    DateTime dt = DateTime.Parse(fecha_fin);
                    string_fechaHasta = (dt.Day.ToString().Length == 1 ? "0" : "") + dt.Day.ToString() + "-" + (dt.Month.ToString().Length == 1 ? "0" : "") + dt.Month.ToString() + "-" + dt.Year.ToString();
                }
            }

            using (SqlConnection con = new SqlConnection(Variables.connectionStr))
            {
                using (SqlCommand cmd = new SqlCommand("GITAR_HEROES.verificar_disponibilidad", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    DateTime dt_desde = DateTime.Parse(string_fechaDesde);
                    cmd.Parameters.Add("@fechaInicioNuevaReserva", System.Data.SqlDbType.DateTime).Value = dt_desde;

                    DateTime dt_hasta = DateTime.Parse(string_fechaHasta);
                    cmd.Parameters.Add("@fechaFinNuevaReserva", System.Data.SqlDbType.DateTime).Value = dt_hasta;

                    int cantidad_dias_reserva = (int)(dt_hasta - dt_desde).TotalDays;

                    cmd.Parameters.Add("@hotelid", SqlDbType.Int).Value = Convert.ToInt32(hotel_id);
                    cmd.Parameters.Add("@tipo_hab", SqlDbType.Int).Value = lista_codigos_tipo_habitacion[comboBoxTipoHabitacion.SelectedIndex];

                    var returnParameter = cmd.Parameters.Add("@num_hab", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.Output;

                    con.Open();
                    cmd.ExecuteNonQuery();

                    int numero_habitacion_disponible = (int)returnParameter.Value;

                    if (numero_habitacion_disponible != -1)
                    {
                        if (numero_habitacion.Length > 0)
                        {
                            editarHabitacion(numero_habitacion_disponible);
                        }
                        else
                        {
                            agregarHabitacion(numero_habitacion_disponible);
                        }
                        editarCodigoEstadoReserva();
                    }
                }
            }
        }

        private void editarCodigoEstadoReserva()
        {
            string query = "select * from GITAR_HEROES.Reserva where codigo = " + codigo_reserva;
            command = new SqlCommand(query);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            string codigo_regimen = ""; ;
            DateTime dt_desde = DateTime.Today;
            DateTime dt_hasta = DateTime.Today;
            
            if (dataTable.Rows.Count > 0)
            {
                codigo_regimen = dataTable.Rows[0]["codigo_regimen"].ToString();
                string fecha_inicio = dataTable.Rows[0]["fecha_inicio"].ToString();
                if (fecha_inicio.Length > 0)
                {
                    dt_desde = DateTime.Parse(fecha_inicio);   
                }

                string fecha_fin = dataTable.Rows[0]["fecha_fin"].ToString();
                if (fecha_fin.Length > 0)
                {
                    dt_hasta = DateTime.Parse(fecha_fin);
                }
            }
            int cant_dias = (int)(dt_hasta - dt_desde).TotalDays;

            int precioTotal = 0;

            string queryTipo = "select h.tipo from GITAR_HEROES.ReservaHabitacion  r inner join GITAR_HEROES.Habitacion h on h.numero = r.numero_habitacion and h.codigo_hotel = "+hotel_id+" where codigo_reserva = "+codigo_reserva;
            command = new SqlCommand(queryTipo);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);
            foreach (DataRow row in dataTable.Rows)
            {
                string tipo_habitacion = row["tipo"].ToString();
                string queryPrecio = "select GITAR_HEROES.precioHabitacion(" + codigo_regimen + "," + hotel_id + "," + tipo_habitacion + ") as precio";
                command = new SqlCommand(queryPrecio);
                command.Connection = connection;
                adapter = new SqlDataAdapter(command);
                dataTable = new DataTable();
                adapter.Fill(dataTable);
                if (!dataTable.HasErrors)
                {
                    precioTotal += Convert.ToInt32(dataTable.Rows[0]["precio"]);
                }
            }
            
            
            string queryUpdate = "update GITAR_HEROES.Reserva set codigo_estado = 2 , costo_base = " + precioTotal + " * " + cant_dias.ToString() +" where codigo = " + codigo_reserva;
            command = new SqlCommand(queryUpdate);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);
        }

        private void editarHabitacion(int numero_hab_nueva)
        {
            //UPDATE
            string query = "update GITAR_HEROES.ReservaHabitacion set numero_habitacion = "+numero_hab_nueva+" where codigo_reserva = "+codigo_reserva+" and numero_habitacion = "+numero_habitacion;
            command = new SqlCommand(query);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            if (!dataTable.HasErrors)
            {
                MessageBox.Show("Se edito la habitación correctamente.");
            }
        }

        private void agregarHabitacion(int numero_hab_nueva)
        {
            //INSERT
            string query = "insert into GITAR_HEROES.ReservaHabitacion (codigo_reserva,codigo_hotel,numero_habitacion) values ("+codigo_reserva+","+hotel_id+","+numero_hab_nueva+")";
            command = new SqlCommand(query);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            if (!dataTable.HasErrors)
            {
                MessageBox.Show("Se agrego la habitación correctamente.");
            }
        }
    }
}
