
using System.ComponentModel.DataAnnotations;
using System.Runtime.Intrinsics.X86;
using System.Text;

namespace Exercise03 {
    internal class Program {
        static void Main(string[] args) {
            var text = "Jackdaws love my big sphinx of quartz";
            #region
            Console.WriteLine("6.3.1");
            Exercise1(text);

            Console.WriteLine("6.3.2");
            Exercise2(text);

            Console.WriteLine("6.3.3");
            Exercise3(text);

            Console.WriteLine("6.3.4");
            Exercise4(text);

            Console.WriteLine("6.3.5");
            Exercise5(text);

            Console.WriteLine("6.3.99");
            Exercise6(text);
            #endregion
        }

        private static void Exercise1(string text) {
            var cnt = text.Count(c => c.Equals(' '));
            Console.WriteLine("空白数：" + cnt);
        }

        private static void Exercise2(string text) {
            var str = text.Replace("big", "small");
            Console.WriteLine(str);
        }

        private static void Exercise3(string text) {
            var words = text.Split(' ');
            var sb = new StringBuilder(words[0]);

            foreach(var word in words.Skip(1)) {
                sb.Append(' ');
                sb.Append(word);
            }

            //for (int i = 1;i < words.Length; i++) {
            //    sb.Append(' ');
            //    sb.Append(words[i]);
            //}

            //末尾はピリオドで終わる
            Console.WriteLine(sb + ".");
        }

        private static void Exercise4(string text) {
            var cnt = text.Split(' ').Length;
            Console.WriteLine($"単語数:{cnt}");
        }

        private static void Exercise5(string text) {
            var cnt = text.Split(' ').Where(s => s.Length <= 4);
            foreach (var s in cnt) {
                Console.WriteLine(s);
            }
        }

        //アルファベットの数をカウントして表示する
        private static void Exercise6(string text) {
            var str = text.ToLower().Replace(" ", "");
            //textの小文字かつ空白を削除したもの

            //辞書（ディクショナリを使った集計
            var alphDicCount = Enumerable.Range('a', 26)//aからzまで連続した値
                .ToDictionary(num => ((char)num), num => 0);

            //var dict = new SortedDictionary<char, int>();
            foreach (var c in str) {
                alphDicCount[c]++;
            }
            foreach (var word in alphDicCount) {
                Console.WriteLine(word.Key + ":" + word.Value);
            }

            Console.WriteLine();

            //配列を用いた集計
            var array = Enumerable.Repeat(0, 26).ToArray();
            foreach (var alph in str) {
                array[alph - 'a']++;
            }
            for (char ch = 'a'; ch <= 'z'; ch++) {
                Console.WriteLine($"{ch}:{array[ch - 'a']}");
            }

            Console.WriteLine();

            //'a'から順にカウントして集計
            for (char ch = 'a'; ch <= 'z'; ch++) {
                Console.WriteLine($"{ch}:{str.Count(s => s == ch)}");
            }
        }
    }
}
