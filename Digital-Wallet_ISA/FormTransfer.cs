using DigitalWallet.BackEnd;
using System;
using System.Windows.Forms;

namespace Digital_Wallet_ISA
{
    public partial class FormTransfer : Form
    {
        private readonly int _userId;
        private decimal _saldoSaatIni;

        public FormTransfer(int userId)
        {
            InitializeComponent();
            _userId = userId;
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
                    labelSaldo.Text = _saldoSaatIni.ToString("N0"); // Menampilkan angka saldo
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat saldo: " + ex.Message);
            }
        }



        private void FormTransfer_Load(object sender, EventArgs e)
        {
            MuatSaldo();

        }

        private void buttonTransfer_Click(object sender, EventArgs e)
        {
            string targetUser = textBoxTujuan.Text.Trim();
            decimal jumlah = numericUpDownNominal.Value;
            string pesan = textBoxPesan.Text.Trim();

            // 2. Validasi Input
            if (string.IsNullOrEmpty(targetUser))
            {
                MessageBox.Show("Username penerima harus diisi.");
                return;
            }

            if (jumlah < 1000)
            {
                MessageBox.Show("Minimal transfer adalah Rp 1.000.");
                return;
            }

            if (jumlah > _saldoSaatIni)
            {
                MessageBox.Show("Saldo Anda tidak mencukupi.");
                return;
            }

            // 3. Konfirmasi
            DialogResult konfirmasi = MessageBox.Show(
                $"Kirim ke: {targetUser}\nJumlah: Rp {jumlah:N0}\nPesan: {pesan}\n\nLanjutkan?",
                "Konfirmasi Transfer", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (konfirmasi != DialogResult.Yes) return;

            try
            {
                buttonTransfer.Enabled = false;

                // Di dalam method button_Click sebelum menjalankan query UPDATE saldo
                int txCount = TransaksiManager.GetTransaksiMenitTerakhir(_userId); // Buat helper untuk hitung jumlah baris di tabel transactions
                var fraudCheck = FraudDetector.AnalyzeTransaction(_userId, jumlah, txCount);

                if (fraudCheck.IsFlagged)
                {
                    // Simpan log kecurigaan ke DB
                    FraudDetector.SaveFraudLog(_userId, -1, fraudCheck.Reason, fraudCheck.Severity);

                    if (fraudCheck.Severity == "high")
                    {
                        MessageBox.Show($"Transaksi DITOLAK: {fraudCheck.Reason}", "Keamanan Tinggi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return; // Batalkan transaksi
                    }
                    else
                    {
                        MessageBox.Show($"Peringatan: {fraudCheck.Reason}. Transaksi akan diawasi.", "Peringatan Keamanan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                // 4. Panggil Logic BackEnd
                // Method Transfer mengembalikan (bool sukses, string pesan, int transactionId)
                var result = TransaksiManager.Transfer(_userId, targetUser, jumlah, pesan);

                if (result.sukses)
                {
                    MessageBox.Show(result.pesan, "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK; // Memberitahu Form Utama untuk refresh saldo
                    this.Close();
                }
                else
                {
                    MessageBox.Show(result.pesan, "Transfer Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                buttonTransfer.Enabled = true;
            }
        }

        private void buttonPin_Click(object sender, EventArgs e)
        {
            try
            {
                int walletId = TransaksiManager.GetWalletId(_userId);
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
                        if (fraudResult.Severity == "High")
                        {
                            buttonTransfer.Enabled = false;
                            return;
                        }
                    }

                    // Jika semua aman atau severity rendah
                    buttonTransfer.Enabled = true;
                    MessageBox.Show("PIN Berhasil diverifikasi.");
                }
                else
                {
                    MessageBox.Show("Pin salah, coba lagi");
                    // Catat kegagalan PIN ke Audit Log untuk memantau Brute Force
                    TransaksiManager.CatatAuditLog(_userId, "Gagal verifikasi PIN (Kemungkinan percobaan Brute Force)");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pastikan nominal sudah diisi: " + ex.Message);
            }
        }
    }
}