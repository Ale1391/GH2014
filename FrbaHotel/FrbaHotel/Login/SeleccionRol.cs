using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaHotel.Login
{
    public partial class SeleccionRol : Form
    {
        
        // Se declaran las variables para llenar el comboBox
        System.Data.SqlClient.SqlConnection connection;
        private SqlCommand command;
        private SqlDataAdapter adapter;
        private DataTable dataTable;

        // Se crean las listas a utilizarse para llenar el comboBox
        private List<int> lista_codigo_roles = new List<int>();

        public SeleccionRol()
        {
            InitializeComponent();
        }

        private void SeleccionRol_Load(object sender, EventArgs e)
        {
            cargarComboBoxRoles();
        }

        private void cargarComboBoxRoles()
        {
            connection = new System.Data.SqlClient.SqlConnection();
            try
            {
                connection.ConnectionString = Variables.connectionStr;
                connection.Open();

                //string query = "select * from GITAR_HEROES.Usuario where username = '"+nombre_usuario+"'";

                string query = "SELECT RU.codigo_rol, R.descripcion FROM GITAR_HEROES.RolUsuario RU JOIN GITAR_HEROES.Rol R ON RU.codigo_rol = R.codigo WHERE username = '" + Variables.usuario + "'";
                command = new SqlCommand(query);
                command.Connection = connection;
                adapter = new SqlDataAdapter(command);
                dataTable = new DataTable();
                adapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    comboBoxRoles.Items.Add(row["descripcion"].ToString());
                    lista_codigo_roles.Add(Convert.ToInt32(row["codigo_rol"]));
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc);
            }
        }
    }
}
