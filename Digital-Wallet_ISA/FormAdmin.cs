using DigitalWallet.BackEnd;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Digital_Wallet_ISA
{
    public partial class FormAdmin : Form
    {
        private int _adminId;

        public FormAdmin(int adminId)
        {
            InitializeComponent();
            _adminId = adminId;
        }

        private void FormAdmin_Load(object sender, EventArgs e)
        {
            MuatStatistik();
            MuatFraudLogs();
        }

        // 1. Memantau Statistik Global
        private void MuatStatistik()
        {
            string qBalance = "SELECT SUM(balance) FROM wallets";
            object totalBalance = Koneksi.JalankanSelectSatu(qBalance);
            lblTotalSaldo.Text = "Total Saldo Sistem: Rp " + Convert.ToDecimal(totalBalance).ToString("N0");

            string qUser = "SELECT COUNT(*) FROM users WHERE role = 'user'";
            lblTotalUser.Text = "Total Pengguna: " + Koneksi.JalankanSelectSatu(qUser).ToString();
        }

        // 2. Memantau Log Kecurangan (Fraud Detection)
        private void MuatFraudLogs()
        {
            // Gunakan LEFT JOIN agar data fraud tetap muncul meskipun relasi tabel lain bermasalah
            string query = @"SELECT f.id, u.username, f.reason, f.severity, t.amount, t.description, f.created_at 
                 FROM fraud_logs f
                 LEFT JOIN users u ON f.user_id = u.id
                 LEFT JOIN transactions t ON f.transaction_id = t.id
                 ORDER BY f.created_at DESC";

            DataTable dt = Koneksi.JalankanSelect(query);

            // Tambahkan kolom baru untuk deskripsi yang sudah didekripsi
            dt.Columns.Add("Decrypted_Description", typeof(string));

            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    // Dekripsi agar Admin bisa membaca alasan transaksi
                    string rawDesc = row["description"].ToString();
                    row["Decrypted_Description"] = Enkripsi.AESDecrypt(rawDesc);
                }
                catch
                {
                    row["Decrypted_Description"] = "Bukan data terenkripsi";
                }
            }

            dgvFraud.DataSource = dt;
            FormatGridFraud();
        }

        // 3. Memberi Warna pada Baris Berbahaya
        private void FormatGridFraud()
        {
            foreach (DataGridViewRow row in dgvFraud.Rows)
            {
                string severity = row.Cells["severity"].Value?.ToString();
                if (severity == "High")
                {
                    row.DefaultCellStyle.BackColor = Color.LightPink; // Merah untuk indikasi fraud tinggi
                    row.DefaultCellStyle.ForeColor = Color.DarkRed;
                }
                else if (severity == "Medium")
                {
                    row.DefaultCellStyle.BackColor = Color.LightYellow;
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            MuatStatistik();
            MuatFraudLogs();
        }
    }
}