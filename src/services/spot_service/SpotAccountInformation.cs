using System.Text;
namespace BinanceApp.SpotService
{
    [Serializable]
    public struct SpotAccountInformation
    {
        #region Static stuff.
        private static StringBuilder toStringBuilder = new StringBuilder();
        #endregion

        public long updateTime;
        public string accountType;
        public SpotSymbol[] balances;
        public string[] permissions;

        public override string ToString()
        {
            toStringBuilder.Clear();

            toStringBuilder.AppendLine($"updateTime: {this.updateTime}, ");
            toStringBuilder.AppendLine($"accountType: {this.accountType}, balances: [");
            if (this.balances != null)
            {
                foreach (SpotSymbol symbol in this.balances)
                {
                    toStringBuilder.AppendLine($"{symbol.ToString()}, ");
                }
            }
            toStringBuilder.AppendLine("], permissions: [");
            if (this.permissions != null)
            {
                foreach (string permission in this.permissions)
                {
                    toStringBuilder.AppendLine($"{permission}, ");
                }
            }
            toStringBuilder.AppendLine("]");

            return toStringBuilder.ToString();
        }
    }
}