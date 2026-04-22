

namespace DistanceConverter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 3)
            {
                if (int.TryParse(args[1], out int start) && int.TryParse(args[2], out int stop))
                {
                    if (args[0] == "-tom")
                    {
                        PrintFeetToMeterList(start, int.Parse(args[2])); //メートルへの変換
                    }
                    else if (args[0] == "-tof")
                    {
                        PrintMeterToFeetList(int.Parse(args[1]), int.Parse(args[2])); //フィートへの変換
                    }
                    else
                    {
                        Console.WriteLine("引数エラー");
                    }
                }
                else
                {
                    Console.WriteLine("引数エラー");
                }
            }
            
        }

        private static void PrintMeterToFeetList(int start, int stop)
        {
            //メートルからフィートへの対応表を出力
            for (int meter = start; meter <= stop; meter++)
            {
                double feet = FeetToMeter(meter);
                Console.WriteLine($"{meter}m = {feet:0.0000}ft");
            }
        }

        static void PrintFeetToMeterList(int start, int stop)
        {
            //フィートからメートルへの対応表を出力
            for (int feet = start; feet <= stop; feet++)
            {
                double meter = FeetToMeter(feet);
                Console.WriteLine($"{feet}ft = {meter:0.0000}m");
            }

        }

        //フィートからメートルを求める
        static double FeetToMeter(int feet)
        {
            return feet * 0.3048;
        }
        //メートルからフィートを求める
        static double FeetToFeet(int meter)
        {
            return meter / 0.3048;
        }
    }
}
