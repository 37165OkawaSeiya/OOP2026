using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistanceConverter
{
    public class FeetConverter
    {
        private static readonly double ratio = 0.3048; //定数
        //フィートからメートルを求める
        public static double ToMeter(double feet)
        {
            return feet * ratio;
        }
        //メートルからフィートを求める
        public static double FromMeter(double meter)
        {
            return meter / ratio;
        }
    }
}
