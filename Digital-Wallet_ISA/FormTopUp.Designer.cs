namespace Digital_Wallet_ISA
{
    partial class FormTopUp
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
            this.buttonTopUp = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownNominal = new System.Windows.Forms.NumericUpDown();
            this.buttonPin = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxPin = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNominal)).BeginInit();
            this.SuspendLayout();
            // 
            // labelHomeMenu
            // 
            this.labelHomeMenu.BackColor = System.Drawing.Color.Crimson;
            this.labelHomeMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHomeMenu.ForeColor = System.Drawing.SystemColors.Control;
            this.labelHomeMenu.Location = new System.Drawing.Point(35, 13);
            this.labelHomeMenu.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelHomeMenu.Name = "labelHomeMenu";
            this.labelHomeMenu.Size = new System.Drawing.Size(475, 43);
            this.labelHomeMenu.TabIndex = 18;
            this.labelHomeMenu.Text = "Top Up";
            this.labelHomeMenu.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.labelHomeMenu.UseCompatibleTextRendering = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.LightGray;
            this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCancel.Location = new System.Drawing.Point(199, 434);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(179, 83);
            this.buttonCancel.TabIndex = 24;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelSaldo
            // 
            this.labelSaldo.AutoSize = true;
            this.labelSaldo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSaldo.Location = new System.Drawing.Point(195, 91);
            this.labelSaldo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSaldo.Name = "labelSaldo";
            this.labelSaldo.Size = new System.Drawing.Size(36, 20);
            this.labelSaldo.TabIndex = 23;
            this.labelSaldo.Text = "000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(131, 91);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 20);
            this.label3.TabIndex = 22;
            this.label3.Text = "Saldo :";
            // 
            // buttonTopUp
            // 
            this.buttonTopUp.BackColor = System.Drawing.Color.Crimson;
            this.buttonTopUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonTopUp.Location = new System.Drawing.Point(199, 306);
            this.buttonTopUp.Margin = new System.Windows.Forms.Padding(4);
            this.buttonTopUp.Name = "buttonTopUp";
            this.buttonTopUp.Size = new System.Drawing.Size(179, 83);
            this.buttonTopUp.TabIndex = 21;
            this.buttonTopUp.Text = "Top Up";
            this.buttonTopUp.UseVisualStyleBackColor = false;
            this.buttonTopUp.Click += new System.EventHandler(this.buttonTopUp_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(143, 229);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 16);
            this.label2.TabIndex = 20;
            this.label2.Text = "Rp";
            // 
            // numericUpDownNominal
            // 
            this.numericUpDownNominal.Location = new System.Drawing.Point(187, 226);
            this.numericUpDownNominal.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDownNominal.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numericUpDownNominal.Name = "numericUpDownNominal";
            this.numericUpDownNominal.Size = new System.Drawing.Size(179, 22);
            this.numericUpDownNominal.TabIndex = 19;
            // 
            // buttonPin
            // 
            this.buttonPin.BackColor = System.Drawing.Color.LightGray;
            this.buttonPin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPin.Location = new System.Drawing.Point(401, 136);
            this.buttonPin.Margin = new System.Windows.Forms.Padding(4);
            this.buttonPin.Name = "buttonPin";
            this.buttonPin.Size = new System.Drawing.Size(106, 34);
            this.buttonPin.TabIndex = 40;
            this.buttonPin.Text = "Send";
            this.buttonPin.UseVisualStyleBackColor = false;
            this.buttonPin.Click += new System.EventHandler(this.buttonPin_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(94, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 32);
            this.label4.TabIndex = 39;
            this.label4.Text = "Pin :";
            // 
            // textBoxPin
            // 
            this.textBoxPin.Location = new System.Drawing.Point(187, 143);
            this.textBoxPin.Name = "textBoxPin";
            this.textBoxPin.PasswordChar = '*';
            this.textBoxPin.Size = new System.Drawing.Size(158, 22);
            this.textBoxPin.TabIndex = 38;
            // 
            // FormTopUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 643);
            this.Controls.Add(this.buttonPin);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxPin);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.labelSaldo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonTopUp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDownNominal);
            this.Controls.Add(this.labelHomeMenu);
            this.Name = "FormTopUp";
            this.Text = "FormTopUp";
            this.Load += new System.EventHandler(this.FormTopUp_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNominal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHomeMenu;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelSaldo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonTopUp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownNominal;
        private System.Windows.Forms.Button buttonPin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxPin;
    }
}