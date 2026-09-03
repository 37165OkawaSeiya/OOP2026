using Microsoft.Data.Sqlite;
using System.Diagnostics;
using System.Xml.Linq;

namespace SQLiteProductSample;

//Productsテーブルに対するDB操作をまとめたクラス
//CRUD (Create / Read / Update / Delete) を担当する

public class ProductRepository {
    public List<Product> GetAll() {

        var products = new List<Product>();

        using var connection = Database.GetConnection();
        connection.Open();

        //SQLを実行するためのコマンドオブジェクトを作る
        using var command = connection.CreateCommand();

        //IF NOT EXISTS により、既にテーブルがあってもエラーにならない
        command.CommandText =
            """
            SELECT Id, Name, Price
            FROM Products
            ORDER BY Id
            """;

        //SELECTを実行し、複数行の検索結果を読み取る
        using var reader = command.ExecuteReader();

        while (reader.Read()) {
            products.Add(new Product {
                Id = reader.GetInt32(0),    //0列目:Id
                Name = reader.GetString(1), //1列目:Name
                Price = reader.GetInt32(2)  //2列目:Price
            });
        }
        return products;
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

    public void Update(Product product) {
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
}

