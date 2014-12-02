using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using FrbaHotel.Listado_Funcionalidades;

namespace FrbaHotel.Cancelar_Reserva
{
    public partial class CancelarReserva : Form
    {
        System.Data.SqlClient.SqlConnection connection;
        private SqlCommand command;
        private SqlDataAdapter adapter;
        private DataTable dataTable;

        public CancelarReserva()
        {
            InitializeComponent();
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
            connection = new System.Data.SqlClient.SqlConnection();
            try
            {
                string connectionStr = "Data Source=localhost\\SQLSERVER2008;Initial Catalog=GD2C2014;User ID=gd;Password=gd2014";
                connection.ConnectionString = connectionStr;
                connection.Open();
                command = new SqlCommand("select fecha_inicio,codigo_estado,codigo_hotel from GITAR_HEROES.Reserva where codigo = " + numero_reserva.Text);
                command.Connection = connection;
                adapter = new SqlDataAdapter(command);
                dataTable = new DataTable();
                adapter.Fill(dataTable);

                if (dataTable.Rows.Count == 0)
                {
                    MessageBox.Show("Error: No se tiene ninguna reserva hecha con el número ingresado.");
                }
                else if (dataTable.Rows.Count == 1)
                {
                    DateTime fecha_reserva = (DateTime)dataTable.Rows[0]["fecha_inicio"];
                    DateTime fecha_actual = DateTime.Today;
                    int diferencia_en_dias = (int)(fecha_reserva - fecha_actual).TotalDays;
                    int estado = Convert.ToInt32(dataTable.Rows[0]["codigo_estado"]);
                    string hotel = dataTable.Rows[0]["codigo_hotel"].ToString();
                    if (estado >= 3)
                    {
                        MessageBox.Show("Error: El estado de la reserva no permite cancelación.");
                    }
                    else 
                    {
                        if (diferencia_en_dias >= 1)
                        {
                            string query_test = "select * from GITAR_HEROES.UsuarioHotel where username = '"+Variables.usuario+"' and codigo_hotel = "+hotel;
                            command = new SqlCommand(query_test);
                            command.Connection = connection;
                            adapter = new SqlDataAdapter(command);
                            dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            if (dataTable.Rows.Count < 1)
                            {
                                MessageBox.Show("Error: No puede cancelar la reserva por no pertenecer al hotel.");
                            }
                            else
                            {
                                string query;
                                
                                if (Variables.tipo_usuario == "Recepcionista")
                                {
                                    query = "UPDATE GITAR_HEROES.Reserva SET codigo_estado = 3 WHERE codigo = " + numero_reserva.Text;
                                }
                                else if (Variables.tipo_usuario == "Guest")
                                {
                                    query = "UPDATE GITAR_HEROES.Reserva SET codigo_estado = 4 WHERE codigo = " + numero_reserva.Text;
                                }
                                else
                                {
                                    query = "UPDATE GITAR_HEROES.Reserva SET codigo_estado = 5 WHERE codigo = " + numero_reserva.Text;
                                }
                                command = new SqlCommand(query);
                                command.Connection = connection;
                                adapter = new SqlDataAdapter(command);
                                dataTable = new DataTable();
                                adapter.Fill(dataTable);

                                if (!dataTable.HasErrors)
                                {

                                    string currentDate = DateTime.Now.ToString("yyyy-dd-MM hh:mm:ss");
                                    query = "INSERT INTO GITAR_HEROES.ReservaCancelada (codigo_reserva,fecha_cancelacion,descripcion_motivo,username) VALUES (" + numero_reserva.Text + ",'" + currentDate + "','" + motivo_textbox.Text + "','" + Variables.usuario + "')";
                                
                                    command = new SqlCommand(query);
                                    command.Connection = connection;
                                    adapter = new SqlDataAdapter(command);
                                    dataTable = new DataTable();
                                    adapter.Fill(dataTable);

                                    if (!dataTable.HasErrors) 
                                    {
                                        MessageBox.Show("ok cancelacion");
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Error: La cancelación solo se puede realizar hasta un día antes de la fecha de inicio.");
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc);
            }
        }
    }
}
