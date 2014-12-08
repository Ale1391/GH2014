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
    public partial class HotelBusqueda : Form
    {
        System.Data.SqlClient.SqlConnection connection;
        private SqlCommand command;
        private SqlDataAdapter adapter;
        private DataTable dataTable;

        private List<int> lista_codigos_hotel = new List<int>();

        public HotelBusqueda()
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

        private void dataGridViewClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.Hide();
            HotelForm hotel_form = new HotelForm();
            hotel_form.hotel_id = lista_codigos_hotel[e.RowIndex].ToString();
            hotel_form.StartPosition = FormStartPosition.CenterScreen;
            hotel_form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lista_codigos_hotel.Clear();
            //FILTROS
            string query = "";
            if (textBoxNombre.Text.Length > 0)
            {
                query = "select * from GITAR_HEROES.Hotel where nombre = '" + textBoxNombre.Text + "'" + (textBoxEstrellas.Text.Length > 0 ? " and cant_estrellas = " + textBoxEstrellas.Text : "") + (textBoxCiudad.Text.Length > 0 ? " and ciudad = '" + textBoxCiudad.Text+ "'" : "") + (textBoxPais.Text.Length > 0 ? " and pais = '" + textBoxPais.Text + "'" : "");
            }
            else if (textBoxEstrellas.Text.Length > 0)
            {
                query = "select * from GITAR_HEROES.Hotel where cant_estrellas = " + textBoxEstrellas.Text + (textBoxCiudad.Text.Length > 0 ? " and ciudad = '" + textBoxCiudad.Text + "'" : "") + (textBoxPais.Text.Length > 0 ? " and pais = '" + textBoxPais.Text + "'" : "");
            }
            else if (textBoxCiudad.Text.Length > 0)
            {
                query = "select * from GITAR_HEROES.Hotel where ciudad = '" + textBoxCiudad.Text + "'" + (textBoxPais.Text.Length > 0 ? " and pais = '" + textBoxPais.Text + "'" : "");                
            }
            else if (textBoxPais.Text.Length > 0)
            {
                query = "select * from GITAR_HEROES.Hotel where pais = '" + textBoxPais.Text + "'";
            }
            else
            {          
                query = "select * from GITAR_HEROES.Hotel";
            }
            command = new SqlCommand(query);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            if (!dataTable.HasErrors)
            {
                dataGridViewHoteles.DataSource = dataTable;
                foreach (DataRow row in dataTable.Rows)
                {
                    lista_codigos_hotel.Add(Convert.ToInt32(row["codigo"].ToString()));
                }
            }
        }

        private void buttonLimpiar_Click(object sender, EventArgs e)
        {
            textBoxEstrellas.Text = "";
            textBoxNombre.Text = "";
            textBoxPais.Text = "";
            textBoxCiudad.Text = "";
        }

        private void HotelBusqueda_Load_1(object sender, EventArgs e)
        {
            iniciarConexion();
        }
    }
}
