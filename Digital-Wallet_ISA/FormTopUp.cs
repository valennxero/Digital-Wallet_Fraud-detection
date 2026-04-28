using DigitalWallet.BackEnd;
using System;
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
    }
}