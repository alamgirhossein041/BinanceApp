using System.Text;
namespace BinanceApp
{
    public struct BinanceExchangeResult
    {
        #region Static stuff.
        private static StringBuilder toStringBuilder = new StringBuilder();

        public static BinanceExchangeResult InvalidResult = new BinanceExchangeResult()
        {
            timezone = "Invalid",
            serverTime = 0,
            symbols = new BinanceExchangeSymbol[] { }
        };
        #endregion

        public string timezone { get; set; }
        public long serverTime { get; set; }
        public BinanceExchangeSymbol[] symbols { get; set; }

        public override string ToString()
        {
            (toStringBuilder ??= new StringBuilder()).Clear();
            toStringBuilder.Append($"Timezone:{this.timezone}");
            toStringBuilder.AppendLine();
            toStringBuilder.Append($"ServerTime:{this.serverTime}");
            for (int i = 0; i < this.symbols.Length; i++)
            {
                toStringBuilder.AppendLine();
                toStringBuilder.Append(this.symbols[i].ToString());
            }
            return toStringBuilder.ToString();
        }
    }
}