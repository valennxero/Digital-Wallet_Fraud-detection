namespace Digital_Wallet_ISA
{
    partial class FormAdmin
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
            this.lblTotalSaldo = new System.Windows.Forms.Label();
            this.lblTotalUser = new System.Windows.Forms.Label();
            this.dgvFraud = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFraud)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTotalSaldo
            // 
            this.lblTotalSaldo.AutoSize = true;
            this.lblTotalSaldo.Location = new System.Drawing.Point(65, 43);
            this.lblTotalSaldo.Name = "lblTotalSaldo";
            this.lblTotalSaldo.Size = new System.Drawing.Size(44, 16);
            this.lblTotalSaldo.TabIndex = 0;
            this.lblTotalSaldo.Text = "label1";
            // 
            // lblTotalUser
            // 
            this.lblTotalUser.AutoSize = true;
            this.lblTotalUser.Location = new System.Drawing.Point(65, 82);
            this.lblTotalUser.Name = "lblTotalUser";
            this.lblTotalUser.Size = new System.Drawing.Size(44, 16);
            this.lblTotalUser.TabIndex = 1;
            this.lblTotalUser.Text = "label1";
            // 
            // dgvFraud
            // 
            this.dgvFraud.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFraud.Location = new System.Drawing.Point(29, 185);
            this.dgvFraud.Name = "dgvFraud";
            this.dgvFraud.RowHeadersWidth = 51;
            this.dgvFraud.RowTemplate.Height = 24;
            this.dgvFraud.Size = new System.Drawing.Size(862, 595);
            this.dgvFraud.TabIndex = 2;
            // 
            // FormAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(918, 860);
            this.Controls.Add(this.dgvFraud);
            this.Controls.Add(this.lblTotalUser);
            this.Controls.Add(this.lblTotalSaldo);
            this.Name = "FormAdmin";
            this.Text = "FormAdmin";
            this.Load += new System.EventHandler(this.FormAdmin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFraud)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTotalSaldo;
        private System.Windows.Forms.Label lblTotalUser;
        private System.Windows.Forms.DataGridView dgvFraud;
    }
}