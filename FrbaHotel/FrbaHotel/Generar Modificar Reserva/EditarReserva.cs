using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrbaHotel.Generar_Modificar_Reserva
{
    public partial class EditarReserva : Form
    {
        public string codigo_reserva;
        public string hotel_id;
        public EditarReserva()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Reserva reserva_inicio = new Reserva();
            reserva_inicio.StartPosition = FormStartPosition.CenterScreen;
            reserva_inicio.Show();
        }

        private void buttonContinuar_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (comboBoxAccion.Text == "Editar habitaciones")
            {
                EditarHabitaciones habitaciones = new EditarHabitaciones();
                habitaciones.codigo_reserva = codigo_reserva;
                habitaciones.hotel_id = hotel_id;
                habitaciones.StartPosition = FormStartPosition.CenterScreen;
                habitaciones.Show();
            }
            else
            {
                EditarReservaCampos reserva = new EditarReservaCampos();
                reserva.codigo_reserva = codigo_reserva;
                reserva.hotel_id = hotel_id;
                reserva.StartPosition = FormStartPosition.CenterScreen;
                reserva.Show();
            }

        }

        private void EditarReserva_Load(object sender, EventArgs e)
        {
            comboBoxAccion.Items.Add("Editar habitaciones");
            comboBoxAccion.Items.Add("Editar reserva");
        }
    }
}
