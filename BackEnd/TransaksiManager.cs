using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace DigitalWallet.BackEnd
{
    public class TransaksiManager
    {
        public static int GetWalletId(int userId)
        {
            string query = $"SELECT id FROM wallets WHERE user_id = {userId}";
            DataTable dt = Koneksi.JalankanSelect(query);
            if (dt.Rows.Count > 0)
                return Convert.ToInt32(dt.Rows[0]["id"]);
            return -1;
        }

        public static int GetWalletIdByUsername(string username)
        {
            string query = $@"SELECT w.id FROM wallets w
                               INNER JOIN users u ON w.user_id = u.id
                               WHERE u.username = '{EscapeString(username)}'";
            DataTable dr = Koneksi.JalankanSelect(query);
            if (dr.Rows.Count > 0)
                // FIX: Ambil kolom "id"
                return Convert.ToInt32(dr.Rows[0]["id"]);
            return -1;
        }

        public static decimal GetSaldo(int walletId)
        {
            string query = $"SELECT balance FROM wallets WHERE id = {walletId}";
            DataTable dt = Koneksi.JalankanSelect(query);
            if (dt.Rows.Count > 0)
                return Convert.ToDecimal(dt.Rows[0]["balance"]);
            return 0;
        }

        // --- Logika Transfer dengan Transaksi ---
        // Kita gunakan instance Koneksi secara manual agar proses pengurangan 
        // dan penambahan saldo terjadi dalam satu kesatuan (Atomic).
        public static (bool sukses, string pesan, int transactionId) Transfer(
            int userId, string usernameTarget, decimal jumlah, string deskripsi = "Transfer")
        {
            if (jumlah < 1000) return (false, "Minimal transfer Rp 1.000", -1);

            int fromId = GetWalletId(userId);
            int toId = GetWalletIdByUsername(usernameTarget);

            if (toId == -1) return (false, "Penerima tidak ditemukan", -1);
            if (fromId == toId) return (false, "Tidak bisa transfer ke diri sendiri", -1);

            decimal saldo = GetSaldo(fromId);
            if (saldo < jumlah) return (false, "Saldo tidak cukup", -1);

            Koneksi k = new Koneksi();
            k.Open();
            MySqlTransaction tr = k.KoneksiDB.BeginTransaction();

            try
            {
                // 1. Kurangi Saldo
                string q1 = $"UPDATE wallets SET balance = balance - {jumlah} WHERE id = {fromId}";
                new MySqlCommand(q1, k.KoneksiDB, tr).ExecuteNonQuery();

                // 2. Tambah Saldo
                string q2 = $"UPDATE wallets SET balance = balance + {jumlah} WHERE id = {toId}";
                new MySqlCommand(q2, k.KoneksiDB, tr).ExecuteNonQuery();

                // 3. Catat Transaksi
                string q3 = $@"INSERT INTO transactions (from_wallet_id, to_wallet_id, amount, type, description, created_at) 
                               VALUES ({fromId}, {toId}, {jumlah}, 'transfer', '{EscapeString(deskripsi)}', NOW())";
                new MySqlCommand(q3, k.KoneksiDB, tr).ExecuteNonQuery();

                tr.Commit();
                CatatAuditLog(userId, $"Transfer ke {usernameTarget} sejumlah {jumlah}");
                return (true, "Transfer Berhasil", 0);
            }
            catch (Exception ex)
            {
                tr.Rollback();
                return (false, "Gagal: " + ex.Message, -1);
            }
            finally { k.Close(); }
        }

        // --- Helper ---
        private static int GetLastInsertId()
        {
            // Menggunakan JalankanSelectSatu dari class Koneksi Anda
            object hasil = Koneksi.JalankanSelectSatu("SELECT LAST_INSERT_ID()");
            return hasil != null ? Convert.ToInt32(hasil) : -1;
        }

        private static string EscapeString(string input)
        {
            return input?.Replace("'", "''") ?? "";
        }

        public static void CatatAuditLog(int userId, string action)
        {
            // Tambahkan kolom ip_address agar query tidak error
            string query = $@"INSERT INTO audit_logs (user_id, action, ip_address, created_at)
                     VALUES ({userId}, '{action.Replace("'", "''")}', '127.0.0.1', NOW())";
            Koneksi.JalankanQuery(query);
        }

        // Tambahkan method ini di dalam class TransaksiManager
        public static (bool sukses, string pesan) TopUp(int userId, decimal jumlah, string deskripsi = "Top Up Saldo")
{
    try
    {
        // 1. Validasi awal
        if (jumlah <= 0) return (false, "Jumlah top up harus lebih dari 0.");

        int walletId = GetWalletId(userId);
        if (walletId == -1) return (false, "Dompet tidak ditemukan.");

        // 2. Cek indikasi Fraud (Ambil jumlah transaksi menit terakhir)
        int txCount = GetTransaksiMenitTerakhir(walletId);
        var fraud = FraudDetector.AnalyzeTransaction(userId, jumlah, txCount);

        // 3. Gunakan Transaction agar update saldo dan insert tabel transaksi sinkron
        Koneksi k = new Koneksi();
        k.Open();
        MySqlTransaction tr = k.KoneksiDB.BeginTransaction();

        try
        {
            // A. Update Saldo di tabel wallets
            string queryUpdate = $"UPDATE wallets SET balance = balance + {jumlah} WHERE id = {walletId}";
            new MySqlCommand(queryUpdate, k.KoneksiDB, tr).ExecuteNonQuery();

            // B. Catat ke tabel transactions
            // Perhatikan: to_wallet_id diisi walletId kita sendiri, from_wallet_id biarkan NULL (karena Top Up)
            string queryInsert = $@"INSERT INTO transactions (to_wallet_id, amount, type, description, is_flagged, created_at) 
                                    VALUES ({walletId}, {jumlah}, 'topup', '{EscapeString(deskripsi)}', {(fraud.IsFlagged ? 1 : 0)}, NOW())";
            new MySqlCommand(queryInsert, k.KoneksiDB, tr).ExecuteNonQuery();

            tr.Commit();

            // C. Ambil ID transaksi terakhir untuk log fraud jika ada
            int transId = GetLastInsertId();
            if (fraud.IsFlagged)
            {
                FraudDetector.SaveFraudLog(userId, transId, fraud.Reason, fraud.Severity);
            }

            CatatAuditLog(userId, $"Top Up berhasil: Rp {jumlah:N0}");

            // Kembalikan pesan sukses (dengan peringatan jika terdeteksi fraud)
            string pesanHasil = fraud.IsFlagged ? "⚠️ Top Up Berhasil (Dicek oleh Sistem Keamanan)" : "Top Up Berhasil";
            return (true, pesanHasil);
        }
        catch (Exception ex)
        {
            tr.Rollback();
            return (false, "Gagal memproses database: " + ex.Message);
        }
        finally
        {
            k.Close();
        }
    }
    catch (Exception ex)
    {
        return (false, "Terjadi kesalahan: " + ex.Message);
    }
}

// Helper untuk menghitung transaksi menit terakhir (untuk Fraud Detection)
public static int GetTransaksiMenitTerakhir(int userId)
{
            // Cari walletId berdasarkan userId terlebih dahulu
            int walletId = GetWalletId(userId);

            if (walletId == -1) return 0;

            // Hitung jumlah transaksi dalam 1 menit terakhir
            // Sesuai ERD, kita cek kolom from_wallet_id atau to_wallet_id
            string query = $@"SELECT COUNT(*) FROM transactions 
                      WHERE (from_wallet_id = {walletId} OR to_wallet_id = {walletId}) 
                      AND created_at >= DATE_SUB(NOW(), INTERVAL 1 MINUTE)";

            object hasil = Koneksi.JalankanSelectSatu(query);
            return hasil != null ? Convert.ToInt32(hasil) : 0;
        }


        public static bool VerifikasiPin(int walletId, string inputPin)
        {
            string query = $"SELECT pin_encrypted FROM wallets WHERE id = {walletId}";
            DataTable dt = Koneksi.JalankanSelect(query);

            if (dt.Rows.Count > 0)
            {
                string pinTerenkripsi = dt.Rows[0]["pin_encrypted"].ToString();
                // Dekripsi untuk melihat aslinya
                string pinAsli = Enkripsi.DecryptDouble(pinTerenkripsi);

                return pinAsli == inputPin;
            }
            return false;
        }
        public static void SetWalletPin(int userId, string pinPolos)
        {
            // 1. Enkripsi PIN menggunakan AES agar aman di database
            string pinTerenkripsi = Enkripsi.EncryptDouble(pinPolos);

            // 2. Update kolom pin_encrypted berdasarkan user_id
            string query = $@"UPDATE wallets SET pin_encrypted = '{pinTerenkripsi}' 
                     WHERE user_id = {userId}";

            Koneksi.JalankanQuery(query);
        }
    }
}

