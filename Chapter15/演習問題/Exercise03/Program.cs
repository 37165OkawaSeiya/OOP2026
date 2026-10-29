using Exercise03;

var service = new Take20Service();
var processor = new TextFileProcessor(service);
Console.Write("パスの入力:");
processor.Run(Console.ReadLine());

