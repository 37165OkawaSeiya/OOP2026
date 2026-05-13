using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise02 {
    internal class InchConverter {
        private static readonly double ratio = 0.0254;//定数
        //インチからメートルを求める
        public static double ToMeter(int inch) {
            return inch * ratio;
        }
        //メートルからインチを求める
        public static double FromMeter(double meter) {
            return meter / ratio;
        }
    }
}
