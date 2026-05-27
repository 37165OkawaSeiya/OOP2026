
namespace Exercise02 {
    internal class Program {
        static void Main(string[] args) {
            Console.WriteLine(" --- 4.2.1 ---");
            Exercise1();
            Console.WriteLine("\n --- 4.2.2 ---");
            Exercise2();
            Console.WriteLine("\n --- 4.2.3 ---");
            Exercise3();
        }

        //P89～P90
        private static void Exercise1() {
            //if-else文を使用
            string input = Console.ReadLine();
            if (int.TryParse(input,out int result)) {
                int count = int.Parse(input);
                if (count < 0) {
                    Console.WriteLine(count);
                } else if (count < 100) {
                    Console.WriteLine(count * 2);
                } else if (count < 500) {
                    Console.WriteLine(count * 3);
                } else {
                    Console.WriteLine(count);
                }
            } else {
                Console.WriteLine("入力に誤りがあります");
            }
            
        }

        private static void Exercise2() {
            //switch文を使用

        }

        private static void Exercise3() {
            //switch式を使用


        }
    }
}
