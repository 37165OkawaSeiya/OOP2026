using System.Security.Cryptography.X509Certificates;

namespace Section01 {
    internal class Program {
        static private Dictionary<string, string> prefOfficeDict = new Dictionary<string, string>();


        static void Main(string[] args) {
            string? pref, prefCaptaLocation;

            Console.WriteLine("県庁所在地の登録【入力終了：Ctrl + 'Z'】");

            while (true) {
                //①都道府県の入力
                Console.Write("都道府県：");
                pref = Console.ReadLine();

                if (pref is null) break;  //無限ループを抜ける(Ctrl + 'Z')

                //②県庁所在地の入力
                Console.Write("県庁所在地:");
                prefCaptaLocation = Console.ReadLine();
                Console.WriteLine();

                //③県庁所在地登録処理
                if (prefOfficeDict.ContainsKey(pref)) {
                    //上書きしますか
                } else {

                }
                prefOfficeDict[pref] = prefCaptaLocation;

            }

            while (true) {
                Console.WriteLine("メニュー");
                Console.WriteLine("1:一覧表示");
                Console.WriteLine("2:検索");
                Console.WriteLine("9:終了");
                var num = Console.ReadLine();
                switch (num) {
                    case "1"://一覧表示
                        foreach (var prefs in prefOfficeDict) {
                            Console.WriteLine($"{prefs.Key}の県庁所在地は{prefs.Value}です");
                        }
                        break;
                    case "2"://検索
                        Console.Write("都道府県：");
                        var RP = Console.ReadLine();
                        Console.WriteLine($"県庁所在地：{prefOfficeDict[RP]}");
                        break;
                    case "9"://終了
                        return;
                }
            }
        }

    }
}
