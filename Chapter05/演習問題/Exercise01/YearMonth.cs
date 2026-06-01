using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise01 {
    //5.1.1 (P114)
    public class YearMonth {

        public int Year { get; init; }

        public int Month { get; init; }

        //5.1.2 (P116)
        public bool Is21Century => 2001 <= Year && Year <= 2100;

        public YearMonth(int year, int month) {
            Year = year;
            Month = month;
        }

        public YearMonth AddOneMonth() {
            if (Month == 12) {
                var add = new YearMonth(Year + 1, 1);
                return add;
            } else {
                var add = new YearMonth(Year,Month + 1);
                return add;
            }
        }
        
    }
}
