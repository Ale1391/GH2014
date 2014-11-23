using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrbaHotel.Listado_Funcionalidades
{
    public partial class Funcionalidades : Form
    {
        System.Data.SqlClient.SqlConnection connection;
        //private SqlConnection connection;
        private SqlCommand command;
        private SqlDataAdapter adapter;
        private DataTable dataTable;


        public Funcionalidades()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void InitializecomboBoxFuncionalidades()
        {
            string[] test = new string[] { "uno", "dos" };
            comboBoxFuncionalidades.Items.AddRange(test);
        }

        private void Funcionalidades_Load(object sender, EventArgs e)
        {
            //this.InitializecomboBoxFuncionalidades();
            connection = new System.Data.SqlClient.SqlConnection();
            try
            {
                string connectionStr = "Data Source=localhost\\SQLSERVER2008;Initial Catalog=GD2C2014;User ID=gd;Password=gd2014";
                //connection = new SqlConnection(connectionStr);
                connection.ConnectionString = connectionStr;
                connection.Open();
                command = new SqlCommand("select GITAR_HEROES.Funcionalidad.descripcion from GITAR_HEROES.Funcionalidad INNER JOIN GITAR_HEROES.RolFuncionalidad ON GITAR_HEROES.Funcionalidad.codigo = GITAR_HEROES.RolFuncionalidad.codigo_funcionalidad AND GITAR_HEROES.RolFuncionalidad.codigo_rol = 3");
                command.Connection = connection;
                adapter = new SqlDataAdapter(command);
                dataTable = new DataTable();
                adapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    comboBoxFuncionalidades.Items.Add(row["descripcion"].ToString());
                }


                
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc);
            }


        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Principal prinForm = new Principal();
            prinForm.Show();
        }
    }
}
