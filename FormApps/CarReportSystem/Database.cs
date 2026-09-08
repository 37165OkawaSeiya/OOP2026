using Microsoft.Data.Sqlite;

namespace CarReportSystem;

public static class Database {
    //DBファイルへの保存場所
    private static readonly string DatabasePath =
        Path.Combine(AppContext.BaseDirectory, "carreport.db");

    //SQLiteへ接続するための接続文字列
    private static readonly string ConnectionString =
        $"Data Source={DatabasePath}";

    // DBファイルの保存場所を外部から確認するための読み取り専用プロパティ
    public static string FilePath => DatabasePath;

    public static SqliteConnection GetConnection() 
        => new SqliteConnection(ConnectionString);
    
    public static void Initialize() {
        //接続オブジェクトを生成する。
        using var connection = GetConnection();

        //DBを開く
        connection.Open();

        //SQLを実行するためのコマンドオブジェクトを作る
        using var command = connection.CreateCommand();

        //IF NOT EXISTS により、既にテーブルがあってもエラーにならない
        command.CommandText =
            """
            CREATE TABLE IF NOT EXISTS CarReports (
                Id      INTEGER PRIMARY KEY AUTOINCREMENT,
                Date    TEXT    NOT NULL,
                Author  TEXT    NOT NULL,
                Maker   INTEGER NOT NULL,
                CarName TEXT    NOT NULL,
                Report  TEXT    NOT NULL,
                Picture BLOB
            );
            """;

        //結果行を返さないSQLを実行する
        command.ExecuteNonQuery();
    }
}