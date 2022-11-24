public static class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello Binance World!");

        BinanceWrapper.BinanceApp binanceApp = new BinanceWrapper.BinanceApp();
        binanceApp.StartApp();
        binanceApp.UpdateApp();
    }
}