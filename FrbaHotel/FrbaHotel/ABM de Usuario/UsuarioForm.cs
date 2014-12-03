using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrbaHotel.Listado_Funcionalidades;
using System.Data.SqlClient;

namespace FrbaHotel.ABM_de_Usuario
{
    public partial class UsuarioForm : Form
    {
        public string nombre_usuario;
        System.Data.SqlClient.SqlConnection connection;
        private SqlCommand command;
        private SqlDataAdapter adapter;
        private DataTable dataTable;

        public UsuarioForm()
        {
            InitializeComponent();
        }

        private void cargarFormulario()
        {
            connection = new System.Data.SqlClient.SqlConnection();
            try
            {
                string connectionStr = "Data Source=localhost\\SQLSERVER2008;Initial Catalog=GD2C2014;User ID=gd;Password=gd2014";
                connection.ConnectionString = connectionStr;
                connection.Open();
                string query = "select * from GITAR_HEROES.Usuario where username = '"+nombre_usuario+"'";
                command = new SqlCommand(query);
                command.Connection = connection;
                adapter = new SqlDataAdapter(command);
                dataTable = new DataTable();
                adapter.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    textBoxUsuario.Text = dataTable.Rows[0]["username"].ToString();
                    //textBoxClave.Text = dataTable.Rows[0]["password"].ToString();
                    textBoxNombre.Text = dataTable.Rows[0]["nombre"].ToString();
                    textBoxApellido.Text = dataTable.Rows[0]["apellido"].ToString();
                    textBoxDni.Text = dataTable.Rows[0]["nro_doc"].ToString();
                    textBoxMail.Text = dataTable.Rows[0]["mail"].ToString();
                    textBoxTelefono.Text = dataTable.Rows[0]["telefono"].ToString();
                    textBoxDireccion.Text = dataTable.Rows[0]["domicilio_calle"].ToString();
                    textBoxFechaNac.Text = dataTable.Rows[0]["fecha_nacimiento"].ToString();
                
                    string query2 = "select descripcion from GITAR_HEROES.Rol inner join GITAR_HEROES.RolUsuario on GITAR_HEROES.Rol.codigo = GITAR_HEROES.RolUsuario.codigo_rol where username = '"+nombre_usuario+"'";
                    command = new SqlCommand(query2);
                    command.Connection = connection;
                    adapter = new SqlDataAdapter(command);
                    dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        comboBoxRol.Text = dataTable.Rows[0]["descripcion"].ToString();
                    }

                    string query3 = "select descripcion from GITAR_HEROES.TipoDocumento inner join GITAR_HEROES.Usuario on GITAR_HEROES.TipoDocumento.codigo = GITAR_HEROES.Usuario.tipo_doc where username = '"+nombre_usuario+"'";
                    command = new SqlCommand(query3);
                    command.Connection = connection;
                    adapter = new SqlDataAdapter(command);
                    dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        comboBoxTipodni.Text = dataTable.Rows[0]["descripcion"].ToString();
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc);
            }
        }

        private void llenarCheckedlist()
        {
            string query = "select domicilio_calle from GITAR_HEROES.Hotel inner join GITAR_HEROES.UsuarioHotel on GITAR_HEROES.UsuarioHotel.codigo_hotel = GITAR_HEROES.Hotel.codigo where GITAR_HEROES.UsuarioHotel.username = '" + nombre_usuario + "'";
            command = new SqlCommand(query);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            foreach (DataRow row in dataTable.Rows)
            {
                hotelesChecklist.Items.Add(row["domicilio_calle"].ToString(),true);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Usuario funcionalidadesForm = new Usuario();
            funcionalidadesForm.StartPosition = FormStartPosition.CenterScreen;
            funcionalidadesForm.Show();
        }

        private void UsuarioForm_Load(object sender, EventArgs e)
        {
            if (nombre_usuario.Length > 0)
            {
                cargarFormulario();
                llenarCheckedlist();
            }
        }
    }
}
