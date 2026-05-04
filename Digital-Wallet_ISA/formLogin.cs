using DigitalWallet.BackEnd;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using BackEnd;
namespace Digital_Wallet_ISA
{
    public partial class formLogin : Form
    {
        public formLogin()
        {
            InitializeComponent();
        }

        private void formLogin_Load(object sender, EventArgs e)
        {
            try
            {
                Koneksi k = new Koneksi(db.Default.DbServer, db.Default.DbName, db.Default.DbUsername, db.Default.DbPassword);
                MessageBox.Show("Koneksi Berhasil");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text.Trim();
            string password = textBoxPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Username dan Password tidak boleh kosong.");
                return;
            }

            string safeUsername = username.Replace("'", "''");

            try
            {
                // 1. Ubah query dan gunakan JalankanSelect yang mereturn DataTable
                string query = $"SELECT * FROM users WHERE username='{safeUsername}'";
                DataTable dtUser = Koneksi.JalankanSelect(query);

                // 2. Cek apakah ada baris data yang ditemukan
                if (dtUser.Rows.Count > 0)
                {
                    // Ambil baris pertama
                    DataRow dr = dtUser.Rows[0];

                    int userId = Convert.ToInt32(dr["id"]);
                    string storedHash = dr["password_hash"].ToString();
                    string salt = dr["salt"].ToString();
                    string role = dr["role"].ToString();
                    int failed = Convert.ToInt32(dr["failed_attempts"]);
                    bool isLocked = Convert.ToBoolean(dr["is_locked"]);

                    // 3. Cek Status Kunci Akun
                    if (isLocked)
                    {
                        MessageBox.Show("Akun ini telah dikunci.", "Account Locked", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    // 4. Verifikasi Password
                    if (Enkripsi.VerifyPassword(password, storedHash, salt))
                    {
                        Koneksi.JalankanQuery($"UPDATE users SET failed_attempts=0 WHERE id={userId}");
                        TransaksiManager.CatatAuditLog(userId, "User logged in successfully");

                        UserSession.UserId = userId;
                        UserSession.Username = dr["username"].ToString();

                        // ✅ AMBIL WALLET ID
                        object walletResult = Koneksi.JalankanSelectSatu(
                            $"SELECT id FROM wallets WHERE user_id = {userId} LIMIT 1"
                        );
                        UserSession.WalletId = walletResult != null ? Convert.ToInt32(walletResult) : 0;

                        if (role.ToLower() == "admin")
                            new FormAdmin(userId).Show();
                        else
                        {
                            if (MasterPassword.SudahPunyaMasterPassword(userId))
                            {
                                new formUtama(userId).Show();
                            }
                            else
                            {
                                MessageBox.Show("Anda belum memiliki master password. Silakan buat master password terlebih dahulu.");
                            }
                        }

                        this.Hide();
                    }
                    else
                    {
                        failed++;
                        Koneksi.JalankanQuery($"UPDATE users SET failed_attempts={failed} WHERE id={userId}");

                        bool nowLocked = FraudDetector.CheckAndLockAccount(userId, failed);
                        if (nowLocked)
                            MessageBox.Show("Akun dikunci karena 3x percobaan gagal.");
                        else
                            MessageBox.Show($"Password salah! Percobaan {failed}/3");
                    }
                }
                else
                {
                    MessageBox.Show("Username tidak ditemukan.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void buttonSignUp_Click(object sender, EventArgs e)
        {
            FormSignUp formSignup = new FormSignUp();
            formSignup.Show();
        }
    }
}
