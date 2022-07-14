namespace BinanceApp.ExchangeService
{
    public struct ExchangeSymbol
    {
        public string symbol { get; set; }
        public string baseAsset { get; set; }
        public string quoteAsset { get; set; }

        public override string ToString()
        {
            return $"Symbol:{this.symbol} BaseAsset:{this.baseAsset} QuoteAsset:{this.quoteAsset}";
        }
    }
}