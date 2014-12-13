namespace FrbaHotel.Registrar_Consumible
{
    partial class Consumible
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
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBoxConsumible = new System.Windows.Forms.ComboBox();
            this.textBoxNumeroReserva = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxConsu = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Elija la opción deseada";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(112, 256);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Continuar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBoxConsumible
            // 
            this.comboBoxConsumible.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxConsumible.FormattingEnabled = true;
            this.comboBoxConsumible.Location = new System.Drawing.Point(51, 64);
            this.comboBoxConsumible.Name = "comboBoxConsumible";
            this.comboBoxConsumible.Size = new System.Drawing.Size(194, 21);
            this.comboBoxConsumible.TabIndex = 5;
            this.comboBoxConsumible.SelectedIndexChanged += new System.EventHandler(this.comboBoxConsumible_SelectedIndexChanged);
            // 
            // textBoxNumeroReserva
            // 
            this.textBoxNumeroReserva.Location = new System.Drawing.Point(51, 121);
            this.textBoxNumeroReserva.Name = "textBoxNumeroReserva";
            this.textBoxNumeroReserva.Size = new System.Drawing.Size(194, 20);
            this.textBoxNumeroReserva.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Número de Reserva";
            // 
            // comboBoxConsu
            // 
            this.comboBoxConsu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxConsu.FormattingEnabled = true;
            this.comboBoxConsu.Location = new System.Drawing.Point(51, 209);
            this.comboBoxConsu.Name = "comboBoxConsu";
            this.comboBoxConsu.Size = new System.Drawing.Size(194, 21);
            this.comboBoxConsu.TabIndex = 9;
            this.comboBoxConsu.SelectedIndexChanged += new System.EventHandler(this.comboBoxConsu_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(48, 191);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Consumible";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(83, 158);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(133, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "Buscar Consumible";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Consumible
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 308);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.comboBoxConsu);
            this.Controls.Add(this.textBoxNumeroReserva);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBoxConsumible);
            this.Controls.Add(this.linkLabel1);
            this.Name = "Consumible";
            this.Text = "Registrar Consumibles";
            this.Load += new System.EventHandler(this.Consumible_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBoxConsumible;
        private System.Windows.Forms.TextBox textBoxNumeroReserva;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxConsu;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
    }
}