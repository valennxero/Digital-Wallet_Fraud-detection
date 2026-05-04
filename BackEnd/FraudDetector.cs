using DigitalWallet.BackEnd;
using System;

namespace DigitalWallet.BackEnd
{
    public class FraudResult
    {
        public bool IsFlagged { get; set; } = false;
        public string Reason { get; set; } = "";
        public string Severity { get; set; } = "low";
    }

    public class FraudDetector
    {
        private const decimal LARGE_AMOUNT = 10_000_000;
        private const int MAX_TX_PER_MINUTE = 5;
        private const int MAX_FAILED_LOGIN = 3;
        private const int MAX_PIN_ATTEMPTS = 5;

        public static FraudResult AnalyzeTransaction(int userId, decimal amount, int txCountLastMinute)
        {
            var result = new FraudResult();

            if (amount > LARGE_AMOUNT)
            {
                result.IsFlagged = true;
                result.Reason = "Transaksi melebihi batas maksimum (Rp 10jt)";
                result.Severity = "high";
            }
            else if (txCountLastMinute >= MAX_TX_PER_MINUTE)
            {
                result.IsFlagged = true;
                result.Reason = "Terlalu banyak transaksi dalam 1 menit";
                result.Severity = "medium";
            }
            else if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 4)
            {
                result.IsFlagged = true;
                result.Reason = "Transaksi di jam tidak wajar (Dini hari)";
                result.Severity = "low";
            }
            

                return result;
        }

        public static void SaveFraudLog(int userId, int transactionId, string reason, string severity, decimal amount, string description)
        {
            // Pastikan deskripsi tetap aman (terenkripsi) atau di-escape jika plain text
            string safeReason = reason?.Replace("'", "''") ?? "";
            string safeDescription = description?.Replace("'", "''") ?? "";

            // Tambahkan kolom amount dan description ke dalam query INSERT
            string query = $@"INSERT INTO fraud_logs (user_id, transaction_id, reason, severity, amount, description, created_at) 
                     VALUES ({userId}, {transactionId}, '{safeReason}', '{severity}', {amount}, '{safeDescription}', NOW())";

            Koneksi.JalankanQuery(query);
        }

        public static bool CheckAndLockAccount(int userId, int failedAttempts)
        {
            if (failedAttempts >= MAX_FAILED_LOGIN)
            {
                Koneksi.JalankanQuery($"UPDATE users SET is_locked=1 WHERE id={userId}");
                return true;
            }
            return false;
        }
    }
}