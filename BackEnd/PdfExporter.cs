using DigitalWallet.BackEnd;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;
using System.Data;

public static class PdfExporter
{
    public static void ExportTransaksi(int userId, string filePath)
    {
        Document doc = new Document(PageSize.A4);
        PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
        doc.Open();

        // Header
        doc.Add(new Paragraph("DIGITAL WALLET - Laporan Transaksi",
            FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16)));
        doc.Add(new Paragraph($"Tanggal Cetak: {DateTime.Now:dd-MM-yyyy HH:mm}"));
        doc.Add(Chunk.NEWLINE);

        // Tabel
        PdfPTable table = new PdfPTable(5);
        table.WidthPercentage = 100;
        table.AddCell("Tanggal");
        table.AddCell("Tipe");
        table.AddCell("Deskripsi");
        table.AddCell("Tujuan/Asal");
        table.AddCell("Jumlah");

        string query = $@"SELECT t.* FROM transactions t 
                          LEFT JOIN wallets w1 ON t.from_wallet_id = w1.id
                          LEFT JOIN wallets w2 ON t.to_wallet_id = w2.id
                          WHERE w1.user_id = {userId} OR w2.user_id = {userId}
                          ORDER BY t.created_at DESC";

        DataTable dt = Koneksi.JalankanSelect(query);

        // FIX: Gunakan foreach agar tidak terjadi infinite loop
        foreach (DataRow dr in dt.Rows)
        {
            table.AddCell(Convert.ToDateTime(dr["created_at"]).ToString("dd-MM-yyyy HH:mm"));
            table.AddCell(dr["type"].ToString());
            table.AddCell(dr["description"].ToString());
            table.AddCell(dr["to_wallet_id"].ToString()); // Bisa disesuaikan untuk menampilkan username
            table.AddCell("Rp " + Convert.ToDecimal(dr["amount"]).ToString("N0"));
        }

        doc.Add(table);
        doc.Close();

        // Membuka file secara aman
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(filePath) { UseShellExecute = true });
    }
}