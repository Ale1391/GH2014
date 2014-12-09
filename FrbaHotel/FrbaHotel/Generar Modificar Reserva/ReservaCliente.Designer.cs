namespace FrbaHotel.Generar_Modificar_Reserva
{
    partial class ReservaCliente
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
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.buttonClienteNuevo = new System.Windows.Forms.Button();
            this.buttonClienteExistente = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(13, 13);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(37, 13);
            this.linkLabel1.TabIndex = 0;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Volver";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // buttonClienteNuevo
            // 
            this.buttonClienteNuevo.Location = new System.Drawing.Point(51, 147);
            this.buttonClienteNuevo.Name = "buttonClienteNuevo";
            this.buttonClienteNuevo.Size = new System.Drawing.Size(75, 55);
            this.buttonClienteNuevo.TabIndex = 1;
            this.buttonClienteNuevo.Text = "Cliente Nuevo";
            this.buttonClienteNuevo.UseVisualStyleBackColor = true;
            this.buttonClienteNuevo.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonClienteExistente
            // 
            this.buttonClienteExistente.Location = new System.Drawing.Point(166, 147);
            this.buttonClienteExistente.Name = "buttonClienteExistente";
            this.buttonClienteExistente.Size = new System.Drawing.Size(75, 55);
            this.buttonClienteExistente.TabIndex = 1;
            this.buttonClienteExistente.Text = "Cliente Existente";
            this.buttonClienteExistente.UseVisualStyleBackColor = true;
            this.buttonClienteExistente.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(90, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Elija la opción correcta";
            // 
            // ReservaCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonClienteExistente);
            this.Controls.Add(this.buttonClienteNuevo);
            this.Controls.Add(this.linkLabel1);
            this.Name = "ReservaCliente";
            this.Text = "ReservaCliente";
            this.Load += new System.EventHandler(this.ReservaCliente_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button buttonClienteNuevo;
        private System.Windows.Forms.Button buttonClienteExistente;
        private System.Windows.Forms.Label label1;
    }
}