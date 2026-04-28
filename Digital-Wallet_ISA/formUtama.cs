using DigitalWallet.BackEnd;
using System;
using System.Data;
using System.Windows.Forms;

namespace Digital_Wallet_ISA
{
    public partial class formUtama : Form
    {
        public readonly int _userId;
        public int _walletId;

        public formUtama(int userId)
        {
            InitializeComponent();
            _userId = userId; // Menyimpan ID user yang login
        }

        private void formUtama_Load(object sender, EventArgs e)
        {
            MuatDataUser();
        }

        public void MuatDataUser()
        {
            try
            {
                // 1. Ambil Wallet ID
                _walletId = TransaksiManager.GetWalletId(_userId);

                if (_walletId != -1)
                {
                    // 2. Ambil Saldo
                    decimal saldo = TransaksiManager.GetSaldo(_walletId);
                    lblNominal.Text = "Rp " + saldo.ToString("N0");

                    // 3. Ambil Nama User (Opsional, untuk greeting)
                    DataTable dt = Koneksi.JalankanSelect($"SELECT username FROM users WHERE id = {_userId}");
                    if (dt.Rows.Count > 0)
                    {
                        labelUserName.Text = $"{dt.Rows[0]["username"]}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data: " + ex.Message);
            }
        }

        private void buttonTopUp_Click(object sender, EventArgs e)
        {
            // Kirim _userId ke FormTopUp agar form tersebut tahu siapa yang top up
            FormTopUp frm = new FormTopUp(_userId);

            // Gunakan ShowDialog agar form utama menunggu sampai Top Up selesai
            frm.ShowDialog();

            // Setelah FormTopUp ditutup, refresh saldo di form utama
            MuatDataUser();
        }

  

        private void buttonTransfer_Click_1(object sender, EventArgs e)
        {
            FormTransfer frm = new FormTransfer(_userId);
            
            frm.Owner = this;
            frm.ShowDialog();
            MuatDataUser();
        }

        private void buttonLogOut_Click_1(object sender, EventArgs e)
        {
            this.Close();
            // Tampilkan kembali form login jika perlu
            Application.OpenForms["formLogin"].Show();
        }

        private void buttonHistory_Click(object sender, EventArgs e)
        {
            // Kirim _userId ke FormTopUp agar form tersebut tahu siapa yang top up
            FormHistory frm = new FormHistory();

            // Gunakan ShowDialog agar form utama menunggu sampai Top Up selesai
            frm.ShowDialog();

            // Setelah FormTopUp ditutup, refresh saldo di form utama
            MuatDataUser();
        }
    }
}