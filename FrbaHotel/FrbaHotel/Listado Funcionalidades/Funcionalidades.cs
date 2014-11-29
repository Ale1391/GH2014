using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrbaHotel.Cancelar_Reserva;

namespace FrbaHotel.Listado_Funcionalidades
{
    public partial class Funcionalidades : Form
    {
        System.Data.SqlClient.SqlConnection connection;
        private SqlCommand command;
        private SqlDataAdapter adapter;
        private DataTable dataTable;
        private int codigo_funcionalidad;
        private List<int> lista_codigos_funcionalidad = new List<int>();

        public Funcionalidades()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            codigo_funcionalidad = lista_codigos_funcionalidad[comboBoxFuncionalidades.SelectedIndex];
            //MessageBox.Show("codigo elegido: " + codigo_funcionalidad.ToString());
        }

        private void Funcionalidades_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            connection = new System.Data.SqlClient.SqlConnection();
            try
            {
                string connectionStr = "Data Source=localhost\\SQLSERVER2008;Initial Catalog=GD2C2014;User ID=gd;Password=gd2014";
                //connection = new SqlConnection(connectionStr);
                connection.ConnectionString = connectionStr;
                connection.Open();
                command = new SqlCommand("SELECT GITAR_HEROES.Funcionalidad.descripcion, GITAR_HEROES.Funcionalidad.codigo FROM GITAR_HEROES.Funcionalidad INNER JOIN GITAR_HEROES.RolFuncionalidad ON GITAR_HEROES.Funcionalidad.codigo = GITAR_HEROES.RolFuncionalidad.codigo_funcionalidad WHERE GITAR_HEROES.RolFuncionalidad.codigo_rol = 3");
                command.Connection = connection;
                adapter = new SqlDataAdapter(command);
                dataTable = new DataTable();
                adapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    comboBoxFuncionalidades.Items.Add(row["descripcion"].ToString());
                    lista_codigos_funcionalidad.Add(Convert.ToInt16(row["codigo"]));
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (codigo_funcionalidad == 9)
            {
                //CONTINUAR
                this.Hide();
                CancelarReserva cancelar_reserva = new CancelarReserva();
                cancelar_reserva.StartPosition = FormStartPosition.CenterScreen;
                cancelar_reserva.ShowDialog();
            }
        }
    }
}
