namespace Exercise02 {
    internal class Program {
        static void Main(string[] args) {
            var str = Console.ReadLine();
            if (int.TryParse(str, out var num)) {
                Console.WriteLine($"{num:#,0}");
            } else {
                Console.WriteLine("int型ではありません");
            }
        }
    }
}
