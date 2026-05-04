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
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewLocked = new System.Windows.Forms.DataGridView();
            this.buttonUnlock = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFraud)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLocked)).BeginInit();
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
            this.dgvFraud.Size = new System.Drawing.Size(1228, 328);
            this.dgvFraud.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 570);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Locked User:";
            // 
            // dataGridViewLocked
            // 
            this.dataGridViewLocked.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewLocked.Location = new System.Drawing.Point(44, 598);
            this.dataGridViewLocked.Name = "dataGridViewLocked";
            this.dataGridViewLocked.RowHeadersWidth = 51;
            this.dataGridViewLocked.RowTemplate.Height = 24;
            this.dataGridViewLocked.Size = new System.Drawing.Size(862, 328);
            this.dataGridViewLocked.TabIndex = 4;
            // 
            // buttonUnlock
            // 
            this.buttonUnlock.Location = new System.Drawing.Point(718, 563);
            this.buttonUnlock.Name = "buttonUnlock";
            this.buttonUnlock.Size = new System.Drawing.Size(140, 23);
            this.buttonUnlock.TabIndex = 5;
            this.buttonUnlock.Text = "Unlock User";
            this.buttonUnlock.UseVisualStyleBackColor = true;
            this.buttonUnlock.Click += new System.EventHandler(this.buttonUnlock_Click);
            // 
            // FormAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1298, 860);
            this.Controls.Add(this.buttonUnlock);
            this.Controls.Add(this.dataGridViewLocked);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvFraud);
            this.Controls.Add(this.lblTotalUser);
            this.Controls.Add(this.lblTotalSaldo);
            this.Name = "FormAdmin";
            this.Text = "FormAdmin";
            this.Load += new System.EventHandler(this.FormAdmin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFraud)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLocked)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTotalSaldo;
        private System.Windows.Forms.Label lblTotalUser;
        private System.Windows.Forms.DataGridView dgvFraud;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridViewLocked;
        private System.Windows.Forms.Button buttonUnlock;
    }
}