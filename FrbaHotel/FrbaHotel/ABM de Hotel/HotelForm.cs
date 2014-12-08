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
            Hotel pantalla_cliente = new Hotel();
            pantalla_cliente.StartPosition = FormStartPosition.CenterScreen;
            pantalla_cliente.Show();
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
            string query = "UPDATE GITAR_HEROES.Hotel SET nombre = '"+textBoxNombre.Text+"', mail = '"+textBoxMail.Text+"', telefono = "+textBoxTelefono.Text+", domicilio_calle = '"+textBoxDireccion.Text+"', cant_estrellas = "+textBoxEstrellas.Text+", ciudad = '"+textBoxCiudad.Text+"', pais = '"+textBoxPais.Text+"', fecha_creacion = '"+textBoxFechaCreacion.Text+" 00:00:00' WHERE codigo = "+hotel_id;
            command = new SqlCommand(query);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            if (dataTable.HasErrors)
            {
                MessageBox.Show("Error al editar el cliente.");
            }
        }

        private void insertarHotel()
        {
            string query = "INSERT INTO GITAR_HEROES.Hotel (nombre,mail,telefono,domicilio_calle,cant_estrellas,ciudad,pais,fecha_creacion) VALUES ('" + textBoxNombre.Text + "','" + textBoxMail.Text + "'," + textBoxTelefono.Text + ",'" + textBoxDireccion.Text + "'," + textBoxEstrellas.Text + ",'" + textBoxCiudad.Text + "','" + textBoxPais.Text + "','" + textBoxFechaCreacion.Text + " 00:00:00')";
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
    }
}
