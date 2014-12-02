using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Data.SqlClient;
using FrbaHotel.Listado_Funcionalidades;

namespace FrbaHotel.Login
{
    public partial class LoginForm : Form
    {
        System.Data.SqlClient.SqlConnection connection;
        private SqlCommand command;
        private SqlDataAdapter adapter;
        private DataTable dataTable;

        public LoginForm()
        {
            InitializeComponent();
            this.Location = new Point(100, 100);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string usuario = usuarioTextbox.Text;
            string contrasenia = contraseniaTextbox.Text;

            SHA256 sha256 = SHA256.Create();
            byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(contrasenia));
            string hashstring = string.Empty;
            foreach (byte x in hash)
            {
                hashstring += string.Format("{0:x2}", x);
            }
            connection = new System.Data.SqlClient.SqlConnection();
            try
            {
                string connectionStr = "Data Source=localhost\\SQLSERVER2008;Initial Catalog=GD2C2014;User ID=gd;Password=gd2014";
                connection.ConnectionString = connectionStr;
                connection.Open();
                string query = "select username from GITAR_HEROES.Usuario where username = '" + usuario + "' and password = '" + hashstring + "'";
                command = new SqlCommand(query);
                command.Connection = connection;
                adapter = new SqlDataAdapter(command);
                dataTable = new DataTable();
                adapter.Fill(dataTable);

                if (dataTable.Rows.Count>0)
                {
                    //LOGIN SUCCESS

                    command = new SqlCommand("select GITAR_HEROES.Rol.descripcion from GITAR_HEROES.RolUsuario inner join GITAR_HEROES.Rol on GITAR_HEROES.Rol.codigo = GITAR_HEROES.RolUsuario.codigo_rol where username = '"+usuario+"'");
                    adapter = new SqlDataAdapter(command);
                    dataTable = new DataTable();
                    command.Connection = connection;
                    adapter.Fill(dataTable);
                    foreach (DataRow row in dataTable.Rows)
                    {
                        string descrip_rol = row["descripcion"].ToString();
                        Variables.tipo_usuario = descrip_rol;
                        Variables.usuario = usuario;
                    }
                    //
                    this.Hide();
                    Funcionalidades func = new Funcionalidades();
                    func.StartPosition = FormStartPosition.CenterScreen;
                     func.ShowDialog();
                }
                else 
                {
                    //LOGIN FAILURE
                    MessageBox.Show("Usuario y/oContraseña incorrecta");
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc);
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Principal prinForm = new Principal();
            prinForm.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
