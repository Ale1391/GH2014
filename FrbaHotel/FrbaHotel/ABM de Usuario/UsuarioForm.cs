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
using System.Globalization;
using System.Security.Cryptography;

namespace FrbaHotel.ABM_de_Usuario
{
    public partial class UsuarioForm : Form
    {
        public List<int> lista_codigos_hoteles = new List<int>();
        public List<string> lista_nombres_hoteles = new List<string>();

        private List<int> lista_codigos_dni = new List<int>();
        private List<string> lista_nombres_dni = new List<string>();

        private List<int> lista_codigos_rol = new List<int>();
        private List<string> lista_nombres_rol = new List<string>();

        public string nombre_usuario;
        System.Data.SqlClient.SqlConnection connection;
        private SqlCommand command;
        private SqlDataAdapter adapter;
        private DataTable dataTable;

        public UsuarioForm()
        {
            InitializeComponent();
        }

        private void cargarFormulario()
        {
            
            string query = "select * from GITAR_HEROES.Usuario where username = '"+nombre_usuario+"'";
            command = new SqlCommand(query);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            if (dataTable.Rows.Count > 0)
            {
                textBoxUsuario.Text = dataTable.Rows[0]["username"].ToString();
                //textBoxClave.Text = dataTable.Rows[0]["password"].ToString();
                textBoxNombre.Text = dataTable.Rows[0]["nombre"].ToString();
                textBoxApellido.Text = dataTable.Rows[0]["apellido"].ToString();
                textBoxDni.Text = dataTable.Rows[0]["nro_doc"].ToString();
                textBoxMail.Text = dataTable.Rows[0]["mail"].ToString();
                textBoxTelefono.Text = dataTable.Rows[0]["telefono"].ToString();
                textBoxDireccion.Text = dataTable.Rows[0]["domicilio"].ToString();
                DateTime dt = DateTime.Parse(dataTable.Rows[0]["fecha_nacimiento"].ToString());
                textBoxFechaNac.Text = (dt.Day.ToString().Length==1?"0":"")+dt.Day.ToString() + "-" + (dt.Month.ToString().Length==1?"0":"")+ dt.Month.ToString() + "-" + dt.Year.ToString();
                if (dataTable.Rows[0]["estado"].ToString() == "1")
                {
                    checkBoxUsuarioActivo.Checked = true;
                }
                else
                {
                    checkBoxUsuarioActivo.Checked = false;
                }

                string query2 = "select descripcion from GITAR_HEROES.Rol inner join GITAR_HEROES.RolUsuario on GITAR_HEROES.Rol.codigo = GITAR_HEROES.RolUsuario.codigo_rol where username = '"+nombre_usuario+"'";
                command = new SqlCommand(query2);
                command.Connection = connection;
                adapter = new SqlDataAdapter(command);
                dataTable = new DataTable();
                adapter.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    comboBoxRol.Text = dataTable.Rows[0]["descripcion"].ToString();

                    for (int i = 0; i < lista_nombres_rol.Count; i++)
                    {
                        string rol = lista_nombres_rol[i];
                        if (rol == comboBoxRol.Text)
                        {
                            comboBoxRol.SelectedItem = i;
                        }
                    }
                }

                string query3 = "select descripcion,codigo from GITAR_HEROES.TipoDocumento inner join GITAR_HEROES.Usuario on GITAR_HEROES.TipoDocumento.codigo = GITAR_HEROES.Usuario.tipo_doc where username = '"+nombre_usuario+"'";
                command = new SqlCommand(query3);
                command.Connection = connection;
                adapter = new SqlDataAdapter(command);
                dataTable = new DataTable();
                adapter.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    comboBoxTipodni.Text = dataTable.Rows[0]["descripcion"].ToString();

                    for (int i = 0; i < lista_nombres_dni.Count; i++)
                    {
                        string tipo_doc = lista_nombres_dni[i];
                        if (tipo_doc == comboBoxTipodni.Text)
                        {
                            comboBoxTipodni.SelectedItem = i;
                        }
                    }
                    
                }
            }
        }

        private void llenarCheckedlistNuevo()
        {
            connection = new System.Data.SqlClient.SqlConnection();
            try
            {
                connection.ConnectionString = Variables.connectionStr;
                connection.Open();
                string query = "select nombre,codigo from GITAR_HEROES.Hotel";
                command = new SqlCommand(query);
                command.Connection = connection;
                adapter = new SqlDataAdapter(command);
                dataTable = new DataTable();
                adapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    hotelesChecklist.Items.Add(row["nombre"].ToString(), false);
                    lista_codigos_hoteles.Add(Convert.ToInt32(row["codigo"]));
                    lista_nombres_hoteles.Add(row["nombre"].ToString());
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc);
            }
        }

        private void tildarCheckedlistEdicion()
        {
            string query = "select nombre from GITAR_HEROES.Hotel inner join GITAR_HEROES.UsuarioHotel on GITAR_HEROES.UsuarioHotel.codigo_hotel = GITAR_HEROES.Hotel.codigo where GITAR_HEROES.UsuarioHotel.username = '" + nombre_usuario + "'";
            command = new SqlCommand(query);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            foreach (DataRow row in dataTable.Rows)
            {
                for (int i = 0;i<hotelesChecklist.Items.Count;i++)
                {
                    object hotel = hotelesChecklist.Items[i];
                    if (hotel.ToString() == row["nombre"].ToString())
                    {
                        hotelesChecklist.SetItemChecked(i, true);
                    }
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Usuario funcionalidadesForm = new Usuario();
            funcionalidadesForm.StartPosition = FormStartPosition.CenterScreen;
            funcionalidadesForm.Show();
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

        private void UsuarioForm_Load(object sender, EventArgs e)
        {
            if (nombre_usuario.Length > 0)
            {
                //USUARIO EXISTENTE
                textBoxUsuario.Enabled = false;
                llenarCheckedlistNuevo();
                tildarCheckedlistEdicion();
                llenarCombos();
                cargarFormulario();
            }
            else
            {
                //USUARIO NUEVO
                checkBoxUsuarioActivo.Enabled = false;
                checkBoxUsuarioActivo.Checked = true;
                llenarCombosNuevo();
                llenarCheckedlistNuevo();
            }
        }

        private void llenarCombosNuevo()
        {
            connection = new System.Data.SqlClient.SqlConnection();
            try
            {
                connection.ConnectionString = Variables.connectionStr;
                connection.Open();
                string query = "select * from GITAR_HEROES.Rol where estado = 1";
                command = new SqlCommand(query);
                command.Connection = connection;
                adapter = new SqlDataAdapter(command);
                dataTable = new DataTable();
                adapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    comboBoxRol.Items.Add(row["descripcion"].ToString());
                    lista_codigos_rol.Add(Convert.ToInt32(row["codigo"]));
                    lista_nombres_rol.Add(row["descripcion"].ToString());
                }

                string query2 = "select * from GITAR_HEROES.TipoDocumento";
                command = new SqlCommand(query2);
                command.Connection = connection;
                adapter = new SqlDataAdapter(command);
                dataTable = new DataTable();
                adapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    comboBoxTipodni.Items.Add(row["descripcion"].ToString());
                    lista_codigos_dni.Add(Convert.ToInt32(row["codigo"]));
                    lista_nombres_dni.Add(row["descripcion"].ToString());
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc);
            }
        }

        private void llenarCombos()
        {
            string query = "select * from GITAR_HEROES.Rol where estado = 1";
            command = new SqlCommand(query);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            foreach (DataRow row in dataTable.Rows)
            {
                comboBoxRol.Items.Add(row["descripcion"].ToString());
                lista_codigos_rol.Add(Convert.ToInt32(row["codigo"]));
                lista_nombres_rol.Add(row["descripcion"].ToString());
            }

            string query2 = "select * from GITAR_HEROES.TipoDocumento";
            command = new SqlCommand(query2);
            command.Connection = connection;
            adapter = new SqlDataAdapter(command);
            dataTable = new DataTable();
            adapter.Fill(dataTable);

            foreach (DataRow row in dataTable.Rows)
            {
                comboBoxTipodni.Items.Add(row["descripcion"].ToString());
                lista_codigos_dni.Add(Convert.ToInt32(row["codigo"]));
                lista_nombres_dni.Add(row["descripcion"].ToString());

            }
        }

        private int validarCampos()
        {
            if (textBoxUsuario.Text.Length == 0 || textBoxClave.Text.Length == 0 ||
                comboBoxRol.Text.Length == 0 || textBoxNombre.Text.Length == 0 ||
                textBoxApellido.Text.Length == 0 || comboBoxTipodni.Text.Length == 0 ||
                textBoxDni.Text.Length == 0 || textBoxMail.Text.Length == 0 ||
                textBoxTelefono.Text.Length == 0 || textBoxDireccion.Text.Length == 0 ||
                textBoxFechaNac.Text.Length == 0)
            {
                MessageBox.Show("Error, campos que faltan completar: "
                    + (textBoxUsuario.Text.Length == 0 ? " Usuario" : "")
                    + (textBoxClave.Text.Length == 0 ? ", Contraseña" : "")
                    + (comboBoxRol.Text.Length == 0 ? ", Rol" : "")
                    + (textBoxNombre.Text.Length == 0 ? ", Nombre" : "")
                    + (textBoxApellido.Text.Length == 0 ? ", Apellido" : "")
                    + (comboBoxTipodni.Text.Length == 0 ? ", Tipo de Documento" : "")
                    + (textBoxDni.Text.Length == 0 ? ", Numero de Documento" : "")
                    + (textBoxMail.Text.Length == 0 ? ", Mail" : "")
                    + (textBoxTelefono.Text.Length == 0 ? ", Telefono" : "")
                    + (textBoxDireccion.Text.Length == 0 ? ", Direccion" : "")
                    + (textBoxFechaNac.Text.Length == 0 ? ", Fecha de nacimiento" : "")
                );
                return 1;
            }
            return 0;
        }

        private void buttonFinalizar_Click(object sender, EventArgs e)
        {
            int resultado = validarCampos();
            if (resultado == 0)
            {
                if (nombre_usuario.Length > 0)
                {
                    editarUsuario();
                }
                else
                {
                    try
                    {
                        string query2 = "select * from GITAR_HEROES.usuario where username = '" + textBoxUsuario.Text + "'";
                        command = new SqlCommand(query2);
                        command.Connection = connection;
                        adapter = new SqlDataAdapter(command);
                        dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        if (dataTable.Rows.Count > 0)
                        {
                            MessageBox.Show("Error: Ya existe un usuario con ese username.");
                        }
                        else
                        {
                            crearUsuario();
                            foreach (int index_checked in hotelesChecklist.CheckedIndices)
                            {
                                crearUsuarioHotel(lista_codigos_hoteles[index_checked]);
                            }

                            MessageBox.Show("Usuario creado exitosamente");
                        }
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("Error: " + exc);
                    }
                }
            }
        }

        private void editarUsuario()
        {
            try
            {
                borrarUsuarioHotel();
                actualizarUsuario();

                foreach (int index_checked in hotelesChecklist.CheckedIndices)
                {
                    crearUsuarioHotel(lista_codigos_hoteles[index_checked]);
                }

                MessageBox.Show("Usuario editado exitosamente");
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc);
            }
        }

        private void actualizarUsuario()
        {
            using (SqlConnection con = new SqlConnection(Variables.connectionStr))
            {
                using (SqlCommand cmd = new SqlCommand("GITAR_HEROES.modificarUsuario", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = textBoxUsuario.Text;

                    SHA256 sha256 = SHA256.Create();
                    byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(textBoxClave.Text));
                    string hashstring = string.Empty;
                    foreach (byte x in hash)
                    {
                        hashstring += string.Format("{0:x2}", x);
                    }

                    cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = hashstring;
                    cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = textBoxNombre.Text;
                    cmd.Parameters.Add("@apellido", SqlDbType.VarChar).Value = textBoxApellido.Text;
                    cmd.Parameters.Add("@tipo_doc", SqlDbType.VarChar).Value = lista_codigos_dni[comboBoxTipodni.SelectedIndex];
                    cmd.Parameters.Add("@nro_doc", SqlDbType.VarChar).Value = Convert.ToInt32(textBoxDni.Text);

                    string string_date = textBoxFechaNac.Text;
                    DateTime dt = DateTime.Parse(string_date);
                    cmd.Parameters.Add("@fecha_nacimiento", System.Data.SqlDbType.DateTime).Value = dt;
                    cmd.Parameters.Add("@domicilio", SqlDbType.VarChar).Value = textBoxDireccion.Text;
                    cmd.Parameters.Add("@mail", SqlDbType.VarChar).Value = textBoxMail.Text;
                    cmd.Parameters.Add("@telefono", SqlDbType.VarChar).Value = Convert.ToInt32(textBoxTelefono.Text);
                    cmd.Parameters.Add("@codigo_rol", SqlDbType.VarChar).Value = lista_codigos_rol[comboBoxRol.SelectedIndex];
                    cmd.Parameters.Add("@codigo_estado_sistema", SqlDbType.VarChar).Value = (checkBoxUsuarioActivo.Checked ? 1 : 0);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void borrarUsuarioHotel()
        {//
            using (SqlConnection con = new SqlConnection(Variables.connectionStr))
            {
                using (SqlCommand cmd = new SqlCommand("GITAR_HEROES.limpiarUsuarioHotel", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = textBoxUsuario.Text;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void crearUsuarioHotel(int codigo_hotel)
        {
            using (SqlConnection con = new SqlConnection(Variables.connectionStr))
            {
                using (SqlCommand cmd = new SqlCommand("GITAR_HEROES.agregarUsuarioHotel", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@codigo_hotel", SqlDbType.VarChar).Value = codigo_hotel;
                    cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = textBoxUsuario.Text;
                            
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void crearUsuario()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Variables.connectionStr))
                {
                    using (SqlCommand cmd = new SqlCommand("GITAR_HEROES.generarUsuario", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = textBoxUsuario.Text;

                        SHA256 sha256 = SHA256.Create();
                        byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(textBoxClave.Text));
                        string hashstring = string.Empty;
                        foreach (byte x in hash)
                        {
                            hashstring += string.Format("{0:x2}", x);
                        }

                        cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = hashstring;
                        cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = textBoxNombre.Text;
                        cmd.Parameters.Add("@apellido", SqlDbType.VarChar).Value = textBoxApellido.Text;
                        cmd.Parameters.Add("@tipo_doc", SqlDbType.VarChar).Value = lista_codigos_dni[comboBoxTipodni.SelectedIndex];
                        cmd.Parameters.Add("@nro_doc", SqlDbType.VarChar).Value = Convert.ToInt32(textBoxDni.Text);
                        
                        string string_date = textBoxFechaNac.Text;
                        DateTime dt = DateTime.Parse(string_date);
                        cmd.Parameters.Add("@fecha_nacimiento", System.Data.SqlDbType.DateTime).Value = dt;
                        cmd.Parameters.Add("@domicilio", SqlDbType.VarChar).Value = textBoxDireccion.Text;
                        cmd.Parameters.Add("@mail", SqlDbType.VarChar).Value = textBoxMail.Text;
                        cmd.Parameters.Add("@telefono", SqlDbType.VarChar).Value = Convert.ToInt32(textBoxTelefono.Text);
                        cmd.Parameters.Add("@codigo_rol", SqlDbType.VarChar).Value = lista_codigos_rol[comboBoxRol.SelectedIndex];

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc);
            }
        }
    }
}
