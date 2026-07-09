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
            DateTime birth = dtpBirth.Value.Date;//¶‚Ü‚ê‚½“ú•t
            DateTime today = DateTime.Today;//¡“ú‚Ì“ú•t

            tbOut.Text = $"‚ ‚È‚½‚Í{GetAge(birth, today)}Î‚Å‚·";

            TimeSpan days = today.Date - birth.Date;
            tbOut2.Text = days.TotalDays + "“ú";

            var culture = new CultureInfo("ja-JP");
            culture.DateTimeFormat.Calendar = new JapaneseCalendar();
            var dayOfWeek = culture.DateTimeFormat.GetShortestDayName(birth.DayOfWeek);

            tbOut3.Text = $"¶‚Ü‚ê‚½{birth.Month}Œ{birth.Day}“ú‚Í‘æ{NthWeek(birth)}T‚Ì{dayOfWeek}—j“ú‚Å‚·";


            //¡”N‚Ì’a¶“ú‚ğì¬‚·‚é
            DateTime thisYearBirthday = new DateTime(today.Year, birth.Month, birth.Day);
            //‚·‚Å‚É’a¶“ú‚ª‰ß‚¬‚½‚©
            if (thisYearBirthday < today) {
                //—ˆ”N‚Ì’a¶“ú‚ğì¬‚·‚é
                thisYearBirthday = thisYearBirthday.AddYears(1);
            }

            var span = thisYearBirthday - today;

            if (span.Days == 0) {
                tbOut4.Text = "’a¶“ú‚Í¡“ú‚Å‚·";
            } else {
                tbOut4.Text = $"’a¶“ú‚Ü‚Å‚ ‚Æ{span.Days}“ú‚Å‚·";
            }

            //”N—î‚ğ‹‚ß‚éƒƒ\ƒbƒh
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
