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

                //③県庁所在地登録処理
                if (prefOfficeDict.ContainsKey(pref)) {
                    Console.WriteLine("上書きしますか?(Y/N)");
                    if (Console.Read() == 'Y') {
                        prefOfficeDict[pref] = prefCaptaLocation;
                    }
                } else {
                    prefOfficeDict[pref] = prefCaptaLocation;
                }
            }

            while (true) {
                switch (menuDisp()) {
                    case 1://一覧表示
                        allDisp();
                        break;
                    case 2://検索
                        searchPrefCaptalLocation();
                        break;
                    case 9://終了
                        return;
                }
            }
        }
        //メニュー表示
        private static int menuDisp() {
            Console.WriteLine("\n**** メニュー ****");
            Console.WriteLine("1:一覧表示");
            Console.WriteLine("2:検索");
            Console.WriteLine("9:終了");
            Console.Write(">");
            int num = int.Parse(Console.ReadLine());
            //メニュー番号を入力させて呼び出し元へ返却

            return num;
        }

        //一覧表示処理
        private static void allDisp() {
            foreach (var prefs in prefOfficeDict) {
                Console.WriteLine($"{prefs.Key}の県庁所在地は{prefs.Value}です。");
            }
        }

        //検索処理
        private static void searchPrefCaptalLocation() {
            Console.Write("都道府県：");
            var RP = Console.ReadLine();
            Console.WriteLine($"{RP}の県庁所在地は{prefOfficeDict[RP]}です。");
        }
    }
}
