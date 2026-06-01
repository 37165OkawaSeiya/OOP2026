using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise01 {
    //5.1.1
    public class YearMonth {

        public int Year { get; init; }

        public int Month { get; init; }

        public YearMonth(int year, int month) {
            Year = year;
            Month = month;
        }
    }
}
