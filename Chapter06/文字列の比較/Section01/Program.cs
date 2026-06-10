using System.Globalization;

namespace Section01 {
    internal class Program {
        static void Main(string[] args) {
            var str1 = "こまんど";
            var str2 = "コマンド";

            var cultureInfo = new CultureInfo("ja-JP");

            if (String.Compare(str1, str2,cultureInfo,
                CompareOptions.IgnoreWidth | CompareOptions.IgnoreKanaType) == 0) {
                Console.WriteLine("等しい");
            } else {
                Console.WriteLine("等しくない");
            }
        }
    }
}
