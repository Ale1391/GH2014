using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaHotel.Registrar_Consumible
{
    public partial class ConsumibleForm : Form
    {
        public string nro_reserva;
        public string consumible_descripcion;
        public string consumible_codigo;
        public string cantidad;

        public List<int> lista_codigos_consumibles = new List<int>();
        public List<string> lista_nombres_consumibles = new List<string>();

        System.Data.SqlClient.SqlConnection connection;
        private SqlCommand command;
        private SqlDataAdapter adapter;
        private DataTable dataTable;

        public ConsumibleForm()
        {
            InitializeComponent();
        }

        private void ConsumibleForm_Load(object sender, EventArgs e)
        {
            iniciarConexion();

            if (nro_reserva.Length > 0)
            {
                textBoxNumeroReserva.Text = nro_reserva;
                textBoxNumeroReserva.Enabled = false;
                comboBoxConsumible.Enabled = false;
                comboBoxConsumible.Text = consumible_descripcion;
                textBoxCantidad.Text = cantidad;
            }
            else
            {
                cargarComboConsumibles();
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Consumible consumible_inicio = new Consumible();
            consumible_inicio.StartPosition = FormStartPosition.CenterScreen;
            consumible_inicio.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (nro_reserva.Length>0)
            {
                try
                {
                    editarConsumible();
                    MessageBox.Show("Se edito exitosamente el consumible.");
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Error: " + exc);
                }
            }
            else
            {
                try
                {
                    registrarConsumible();
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Error: " + exc);
                }
            }
        }

        private void editarConsumible()
        {
            using (SqlConnection con = new SqlConnection(Variables.connectionStr))
            {
                using (SqlCommand cmd = new SqlCommand("GITAR_HEROES.modificarConsumible", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@descripcion_consumible", SqlDbType.VarChar).Value = comboBoxConsumible.Text;
                    cmd.Parameters.Add("@codigo_reserva", SqlDbType.Int).Value = Convert.ToUInt32(textBoxNumeroReserva.Text);
                    cmd.Parameters.Add("@cantidad", SqlDbType.Int).Value = Convert.ToUInt32(textBoxCantidad.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void registrarConsumible()
        {
            if (comboBoxConsumible.Text == "Finalizacion carga consumibles")
            {
                using (SqlConnection con = new SqlConnection(Variables.connectionStr))
                {
                    using (SqlCommand cmd = new SqlCommand("GITAR_HEROES.finalizarCargaConsumibles", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@codigo_reserva", SqlDbType.Int).Value = Convert.ToUInt32(textBoxNumeroReserva.Text);

                        con.Open();
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Se finalizo exitosamente el ingreso de consumibles.");
                    }
                }
            }
            else
            {
                using (SqlConnection con = new SqlConnection(Variables.connectionStr))
                {
                    using (SqlCommand cmd = new SqlCommand("GITAR_HEROES.cargarConsumible", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@descripcion_consumible", SqlDbType.VarChar).Value = comboBoxConsumible.Text;
                        cmd.Parameters.Add("@codigo_reserva", SqlDbType.Int).Value = Convert.ToUInt32(textBoxNumeroReserva.Text);
                        cmd.Parameters.Add("@cantidad", SqlDbType.Int).Value = Convert.ToUInt32(textBoxCantidad.Text);

                        con.Open();
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Se ingreso exitosamente el consumible.");
                    }
                }
            }
        }

        private void cargarComboConsumibles()
        {
            string query = "select * from GITAR_HEROES.TipoConsumible";
            command = new SqlCommand(query);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            foreach (DataRow row in dataTable.Rows)
            {
                comboBoxConsumible.Items.Add(row["descripción"].ToString());
                lista_codigos_consumibles.Add(Convert.ToInt32(row["codigo"]));
                lista_nombres_consumibles.Add(row["descripción"].ToString());
            }
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

        private void comboBoxConsumible_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxConsumible.Text == "Finalizacion carga consumibles")
            {
                textBoxCantidad.Enabled = false;
                textBoxCantidad.Text = "";
            }
            else
            {
                textBoxCantidad.Enabled = true;
            }
        }
    }
}
