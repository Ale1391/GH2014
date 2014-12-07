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
    public partial class ClienteForm : Form
    {
        private List<int> lista_codigos_tipo_documento = new List<int>();
        private List<string> lista_nombres_tipo_documento = new List<string>();

        System.Data.SqlClient.SqlConnection connection;
        private SqlCommand command;
        private SqlDataAdapter adapter;
        private DataTable dataTable;

        public ClienteForm()
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                insertarCliente();
                MessageBox.Show("Cliente creado exitosamente.");
            }
            catch (Exception exc)
            {
                MessageBox.Show("No se pudo crear el nuevo cliente. Error: " + exc);
            }
        }

        private void insertarCliente()
        {
            string query = "INSERT INTO GITAR_HEROES.Cliente (nombre,apellido,tipo_doc,nro_doc,mail,telefono,domicilio_calle,nacionalidad,fecha_nacimiento,estado) VALUES ('"+textBoxNombre.Text+"','"+textBoxApellido.Text+"',"+lista_codigos_tipo_documento[comboBoxTipoDocumento.SelectedIndex].ToString()+","+textBoxDocumento.Text+",'"+textBoxMail.Text+"',"+textBoxTelefono.Text+",'"+textBoxDireccion.Text+"','"+textBoxNacionalidad.Text+"','"+textBox1FechaNacimiento.Text+" 00:00:00"+"',1)";
            command = new SqlCommand(query);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            if (dataTable.HasErrors)
            {
                MessageBox.Show("Error al realizar update del rol.");
            }
            else
            {
                
            }
        }

        private void ClienteForm_Load(object sender, EventArgs e)
        {
            iniciarConexion();
            llenarComboBoxTipoDocumento();
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
    }
}
