namespace Digital_Wallet_ISA
{
    partial class FormHistory
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
            this.dataGridViewDataRiwayat = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tanggal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Biaya = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.driver = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Berat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.dateTimePickerAkhir = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePickerAwal = new System.Windows.Forms.DateTimePicker();
            this.buttonExportPdf = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDataRiwayat)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewDataRiwayat
            // 
            this.dataGridViewDataRiwayat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDataRiwayat.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Tanggal,
            this.Biaya,
            this.driver,
            this.Tip,
            this.Berat});
            this.dataGridViewDataRiwayat.Location = new System.Drawing.Point(28, 154);
            this.dataGridViewDataRiwayat.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewDataRiwayat.Name = "dataGridViewDataRiwayat";
            this.dataGridViewDataRiwayat.RowHeadersWidth = 51;
            this.dataGridViewDataRiwayat.Size = new System.Drawing.Size(999, 321);
            this.dataGridViewDataRiwayat.TabIndex = 15;
            this.dataGridViewDataRiwayat.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewDataRiwayat_CellContentClick);
            // 
            // Id
            // 
            this.Id.HeaderText = "Id Transaksi";
            this.Id.MinimumWidth = 6;
            this.Id.Name = "Id";
            this.Id.Width = 125;
            // 
            // Tanggal
            // 
            this.Tanggal.HeaderText = "Tanggal";
            this.Tanggal.MinimumWidth = 6;
            this.Tanggal.Name = "Tanggal";
            this.Tanggal.Width = 125;
            // 
            // Biaya
            // 
            this.Biaya.HeaderText = "Jumlah Transaksi (Rp)";
            this.Biaya.MinimumWidth = 6;
            this.Biaya.Name = "Biaya";
            this.Biaya.Width = 125;
            // 
            // driver
            // 
            this.driver.HeaderText = "Tipe";
            this.driver.MinimumWidth = 6;
            this.driver.Name = "driver";
            this.driver.Width = 125;
            // 
            // Tip
            // 
            this.Tip.HeaderText = "Keterangan";
            this.Tip.MinimumWidth = 6;
            this.Tip.Name = "Tip";
            this.Tip.Width = 125;
            // 
            // Berat
            // 
            this.Berat.HeaderText = "Status Fraud";
            this.Berat.MinimumWidth = 6;
            this.Berat.Name = "Berat";
            this.Berat.Width = 125;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 16);
            this.label1.TabIndex = 14;
            this.label1.Text = "Histori Transaksi";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(747, 91);
            this.buttonSearch.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(100, 28);
            this.buttonSearch.TabIndex = 20;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // dateTimePickerAkhir
            // 
            this.dateTimePickerAkhir.Location = new System.Drawing.Point(419, 95);
            this.dateTimePickerAkhir.Margin = new System.Windows.Forms.Padding(4);
            this.dateTimePickerAkhir.Name = "dateTimePickerAkhir";
            this.dateTimePickerAkhir.Size = new System.Drawing.Size(265, 22);
            this.dateTimePickerAkhir.TabIndex = 19;
            this.dateTimePickerAkhir.ValueChanged += new System.EventHandler(this.dateTimePickerAkhir_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(364, 103);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 16);
            this.label3.TabIndex = 18;
            this.label3.Text = "s/d";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(72, 73);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 16);
            this.label2.TabIndex = 17;
            this.label2.Text = "Pilih Periode";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // dateTimePickerAwal
            // 
            this.dateTimePickerAwal.Location = new System.Drawing.Point(72, 96);
            this.dateTimePickerAwal.Margin = new System.Windows.Forms.Padding(4);
            this.dateTimePickerAwal.Name = "dateTimePickerAwal";
            this.dateTimePickerAwal.Size = new System.Drawing.Size(265, 22);
            this.dateTimePickerAwal.TabIndex = 16;
            this.dateTimePickerAwal.ValueChanged += new System.EventHandler(this.dateTimePickerAwal_ValueChanged);
            // 
            // buttonExportPdf
            // 
            this.buttonExportPdf.Location = new System.Drawing.Point(952, 493);
            this.buttonExportPdf.Name = "buttonExportPdf";
            this.buttonExportPdf.Size = new System.Drawing.Size(75, 23);
            this.buttonExportPdf.TabIndex = 21;
            this.buttonExportPdf.Text = "Export";
            this.buttonExportPdf.UseVisualStyleBackColor = true;
            this.buttonExportPdf.Click += new System.EventHandler(this.buttonExportPdf_Click);
            // 
            // FormHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1135, 602);
            this.Controls.Add(this.buttonExportPdf);
            this.Controls.Add(this.dataGridViewDataRiwayat);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.dateTimePickerAkhir);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePickerAwal);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormHistory";
            this.Text = "FormHistory";
            this.Load += new System.EventHandler(this.FormHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDataRiwayat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewDataRiwayat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.DateTimePicker dateTimePickerAkhir;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePickerAwal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tanggal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Biaya;
        private System.Windows.Forms.DataGridViewTextBoxColumn driver;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tip;
        private System.Windows.Forms.DataGridViewTextBoxColumn Berat;
        private System.Windows.Forms.Button buttonExportPdf;
    }
}