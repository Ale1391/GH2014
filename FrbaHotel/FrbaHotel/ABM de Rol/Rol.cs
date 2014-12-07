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

namespace FrbaHotel.ABM_de_Rol
{
    public partial class Rol : Form
    {
        private List<int> lista_codigos_rol = new List<int>();

        System.Data.SqlClient.SqlConnection connection;
        private SqlCommand command;
        private SqlDataAdapter adapter;
        private DataTable dataTable;

        public Rol()
        {
            InitializeComponent();
            comboBoxFuncionalidad.Items.Add("Editar Rol Existente");
            comboBoxFuncionalidad.Items.Add("Crear Rol Nuevo");
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
            if (comboBoxFuncionalidad.Text == "Editar Rol Existente")
            {
                comboBoxRoles.Enabled = true;
                connection = new System.Data.SqlClient.SqlConnection();
                if (comboBoxRoles.Items.Count < 1)
                {
                    try
                    {
                        connection.ConnectionString = Variables.connectionStr;
                        connection.Open();

                        string query = "select * from GITAR_HEROES.Rol";
                        command = new SqlCommand(query);
                        command.Connection = connection;
                        adapter = new SqlDataAdapter(command);
                        dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        foreach (DataRow row in dataTable.Rows)
                        {
                            comboBoxRoles.Items.Add(row["descripcion"].ToString());
                            lista_codigos_rol.Add(Convert.ToInt32(row["codigo"]));
                        }
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Error: " + exc);
                    }
                }
            }
            else if (comboBoxFuncionalidad.Text == "Crear Rol Nuevo")
            {
                comboBoxRoles.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBoxFuncionalidad.Text.Length == 0)
            {
                MessageBox.Show("Primero debes elegir una opción.");
            }
            else if (comboBoxFuncionalidad.Text == "Editar Rol Existente" && comboBoxRoles.Text.Length == 0)
            {
                MessageBox.Show("Primero debes elegir un rol.");
            }
            else if (comboBoxFuncionalidad.Text == "Crear Rol Nuevo")
            {
                this.Hide();
                RolForm abm_rol = new RolForm();
                abm_rol.nombre_rol = "";
                abm_rol.StartPosition = FormStartPosition.CenterScreen;
                abm_rol.ShowDialog();
            }
            else
            {
                this.Hide();
                RolForm abm_rol = new RolForm();
                abm_rol.nombre_rol = comboBoxRoles.Text;
                abm_rol.codigo_rol = lista_codigos_rol[comboBoxRoles.SelectedIndex];
                abm_rol.StartPosition = FormStartPosition.CenterScreen;
                abm_rol.ShowDialog();
            }
        }

        private void Rol_Load(object sender, EventArgs e)
        {
            comboBoxRoles.Enabled = false;
        }
    }
}
