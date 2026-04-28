using DigitalWallet.BackEnd;
using System;
using System.Data;
using System.Windows.Forms;

namespace Digital_Wallet_ISA
{
    public partial class FormSignUp : Form
    {
        public FormSignUp()
        {
            InitializeComponent();
        }

        private void buttonSignUp_Click(object sender, EventArgs e)
        {
            // 1. Ambil dan validasi input
            string username = textBoxUsername.Text.Trim();
            string email = textBoxEmail.Text.Trim();
            string password = textBoxPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Semua kolom harus diisi!");
                return;
            }

            // Sanitasi input manual
            string safeUsername = username.Replace("'", "''");
            string safeEmail = email.Replace("'", "''");
            string pin = textBoxPin.Text.Trim();

            // Validasi sederhana: PIN harus 6 angka
            if (pin.Length != 6 || !int.TryParse(pin, out _))
            {
                MessageBox.Show("PIN harus berupa 6 digit angka!");
                return;
            }

            try
            {
                // 2. Cek apakah username sudah ada (mencegah duplikasi)
                string checkQuery = $"SELECT id FROM users WHERE username='{safeUsername}'";
                DataTable dtCheck = Koneksi.JalankanSelect(checkQuery);

                if (dtCheck.Rows.Count > 0)
                {
                    MessageBox.Show("Username sudah digunakan, silakan pilih username lain.");
                    return;
                }

                // 3. Proses Hashing Password Login
                string salt = Enkripsi.GenerateSalt();
                string hash = Enkripsi.HashPassword(password, salt);

                // 4. Insert User Baru
                string queryInsert = $@"INSERT INTO users (username, email, password_hash, salt, role) 
                                 VALUES ('{safeUsername}', '{safeEmail}', '{hash}', '{salt}', 'user')";
                Koneksi.JalankanQuery(queryInsert);


                // 5. Ambil ID User yang baru dibuat
                string getIdQuery = $"SELECT id FROM users WHERE username='{safeUsername}'";
                DataTable dtUser = Koneksi.JalankanSelect(getIdQuery);

                if (dtUser.Rows.Count > 0)
                {
                    int newUserId = Convert.ToInt32(dtUser.Rows[0]["id"]);

                    // 6. Inisialisasi Keamanan Tambahan

                    // A. Buat Wallet baru dengan saldo 0
                    string pinEnc = Enkripsi.AESEncrypt(pin);
                    string queryWallet = $@"INSERT INTO wallets (user_id, balance, pin_encrypted) 
                                   VALUES ({newUserId}, 0, '{pinEnc}')";
                    Koneksi.JalankanQuery(queryWallet);



                    // B. Set Master Password awal (Default sama dengan password login atau minta input baru)
                    // Disarankan menggunakan password login sebagai master password awal agar user tidak bingung
                    MasterPassword.SetMasterPassword(newUserId, password);

                    // C. Catat log pendaftaran
                    TransaksiManager.CatatAuditLog(newUserId, "User registered, wallet created, and Master Password initialized.");
                }

                MessageBox.Show("Registrasi berhasil! Master Password Anda telah diatur sama dengan password login Anda.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan saat registrasi: " + ex.Message);
            }
        }

        private void FormSignUp_Load(object sender, EventArgs e)
        {

        }
    }
}