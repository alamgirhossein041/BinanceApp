public static class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World!");

        BinanceApp.App binanceApp = new BinanceApp.App();
        binanceApp.StartApp();
        binanceApp.UpdateApp();
    }
}