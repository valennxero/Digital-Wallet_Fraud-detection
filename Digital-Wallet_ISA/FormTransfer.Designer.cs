namespace Digital_Wallet_ISA
{
    partial class FormTransfer
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
            this.labelHomeMenu = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelSaldo = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonTransfer = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownNominal = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPesan = new System.Windows.Forms.TextBox();
            this.textBoxTujuan = new System.Windows.Forms.TextBox();
            this.Tujuan = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxPin = new System.Windows.Forms.TextBox();
            this.buttonPin = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNominal)).BeginInit();
            this.SuspendLayout();
            // 
            // labelHomeMenu
            // 
            this.labelHomeMenu.BackColor = System.Drawing.Color.Crimson;
            this.labelHomeMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHomeMenu.ForeColor = System.Drawing.SystemColors.Control;
            this.labelHomeMenu.Location = new System.Drawing.Point(35, 9);
            this.labelHomeMenu.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelHomeMenu.Name = "labelHomeMenu";
            this.labelHomeMenu.Size = new System.Drawing.Size(475, 43);
            this.labelHomeMenu.TabIndex = 18;
            this.labelHomeMenu.Text = "Transfer";
            this.labelHomeMenu.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.labelHomeMenu.UseCompatibleTextRendering = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.LightGray;
            this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.Location = new System.Drawing.Point(317, 406);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(179, 83);
            this.buttonCancel.TabIndex = 30;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            // 
            // labelSaldo
            // 
            this.labelSaldo.AutoSize = true;
            this.labelSaldo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSaldo.Location = new System.Drawing.Point(104, 74);
            this.labelSaldo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSaldo.Name = "labelSaldo";
            this.labelSaldo.Size = new System.Drawing.Size(36, 20);
            this.labelSaldo.TabIndex = 29;
            this.labelSaldo.Text = "000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(40, 74);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 20);
            this.label3.TabIndex = 28;
            this.label3.Text = "Saldo :";
            // 
            // buttonTransfer
            // 
            this.buttonTransfer.BackColor = System.Drawing.Color.Crimson;
            this.buttonTransfer.Enabled = false;
            this.buttonTransfer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonTransfer.Location = new System.Drawing.Point(108, 406);
            this.buttonTransfer.Margin = new System.Windows.Forms.Padding(4);
            this.buttonTransfer.Name = "buttonTransfer";
            this.buttonTransfer.Size = new System.Drawing.Size(179, 83);
            this.buttonTransfer.TabIndex = 27;
            this.buttonTransfer.Text = "Transfer";
            this.buttonTransfer.UseVisualStyleBackColor = false;
            this.buttonTransfer.Click += new System.EventHandler(this.buttonTransfer_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(167, 227);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 16);
            this.label2.TabIndex = 26;
            this.label2.Text = "Rp";
            // 
            // numericUpDownNominal
            // 
            this.numericUpDownNominal.Location = new System.Drawing.Point(211, 224);
            this.numericUpDownNominal.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDownNominal.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numericUpDownNominal.Name = "numericUpDownNominal";
            this.numericUpDownNominal.Size = new System.Drawing.Size(179, 22);
            this.numericUpDownNominal.TabIndex = 25;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(56, 332);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 32);
            this.label1.TabIndex = 31;
            this.label1.Text = "Pesan";
            // 
            // textBoxPesan
            // 
            this.textBoxPesan.Location = new System.Drawing.Point(193, 342);
            this.textBoxPesan.Name = "textBoxPesan";
            this.textBoxPesan.Size = new System.Drawing.Size(379, 22);
            this.textBoxPesan.TabIndex = 32;
            // 
            // textBoxTujuan
            // 
            this.textBoxTujuan.Location = new System.Drawing.Point(193, 268);
            this.textBoxTujuan.Name = "textBoxTujuan";
            this.textBoxTujuan.Size = new System.Drawing.Size(379, 22);
            this.textBoxTujuan.TabIndex = 33;
            // 
            // Tujuan
            // 
            this.Tujuan.AutoSize = true;
            this.Tujuan.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tujuan.Location = new System.Drawing.Point(27, 268);
            this.Tujuan.Name = "Tujuan";
            this.Tujuan.Size = new System.Drawing.Size(147, 32);
            this.Tujuan.TabIndex = 34;
            this.Tujuan.Text = "Id Tujuan :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(27, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 32);
            this.label4.TabIndex = 36;
            this.label4.Text = "Pin :";
            // 
            // textBoxPin
            // 
            this.textBoxPin.Location = new System.Drawing.Point(120, 105);
            this.textBoxPin.Name = "textBoxPin";
            this.textBoxPin.PasswordChar = '*';
            this.textBoxPin.Size = new System.Drawing.Size(158, 22);
            this.textBoxPin.TabIndex = 35;
            // 
            // buttonPin
            // 
            this.buttonPin.BackColor = System.Drawing.Color.LightGray;
            this.buttonPin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPin.Location = new System.Drawing.Point(334, 98);
            this.buttonPin.Margin = new System.Windows.Forms.Padding(4);
            this.buttonPin.Name = "buttonPin";
            this.buttonPin.Size = new System.Drawing.Size(106, 34);
            this.buttonPin.TabIndex = 37;
            this.buttonPin.Text = "Send";
            this.buttonPin.UseVisualStyleBackColor = false;
            this.buttonPin.Click += new System.EventHandler(this.buttonPin_Click);
            // 
            // FormTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 562);
            this.Controls.Add(this.buttonPin);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxPin);
            this.Controls.Add(this.Tujuan);
            this.Controls.Add(this.textBoxTujuan);
            this.Controls.Add(this.textBoxPesan);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.labelSaldo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonTransfer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDownNominal);
            this.Controls.Add(this.labelHomeMenu);
            this.Name = "FormTransfer";
            this.Text = "FormTransfer";
            this.Load += new System.EventHandler(this.FormTransfer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNominal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHomeMenu;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelSaldo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonTransfer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownNominal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPesan;
        private System.Windows.Forms.TextBox textBoxTujuan;
        private System.Windows.Forms.Label Tujuan;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxPin;
        private System.Windows.Forms.Button buttonPin;
    }
}