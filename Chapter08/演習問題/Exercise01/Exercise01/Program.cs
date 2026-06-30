
using static System.Net.Mime.MediaTypeNames;

namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            var text = "Cozy lummox gives smart squid who asks for job pen";

            Exercise01(text);
            Console.WriteLine();
            Exercise02(text);

        }
        private static void Exercise01(string text) {
            var dict = new Dictionary<char, int>();
            foreach (var ch in text.ToUpper()) {
                if ('A' <= ch && ch <= 'Z') {
                    if (dict.ContainsKey(ch)) {
                        dict[ch]++;
                    } else {
                        dict[ch] = 1;
                    }
                }
            }
            foreach (var alphabet in dict.OrderBy(c => c.Key)) {
                Console.WriteLine($"{alphabet.Key}:{alphabet.Value}");
            }
        }

        private static void Exercise02(string text) {
            var dict = new SortedDictionary<char, int>();
            foreach (var ch in text.ToUpper()) {
                if ('A' <= ch && ch <= 'Z') {
                    if (dict.ContainsKey(ch)) {
                        dict[ch]++;
                    } else {
                        dict[ch] = 1;
                    }
                }
            }
            foreach (var alphabet in dict) {
                Console.WriteLine($"{alphabet.Key}:{alphabet.Value}");
            }
        }

        
    }
}
