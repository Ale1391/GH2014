﻿using System;
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

        private int validarCampos()
        {
            if (numero_reserva.Text.Length == 0 || motivo_textbox.Text.Length == 0)
            {
                MessageBox.Show("Error, campos que faltan completar: "
                    + (numero_reserva.Text.Length == 0 ? " Numero de reserva" : "")
                    + (motivo_textbox.Text.Length == 0 ? ", Motivo" : "")
                );
                return 1;
            }
            return 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int resultado = validarCampos();
            if (resultado == 0)
            {
                connection = new System.Data.SqlClient.SqlConnection();
                try
                {
                    connection.ConnectionString = Variables.connectionStr;
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
                                string query_test = "select * from GITAR_HEROES.UsuarioHotel where username = '" + Variables.usuario + "' and codigo_hotel = " + hotel;
                                command = new SqlCommand(query_test);
                                command.Connection = connection;
                                adapter = new SqlDataAdapter(command);
                                dataTable = new DataTable();
                                adapter.Fill(dataTable);
                                if (dataTable.Rows.Count < 1)
                                {
                                    MessageBox.Show("Error: No puede cancelar la reserva por no pertenecer al hotel.");
                                }
                                else if ((Convert.ToInt32(dataTable.Rows[0]["codigo_hotel"]) != Variables.hotel_id) && (Variables.usuario != "guest"))
                                {
                                    MessageBox.Show("Error: No puede cancelar la reserva por no haber ingresado al hotel en donde se encuentra.");
                                }
                                else
                                {
                                    string query;

                                    if (Variables.tipo_usuario == "Recepcionista")
                                    {
                                        query = "UPDATE GITAR_HEROES.Reserva SET codigo_estado = 3 WHERE codigo = " + numero_reserva.Text;
                                    }
                                    else// if (Variables.tipo_usuario == "Guest")
                                    {
                                        query = "UPDATE GITAR_HEROES.Reserva SET codigo_estado = 4 WHERE codigo = " + numero_reserva.Text;
                                    }

                                    command = new SqlCommand(query);
                                    command.Connection = connection;
                                    adapter = new SqlDataAdapter(command);
                                    dataTable = new DataTable();
                                    adapter.Fill(dataTable);

                                    if (!dataTable.HasErrors)
                                    {
                                        query = "INSERT INTO GITAR_HEROES.ReservaCancelada (codigo_reserva,fecha_cancelacion,descripcion_motivo,username) VALUES (" + numero_reserva.Text + ",'" + Variables.fecha_sistema + " 00:00:00','" + motivo_textbox.Text + "','" + Variables.usuario + "')";

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

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void CancelarReserva_Load(object sender, EventArgs e)
        {

        }

    }
}
