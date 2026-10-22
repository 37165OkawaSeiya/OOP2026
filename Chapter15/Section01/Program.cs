List<IGreetingBase> list = [
    new GreetingMorning(),
    new GreetingAfternoon(),
    new GreetingEvening(),
    ];

foreach (var obj in list) {
    string msg = obj.GetMessage();
    Console.WriteLine(msg);
}

//インターフェース
interface IGreetingBase {
    public virtual string GetMessage() => "";
}

//派生クラス
class GreetingMorning : IGreetingBase {
    public string GetMessage() => "おはよう";
}

class GreetingAfternoon : IGreetingBase {
    public string GetMessage() => "こんにちは";
}

class GreetingEvening : IGreetingBase {
    public string GetMessage() => "こんばんは";
}
