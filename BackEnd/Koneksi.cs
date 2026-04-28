using MySql.Data.MySqlClient;
using System;
using System.Configuration;

namespace DigitalWallet.BackEnd
{
    public class Koneksi
    {
        // =============================================
        //  PROPERTI — bisa diakses dari luar
        // =============================================
        public MySqlConnection KoneksiDB { get; private set; }

        // =============================================
        //  KONSTRUKTOR: pakai Settings.settings
        // =============================================
        public Koneksi()
        {
            try
            {
                Configuration myConf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                ConfigurationSectionGroup userSettings = myConf.SectionGroups["userSettings"];

                // Pastikan nama di bawah ini sama dengan yang ada di App.config
                var settingsSection = userSettings?.Sections["Digital_Wallet_ISA.db"] as ClientSettingsSection;

                if (settingsSection != null)
                {
                    // Ambil data dengan nama Key yang sesuai di Application Settings Anda
                    string server = settingsSection.Settings.Get("DbServer").Value.ValueXml.InnerText;
                    string database = settingsSection.Settings.Get("DbName").Value.ValueXml.InnerText;
                    string uid = settingsSection.Settings.Get("DbUsername").Value.ValueXml.InnerText;
                    string password = settingsSection.Settings.Get("DbPassword").Value.ValueXml.InnerText;

                    string conString = $"Server={server};Database={database};Uid={uid};Pwd={password};";
                    KoneksiDB = new MySqlConnection(conString);
                }
                else
                {
                    // Fallback: Jika config tidak terbaca, gunakan manual agar tidak error Null
                    KoneksiDB = new MySqlConnection("Server=localhost;Database=digitalwallet;Uid=root;Pwd=;");
                }
            }
            catch
            {
                // Fallback terakhir jika terjadi error sistem
                KoneksiDB = new MySqlConnection("Server=localhost;Database=digitalwallet;Uid=root;Pwd=;");
            }
        }

        //  KONSTRUKTOR: manual (untuk testing)

        public Koneksi(string server, string database, string uid, string password)
        {
            string conString = $"Server={server};Database={database};Uid={uid};Pwd={password};";
            KoneksiDB = new MySqlConnection(conString);
        }

    
        // =============================================
        public void Open()
        {
            if (KoneksiDB.State != System.Data.ConnectionState.Open)
                KoneksiDB.Open();
        }

        public void Close()
        {
            if (KoneksiDB.State == System.Data.ConnectionState.Open)
                KoneksiDB.Close();
        }

        // =============================================
        //  STATIC: untuk query INSERT / UPDATE / DELETE
        // =============================================
        public static void JalankanQuery(string query)
        {
            Koneksi k = new Koneksi();
            try
            {
                k.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, k.KoneksiDB))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                k.Close();
            }
        }

        public static System.Data.DataTable JalankanSelect(string query)
        {
            Koneksi k = new Koneksi();
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                k.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, k.KoneksiDB))
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);   // koneksi otomatis ditutup setelah Fill
                }
            }
            finally
            {
                k.Close();
            }
            return dt;
        }

 
        public static object JalankanSelectSatu(string query)
        {
            Koneksi k = new Koneksi();
            object hasil = null;
            try
            {
                k.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, k.KoneksiDB))
                {
                    hasil = cmd.ExecuteScalar();
                }
            }
            finally
            {
                k.Close();
            }
            return hasil;
        }

        // Koneksi.cs
        public class Connection
        {
            private static string connectionString =
                "Server=localhost;Database=digitalwallet;Uid=root;Pwd=;";

            public static MySqlConnection GetConnection()
            {
                return new MySqlConnection(connectionString);
            }
        }
    }
}