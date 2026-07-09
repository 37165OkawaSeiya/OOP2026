namespace Exercise01 {
    using System.Globalization;
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        DateTime today = DateTime.Now;

        private void btButton1_Click(object sender, EventArgs e) {
            tbOut1.Text = $"{today:yyyy/MM/dd/ HH:mm}";
        }

        private void btButton2_Click(object sender, EventArgs e) {
            tbOut2.Text = $"{today:yyyy”NMMŒŽdd“ú HHŽžmm•ªss•b}";
        }

        private void btButton3_Click(object sender, EventArgs e) {
            var culture = new CultureInfo("ja-JP");
            culture.DateTimeFormat.Calendar = new JapaneseCalendar();
            tbOut3.Text = today.ToString("gg y”N MŒŽ d“ú(dddd)",culture);
        }
    }
}
