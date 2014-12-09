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
    public partial class ReservaClienteBusqueda : Form
    {
        public string fecha_inicio;
        public string fecha_fin;
        public string codigo_hotel;
        public string codigo_regimen;
        public string codigo_tipo_habitacion;
        public string costo_habitacion;

        public ReservaClienteBusqueda()
        {
            InitializeComponent();
        }

        private void ReservaClienteBusqueda_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            ReservaCliente reserva_cliente = new ReservaCliente();
            reserva_cliente.fecha_fin = fecha_fin;
            reserva_cliente.fecha_inicio = fecha_inicio;
            reserva_cliente.codigo_hotel = codigo_hotel;
            reserva_cliente.codigo_tipo_habitacion = codigo_tipo_habitacion;
            reserva_cliente.codigo_regimen = codigo_regimen;
            reserva_cliente.StartPosition = FormStartPosition.CenterScreen;
            reserva_cliente.Show();
        }
    }
}
