using System.Globalization;

namespace Section01 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void btGet_Click(object sender, EventArgs e) {
            DateTime date = dtpDate.Value;
            tbOut.Text = date.AddDays((double)nudDay.Value).ToString();
        }

        private void btBirthCalc_Click(object sender, EventArgs e) {
            DateTime birth = dtpBirth.Value.Date;//生まれた日付
            DateTime today = DateTime.Today;//今日の日付

            tbOut.Text = $"あなたは{GetAge(birth, today)}歳です";

            TimeSpan days = today.Date - birth.Date;
            tbOut2.Text = days.TotalDays + "日";

            var culture = new CultureInfo("ja-JP");
            culture.DateTimeFormat.Calendar = new JapaneseCalendar();
            var dayOfWeek = culture.DateTimeFormat.GetShortestDayName(birth.DayOfWeek);
            tbOut3.Text = $"生まれた{birth.Month}月{birth.Day}日は第{NthWeek(birth)}週の{dayOfWeek}曜日です";

            var birthday = new DateTime(today.Year, birth.Month, birth.Day);
            var next_birthday = new DateTime(today.Year+1, birth.Month, birth.Day);

            if (birth.Month.Equals(today.Month) && birth.Day.Equals(today.Day)) {
                tbOut4.Text = "誕生日は今日です";
            } else if (today > birthday){
                tbOut4.Text = $"{(next_birthday.Date - today.Date).Days}";
            } else {
                tbOut4.Text = $"{(birthday.Date - today.Date).Days}";
            }
        }

        //年齢を求めるメソッド
        static int GetAge(DateTime birthday, DateTime targetDay) {
            var age = targetDay.Year - birthday.Year;
            if (targetDay < birthday.AddYears(age)) {
                age--;
            }
            return age;
        }

        static int NthWeek(DateTime date) {
            var firstDay = new DateTime(date.Year, date.Month, 1);
            var firstDayOfWeek = (int)(firstDay.DayOfWeek);
            return (date.Day + firstDayOfWeek - 1) / 7 + 1;
        }
    }
}
