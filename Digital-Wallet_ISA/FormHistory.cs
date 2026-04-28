using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BackEnd;
using DigitalWallet.BackEnd;
using MySql.Data.MySqlClient;
using static DigitalWallet.BackEnd.Koneksi;

namespace Digital_Wallet_ISA
{
    public partial class FormHistory : Form
    {
        public FormHistory()
        {
            InitializeComponent();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime tanggalMulai = dateTimePickerAwal.Value.Date;
                DateTime tanggalAkhir = dateTimePickerAkhir.Value.Date.AddDays(1).AddSeconds(-1);

                Koneksi k = new Koneksi();
                k.Open();

                string query = $@"
            SELECT 
                t.id,
                t.created_at,
                t.amount,
                t.type,
                t.description,
                CASE t.is_flagged WHEN 1 THEN 'Ya' ELSE 'Tidak' END AS is_flagged
            FROM transactions t
            WHERE 
                (t.from_wallet_id = {UserSession.WalletId} 
                 OR t.to_wallet_id = {UserSession.WalletId})
                AND t.created_at BETWEEN '{tanggalMulai:yyyy-MM-dd HH:mm:ss}' 
                                      AND '{tanggalAkhir:yyyy-MM-dd HH:mm:ss}'
            ORDER BY t.created_at DESC";

                MySqlCommand cmd = new MySqlCommand(query, k.KoneksiDB);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                k.Close();

                dataGridViewDataRiwayat.DataSource = null;
                dataGridViewDataRiwayat.Rows.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    dataGridViewDataRiwayat.Rows.Add(
                        row[0],  // Id Transaksi
                        row[1],  // Tanggal
                        row[2],  // Jumlah (Rp)
                        row[3],  // Tipe
                        row[4],  // Keterangan
                        row[5]   // Status Fraud
                    );
                }

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "Tidak ada transaksi pada periode yang dipilih.",
                        "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormHistory_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridViewDataRiwayat_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dateTimePickerAkhir_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePickerAwal_ValueChanged(object sender, EventArgs e)
        {

        }

        private void buttonExportPdf_Click(object sender, EventArgs e)
        {
            // 1. Siapkan dialog penyimpanan file
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PDF Files (*.pdf)|*.pdf";
            sfd.FileName = "Laporan_Transaksi_" + DateTime.Now.ToString("yyyyMMdd") + ".pdf";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // 2. Panggil class exporter (Gunakan _userId yang sedang login)
                    // Pastikan Anda mengirimkan filePath dari SaveFileDialog
                    PdfExporter.ExportTransaksi(UserSession.UserId, sfd.FileName);

                    MessageBox.Show("Laporan PDF berhasil dicetak dan dibuka!", "Sukses",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal mengekspor PDF: " + ex.Message, "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
