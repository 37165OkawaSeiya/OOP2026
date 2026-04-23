using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistanceConverter
{
    public class FeetConverter
    {
        //フィートからメートルを求める
        public static double ToMeter(double feet)
        {
            return feet * 0.3048;
        }
        //メートルからフィートを求める
        public static double FromMeter(double meter)
        {
            return meter / 0.3048;
        }
    }
}
