using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrbaHotel.Listado_Funcionalidades;

namespace FrbaHotel.Login
{
    public partial class HotelSelect : Form
    {
        public List<int> lista_codigos_hoteles = new List<int>();
        public List<string> lista_nombres_hoteles = new List<string>();

        public HotelSelect()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Variables.hotel_id = lista_codigos_hoteles[comboBoxHotel.SelectedIndex];
            Funcionalidades func = new Funcionalidades();
            func.StartPosition = FormStartPosition.CenterScreen;
            func.ShowDialog();
        }

        private void HotelSelect_Load(object sender, EventArgs e)
        {
            foreach (string nombre in lista_nombres_hoteles)
            {
                comboBoxHotel.Items.Add(nombre);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Principal prinForm = new Principal();
            prinForm.Show();
        }
    }
}
