
using System.Runtime.Intrinsics.X86;

namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            List<string> langs = [
                "C#", "Java", "Ruby", "PHP", "Python", "TypeScript",
                "JavaScript", "Swift", "Go",
            ];

            Exercise1(langs);
            Console.WriteLine("---");
            Exercise2(langs);
            Console.WriteLine("---");
            Exercise3(langs);

        }

        private static void Exercise1(List<string> langs) {
            Console.WriteLine("---4.1.1---");

            //foreach文
            Console.WriteLine("foreach文で出力");
            foreach(var s in langs) {
                if (s.Contains('S')) {
                    Console.WriteLine(s);
                }
            }
            //for文
            Console.WriteLine("\for文で出力");
            for (int i = 0;i < langs.Count;i++) {
                if (langs[i].Contains('S')) {
                    Console.WriteLine(langs[i]);
                }
            }

            //while文
            Console.WriteLine("\nwhile文で出力");
            int cnt = 0;
            while (true) {
                if (langs[cnt].Contains('S')) {
                    Console.WriteLine(langs[cnt]);
                }
                cnt++;
                if (cnt == langs.Count) {
                    break;
                }
            }
        }

        private static void Exercise2(List<string> langs) {
            Console.WriteLine("---4.1.2---");
            var query = langs.Where(s => s.Contains('S'));
            foreach (var s in query) {
                Console.WriteLine(s);
            }
        }

        private static void Exercise3(List<string> langs) {
         
        }
    }
}
