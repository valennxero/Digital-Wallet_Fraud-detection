using DigitalWallet.BackEnd;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class UserManager
    {
        public static DataTable GetLockedUsers()
        {
            // Mengambil data user yang status is_locked-nya bernilai 1 (True)
            string query = "SELECT id, username, email, failed_attempts FROM users WHERE is_locked = 1";
            return Koneksi.JalankanSelect(query);
        }

        public static void UnlockUser(int userId)
        {
            // Membuka kunci akun dan mereset hitungan percobaan gagal
            string query = $@"UPDATE users SET is_locked = 0, failed_attempts = 0 WHERE id = {userId}";
            Koneksi.JalankanQuery(query);
        }
    }
}
