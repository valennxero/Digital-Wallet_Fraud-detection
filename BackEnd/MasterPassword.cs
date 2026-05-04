using DigitalWallet.BackEnd;
using System;
using MySql.Data.MySqlClient;
using System.Data;

namespace DigitalWallet.BackEnd
{
    public class MasterPassword
    {
        // =============================================
        //  SET MASTER PASSWORD (pertama kali / reset)
        //  Dipanggil saat: Register atau Setup awal
        // =============================================
        public static void SetMasterPassword(int userId, string masterPassword)
        {
            string salt = Enkripsi.GenerateSalt();
            string hash = Enkripsi.HashPassword(masterPassword, salt);

            // Cek apakah user sudah punya master password
            var dt = Koneksi.JalankanSelect(
                $"SELECT id FROM master_config WHERE user_id = {userId}");

            if (dt.Rows.Count > 0)
            {
                // Update jika sudah ada
                Koneksi.JalankanQuery(
                    $"UPDATE master_config SET master_hash='{hash}', salt='{salt}' " +
                    $"WHERE user_id = {userId}");
            }
            else
            {
                // Insert baru
                Koneksi.JalankanQuery(
                    $"INSERT INTO master_config (user_id, master_hash, salt) " +
                    $"VALUES ({userId}, '{hash}', '{salt}')");
            }
        }

        // =============================================
        //  CEK: apakah user sudah punya master password
        // =============================================
        public static bool SudahPunyaMasterPassword(int userId)
        {
            var dt = Koneksi.JalankanSelect(
                $"SELECT id FROM master_config WHERE user_id = {userId}");
            return dt.Rows.Count > 0;
        }

        // =============================================
        //  VERIFIKASI MASTER PASSWORD
        //  Return true jika cocok, false jika salah
        // =============================================
       
    }
}