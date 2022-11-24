using System.Text;

namespace BinanceWrapper.SpotService
{
    public struct BinanceSpotSymbolTrade
    {
        private static StringBuilder toStringBuilder = new StringBuilder();

        public string symbol { get; set; }
        public long id { get; set; }
        public int orderListId { get; set; }
        public decimal price { get; set; }
        public decimal qty { get; set; }
        public decimal quoteQty { get; set; }
        public decimal commission { get; set; }
        public string commissionAsset { get; set; }
        public long time { get; set; }
        public bool isBuyer { get; set; }
        public bool isMaker { get; set; }
        public bool isBestMatch { get; set; }

        public override string ToString()
        {
            toStringBuilder.Clear();

            toStringBuilder.AppendLine($"symbol: {this.symbol}");
            toStringBuilder.AppendLine($"id: {this.id}");
            // toStringBuilder.AppendLine($"orderListId: {this.orderListId}");
            toStringBuilder.AppendLine($"price: {this.price}");
            toStringBuilder.AppendLine($"qty: {this.qty}");
            toStringBuilder.AppendLine($"quoteQty: {this.quoteQty}");
            toStringBuilder.AppendLine($"commission: {this.commission}");
            toStringBuilder.AppendLine($"commissionAsset: {this.commissionAsset}");
            toStringBuilder.AppendLine($"time: {this.time}");
            toStringBuilder.AppendLine($"isBuyer: {this.isBuyer}");
            toStringBuilder.AppendLine($"isMaker: {this.isMaker}");
            toStringBuilder.AppendLine($"isBestMatch: {this.isBestMatch}");

            return toStringBuilder.ToString();
        }
    }
}