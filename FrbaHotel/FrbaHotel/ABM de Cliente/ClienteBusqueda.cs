using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaHotel.ABM_de_Cliente
{
    public partial class ClienteBusqueda : Form
    {
        System.Data.SqlClient.SqlConnection connection;
        private SqlCommand command;
        private SqlDataAdapter adapter;
        private DataTable dataTable;

        private List<int> lista_codigos_dni = new List<int>();
        private List<string> lista_nombres_dni = new List<string>();
        private List<string> lista_mails = new List<string>();

        public ClienteBusqueda()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Cliente pantalla_cliente = new Cliente();
            pantalla_cliente.StartPosition = FormStartPosition.CenterScreen;
            pantalla_cliente.Show();
        }

        private void ClienteBusqueda_Load(object sender, EventArgs e)
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
            string query2 = "select * from GITAR_HEROES.TipoDocumento";
            command = new SqlCommand(query2);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            foreach (DataRow row in dataTable.Rows)
            {
                comboBoxTipoDocumento.Items.Add(row["descripcion"].ToString());
                lista_codigos_dni.Add(Convert.ToInt32(row["codigo"]));
                lista_nombres_dni.Add(row["descripcion"].ToString());
            }
        }

        private void dataGridViewClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 15)
            {
                this.Hide();
                ClienteForm cliente_form = new ClienteForm();
                cliente_form.mail = lista_mails[e.RowIndex].ToString();
                cliente_form.StartPosition = FormStartPosition.CenterScreen;
                cliente_form.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lista_mails.Clear();
            //FILTROS
            string query = "";
            if (textBoxApellido.Text.Length > 0)
            {
                query = "select c.*,'Doble click' as Seleccionar from GITAR_HEROES.Cliente c where apellido = '" + textBoxApellido.Text + "'" + (textBoxNombre.Text.Length > 0 ? " and nombre = '" + textBoxNombre.Text + "'" : "") + (textBoxNumeroDocumento.Text.Length > 0 ? " and nro_doc = " + textBoxNumeroDocumento.Text : "") + (comboBoxTipoDocumento.Text.Length > 0 ? " and tipo_doc = " + lista_codigos_dni[comboBoxTipoDocumento.SelectedIndex] : "") + (textBoxEmail.Text.Length > 0 ? " and mail = '" + textBoxEmail.Text + "'" : "");
            }
            else if (textBoxNombre.Text.Length > 0)
            {
                query = "select c.*,'Doble click' as Seleccionar from GITAR_HEROES.Cliente c where nombre = '" + textBoxNombre.Text + "'" + (textBoxNumeroDocumento.Text.Length > 0 ? " and nro_doc = " + textBoxNumeroDocumento.Text : "") + (comboBoxTipoDocumento.Text.Length > 0 ? " and tipo_doc = " + lista_codigos_dni[comboBoxTipoDocumento.SelectedIndex] : "") + (textBoxEmail.Text.Length > 0 ? " and mail = '" + textBoxEmail.Text + "'" : "");
            }
            else if (textBoxNumeroDocumento.Text.Length > 0)
            {
                query = "select c.*,'Doble click' as Seleccionar from GITAR_HEROES.Cliente c where nro_doc = " + textBoxNumeroDocumento.Text + (comboBoxTipoDocumento.Text.Length > 0 ? " and tipo_doc = " + lista_codigos_dni[comboBoxTipoDocumento.SelectedIndex] : "") + (textBoxEmail.Text.Length > 0 ? " and mail = '" + textBoxEmail.Text + "'" : "");
            }
            else if (comboBoxTipoDocumento.Text.Length > 0)
            {
                query = "select c.*,'Doble click' as Seleccionar from GITAR_HEROES.Cliente c where tipo_doc = " + lista_codigos_dni[comboBoxTipoDocumento.SelectedIndex] + (textBoxEmail.Text.Length > 0 ? " and mail = '" + textBoxEmail.Text + "'" : "");
            }
            else if (textBoxEmail.Text.Length > 0)
            {
                query = "select c.*,'Doble click' as Seleccionar from GITAR_HEROES.Cliente c where mail = '" + textBoxEmail.Text + "'";
            }
            else
            {
                query = "select c.*,'Doble click' as Seleccionar from GITAR_HEROES.Cliente c";
            }
            command = new SqlCommand(query);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            if (!dataTable.HasErrors)
            {
                dataGridViewClientes.DataSource = dataTable;
                foreach (DataRow row in dataTable.Rows)
                {
                    lista_mails.Add(row["mail"].ToString());
                }
            }
        }

        private void buttonLimpiar_Click(object sender, EventArgs e)
        {
            textBoxApellido.Text = "";
            textBoxNombre.Text = "";
            textBoxNumeroDocumento.Text = "";
            comboBoxTipoDocumento.Text = "";
            comboBoxTipoDocumento.SelectedIndex = -1;
            textBoxEmail.Text = "";
        }
    }
}
