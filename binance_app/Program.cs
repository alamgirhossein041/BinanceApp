public static class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello Binance World!");

        BinanceApp.App binanceApp = new BinanceApp.App();
        binanceApp.StartApp();
        binanceApp.UpdateApp();
    }
}