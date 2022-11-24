using System.Text;

namespace BinanceWrapper.SpotService
{
    [Serializable]
    public struct BinanceSpotAccountInformation
    {
        #region Static stuff.
        private static StringBuilder toStringBuilder = new StringBuilder();

        public readonly static BinanceSpotAccountInformation InvalidSpotAccount = new BinanceSpotAccountInformation()
        {
            updateTime = 0,
            accountType = "Invalid",
            balances = new BinanceSpotSymbol[] { },
            permissions = new string[] { }
        };
        #endregion

        public long updateTime { get; set; }
        public string accountType { get; set; }
        public BinanceSpotSymbol[] balances { get; set; }
        public string[] permissions { get; set; }

        public override string ToString()
        {
            toStringBuilder.Clear();

            toStringBuilder.AppendLine($"updateTime: {this.updateTime}");
            toStringBuilder.AppendLine($"accountType: {this.accountType}");
            toStringBuilder.AppendLine($"balances: [");
            if (this.balances != null)
            {
                foreach (BinanceSpotSymbol symbol in this.balances)
                {
                    toStringBuilder.AppendLine($"{symbol.ToString()}, ");
                }
            }
            else
            {
                toStringBuilder.AppendLine("null");
            }

            toStringBuilder.AppendLine("]");
            toStringBuilder.AppendLine($"permissions: [");
            if (this.permissions != null)
            {
                foreach (string permission in this.permissions)
                {
                    toStringBuilder.AppendLine($"{permission}, ");
                }
            }
            else
            {
                toStringBuilder.AppendLine("null");
            }
            toStringBuilder.AppendLine("]");

            return toStringBuilder.ToString();
        }
    }
}