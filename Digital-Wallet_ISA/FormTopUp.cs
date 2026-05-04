using DigitalWallet.BackEnd;
using System;
using System.Data;
using System.Windows.Forms;

namespace Digital_Wallet_ISA
{
    public partial class FormTopUp : Form
    {
        private readonly int _userId;
        private decimal _saldoSaatIni;

        public FormTopUp(int userId)
        {
            _userId = userId;
            InitializeComponent();
            MuatSaldo();
        }

        private void MuatSaldo()
        {
            try
            {
                int walletId = TransaksiManager.GetWalletId(_userId);
                if (walletId != -1)
                {
                    _saldoSaatIni = TransaksiManager.GetSaldo(walletId);
                    labelSaldo.Text = "Rp " + _saldoSaatIni.ToString("N0");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat saldo: " + ex.Message);
            }
        }

        private void buttonTopUp_Click(object sender, EventArgs e)
        {
            // 1. Validasi Input (Menyesuaikan dengan numericUpDown atau TextBox)
            decimal jumlah = numericUpDownNominal.Value;

            if (jumlah <= 0)
            {
                MessageBox.Show("Masukkan jumlah top up yang valid (lebih dari 0).",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

        

            // 2. Konfirmasi
            DialogResult konfirmasi = MessageBox.Show(
                $"Konfirmasi Top Up\n\n" +
                $"Jumlah    : Rp {jumlah:N0}\n" +
                $"Keterangan: Top Up\n\n" +
                $"Lanjutkan?",
                "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (konfirmasi != DialogResult.Yes) return;

            try
            {
                // Menonaktifkan tombol agar tidak terjadi double-click (spam)
                buttonTopUp.Enabled = false;

                // 3. Panggil Logic dari BackEnd
                // Parameter: userId, jumlah, deskripsi
                var hasil = TransaksiManager.TopUp(_userId, jumlah);
                bool sukses = hasil.sukses;
                string pesan = hasil.pesan;

                if (sukses)
                {
                    MuatSaldo(); // Refresh label saldo setelah berhasil

                    // Reset Input
                    numericUpDownNominal.Value = 0;

                    // Cek apakah pesan mengandung indikasi fraud/peringatan
                    MessageBoxIcon icon = pesan.ToLower().Contains("peringatan") || pesan.ToLower().Contains("fraud")
                        ? MessageBoxIcon.Warning
                        : MessageBoxIcon.Information;

                    MessageBox.Show(pesan + $"\n\nSaldo sekarang: Rp {_saldoSaatIni:N0}",
                        "Status Top Up", MessageBoxButtons.OK, icon);
                }
                else
                {
                    MessageBox.Show(pesan, "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan sistem: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Mengaktifkan kembali tombol setelah proses selesai
                buttonTopUp.Enabled = true;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonPin_Click(object sender, EventArgs e)
        {
            try
            {
                int walletId = TransaksiManager.GetWalletId(_userId);
                string query = $"SELECT * FROM users WHERE id='{_userId}'";
                DataTable dtUser = Koneksi.JalankanSelect(query);


                int failed = Convert.ToInt32(dtUser.Rows[0]["failed_attempts"]);
                string pin = textBoxPin.Text;

                // 1. Verifikasi PIN Dasar
                bool pinValid = TransaksiManager.VerifikasiPin(walletId, pin);

                if (pinValid)
                {
                    // 2. Ambil data pendukung untuk Fraud Detection
                    decimal jumlahTransfer = decimal.Parse(numericUpDownNominal.Text); // Ambil dari input nominal
                    int txMenitTerakhir = TransaksiManager.GetTransaksiMenitTerakhir(_userId);

                    // 3. Jalankan Analisis Fraud
                    var fraudResult = FraudDetector.AnalyzeTransaction(_userId, jumlahTransfer, txMenitTerakhir);

                    if (fraudResult.IsFlagged)
                    {
                        // Jika terdeteksi fraud (misal: transfer terlalu sering dalam 1 menit)
                        string pesan = $"Peringatan Keamanan: {fraudResult.Reason}\n" +
                                       $"Keputusan: {fraudResult.Severity}";

                        MessageBox.Show(pesan, "Fraud Detection", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        // Opsional: Tetap izinkan tapi catat, atau blokir total
                        if (fraudResult.Severity == "high")
                        {
                            buttonTopUp.Enabled = false;
                            numericUpDownNominal.Enabled = false;
                            return;
                        }
                    }

                    // Jika semua aman atau severity rendah
                    buttonTopUp.Enabled = true;
                    numericUpDownNominal.Enabled = true;
                    MessageBox.Show("PIN Berhasil diverifikasi.");
                }
                else
                {
                    failed++;
                    Koneksi.JalankanQuery($"UPDATE users SET failed_attempts={failed} WHERE id={_userId}");

                    bool nowLocked = FraudDetector.CheckAndLockAccount(_userId, failed);
                    if (nowLocked)
                    {
                        MessageBox.Show("Akun dikunci karena 3x percobaan gagal.");
                        this.Close(); // Tutup form karena akun terkunci
                        Owner.Close();
                        Application.OpenForms["formLogin"].Show();

                    }
                    else
                        MessageBox.Show($"Pin salah! Percobaan {failed}/3");
                    // Catat kegagalan PIN ke Audit Log untuk memantau Brute Force
                    TransaksiManager.CatatAuditLog(_userId, "Gagal verifikasi PIN (Kemungkinan percobaan Brute Force)");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pastikan nominal sudah diisi: " + ex.Message);
            }
        }

        private void FormTopUp_Load(object sender, EventArgs e)
        {
            buttonTopUp.Enabled = false; // Nonaktifkan tombol Top Up sampai PIN diverifikasi
            numericUpDownNominal.Enabled = false; // Nonaktifkan input nominal sampai PIN diverifikasi
        }
    }
}