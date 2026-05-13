
namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {

            var songs = new List<Song>();
            while(true) {
                
                Console.Write("曲名：");
                string? title = Console.ReadLine();
                if(title.Equals("end",StringComparison.OrdinalIgnoreCase)) {//P131
                    break;
                }
                Console.Write("アーティスト名：");
                string? artistname = Console.ReadLine();

                Console.Write("演奏時間(秒)：");
                int length = int.Parse(Console.ReadLine());

                Song song = new Song(title, artistname, length);

                songs.Add(song);
                PrintSongs(songs);

            }
        }
        //2.1.4
        private static void PrintSongs(IEnumerable<Song> songs) {
            foreach (var song in songs) {
                Console.WriteLine($"{song.Title},{song.ArtistName},{song.Length / 60}:{(song.Length % 60):00}");
            }
        }

        // 2.1.3
        //var songs = new Song[] {
        //    new Song(Title,ArtistName,count)
        //new Song("Let it be", "The Beatles", 243),
        //new Song("Bridge Over Troubled Water", "Simon & Garfunkel", 293),
        //new Song("Close To You", "Carpenters", 276),
        //new Song("Honesty", "Billy Joel", 231),
        //new Song("I Will Always Love You", "Whitney Houston", 273),
        //};
        //PrintSongs(songs);

    }
}
