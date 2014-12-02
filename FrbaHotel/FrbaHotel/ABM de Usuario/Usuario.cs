using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrbaHotel.Listado_Funcionalidades;

namespace FrbaHotel.ABM_de_Usuario
{
    public partial class Usuario : Form
    {
        public Usuario()
        {
            InitializeComponent();
            llenarCheckedlist();
        }

        private void llenarCheckedlist()
        {
            var items = hotelesChecklist.Items;
            items.Add("1");
            items.Add("2");
            items.Add("1");
            items.Add("2");
            items.Add("1");
            items.Add("2");
            items.Add("1");
            items.Add("2");
            items.Add("1");
            items.Add("2");
            items.Add("1");
            items.Add("2");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Funcionalidades funcionalidadesForm = new Funcionalidades();
            funcionalidadesForm.StartPosition = FormStartPosition.CenterScreen;
            funcionalidadesForm.Show();
        }

        private void hotelesChecklist_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
