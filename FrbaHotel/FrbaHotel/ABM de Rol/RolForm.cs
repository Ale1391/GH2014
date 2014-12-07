using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaHotel.ABM_de_Rol
{
    public partial class RolForm : Form
    {
        public string nombre_rol;
        public int codigo_rol;
        System.Data.SqlClient.SqlConnection connection;
        private SqlCommand command;
        private SqlDataAdapter adapter;
        private DataTable dataTable;

        private List<int> lista_codigos_rol = new List<int>();
        private List<int> lista_codigos_funcionalidades = new List<int>();
        private List<string> lista_nombres_rol = new List<string>();

        public RolForm()
        {
            InitializeComponent();
        }

        private void RolForm_Load(object sender, EventArgs e)
        {
            textBoxRol.Text = nombre_rol;
            iniciarConexion();
            validarEstado();
            llenarListaFuncionalidades();
            tildarListaFuncionalidades();
        }

        private void validarEstado()
        {
            string query = "select * from GITAR_HEROES.Rol where descripcion = '" + nombre_rol + "'";
            command = new SqlCommand(query);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            if (dataTable.Rows.Count > 0)
            {
                //EDICION ROL
                if (dataTable.Rows[0]["estado"].ToString() == "1")
                {
                    checkBoxRolActivo.Checked = true;
                }
            }
            else
            {
                //NUEVO ROL
                checkBoxRolActivo.Checked = true;
                checkBoxRolActivo.Enabled = false;
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

        private void llenarListaFuncionalidades()
        {
            string query = "select * from GITAR_HEROES.Funcionalidad";
            command = new SqlCommand(query);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            foreach (DataRow row in dataTable.Rows)
            {
                checkedListBoxFuncionalidades.Items.Add(row["descripcion"].ToString());
                lista_codigos_funcionalidades.Add(Convert.ToInt32(row["codigo"]));
            }
        }

        private void tildarListaFuncionalidades()
        {
            string query = "SELECT GITAR_HEROES.Funcionalidad.descripcion, GITAR_HEROES.Funcionalidad.codigo FROM GITAR_HEROES.Funcionalidad INNER JOIN GITAR_HEROES.RolFuncionalidad ON GITAR_HEROES.Funcionalidad.codigo = GITAR_HEROES.RolFuncionalidad.codigo_funcionalidad WHERE GITAR_HEROES.RolFuncionalidad.codigo_rol = (select codigo from GITAR_HEROES.Rol where descripcion = '" + nombre_rol + "')";
            command = new SqlCommand(query);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            foreach (DataRow row in dataTable.Rows)
            {
                for (int i = 0; i < checkedListBoxFuncionalidades.Items.Count; i++)
                {
                    object funcionalidad = checkedListBoxFuncionalidades.Items[i];
                    if (funcionalidad.ToString() == row["descripcion"].ToString())
                    {
                        checkedListBoxFuncionalidades.SetItemChecked(i, true);
                    }
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Rol pantalla_rol = new Rol();
            pantalla_rol.StartPosition = FormStartPosition.CenterScreen;
            pantalla_rol.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (nombre_rol.Length > 0)
            {
                try
                {
                    realizarUpdateRol();
                    eliminarFuncionalidades();
                    agregarFuncionalidades();
                    MessageBox.Show("Rol editado exitosamente.");
                }
                catch (Exception exc)
                {
                    MessageBox.Show("No se pudo realizar la edicion del rol. Error: " + exc);
                }
            }
            else
            {
                try
                {
                    insertarRol();
                    agregarFuncionalidades();
                    MessageBox.Show("Rol creado exitosamente.");
                }
                catch (Exception exc)
                {
                    MessageBox.Show("No se pudo crear el nuevo rol. Error: " + exc);
                }
            }
        }

        private void insertarRol()
        {
            string query = "INSERT INTO GITAR_HEROES.Rol (descripcion, estado) VALUES ('" + textBoxRol.Text + "',1)";
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
                string query2 = "select * from GITAR_HEROES.Rol where descripcion ='" + textBoxRol.Text + "'";
                command = new SqlCommand(query2);
                command.Connection = connection;
                adapter = new SqlDataAdapter(command);
                dataTable = new DataTable();
                adapter.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    codigo_rol = Convert.ToInt32(dataTable.Rows[0]["codigo"].ToString());
                }
            }
        }

        private void realizarUpdateRol()
        {
            string query = "UPDATE GITAR_HEROES.Rol SET descripcion = '" + textBoxRol.Text + "', estado = " + (checkBoxRolActivo.Checked ? "1" : "0") + " WHERE descripcion = '" + nombre_rol + "'";
            command = new SqlCommand(query);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            if (dataTable.HasErrors)
            {
                MessageBox.Show("Error al realizar update del rol.");
            }
        }

        private void eliminarFuncionalidades()
        {
            string query = "delete from GITAR_HEROES.RolFuncionalidad where codigo_rol = "+codigo_rol;
            command = new SqlCommand(query);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            if (dataTable.HasErrors)
            {
                MessageBox.Show("Error al realizar update del rol.");
            }
        }

        private void agregarFuncionalidades()
        {
            foreach (int indices_funcionalidades in checkedListBoxFuncionalidades.CheckedIndices) 
            {
                string codigo_funcionalidad = lista_codigos_funcionalidades[indices_funcionalidades].ToString();
                string query = "INSERT INTO GITAR_HEROES.RolFuncionalidad (codigo_rol, codigo_funcionalidad) VALUES ("+codigo_rol+","+codigo_funcionalidad+")";
                command = new SqlCommand(query);
                command.Connection = connection;
                adapter = new SqlDataAdapter(command);
                dataTable = new DataTable();
                adapter.Fill(dataTable);

                if (dataTable.HasErrors)
                {
                    MessageBox.Show("Error al realizar update del rol.");
                }
            }
        }
    }
}
