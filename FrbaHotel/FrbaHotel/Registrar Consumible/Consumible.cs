using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FrbaHotel.Listado_Funcionalidades;
using System.Data.SqlClient;

namespace FrbaHotel.Registrar_Consumible
{
    public partial class Consumible : Form
    {

        System.Data.SqlClient.SqlConnection connection;
        private SqlCommand command;
        private SqlDataAdapter adapter;
        private DataTable dataTable;

        public List<int> lista_codigos_consumibles = new List<int>();
        public List<string> lista_nombres_consumibles = new List<string>();
        public List<string> lista_cantidades = new List<string>();

        public Consumible()
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
            if (comboBoxConsumible.Text.Length == 0)
            {
                MessageBox.Show("Primero debes elegir una opción.");
            }
            else if (comboBoxConsumible.Text == "Registrar Consumible/Finalizar Carga")
            {
                this.Hide();
                ConsumibleForm consumible_form = new ConsumibleForm();
                consumible_form.nro_reserva = "";
                consumible_form.StartPosition = FormStartPosition.CenterScreen;
                consumible_form.ShowDialog();
            }
            else
            {
                this.Hide();
                ConsumibleForm consumible_form = new ConsumibleForm();
                consumible_form.nro_reserva = textBoxNumeroReserva.Text;
                consumible_form.consumible_codigo = lista_codigos_consumibles[comboBoxConsu.SelectedIndex].ToString();
                consumible_form.consumible_descripcion = comboBoxConsu.Text;
                consumible_form.cantidad = lista_cantidades[comboBoxConsu.SelectedIndex];
                consumible_form.StartPosition = FormStartPosition.CenterScreen;
                consumible_form.ShowDialog();
            }
        }

        private void Consumible_Load(object sender, EventArgs e)
        {
            iniciarConexion();

            comboBoxConsumible.Items.Add("Registrar Consumible/Finalizar Carga");
            comboBoxConsumible.Items.Add("Modificar Consumible");

            textBoxNumeroReserva.Enabled = false;
            comboBoxConsu.Enabled = false;
            button2.Enabled = false;
            button1.Enabled = false;
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

        private void cargarComboConsumibles()
        {
            button1.Enabled = false;
            comboBoxConsu.Text = "";
            comboBoxConsu.Items.Clear();
            string query = "select GITAR_HEROES.TipoConsumible.codigo,GITAR_HEROES.TipoConsumible.descripción,GITAR_HEROES.ConsumibleAdquirido.cantidad from GITAR_HEROES.ConsumibleAdquirido inner join GITAR_HEROES.TipoConsumible on GITAR_HEROES.TipoConsumible.codigo = GITAR_HEROES.ConsumibleAdquirido.codigo_consumible where codigo_reserva = " + textBoxNumeroReserva.Text;
            command = new SqlCommand(query);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            if (dataTable.Rows.Count == 0)
            {
                MessageBox.Show("Error: No hay ningun consumible editable para esa reserva.");
            }
            else
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    comboBoxConsu.Items.Add(row["descripción"].ToString());
                    lista_codigos_consumibles.Add(Convert.ToInt32(row["codigo"]));
                    lista_nombres_consumibles.Add(row["descripción"].ToString());
                    lista_cantidades.Add(row["cantidad"].ToString());
                }
            }
        }

        private void comboBoxConsumible_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxConsumible.Text == "Modificar Consumible")
            {
                textBoxNumeroReserva.Enabled = true;
                button2.Enabled = true;
                button1.Enabled = false;
            }
            else if (comboBoxConsumible.Text == "Registrar Consumible/Finalizar Carga")
            {
                textBoxNumeroReserva.Enabled = false;
                comboBoxConsu.Enabled = false;
                button1.Enabled = true;
                button2.Enabled = false;
            }
        }

     
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBoxNumeroReserva.Text.Length > 0)
            {
                comboBoxConsu.Enabled = true;
                cargarComboConsumibles();
            }
            else
            {
                MessageBox.Show("Error. Debes ingresar un número de reserva.");
            }
        }

        private void comboBoxConsu_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }
    }
}
