using CarReportSystem;
using Microsoft.Data.Sqlite;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Globalization;
using System.Xml.Linq;

namespace CarReportSystem;

//Productsテーブルに対するDB操作をまとめたクラス
//CRUD (Create / Read / Update / Delete) を担当する

public class ProductRepository {
    public List<CarReport> GetAll() {

        var reports = new List<CarReport>();

        using var connection = Database.GetConnection();
        connection.Open();

        //SQLを実行するためのコマンドオブジェクトを作る
        using var command = connection.CreateCommand();

        //IF NOT EXISTS により、既にテーブルがあってもエラーにならない
        command.CommandText =
            """
            SELECT Id, Date, Author, Maker, CarName, Report, Picture
            FROM CarReports
            ORDER BY Id
            """;

        //SELECTを実行し、複数行の検索結果を読み取る
        using var reader = command.ExecuteReader();

        while (reader.Read()) {
            reports.Add(new CarReport {
                Id = reader.GetInt32(0),
                Date = DateTime.ParseExact(
                    reader.GetString(1),
                    "yyyy-MM-dd",
                    CultureInfo.InvariantCulture),
                Author = reader.GetString(2),
                Maker = (CarReport.MakerGroup)reader.GetInt32(3),
                CarName = reader.GetString(4),
                Report = reader.GetString(5),
                Picture = reader.IsDBNull(6) ? null : BytesToImage(reader.GetFieldValue<byte[]>(6))
            });
        }
        return reports;
    }

    public void Add(CarReport carReport) {
        //接続オブジェクトを生成する
        using var connection = Database.GetConnection();

        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText =
            """
            INSERT INTO CarReports
            (Date, Author, Maker, CarName, Report, Picture)
            VALUES
            ($date, $author, $maker, $carName, $report, $picture);

            SELECT last_insert_rowid();
            """;
        command.Parameters.AddWithValue("$date", carReport.Date.ToString("yyyy-MM-dd"));
        command.Parameters.AddWithValue("$author", carReport.Author);
        command.Parameters.AddWithValue("$maker", (int)carReport.Maker);
        command.Parameters.AddWithValue("$carName", carReport.CarName);
        command.Parameters.AddWithValue("$report", carReport.Report ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("$picture", ImageToBytes(carReport.Picture));
    }

    public void Delete(int id) {
        using var connection = Database.GetConnection();
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText =
        """
        DELETE FROM Products
        WHERE Id = $id;
        """;

        command.Parameters.AddWithValue("$id", id);
        command.ExecuteNonQuery();
    }

    public void Update(CarReport carReport) {
        using var connection = Database.GetConnection();
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText =
            """
            UPDATE CarReports
            SET Date = $date, Author = $author, Maker = $maker,
                CarName = $carName, Report = $report, Picture = $picture
            WHERE Id = $id;
            """;

        command.Parameters.AddWithValue("$date", carReport.Date.ToString("yyyy-MM-dd"));
        command.Parameters.AddWithValue("$author", carReport.Author);
        command.Parameters.AddWithValue("$maker", (int)carReport.Maker);
        command.Parameters.AddWithValue("$carName", carReport.CarName);
        command.Parameters.AddWithValue("$report", carReport.Report ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("$picture", ImageToBytes(carReport.Picture));
        command.Parameters.AddWithValue("$id", carReport.Id);

        command.ExecuteNonQuery();

    }

    // ImageをSQLiteへ保存できるbyte[]へ変換する
    private static byte[]? ImageToBytes(Image? image) {
        if (image is null) return null;

        using var stream = new MemoryStream();
        // DBへはPNG形式で保存
        image.Save(stream, ImageFormat.Png);
        return stream.ToArray();
    }

    // SQLiteのBLOB（byte[]）をImageへ変換する
    private static Image BytesToImage(byte[] data) {
        using var stream = new MemoryStream(data);
        using var image = Image.FromStream(stream);
        // MemoryStream破棄後も利用できるようBitmapとしてコピーする。
        return new Bitmap(image);
    }
}

