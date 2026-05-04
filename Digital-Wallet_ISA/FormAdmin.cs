using BackEnd;
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
            MuatUserTerkunci();
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
            string query = @"SELECT f.id, u.username, f.reason, f.severity, f.amount, f.description, f.created_at 
                 FROM fraud_logs f
                    LEFT JOIN users u ON f.user_id = u.id
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

        private void buttonUnlock_Click(object sender, EventArgs e)
        {
            if (dataGridViewLocked.SelectedRows.Count > 0)
            {
                int userId = Convert.ToInt32(dataGridViewLocked.SelectedRows[0].Cells["id"].Value);
                string username = dataGridViewLocked.SelectedRows[0].Cells["username"].Value.ToString();

                DialogResult res = MessageBox.Show($"Buka kunci akun untuk {username}?", "Konfirmasi", MessageBoxButtons.YesNo);

                if (res == DialogResult.Yes)
                {
                    UserManager.UnlockUser(userId);
                    MessageBox.Show("Akun berhasil dibuka!");

                    // Audit Log: Catat tindakan admin
                    string logAction = $"Admin (ID:{_adminId}) unlocked user {username}";
                    Koneksi.JalankanQuery($"INSERT INTO audit_logs (user_id, action, created_at) VALUES ({_adminId}, '{logAction}', NOW())");

                    MessageBox.Show("Akun berhasil dibuka!");
                    MuatUserTerkunci(); // Refresh list
                }
            }
        }

        private void MuatUserTerkunci()
        {
            // Panggil method dari UserManager yang sudah kita bahas sebelumnya
            dataGridViewLocked.DataSource = UserManager.GetLockedUsers();
        }
    }
}