using System.Text;
namespace BinanceApp.ExchangeService
{
    [Serializable]
    public struct BinanceExchangeInfo
    {
        #region Static stuff.
        private static StringBuilder toStringBuilder = new StringBuilder();

        public static BinanceExchangeInfo InvalidResult = new BinanceExchangeInfo()
        {
            timezone = "Invalid",
            serverTime = 0,
            symbols = new ExchangeSymbol[] { }
        };
        #endregion

        public string timezone { get; set; }
        public long serverTime { get; set; }
        public ExchangeSymbol[] symbols { get; set; }

        public override string ToString()
        {
            (toStringBuilder ??= new StringBuilder()).Clear();
            toStringBuilder.AppendLine($"Timezone:{this.timezone}");
            toStringBuilder.AppendLine($"ServerTime:{this.serverTime}");
            for (int i = 0; i < this.symbols.Length; i++)
            {
                toStringBuilder.AppendLine(this.symbols[i].ToString());
            }
            return toStringBuilder.ToString();
        }
    }
}