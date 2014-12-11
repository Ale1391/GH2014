using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using FrbaHotel.Listado_Funcionalidades;
using FrbaHotel.Generar_Modificar_Reserva;
using FrbaHotel.Facturar_Estadia;
using FrbaHotel.ABM_de_Cliente;

namespace FrbaHotel.Registrar_Estadia
{
    public partial class RegistrarEstadia : Form
    {
        private int estado_registro;
                
        public RegistrarEstadia()
        {
            InitializeComponent();
        }

        private void RegistrarEstadia_Load(object sender, EventArgs e)
        {
            // Se desactivan los links no disponibles al iniciar la funcionalidad
            linkLabelReservar.Enabled = false;
            linkLabelClientes.Enabled = false;

            // Se conecta con la base de datos
            iniciarConexion();
        }

        private void iniciarConexion()
        {
            // Genera conexion con la base de datos
            System.Data.SqlClient.SqlConnection connection;
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Funcionalidades funcionalidadesForm = new Funcionalidades();
            funcionalidadesForm.StartPosition = FormStartPosition.CenterScreen;
            funcionalidadesForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxCodReserva.Text.Length == 0)
            {
                MessageBox.Show("Falta especificar Codigo de Reserva.");
            }
            else if (textBoxFecha.Text.Length == 0)
            {
                MessageBox.Show("Falta especificar fecha.");
            }
            else
            {
                ingresoEgresoEstadia();
               
                // Se borran los datos ingresados para un nuevo registro de estadia
                this.textBoxCodReserva.Text = "";
                this.textBoxFecha.Text = "";
            }
            
        }

        private void ingresoEgresoEstadia()
        {
            try
            {
                // Se utilizan las variables grobales definidas en Variables.cs
                using (SqlConnection con = new SqlConnection(Variables.connectionStr))
                {
                    using (SqlCommand cmd = new SqlCommand("GITAR_HEROES.ingresoEgresoEstadia", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Se pasa el hotel logueado y la reserva como primeros parametros
                        cmd.Parameters.Add("@codigo_hotel", SqlDbType.Int).Value = Variables.hotel_id;
                        cmd.Parameters.Add("@codigo_reserva", SqlDbType.Int).Value = Convert.ToInt32(textBoxCodReserva.Text);
                        cmd.Parameters.Add("@fecha", SqlDbType.Date).Value = Convert.ToDateTime(textBoxFecha.Text);
                        cmd.Parameters.Add("@username", SqlDbType.Char).Value = Variables.usuario;
                        
                        var returnParameter = cmd.Parameters.Add("@estado_registro", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.Output;

                        con.Open();
                        cmd.ExecuteNonQuery();

                        estado_registro = (int)returnParameter.Value;
                        //estado_registro = estado_registro.ToString();

                    }
                }
                if (estado_registro == 1)
                {
                    MessageBox.Show("Check in de estadia realizado correctamente.");

                    // Se activa la funcion para reservar nuevamente en el caso de haberse perdido la estadia
                    linkLabelClientes.Enabled = true;
                }
                else if (estado_registro == 0)
                {
                    MessageBox.Show("Check out de estadia realizado correctamente.");
                    
                    // Cerramos la funcionalidad actual y procedemos a facturar directamente la estadia
                    this.Hide();
                    FacturarEstadia facturar_estadia = new FacturarEstadia();
                    facturar_estadia.StartPosition = FormStartPosition.CenterScreen;
                    facturar_estadia.Show();

                }
                else if (estado_registro == -1)
                {
                    MessageBox.Show("Check in NO realizado correctamente. Genere una nueva reserva.");

                    // Se activa la funcion para reservar nuevamente en el caso de haberse perdido la estadia
                    linkLabelReservar.Enabled = true;
                }
                    
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
            } 
        }

        private void linkLabelReservar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Reserva reserva = new Reserva();
            reserva.StartPosition = FormStartPosition.CenterScreen;
            reserva.Show();
        }

        private void linkLabelClientes_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Cliente cliente = new Cliente();
            cliente.StartPosition = FormStartPosition.CenterScreen;
            cliente.Show();
        }
    }
}
