using System.Threading.Channels;
using TextFileProcessor;

string path = @"C:\Users\infosys\source\repos\OOP2026\Chapter14\Section04\Section4\Program.cs";
TextProcessor.Run<LineCounterProcessor>(path);

public class LineCounterProcessor : TextProcessor {

    private int _count = 0;
    private string _searchWord = "";

    protected override void Initialize(string line) {
        _count = 0;
        Console.Write("カウントしたい単語：");
        _searchWord = Console.ReadLine();
    }

    protected override void Execute(string line) =>
        _count += line.Split(_searchWord).Length - 1;

    protected override void Terminate() =>
        Console.WriteLine($"「{_searchWord}」は{_count}つ出現します");
}