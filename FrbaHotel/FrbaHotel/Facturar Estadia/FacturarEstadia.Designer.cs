﻿namespace FrbaHotel.Facturar_Estadia
{
    partial class FacturarEstadia
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxCodReserva = new System.Windows.Forms.TextBox();
            this.textBoxNumTarjeta = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBoxFormaPago = new System.Windows.Forms.ComboBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.linkLabelReservar = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Codigo de reserva:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(47, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Forma de pago:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(47, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Número de tarjeta:";
            // 
            // textBoxCodReserva
            // 
            this.textBoxCodReserva.Location = new System.Drawing.Point(160, 34);
            this.textBoxCodReserva.Name = "textBoxCodReserva";
            this.textBoxCodReserva.Size = new System.Drawing.Size(121, 20);
            this.textBoxCodReserva.TabIndex = 3;
            // 
            // textBoxNumTarjeta
            // 
            this.textBoxNumTarjeta.Location = new System.Drawing.Point(160, 91);
            this.textBoxNumTarjeta.Name = "textBoxNumTarjeta";
            this.textBoxNumTarjeta.Size = new System.Drawing.Size(121, 20);
            this.textBoxNumTarjeta.TabIndex = 5;
            this.textBoxNumTarjeta.TextChanged += new System.EventHandler(this.textBoxNumTarjeta_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(126, 143);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Facturar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBoxFormaPago
            // 
            this.comboBoxFormaPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFormaPago.FormattingEnabled = true;
            this.comboBoxFormaPago.Location = new System.Drawing.Point(160, 62);
            this.comboBoxFormaPago.Name = "comboBoxFormaPago";
            this.comboBoxFormaPago.Size = new System.Drawing.Size(121, 21);
            this.comboBoxFormaPago.TabIndex = 7;
            this.comboBoxFormaPago.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(12, 9);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(37, 13);
            this.linkLabel1.TabIndex = 8;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Volver";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(47, 198);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Otras acciones:";
            // 
            // linkLabelReservar
            // 
            this.linkLabelReservar.AutoSize = true;
            this.linkLabelReservar.Location = new System.Drawing.Point(47, 224);
            this.linkLabelReservar.Name = "linkLabelReservar";
            this.linkLabelReservar.Size = new System.Drawing.Size(94, 13);
            this.linkLabelReservar.TabIndex = 10;
            this.linkLabelReservar.TabStop = true;
            this.linkLabelReservar.Text = "Reservar mas dias";
            this.linkLabelReservar.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelReservar_LinkClicked);
            // 
            // FacturarEstadia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 344);
            this.Controls.Add(this.linkLabelReservar);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.comboBoxFormaPago);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoxNumTarjeta);
            this.Controls.Add(this.textBoxCodReserva);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FacturarEstadia";
            this.Load += new System.EventHandler(this.FacturarEstadia_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxCodReserva;
        private System.Windows.Forms.TextBox textBoxNumTarjeta;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBoxFormaPago;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel linkLabelReservar;
    }
}