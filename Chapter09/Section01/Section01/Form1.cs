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
            DateTime birth = dtpBirth.Value;//ê∂Ç‹ÇÍÇΩì˙ït
            DateTime today = DateTime.Today;//ç°ì˙ÇÃì˙ït
            int age = today.Year - birth.Year;
            if (today < birth.AddYears(age)) {
                age--;
            }
            TimeSpan days = today.Date - birth.Date;
            tbOut.Text = $"Ç†Ç»ÇΩÇÕ{age}çŒÇ≈Ç∑";
            tbOut2.Text = days.TotalDays + "ì˙";
        }
    }
}
