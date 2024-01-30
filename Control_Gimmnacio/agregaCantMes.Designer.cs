namespace Control_Gimmnacio
{
    partial class agregaCantMes
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
               System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(agregaCantMes));
               this.label1 = new System.Windows.Forms.Label();
               this.numMes = new System.Windows.Forms.NumericUpDown();
               this.btnAceptar = new System.Windows.Forms.Button();
               this.cmbSocio = new System.Windows.Forms.ComboBox();
               this.txtNombre = new System.Windows.Forms.TextBox();
               this.pictureBox2 = new System.Windows.Forms.PictureBox();
               this.txtTotal = new System.Windows.Forms.TextBox();
               this.checkBoxSocio = new System.Windows.Forms.CheckBox();
               ((System.ComponentModel.ISupportInitialize)(this.numMes)).BeginInit();
               ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
               this.SuspendLayout();
               // 
               // label1
               // 
               this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
               this.label1.AutoSize = true;
               this.label1.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
               this.label1.Location = new System.Drawing.Point(94, 34);
               this.label1.Name = "label1";
               this.label1.Size = new System.Drawing.Size(228, 25);
               this.label1.TabIndex = 0;
               this.label1.Text = "Numero de Visitantes";
               this.label1.Click += new System.EventHandler(this.label1_Click);
               // 
               // numMes
               // 
               this.numMes.Anchor = System.Windows.Forms.AnchorStyles.None;
               this.numMes.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
               this.numMes.Location = new System.Drawing.Point(341, 28);
               this.numMes.Name = "numMes";
               this.numMes.Size = new System.Drawing.Size(50, 31);
               this.numMes.TabIndex = 1;
               // 
               // btnAceptar
               // 
               this.btnAceptar.Anchor = System.Windows.Forms.AnchorStyles.None;
               this.btnAceptar.Cursor = System.Windows.Forms.Cursors.Hand;
               this.btnAceptar.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
               this.btnAceptar.Location = new System.Drawing.Point(271, 201);
               this.btnAceptar.Name = "btnAceptar";
               this.btnAceptar.Size = new System.Drawing.Size(101, 37);
               this.btnAceptar.TabIndex = 2;
               this.btnAceptar.Text = "Aceptar";
               this.btnAceptar.UseVisualStyleBackColor = true;
               this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
               // 
               // cmbSocio
               // 
               this.cmbSocio.Anchor = System.Windows.Forms.AnchorStyles.None;
               this.cmbSocio.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
               this.cmbSocio.FormattingEnabled = true;
               this.cmbSocio.Location = new System.Drawing.Point(230, 79);
               this.cmbSocio.Name = "cmbSocio";
               this.cmbSocio.Size = new System.Drawing.Size(262, 29);
               this.cmbSocio.TabIndex = 5;
               this.cmbSocio.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cmbSocio_KeyUp);
               // 
               // txtNombre
               // 
               this.txtNombre.Anchor = System.Windows.Forms.AnchorStyles.None;
               this.txtNombre.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
               this.txtNombre.Location = new System.Drawing.Point(230, 110);
               this.txtNombre.Name = "txtNombre";
               this.txtNombre.Size = new System.Drawing.Size(262, 26);
               this.txtNombre.TabIndex = 7;
               // 
               // pictureBox2
               // 
               this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
               this.pictureBox2.Location = new System.Drawing.Point(125, 123);
               this.pictureBox2.Name = "pictureBox2";
               this.pictureBox2.Size = new System.Drawing.Size(89, 62);
               this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
               this.pictureBox2.TabIndex = 10;
               this.pictureBox2.TabStop = false;
               // 
               // txtTotal
               // 
               this.txtTotal.Font = new System.Drawing.Font("Century Gothic", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
               this.txtTotal.ForeColor = System.Drawing.Color.Red;
               this.txtTotal.Location = new System.Drawing.Point(232, 142);
               this.txtTotal.Name = "txtTotal";
               this.txtTotal.Size = new System.Drawing.Size(140, 43);
               this.txtTotal.TabIndex = 11;
               this.txtTotal.Click += new System.EventHandler(this.txtTotal_Click);
               this.txtTotal.TextChanged += new System.EventHandler(this.txtTotal_TextChanged);
               // 
               // checkBoxSocio
               // 
               this.checkBoxSocio.AutoSize = true;
               this.checkBoxSocio.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
               this.checkBoxSocio.ForeColor = System.Drawing.Color.Black;
               this.checkBoxSocio.Location = new System.Drawing.Point(76, 83);
               this.checkBoxSocio.Name = "checkBoxSocio";
               this.checkBoxSocio.Size = new System.Drawing.Size(138, 22);
               this.checkBoxSocio.TabIndex = 12;
               this.checkBoxSocio.Text = "Socio/Visitante";
               this.checkBoxSocio.UseVisualStyleBackColor = true;
               this.checkBoxSocio.CheckedChanged += new System.EventHandler(this.checkBoxSocio_CheckedChanged_1);
               // 
               // agregaCantMes
               // 
               this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
               this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
               this.ClientSize = new System.Drawing.Size(631, 266);
               this.Controls.Add(this.checkBoxSocio);
               this.Controls.Add(this.txtTotal);
               this.Controls.Add(this.pictureBox2);
               this.Controls.Add(this.txtNombre);
               this.Controls.Add(this.cmbSocio);
               this.Controls.Add(this.btnAceptar);
               this.Controls.Add(this.numMes);
               this.Controls.Add(this.label1);
               this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
               this.MaximizeBox = false;
               this.MinimizeBox = false;
               this.Name = "agregaCantMes";
               this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
               this.Text = "Visitantes";
               this.Load += new System.EventHandler(this.agregaCantMes_Load);
               ((System.ComponentModel.ISupportInitialize)(this.numMes)).EndInit();
               ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
               this.ResumeLayout(false);
               this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numMes;
        private System.Windows.Forms.Button btnAceptar;
          private System.Windows.Forms.ComboBox cmbSocio;
          public System.Windows.Forms.TextBox txtNombre;
          private System.Windows.Forms.PictureBox pictureBox2;
          private System.Windows.Forms.TextBox txtTotal;
          private System.Windows.Forms.CheckBox checkBoxSocio;
     }
}