using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrbaHotel.Listado_Funcionalidades;

namespace FrbaHotel.ABM_de_Hotel
{
    public partial class Hotel : Form
    {

        public Hotel()
        {
            InitializeComponent();
            comboBoxHotel.Items.Add("Editar Hotel Existente");
            comboBoxHotel.Items.Add("Crear Hotel Nuevo");
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
            if (comboBoxHotel.Text.Length == 0)
            {
                MessageBox.Show("Primero debes elegir una opción.");
            }
            else if (comboBoxHotel.Text == "Crear Hotel Nuevo")
            {
                this.Hide();
                HotelForm Hotel_form = new HotelForm();
                Hotel_form.hotel_id = "";
                Hotel_form.StartPosition = FormStartPosition.CenterScreen;
                Hotel_form.ShowDialog();
            }
            else if (comboBoxHotel.Text == "Editar Hotel Existente")
            {
                this.Hide();
                HotelBusqueda busqueda_hotel = new HotelBusqueda();
                busqueda_hotel.StartPosition = FormStartPosition.CenterScreen;
                busqueda_hotel.ShowDialog();
            }
        }

        private void Rol_Load(object sender, EventArgs e)
        {
            
        }
    }
}
