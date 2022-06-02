using System.Text.Json.Serialization;

namespace BinanceApp
{
    public struct BinanceExchangeSymbol
    {
        public readonly string symbol { get; }
        public readonly string baseAsset { get; }
        public readonly double quoteAsset { get; }

        public override string ToString()
        {
            return $"Symbol:{this.symbol} BaseAsset:{this.baseAsset} QuoteAsset:{this.quoteAsset}";
        }
    }
}