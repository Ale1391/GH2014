namespace FrbaHotel.Generar_Modificar_Reserva
{
    partial class ReservaBusqueda
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxFechaDesde = new System.Windows.Forms.TextBox();
            this.textBoxFechaHasta = new System.Windows.Forms.TextBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxTipoHabitacion = new System.Windows.Forms.ComboBox();
            this.comboBoxTipoRegimen = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridViewRegimenes = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRegimenes)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxFechaDesde
            // 
            this.textBoxFechaDesde.Location = new System.Drawing.Point(38, 65);
            this.textBoxFechaDesde.Name = "textBoxFechaDesde";
            this.textBoxFechaDesde.Size = new System.Drawing.Size(147, 20);
            this.textBoxFechaDesde.TabIndex = 0;
            // 
            // textBoxFechaHasta
            // 
            this.textBoxFechaHasta.Location = new System.Drawing.Point(224, 65);
            this.textBoxFechaHasta.Name = "textBoxFechaHasta";
            this.textBoxFechaHasta.Size = new System.Drawing.Size(147, 20);
            this.textBoxFechaHasta.TabIndex = 0;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(13, 13);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(37, 13);
            this.linkLabel1.TabIndex = 1;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Volver";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Fecha Desde (DD-MM-AAAA)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(224, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fecha Hasta (DD-MM-AAAA)";
            // 
            // comboBoxTipoHabitacion
            // 
            this.comboBoxTipoHabitacion.FormattingEnabled = true;
            this.comboBoxTipoHabitacion.Location = new System.Drawing.Point(38, 112);
            this.comboBoxTipoHabitacion.Name = "comboBoxTipoHabitacion";
            this.comboBoxTipoHabitacion.Size = new System.Drawing.Size(147, 21);
            this.comboBoxTipoHabitacion.TabIndex = 3;
            // 
            // comboBoxTipoRegimen
            // 
            this.comboBoxTipoRegimen.FormattingEnabled = true;
            this.comboBoxTipoRegimen.Location = new System.Drawing.Point(224, 112);
            this.comboBoxTipoRegimen.Name = "comboBoxTipoRegimen";
            this.comboBoxTipoRegimen.Size = new System.Drawing.Size(147, 21);
            this.comboBoxTipoRegimen.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Tipo Habitación";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(223, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Tipo Régimen";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(139, 370);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(146, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Buscar Disponibilidad";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridViewRegimenes
            // 
            this.dataGridViewRegimenes.AllowUserToAddRows = false;
            this.dataGridViewRegimenes.AllowUserToDeleteRows = false;
            this.dataGridViewRegimenes.AllowUserToResizeRows = false;
            this.dataGridViewRegimenes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRegimenes.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridViewRegimenes.Location = new System.Drawing.Point(38, 174);
            this.dataGridViewRegimenes.MultiSelect = false;
            this.dataGridViewRegimenes.Name = "dataGridViewRegimenes";
            this.dataGridViewRegimenes.ReadOnly = true;
            this.dataGridViewRegimenes.Size = new System.Drawing.Size(331, 173);
            this.dataGridViewRegimenes.TabIndex = 5;
            this.dataGridViewRegimenes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewRegimenes_CellDoubleClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(39, 151);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(171, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Régimenes Disponibles en el Hotel";
            // 
            // ReservaBusqueda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 428);
            this.Controls.Add(this.dataGridViewRegimenes);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBoxTipoRegimen);
            this.Controls.Add(this.comboBoxTipoHabitacion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.textBoxFechaHasta);
            this.Controls.Add(this.textBoxFechaDesde);
            this.Name = "ReservaBusqueda";
            this.Text = "Generar Reserva";
            this.Load += new System.EventHandler(this.ReservaBusqueda_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRegimenes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxFechaDesde;
        private System.Windows.Forms.TextBox textBoxFechaHasta;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxTipoHabitacion;
        private System.Windows.Forms.ComboBox comboBoxTipoRegimen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridViewRegimenes;
        private System.Windows.Forms.Label label5;
    }
}