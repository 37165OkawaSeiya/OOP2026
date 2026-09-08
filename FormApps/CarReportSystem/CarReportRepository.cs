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

    public void Add(string name, int price) {
        //接続オブジェクトを生成する
        using var connection = Database.GetConnection();

        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText =
            """
            INSERT INTO Products (Name,Price)
            VALUES ($name,$price);

            SELECT last_insert_rowid();
            """;
        command.Parameters.AddWithValue("$name", name);
        command.Parameters.AddWithValue("$price", price);
        command.ExecuteNonQuery();
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

    public void Update(CarReport product) {
        using var connection = Database.GetConnection();
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText =
            """
            UPDATE Products
            SET Name = $name,
                Price = $price
            WHERE Id = $id;
            """;

        command.Parameters.AddWithValue("$name", product.Name);
        command.Parameters.AddWithValue("$price", product.Price);
        command.Parameters.AddWithValue("$id", product.Id);

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

