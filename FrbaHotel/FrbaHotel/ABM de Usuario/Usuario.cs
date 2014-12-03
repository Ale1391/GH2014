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
    public partial class Usuario : Form
    {
        System.Data.SqlClient.SqlConnection connection;
        private SqlCommand command;
        private SqlDataAdapter adapter;
        private DataTable dataTable;

        public Usuario()
        {
            InitializeComponent();
            comboBoxFuncionalidad.Items.Add("Editar/Eliminar Usuario existente");
            comboBoxFuncionalidad.Items.Add("Crear Usuario Nuevo");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Funcionalidades funcionalidadesForm = new Funcionalidades();
            funcionalidadesForm.StartPosition = FormStartPosition.CenterScreen;
            funcionalidadesForm.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxFuncionalidad.Text == "Editar/Eliminar Usuario existente")
            {
                usuarioTextbox.Enabled = true;
            }
            else if (comboBoxFuncionalidad.Text == "Crear Usuario Nuevo")
            {
                usuarioTextbox.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBoxFuncionalidad.Text.Length == 0)
            {
                MessageBox.Show("Primero debes elegir una opción.");
            }
            else if (comboBoxFuncionalidad.Text == "Editar/Eliminar Usuario existente" && usuarioTextbox.Text.Length == 0)
            {
                MessageBox.Show("Primero debes ingresar un usuario valido.");
            }
            else if (comboBoxFuncionalidad.Text == "Crear Usuario Nuevo")
            {
                this.Hide();
                UsuarioForm abm_usuario = new UsuarioForm();
                abm_usuario.nombre_usuario = "";
                abm_usuario.StartPosition = FormStartPosition.CenterScreen;
                abm_usuario.ShowDialog();
            }
            else
            {

                connection = new System.Data.SqlClient.SqlConnection();
                try
                {
                    string connectionStr = "Data Source=localhost\\SQLSERVER2008;Initial Catalog=GD2C2014;User ID=gd;Password=gd2014";
                    //connection = new SqlConnection(connectionStr);
                    connection.ConnectionString = connectionStr;
                    connection.Open();
                    string query = "select * from GITAR_HEROES.Usuario where username = '" + usuarioTextbox.Text + "'";
                    command = new SqlCommand(query);
                    command.Connection = connection;
                    adapter = new SqlDataAdapter(command);
                    dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        this.Hide();
                        UsuarioForm abm_usuario = new UsuarioForm();
                        abm_usuario.nombre_usuario = usuarioTextbox.Text;
                        abm_usuario.StartPosition = FormStartPosition.CenterScreen;
                        abm_usuario.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Error: No existe ningún usuario con dicho nombre.");
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Error: " + exc);
                }

                
            }
        }
    }
}
