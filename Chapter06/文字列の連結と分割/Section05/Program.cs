using System.Text;

namespace Section05 {
    internal class Program {
        static void Main(string[] args) {
            var words = String.Join(",", GetWords());
            Console.WriteLine(words);

            //var sb = new StringBuilder();
            //foreach (var word in GetWords()) {
            //    sb.Append(word);
            //}
            //Console.WriteLine(sb);
        }

        private static IEnumerable<string> GetWords() {
            return ["Orange", "Lemon", "Strawberry"];
        }
    }
}
