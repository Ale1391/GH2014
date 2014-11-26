namespace FrbaHotel.Cancelar_Reserva
{
    partial class CancelarReserva
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
            this.numero_reserva = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.motivo_textbox = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.usuario_textBox = new System.Windows.Forms.TextBox();
            this.cancelarButton = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Número de Reserva";
            // 
            // numero_reserva
            // 
            this.numero_reserva.Location = new System.Drawing.Point(30, 57);
            this.numero_reserva.Name = "numero_reserva";
            this.numero_reserva.Size = new System.Drawing.Size(231, 20);
            this.numero_reserva.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Motivo";
            // 
            // motivo_textbox
            // 
            this.motivo_textbox.Location = new System.Drawing.Point(30, 102);
            this.motivo_textbox.Name = "motivo_textbox";
            this.motivo_textbox.Size = new System.Drawing.Size(231, 60);
            this.motivo_textbox.TabIndex = 3;
            this.motivo_textbox.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 176);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Usuario que cancela";
            // 
            // usuario_textBox
            // 
            this.usuario_textBox.Location = new System.Drawing.Point(30, 195);
            this.usuario_textBox.Name = "usuario_textBox";
            this.usuario_textBox.Size = new System.Drawing.Size(231, 20);
            this.usuario_textBox.TabIndex = 5;
            // 
            // cancelarButton
            // 
            this.cancelarButton.Location = new System.Drawing.Point(87, 233);
            this.cancelarButton.Name = "cancelarButton";
            this.cancelarButton.Size = new System.Drawing.Size(120, 23);
            this.cancelarButton.TabIndex = 6;
            this.cancelarButton.Text = "Cancelar reserva";
            this.cancelarButton.UseVisualStyleBackColor = true;
            this.cancelarButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(13, 10);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(37, 13);
            this.linkLabel1.TabIndex = 7;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Volver";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // CancelarReserva
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.cancelarButton);
            this.Controls.Add(this.usuario_textBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.motivo_textbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numero_reserva);
            this.Controls.Add(this.label1);
            this.Name = "CancelarReserva";
            this.Text = "Cancelar Reserva";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox numero_reserva;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox motivo_textbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox usuario_textBox;
        private System.Windows.Forms.Button cancelarButton;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}