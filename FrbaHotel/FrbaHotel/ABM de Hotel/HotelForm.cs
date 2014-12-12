using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaHotel.ABM_de_Hotel
{
    public partial class HotelForm : Form
    {
        private string estado = "-1";
        public string hotel_id;
        private List<int> lista_codigos_regimenes = new List<int>();
        private List<string> lista_nombres_regimenes = new List<string>();

        System.Data.SqlClient.SqlConnection connection;
        private SqlCommand command;
        private SqlDataAdapter adapter;
        private DataTable dataTable;

        public HotelForm()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Hotel pantalla_hotel = new Hotel();
            pantalla_hotel.StartPosition = FormStartPosition.CenterScreen;
            pantalla_hotel.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (hotel_id.Length>0)
            {
                try
                {
                    editarHotel();
                    borrarRegimenes();
                    agregarRegimenes();
                    MessageBox.Show("Hotel editado exitosamente.");
                }
                catch (Exception exc)
                {
                    MessageBox.Show("No se pudo editar el hotel. Error: " + exc);
                }
            }
            else
            {
                try
                {
                    insertarHotel();
                    agregarRegimenes();
                    MessageBox.Show("Hotel creado exitosamente.");
                }
                catch (Exception exc)
                {
                    MessageBox.Show("No se pudo crear el nuevo hotel. Error: " + exc);
                }
            }
        }

        private void agregarRegimenes()
        {
            foreach (int indices_regimenes in checkedListBoxRegimenes.CheckedIndices)
            {
                string codigo_regimen = lista_codigos_regimenes[indices_regimenes].ToString();
                string query = "INSERT INTO GITAR_HEROES.RegimenHotel (codigo_hotel, codigo_regimen) VALUES ("+hotel_id+","+codigo_regimen+")";
                command = new SqlCommand(query);
                command.Connection = connection;
                adapter = new SqlDataAdapter(command);
                dataTable = new DataTable();
                adapter.Fill(dataTable);

                if (dataTable.HasErrors)
                {
                    MessageBox.Show("Error al realizar update del hotel.");
                }
            }
        }

        private void borrarRegimenes()
        {
            string query = "delete from GITAR_HEROES.RegimenHotel where codigo_hotel = "+hotel_id;
            command = new SqlCommand(query);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            if (dataTable.HasErrors)
            {
                MessageBox.Show("Error al editar el hotel.");
            } 
        }

        private void editarHotel()
        {
            string query = "UPDATE GITAR_HEROES.Hotel SET nombre = '"+textBoxNombre.Text+"', mail = '"+textBoxMail.Text+"', telefono = "+textBoxTelefono.Text+", domicilio_calle = '"+textBoxDireccion.Text+"', cant_estrellas = "+textBoxEstrellas.Text+", ciudad = '"+textBoxCiudad.Text+"', pais = '"+textBoxPais.Text+"', fecha_creacion = '"+textBoxFechaCreacion.Text+" 00:00:00',estado = "+(checkBoxEstado.Checked?"1":"0")+" WHERE codigo = "+hotel_id;
            command = new SqlCommand(query);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);
            
            if (dataTable.HasErrors)
            {
                MessageBox.Show("Error al editar el hotel.");
            }

            if (estado == "0" && checkBoxEstado.Checked == true)
            {
                altaHotel();
            }
            else if (estado == "1" && checkBoxEstado.Checked == false)
            {
                bajaHotel();
            }
        }

        private void insertarHotel()
        {
            using (SqlConnection con = new SqlConnection(Variables.connectionStr))
            {
                using (SqlCommand cmd = new SqlCommand("GITAR_HEROES.cargarHotel", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = textBoxNombre.Text;
                    cmd.Parameters.Add("@domicilio_calle", SqlDbType.VarChar).Value = textBoxDireccion.Text;
                    cmd.Parameters.Add("@domicilio_numero", SqlDbType.Int).Value = 0;
                    cmd.Parameters.Add("@recarga_estrellas", SqlDbType.Int).Value = 10;
                    cmd.Parameters.Add("@ciudad", SqlDbType.VarChar).Value = textBoxCiudad.Text;
                    cmd.Parameters.Add("@pais", SqlDbType.VarChar).Value = textBoxPais.Text;
                    cmd.Parameters.Add("@telefono", SqlDbType.Int).Value = Convert.ToUInt32(textBoxTelefono.Text);
                    cmd.Parameters.Add("@cant_estrellas", SqlDbType.Int).Value = Convert.ToInt32(textBoxEstrellas.Text);

                    string string_date = textBoxFechaCreacion.Text;
                    DateTime dt = DateTime.Parse(string_date);
                    cmd.Parameters.Add("@fecha_creacion", System.Data.SqlDbType.DateTime).Value = dt;
                    cmd.Parameters.Add("@mail", SqlDbType.VarChar).Value = textBoxMail.Text;

                    var returnParameter = cmd.Parameters.Add("@codigo_hotel", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.Output;
                    
                    con.Open();
                    cmd.ExecuteNonQuery();

                    int id_creado = (int)returnParameter.Value;
                    hotel_id = id_creado.ToString();
                }
            }
        }

        private void llenarCampos()
        {
            string query = "select * from GITAR_HEROES.Hotel where codigo = "+hotel_id;
            command = new SqlCommand(query);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            if (!dataTable.HasErrors)
            {
                estado = dataTable.Rows[0]["estado"].ToString();
                textBoxDescripcion.Enabled = (dataTable.Rows[0]["estado"].ToString() == "1" && estado != "0") ? true : false;
                checkBoxEstado.Checked = (dataTable.Rows[0]["estado"].ToString() == "1") ? true : false;
                textBoxNombre.Text = dataTable.Rows[0]["nombre"].ToString();
                textBoxMail.Text = dataTable.Rows[0]["mail"].ToString();
                textBoxEstrellas.Text = dataTable.Rows[0]["cant_estrellas"].ToString();
                textBoxPais.Text = dataTable.Rows[0]["pais"].ToString();
                textBoxCiudad.Text = dataTable.Rows[0]["ciudad"].ToString();
                textBoxTelefono.Text = dataTable.Rows[0]["telefono"].ToString();
                textBoxDireccion.Text = dataTable.Rows[0]["domicilio_calle"].ToString();
                string fecha_creacion = dataTable.Rows[0]["fecha_creacion"].ToString();
                if (fecha_creacion.Length>0)
                {
                    DateTime dt = DateTime.Parse(fecha_creacion);
                    textBoxFechaCreacion.Text = (dt.Day.ToString().Length == 1 ? "0" : "") + dt.Day.ToString() + "-" + (dt.Month.ToString().Length == 1 ? "0" : "") + dt.Month.ToString() + "-" + dt.Year.ToString();
                }
            }
        }

        private void bajaHotel()
        {
            string query = "insert into GITAR_HEROES.HotelInhabilitado (codigo_hotel,fecha_inicio,descripcion) values ("+hotel_id+",GETDATE(),'"+textBoxDescripcion.Text+"')";
            command = new SqlCommand(query);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            if (dataTable.HasErrors)
            {
                MessageBox.Show("Error al editar el hotel.");
            }
        }

        private void altaHotel()
        {
            string query = "update GITAR_HEROES.HotelInhabilitado set fecha_fin = GETDATE() where codigo_hotel = " + hotel_id + "and fecha_fin is null";
            command = new SqlCommand(query);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            if (dataTable.HasErrors)
            {
                MessageBox.Show("Error al editar el hotel.");
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

        private void HotelForm_Load_1(object sender, EventArgs e)
        {
            iniciarConexion();
            llenarChecklistRegimenes();
            if (hotel_id.Length > 0)
            {
                llenarCampos();
                tildarRegimenes();
            }
            else
            {
                checkBoxEstado.Checked = true;
                checkBoxEstado.Enabled = false;
                textBoxDescripcion.Enabled = false;
            }
        }

        private void llenarChecklistRegimenes()
        {
            string query = "select * from GITAR_HEROES.Regimen";
            command = new SqlCommand(query);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            foreach (DataRow row in dataTable.Rows)
            {
                checkedListBoxRegimenes.Items.Add(row["descripcion"].ToString());
                lista_codigos_regimenes.Add(Convert.ToInt32(row["codigo"]));
                lista_nombres_regimenes.Add(row["descripcion"].ToString());
            }
        }

        private void tildarRegimenes()
        {
            string query = "select GITAR_HEROES.Regimen.descripcion,GITAR_HEROES.Regimen.codigo from GITAR_HEROES.Regimen inner join GITAR_HEROES.RegimenHotel on GITAR_HEROES.RegimenHotel.codigo_regimen = GITAR_HEROES.Regimen.codigo where GITAR_HEROES.RegimenHotel.codigo_hotel = "+hotel_id;
            command = new SqlCommand(query);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            foreach (DataRow row in dataTable.Rows)
            {
                for (int i = 0; i < checkedListBoxRegimenes.Items.Count; i++)
                {
                    object regimen = checkedListBoxRegimenes.Items[i];
                    if (regimen.ToString() == row["descripcion"].ToString())
                    {
                        checkedListBoxRegimenes.SetItemChecked(i, true);
                    }
                }
            }
        }

        private void checkBoxEstado_CheckedChanged(object sender, EventArgs e)
        {
            textBoxDescripcion.Enabled = (checkBoxEstado.Checked || estado == "0") ? false : true;
            textBoxDescripcion.Text = "";
        }
    }
}
