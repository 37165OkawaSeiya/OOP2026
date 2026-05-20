namespace Section01 {
    internal class Program {

        static void Main(string[] args) {
            var cities = new List<string> {
                "Tokyo",
                "New Delhi",
                "Bangkok",
                "London",
                "Paris",
                "Berlin",
                "Canberra",
                "Hong Kong",
            };

            //以下の条件を満たすものが存在するか調べる
            //文字数が6文字以上
            //oを含む
            //最後がn
            var exists = cities.Exists(s => s.Length >= 6 && s.Contains('o') && s.EndsWith('n'));
            Console.WriteLine(exists);
        }
    }
}
