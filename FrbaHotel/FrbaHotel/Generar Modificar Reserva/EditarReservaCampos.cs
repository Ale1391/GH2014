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
    public partial class EditarReservaCampos : Form
    {
        public string codigo_reserva;
        public string hotel_id;

        private List<int> lista_codigos_regimenes = new List<int>();
        private List<string> lista_nombres_regimenes = new List<string>();

        private List<int> lista_habitaciones_tipos = new List<int>();
        private List<int> lista_habitaciones_nros = new List<int>();
        private List<int> lista_habitaciones_nuevas_nros = new List<int>();

        System.Data.SqlClient.SqlConnection connection;
        private SqlCommand command;
        private SqlDataAdapter adapter;
        private DataTable dataTable;

        public EditarReservaCampos()
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

            string query = "select rh.*,h.tipo from GITAR_HEROES.ReservaHabitacion rh inner join GITAR_HEROES.Habitacion h on h.numero = rh.numero_habitacion and h.codigo_hotel = rh.codigo_hotel where rh.codigo_reserva = "+codigo_reserva;
            command = new SqlCommand(query);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            foreach (DataRow row in dataTable.Rows)
            {
                lista_habitaciones_tipos.Add(Convert.ToInt32(row["tipo"]));
                lista_habitaciones_nros.Add(Convert.ToInt32(row["numero_habitacion"]));
            }

            bool dispo = true;

            for (int i = 0; i < lista_habitaciones_nros.Count; i++)
            {
                int tipo_habitacion = lista_habitaciones_tipos[i];

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

                        int cantidad_dias_reserva = (int)(dt_hasta - dt_desde).TotalDays;

                        cmd.Parameters.Add("@hotelid", SqlDbType.Int).Value = Convert.ToInt32(hotel_id);
                        cmd.Parameters.Add("@tipo_hab", SqlDbType.Int).Value = tipo_habitacion;

                        var returnParameter = cmd.Parameters.Add("@num_hab", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.Output;

                        con.Open();
                        cmd.ExecuteNonQuery();

                        int numero_habitacion_disponible = (int)returnParameter.Value;

                        if (numero_habitacion_disponible == -1)
                        {
                            dispo = false;
                        }
                        else
                        {
                            lista_habitaciones_nuevas_nros.Add(numero_habitacion_disponible);
                        }
                    }
                }
            }

            if (!dispo)
            {
                MessageBox.Show("No hay habitaciones disponibles de ese tipo en el rango de fechas especificadas.");
            }
            else
            {
                int precioTotal = 0;
                for (int i = 0; i < lista_habitaciones_nuevas_nros.Count; i++)
                {
                    int nro_habitacion_nuevo = lista_habitaciones_nuevas_nros[i];
                    int nro_habitacion_viejo = lista_habitaciones_nros[i];

                    string query2 = "update GITAR_HEROES.ReservaHabitacion set numero_habitacion = " + nro_habitacion_nuevo.ToString() + " where codigo_reserva = " + codigo_reserva + " and numero_habitacion = " + nro_habitacion_viejo.ToString(); ;
                    command = new SqlCommand(query2);
                    command.Connection = connection;
                    adapter = new SqlDataAdapter(command);
                    dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    if (!dataTable.HasErrors)
                    {
                        string codigo_regimen = lista_codigos_regimenes[comboBoxTipoRegimen.SelectedIndex].ToString();
                        string codigo_tipo_habitacion = "";
                        
                        string queryHab = "select * from GITAR_HEROES.Habitacion where codigo_hotel = "+hotel_id+" and numero = "+nro_habitacion_nuevo;
                        command = new SqlCommand(queryHab);
                        command.Connection = connection;
                        adapter = new SqlDataAdapter(command);
                        dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        if (dataTable.Rows.Count > 0)
                        {
                            codigo_tipo_habitacion = dataTable.Rows[0]["tipo"].ToString();

                            string queryPrecio = "select GITAR_HEROES.precioHabitacion("+lista_codigos_regimenes[comboBoxTipoRegimen.SelectedIndex]+","+hotel_id+","+codigo_tipo_habitacion+") as precio";
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
                    }
                }

                //CAMBIAR LA TABLA RESERVA
                
                string string_fechaDesde = textBoxFechaDesde.Text;
                DateTime dt_desde = DateTime.Parse(string_fechaDesde);

                string string_fechaHasta = textBoxFechaHasta.Text;
                DateTime dt_hasta = DateTime.Parse(string_fechaHasta);

                int cant_dias = (int)(dt_hasta - dt_desde).TotalDays;

                string query3 = "update GITAR_HEROES.Reserva set fecha_inicio = '" + textBoxFechaDesde.Text + " 00:00:00',fecha_fin = '" + textBoxFechaHasta.Text + " 00:00:00',codigo_regimen = " + lista_codigos_regimenes[comboBoxTipoRegimen.SelectedIndex] + ",costo_base = " + precioTotal + "*" + cant_dias.ToString() + ",codigo_estado = 2 where codigo = " + codigo_reserva;
                command = new SqlCommand(query3);
                command.Connection = connection;
                adapter = new SqlDataAdapter(command);
                dataTable = new DataTable();
                adapter.Fill(dataTable);
                if (!dataTable.HasErrors)
                {
                    MessageBox.Show("Se edito exitosamente la reserva.");
                }
            }
            lista_habitaciones_tipos.Clear();
            lista_habitaciones_nros.Clear();
            lista_habitaciones_nuevas_nros.Clear();
                
        }

        private void EditarReservaCampos_Load(object sender, EventArgs e)
        {
            iniciarConexion();
            llenarComboRegimenes();
            llenarFormulario();
        }

        private void llenarComboRegimenes()
        {
            string query = "select * from GITAR_HEROES.Regimen inner join GITAR_HEROES.RegimenHotel on GITAR_HEROES.RegimenHotel.codigo_regimen = GITAR_HEROES.Regimen.codigo where GITAR_HEROES.RegimenHotel.codigo_hotel = " + hotel_id;
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

        private void llenarFormulario()
        {
            string query = "select r.*,reg.descripcion as regimen_descripcion,rh.numero_habitacion,h.tipo as tipo_hab,th.descripcion as tipo_hab_descripcion  from GITAR_HEROES.Reserva r inner join GITAR_HEROES.Regimen reg on reg.codigo = r.codigo_regimen inner join GITAR_HEROES.ReservaHabitacion rh on rh.codigo_reserva = r.codigo inner join GITAR_HEROES.Habitacion h on h.numero = rh.numero_habitacion and h.codigo_hotel = rh.codigo_hotel inner join GITAR_HEROES.TipoHabitacion th on h.tipo = th.codigo where r.codigo = " + codigo_reserva;
            command = new SqlCommand(query);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            if (!dataTable.HasErrors)
            {
                comboBoxTipoRegimen.Text = dataTable.Rows[0]["regimen_descripcion"].ToString();

                string fecha_inicio = dataTable.Rows[0]["fecha_inicio"].ToString();
                if (fecha_inicio.Length > 0)
                {
                    DateTime dt = DateTime.Parse(fecha_inicio);
                    textBoxFechaDesde.Text = (dt.Day.ToString().Length == 1 ? "0" : "") + dt.Day.ToString() + "-" + (dt.Month.ToString().Length == 1 ? "0" : "") + dt.Month.ToString() + "-" + dt.Year.ToString();
                }

                string fecha_fin = dataTable.Rows[0]["fecha_fin"].ToString();
                if (fecha_fin.Length > 0)
                {
                    DateTime dt = DateTime.Parse(fecha_fin);
                    textBoxFechaHasta.Text = (dt.Day.ToString().Length == 1 ? "0" : "") + dt.Day.ToString() + "-" + (dt.Month.ToString().Length == 1 ? "0" : "") + dt.Month.ToString() + "-" + dt.Year.ToString();
                }
            }
        }
    }
}
