namespace BinanceApp
{
    public struct BinanceExchangeResult
    {
        public string timezone { get; }
        public long serverTime { get; }
        public BinanceExchangeSymbol[] symbols { get; }

        public override string ToString()
        {
            return $"Timezone:{this.timezone} ServerTime:{this.serverTime} Symbols:{this.symbols}";
        }
    }
}