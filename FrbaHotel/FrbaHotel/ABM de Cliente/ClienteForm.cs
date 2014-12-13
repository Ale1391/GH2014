using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaHotel.ABM_de_Cliente
{
    public partial class ClienteForm : Form
    {
        public string mail;
        private List<int> lista_codigos_tipo_documento = new List<int>();
        private List<string> lista_nombres_tipo_documento = new List<string>();

        System.Data.SqlClient.SqlConnection connection;
        private SqlCommand command;
        private SqlDataAdapter adapter;
        private DataTable dataTable;

        public ClienteForm()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Cliente pantalla_cliente = new Cliente();
            pantalla_cliente.StartPosition = FormStartPosition.CenterScreen;
            pantalla_cliente.Show();
        }

        private int validarCampos()
        {
            if (textBoxApellido.Text.Length == 0 || textBoxNombre.Text.Length == 0 ||
                comboBoxTipoDocumento.Text.Length == 0 || textBoxMail.Text.Length == 0 ||
                textBoxDocumento.Text.Length == 0 || textBoxTelefono.Text.Length == 0 ||
                textBoxDireccion.Text.Length == 0 || textBoxLocalidad.Text.Length == 0 ||
                textBoxPais.Text.Length == 0 || textBoxNacionalidad.Text.Length == 0 ||
                textBox1FechaNacimiento.Text.Length == 0)
            {
                MessageBox.Show("Error, campos que faltan completar: "
                    + (textBoxApellido.Text.Length == 0 ? " Apellido" : "")
                    + (textBoxNombre.Text.Length == 0 ? ", Nombre" : "")
                    + (comboBoxTipoDocumento.Text.Length == 0 ? ", Tipo de Documento" : "")
                    + (textBoxMail.Text.Length == 0 ? ", Email" : "")
                    + (textBoxDocumento.Text.Length == 0 ? ", Numero de Documento" : "")
                    + (textBoxTelefono.Text.Length == 0 ? ", Telefono" : "")
                    + (textBoxDireccion.Text.Length == 0 ? ", Direccion" : "")
                    + (textBoxLocalidad.Text.Length == 0 ? ", Localidad" : "")
                    + (textBoxPais.Text.Length == 0 ? ", Pais" : "")
                    + (textBoxNacionalidad.Text.Length == 0 ? ", Nacionalidad" : "")
                    + (textBox1FechaNacimiento.Text.Length == 0 ? ", Fecha de nacimiento" : "")
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
                if (mail.Length>0)
                {
                    try
                    {
                        editarCliente();
                        MessageBox.Show("Cliente editado exitosamente.");
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("No se pudo editar el cliente. Error: " + exc);
                    }
                }
                else
                {
                    string query2 = "select * from GITAR_HEROES.Cliente where mail = '"+textBoxMail.Text+"'";
                    command = new SqlCommand(query2);
                    command.Connection = connection;
                    adapter = new SqlDataAdapter(command);
                    dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        MessageBox.Show("Error: Ya existe un usuario con ese mail.");
                    }
                    else
                    {
                        string query3 = "select * from GITAR_HEROES.Cliente where tipo_doc = "+lista_codigos_tipo_documento[comboBoxTipoDocumento.SelectedIndex]+" and nro_doc = "+textBoxDocumento.Text;
                        command = new SqlCommand(query3);
                        command.Connection = connection;
                        adapter = new SqlDataAdapter(command);
                        dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        if (dataTable.Rows.Count > 0)
                        {
                            MessageBox.Show("Error: Ya existe un usuario con ese número y tipo de documento.");
                        }
                        else
                        {
                            try
                            {
                                insertarCliente();
                                MessageBox.Show("Cliente creado exitosamente.");
                            }
                            catch (Exception exc)
                            {
                                MessageBox.Show("No se pudo crear el nuevo cliente. Error: " + exc);
                            }
                        }
                    }
                }
            }
        }

        private void editarCliente()
        {
            string query = "UPDATE GITAR_HEROES.Cliente SET nombre = '" + textBoxNombre.Text + "', apellido = '" + textBoxApellido.Text + "', tipo_doc = " + lista_codigos_tipo_documento[comboBoxTipoDocumento.SelectedIndex] + ", nro_doc = " + textBoxDocumento.Text + ", mail = '" + textBoxMail.Text + "', telefono = " + textBoxTelefono.Text + ", domicilio_calle = '" + textBoxDireccion.Text + "', nacionalidad = '" + textBoxNacionalidad.Text + "', fecha_nacimiento = '" + textBox1FechaNacimiento.Text + " 00:00:00" + "', localidad = '" + textBoxLocalidad.Text + "', pais_origen = '" + textBoxPais.Text + "', estado = " + (checkBoxActivo.Checked ? "1" : "0") + " where mail = '" + mail + "'";
            command = new SqlCommand(query);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            if (dataTable.HasErrors)
            {
                MessageBox.Show("Error al editar el cliente.");
            }
        }

        private void insertarCliente()
        {
            string query = "INSERT INTO GITAR_HEROES.Cliente (nombre,apellido,tipo_doc,nro_doc,mail,telefono,domicilio_calle,nacionalidad,fecha_nacimiento,localidad,pais_origen,estado) VALUES ('"+textBoxNombre.Text+"','"+textBoxApellido.Text+"',"+lista_codigos_tipo_documento[comboBoxTipoDocumento.SelectedIndex].ToString()+","+textBoxDocumento.Text+",'"+textBoxMail.Text+"',"+textBoxTelefono.Text+",'"+textBoxDireccion.Text+"','"+textBoxNacionalidad.Text+"','"+textBox1FechaNacimiento.Text+" 00:00:00"+"','"+textBoxLocalidad.Text+"','"+textBoxPais.Text+"',1)";
            command = new SqlCommand(query);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            if (dataTable.HasErrors)
            {
                MessageBox.Show("Error al realizar el insert del cliente.");
            }
        }

        private void ClienteForm_Load(object sender, EventArgs e)
        {
            iniciarConexion();
            llenarComboBoxTipoDocumento();
            if (mail.Length > 0)
            {
                llenarCampos();
            }
            else
            {
                checkBoxActivo.Checked = true;
                checkBoxActivo.Enabled = false;
            }
        }

        private void llenarCampos()
        { 
            string query = "select GITAR_HEROES.Cliente.nombre,GITAR_HEROES.Cliente.apellido,GITAR_HEROES.Cliente.localidad,GITAR_HEROES.Cliente.telefono,GITAR_HEROES.Cliente.domicilio_calle,GITAR_HEROES.Cliente.fecha_nacimiento,GITAR_HEROES.Cliente.nacionalidad,GITAR_HEROES.Cliente.estado,GITAR_HEROES.Cliente.mail,GITAR_HEROES.Cliente.nro_doc,GITAR_HEROES.Cliente.pais_origen,GITAR_HEROES.TipoDocumento.descripcion as tipo_doc_descripcion from GITAR_HEROES.Cliente inner join GITAR_HEROES.TipoDocumento on GITAR_HEROES.TipoDocumento.codigo = GITAR_HEROES.Cliente.tipo_doc where mail = '"+mail+"'";
            command = new SqlCommand(query);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            if (!dataTable.HasErrors)
            {
                textBoxNombre.Text = dataTable.Rows[0]["nombre"].ToString();
                textBoxApellido.Text = dataTable.Rows[0]["apellido"].ToString();
                textBoxMail.Text = dataTable.Rows[0]["mail"].ToString();
                textBoxDocumento.Text = dataTable.Rows[0]["nro_doc"].ToString();
                textBoxPais.Text = dataTable.Rows[0]["pais_origen"].ToString();
                textBoxLocalidad.Text = dataTable.Rows[0]["localidad"].ToString();
                textBoxTelefono.Text = dataTable.Rows[0]["telefono"].ToString();
                textBoxDireccion.Text = dataTable.Rows[0]["domicilio_calle"].ToString();

                DateTime dt = DateTime.Parse(dataTable.Rows[0]["fecha_nacimiento"].ToString());
                textBox1FechaNacimiento.Text = (dt.Day.ToString().Length == 1 ? "0" : "") + dt.Day.ToString() + "-" + (dt.Month.ToString().Length == 1 ? "0" : "") + dt.Month.ToString() + "-" + dt.Year.ToString();
                textBoxNacionalidad.Text = dataTable.Rows[0]["nacionalidad"].ToString();
                checkBoxActivo.Checked = (dataTable.Rows[0]["estado"].ToString() == "1" ? true : false);
                comboBoxTipoDocumento.Text = dataTable.Rows[0]["tipo_doc_descripcion"].ToString();
            }
        }

        private void llenarComboBoxTipoDocumento()
        {
            string query = "select * from GITAR_HEROES.TipoDocumento";
            command = new SqlCommand(query);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            foreach (DataRow row in dataTable.Rows)
            {
                comboBoxTipoDocumento.Items.Add(row["descripcion"].ToString());
                lista_codigos_tipo_documento.Add(Convert.ToInt32(row["codigo"]));
                lista_nombres_tipo_documento.Add(row["descripcion"].ToString());
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

        private void textBoxDocumento_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}
