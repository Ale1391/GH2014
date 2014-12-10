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

namespace FrbaHotel.Facturar_Estadia
{
    public partial class FacturarEstadia : Form
    {
        // Genera conexion con la base de datos
        System.Data.SqlClient.SqlConnection connection;
        private SqlCommand command;
        private SqlDataAdapter adapter;
        private DataTable dataTable;

        
        public FacturarEstadia()
        {
            InitializeComponent();

            comboBoxFormaPago.Items.Add("Efectivo");
            comboBoxFormaPago.Items.Add("Tarjeta de Credito");

        }

        private void FacturarEstadia_Load(object sender, EventArgs e)
        {
            // Se conecta con la base de datos
            iniciarConexion();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

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
                MessageBox.Show("Faltan especificar Codigo de Reserva.");
            }
            else if (comboBoxFormaPago.Text.Length == 0)
            {
                MessageBox.Show("Faltan especificar Tipo de Pago.");
            }
            else
            {
                


                facturar();
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

                        // Se pasa la reserva como primer parametro
                        cmd.Parameters.Add("@codigo_reserva", SqlDbType.Int).Value = Convert.ToInt32(textBoxCodReserva.Text);

                        if (comboBoxFormaPago.Text == "Efectivo")
                        {
                            cmd.Parameters.Add("@codigo_tipo_pago", SqlDbType.Int).Value = 1;
                            cmd.Parameters.Add("@nro_tarjeta", SqlDbType.Int).Value = 0;
                            textBoxNumTarjeta.Enabled = false;
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

        private int trabajaSobreHotelReservado
        {
            //return 1
        }

        private void textBoxNumTarjeta_TextChanged(object sender, EventArgs e)
        {

        }
        
    }

}
