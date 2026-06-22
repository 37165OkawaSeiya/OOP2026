namespace Test01_01 {
    public class ScoreCounter {
        private IEnumerable<Student> _score;

        // コンストラクタ
        public ScoreCounter(string filePath) {
            _score = ReadScore(filePath);
        }

        //メソッドの概要：
        private static IEnumerable<Student> ReadScore(string filePath) {
            var students = new List<Student>();
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines) {
                var items = line.Split(',');//カンマ区切りで分割
                var sale = new Student {
                    Name = items[0],
                    Subject = items[1],
                    Score = int.Parse(items[2])
                };
                students.Add(sale);
            }
            return students;
        }

        //メソッドの概要：
        public IDictionary<string, int> GetPerStudentScore() {
            var dict = new Dictionary<string, int>();
            foreach (var student in _score) {
                //すでに生徒名が辞書のキーに登録されているか
                if (dict.ContainsKey(student.Name))
                    //登録されている場合
                    dict[student.Name] += student.Score;//点数足し込み
                else
                    //未登録の場合
                    dict[student.Name] = student.Score;//新規に点数を登録
            }
            return dict;
        }
    }
}
