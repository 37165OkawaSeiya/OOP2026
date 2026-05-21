
namespace Exercise02 {
    internal class Program {
        static void Main(string[] args) {
            var cities = new List<string> {
                "Tokyo", "New Delhi", "Bangkok", "London",
                "Paris", "Berlin", "Canberra", "Hong Kong",
            };

            Console.WriteLine("***** 3.2.1 *****");
            Exercise2_1(cities);
            Console.WriteLine();

            Console.WriteLine("***** 3.2.2 *****");
            Exercise2_2(cities);
            Console.WriteLine();

            Console.WriteLine("***** 3.2.3 *****");
            Exercise2_3(cities);
            Console.WriteLine();

            Console.WriteLine("***** 3.2.4 *****");
            Exercise2_4(cities);
            Console.WriteLine();
        }

        private static void Exercise2_1(List<string> cities) {
            //できたらGitのコメント「問題3.2.1完成」
            Console.Write("都市：");
            var name = Console.ReadLine();
            var index = cities.FindIndex(s => s.Equals(name));
            Console.WriteLine(index);
	        
        }

        private static void Exercise2_2(List<string> cities) {
            //できたらGitのコメント「問題3.2.2完成」
            var count = cities.Count(s => s.Contains('o'));
            Console.WriteLine(count);
        }

        private static void Exercise2_3(List<string> cities) {
            //できたらGitのコメント「問題3.2.3完成」
            var query = cities
                .Where(s => s.Contains('o'))
                .ToArray();
            foreach (var s in query) {
                Console.WriteLine(s);
            }
        }

        private static void Exercise2_4(List<string> cities) {
            //できたらGitのコメント「問題3.2.4完成」
            var query = cities
                .Where(s => s.Contains("B"))
                .Select(s => s.Length);
            foreach (var s in query) {
                Console.WriteLine();
            }
        }
    }
}
