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
using FrbaHotel.ABM_de_Usuario;
using FrbaHotel.ABM_de_Rol;
using FrbaHotel.ABM_de_Cliente;
using FrbaHotel.ABM_de_Hotel;
using FrbaHotel.ABM_de_Habitacion;
using FrbaHotel.Generar_Modificar_Reserva;
using FrbaHotel.Registrar_Estadia;
using FrbaHotel.Facturar_Estadia;
using FrbaHotel.Listado_Estadistico;

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
                connection.ConnectionString = Variables.connectionStr;
                connection.Open();
                command = new SqlCommand("SELECT GITAR_HEROES.Funcionalidad.descripcion, GITAR_HEROES.Funcionalidad.codigo FROM GITAR_HEROES.Funcionalidad INNER JOIN GITAR_HEROES.RolFuncionalidad ON GITAR_HEROES.Funcionalidad.codigo = GITAR_HEROES.RolFuncionalidad.codigo_funcionalidad WHERE GITAR_HEROES.RolFuncionalidad.codigo_rol = (select codigo from GITAR_HEROES.Rol where descripcion = '" + Variables.tipo_usuario + "')");
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
            //CONTINUAR
            if (codigo_funcionalidad == 9)
            {
                //CANCELAR RESERVA
                this.Hide();
                CancelarReserva cancelar_reserva = new CancelarReserva();
                cancelar_reserva.StartPosition = FormStartPosition.CenterScreen;
                cancelar_reserva.ShowDialog();
            }
            else if (codigo_funcionalidad == 3)
            {
                //ABM USUARIO
                this.Hide();
                Usuario abm_usuario = new Usuario();
                abm_usuario.StartPosition = FormStartPosition.CenterScreen;
                abm_usuario.ShowDialog();
            }
            else if (codigo_funcionalidad == 1)
            {
                //ABM ROL
                this.Hide();
                Rol abm_rol = new Rol();
                abm_rol.StartPosition = FormStartPosition.CenterScreen;
                abm_rol.ShowDialog();
            }
            else if (codigo_funcionalidad == 4)
            {
                //ABM CLIENTE
                this.Hide();
                Cliente abm_cliente = new Cliente();
                abm_cliente.StartPosition = FormStartPosition.CenterScreen;
                abm_cliente.ShowDialog();
            }
            else if (codigo_funcionalidad == 5)
            {
                //ABM HOTEL
                this.Hide();
                Hotel abm_hotel = new Hotel();
                abm_hotel.StartPosition = FormStartPosition.CenterScreen;
                abm_hotel.ShowDialog();
            }
            else if (codigo_funcionalidad == 6)
            {
                //ABM HABITACION
                this.Hide();
                Habitacion abm_habitacion = new Habitacion();
                abm_habitacion.StartPosition = FormStartPosition.CenterScreen;
                abm_habitacion.ShowDialog();
            }
            else if (codigo_funcionalidad == 8)
            {
                //GENERAR O MODIFICAR RESERVA
                this.Hide();
                Reserva abm_reserva = new Reserva();
                abm_reserva.StartPosition = FormStartPosition.CenterScreen;
                abm_reserva.ShowDialog();
            }
            else if (codigo_funcionalidad == 10)
            {
                //REGISTRAR ESTADIA
                this.Hide();
                RegistrarEstadia registrar_estadia = new RegistrarEstadia();
                registrar_estadia.StartPosition = FormStartPosition.CenterScreen;
                registrar_estadia.ShowDialog();

            }
            
            else if (codigo_funcionalidad == 12)
            {
                //FACTURAR ESTADIA
                this.Hide();
                FacturarEstadia facturar_estadia = new FacturarEstadia();
                facturar_estadia.StartPosition = FormStartPosition.CenterScreen;
                facturar_estadia.ShowDialog();

            }
            else if (codigo_funcionalidad == 13)
            {
                //LISTADO ESTADISTICO
                this.Hide();
                ListadoEstadistico listado_estadistico = new ListadoEstadistico();
                listado_estadistico.StartPosition = FormStartPosition.CenterScreen;
                listado_estadistico.ShowDialog();
            }
        }
    }
}
