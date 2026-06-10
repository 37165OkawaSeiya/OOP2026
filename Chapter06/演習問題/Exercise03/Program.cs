
using System.Runtime.Intrinsics.X86;

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
            
        }

        private static void Exercise4(string text) {
            
        }

        private static void Exercise5(string text) {
            
        }

        private static void Exercise6(string text) {
            
        }
    }
}
