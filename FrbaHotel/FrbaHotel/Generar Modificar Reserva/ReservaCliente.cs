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
    public partial class ReservaCliente : Form
    {
        public string fecha_inicio;
        public string fecha_fin;
        public string codigo_hotel;
        public string codigo_regimen;
        public string codigo_tipo_habitacion;

        public ReservaCliente()
        {
            InitializeComponent();
        }

        private void ReservaCliente_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            ReservaBusqueda reserva_busqueda = new ReservaBusqueda();
            reserva_busqueda.hotel_id = codigo_hotel;
            reserva_busqueda.StartPosition = FormStartPosition.CenterScreen;
            reserva_busqueda.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //CLIENTE EXISTENTE
            this.Hide();
            ReservaClienteBusqueda reserva_cliente_busqueda = new ReservaClienteBusqueda();
            reserva_cliente_busqueda.fecha_fin = fecha_fin;
            reserva_cliente_busqueda.fecha_inicio = fecha_inicio;
            reserva_cliente_busqueda.codigo_hotel = codigo_hotel;
            reserva_cliente_busqueda.codigo_tipo_habitacion = codigo_tipo_habitacion;
            reserva_cliente_busqueda.codigo_regimen = codigo_regimen;
            reserva_cliente_busqueda.StartPosition = FormStartPosition.CenterScreen;
            reserva_cliente_busqueda.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //CLIENTE NUEVO
            this.Hide();
            ReservaClienteForm reserva_cliente_nuevo = new ReservaClienteForm();
            reserva_cliente_nuevo.fecha_fin = fecha_fin;
            reserva_cliente_nuevo.fecha_inicio = fecha_inicio;
            reserva_cliente_nuevo.codigo_hotel = codigo_hotel;
            reserva_cliente_nuevo.codigo_tipo_habitacion = codigo_tipo_habitacion;
            reserva_cliente_nuevo.codigo_regimen = codigo_regimen;
            reserva_cliente_nuevo.StartPosition = FormStartPosition.CenterScreen;
            reserva_cliente_nuevo.Show();
        }
    }
}
