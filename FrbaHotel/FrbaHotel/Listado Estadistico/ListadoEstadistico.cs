using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using FrbaHotel.Listado_Funcionalidades;

namespace FrbaHotel.Listado_Estadistico
{
    public partial class ListadoEstadistico : Form
    {
        // Variables para cargar la tabla de listados
        System.Data.SqlClient.SqlConnection connection;
        private SqlCommand command;
        private SqlDataAdapter adapter;
        private DataTable dataTable;
        
        // Se crean las listas a utilizarse para llenar el comboBox
        private List<int> lista_codigo_listado = new List<int>();
        private List<string> lista_descripcion_listado = new List<string>();

        private int codigo_listado;
        public ListadoEstadistico()
        {
            InitializeComponent();
        }

        private void ListadoEstadistico_Load(object sender, EventArgs e)
        {
            iniciarConexion();
            connection = new System.Data.SqlClient.SqlConnection();

            cargarComboBoxListado();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Funcionalidades funcionalidadesForm = new Funcionalidades();
            funcionalidadesForm.StartPosition = FormStartPosition.CenterScreen;
            funcionalidadesForm.Show();
        }

        private void iniciarConexion()
        {
            // Genera conexion con la base de datos
            System.Data.SqlClient.SqlConnection connection;
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            codigo_listado = lista_codigo_listado[comboBoxListado.SelectedIndex];
        }

        private void cargarComboBoxListado()
        {
        try
        {
            
            command = new SqlCommand("SELECT * FROM GITAR_HEROES.Listado");
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            foreach (DataRow row in dataTable.Rows)
            {
                comboBoxListado.Items.Add(row["descripcion"].ToString());
                lista_codigo_listado.Add(Convert.ToInt32(row["codigo"]));
            }
        }
        catch (Exception exc)
        {
            MessageBox.Show("Error: " + exc);
        }

        }
    }
}
