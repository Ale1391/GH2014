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

namespace FrbaHotel.Facturar_Estadia
{
    public partial class FacturarEstadia : Form
    {               
        public FacturarEstadia()
        {
            InitializeComponent();

            comboBoxFormaPago.Items.Add("Efectivo");
            comboBoxFormaPago.Items.Add("Tarjeta de Credito");
        }

        private void FacturarEstadia_Load(object sender, EventArgs e)
        {
            // Se deshabilitan por defecto otras acciones
            linkLabelReservar.Enabled = false;

            // Se conecta con la base de datos
            iniciarConexion();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxFormaPago.Text == "Efectivo")
                textBoxNumTarjeta.Enabled = false;
            else
            {
                textBoxNumTarjeta.Enabled = true;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Funcionalidades funcionalidadesForm = new Funcionalidades();
            funcionalidadesForm.StartPosition = FormStartPosition.CenterScreen;
            funcionalidadesForm.Show();
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

        private void button1_Click(object sender, EventArgs e)
        {  
            if (textBoxCodReserva.Text.Length == 0)
            {
                MessageBox.Show("Falta especificar Codigo de Reserva.");
            }
            else if (comboBoxFormaPago.Text.Length == 0)
            {
                MessageBox.Show("Falta especificar Tipo de Pago.");
            }
            else
            {
                facturar();

                // Se limpia el contenido de los campos
                textBoxCodReserva.Text = "";
                comboBoxFormaPago.Text = "";
                textBoxNumTarjeta.Text = "";

                // Se habilita la posibilidad de reservar mas dias
                linkLabelReservar.Enabled = true;
            }

        }

        // Procedimiento para efectuar la facturacion
        private void facturar()
        {
            try
            {
                // Se utilizan las variables grobales definidas en Variables.cs
                using (SqlConnection con = new SqlConnection(Variables.connectionStr))
                {
                    using (SqlCommand cmd = new SqlCommand("GITAR_HEROES.facturar", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Se pasa el hotel logueado y la reserva como primeros parametros
                        cmd.Parameters.Add("@codigo_hotel", SqlDbType.Int).Value = Variables.hotel_id;
                        cmd.Parameters.Add("@codigo_reserva", SqlDbType.Int).Value = Convert.ToInt32(textBoxCodReserva.Text);

                        // Dependiendo del contenido del combo se pasan los demas parametros
                        if (comboBoxFormaPago.Text == "Efectivo")
                        {
                            cmd.Parameters.Add("@codigo_tipo_pago", SqlDbType.Int).Value = 1;
                            cmd.Parameters.Add("@nro_tarjeta", SqlDbType.Int).Value = 0;
                            //MessageBox.Show("Tipo de pago en efectivo");
                        }
                        else if (comboBoxFormaPago.Text == "Tarjeta de Credito")
                        {
                            cmd.Parameters.Add("@codigo_tipo_pago", SqlDbType.Int).Value = 2;
                            cmd.Parameters.Add("@nro_tarjeta", SqlDbType.Int).Value = Convert.ToInt32(textBoxNumTarjeta.Text);
                            //MessageBox.Show("Tipo de pago por Tarjeta");
                        }

                        con.Open();
                        cmd.ExecuteNonQuery();

                    }
                }

                MessageBox.Show("Facturacion realizada correctamente.");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
            }
        }

        private void textBoxNumTarjeta_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabelReservar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Reserva reserva = new Reserva();
            reserva.StartPosition = FormStartPosition.CenterScreen;
            reserva.Show();

        }
        
    }

}
