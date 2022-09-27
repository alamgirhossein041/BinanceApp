using System.Text;

namespace BinanceApp.SpotService
{
    [Serializable]
    public struct SpotSymbolOrder
    {
        private static StringBuilder toStringBuilder = new StringBuilder();

        public string symbol { get; set; }
        public long orderId { get; set; }
        public int orderListId { get; set; }
        public string clientOrderId { get; set; }
        public decimal price { get; set; }
        public decimal origQty { get; set; }
        public decimal executedQty { get; set; }
        public decimal cummulativeQuoteQty { get; set; }
        public string status { get; set; }
        public string timeInForce { get; set; }
        public string type { get; set; }
        public string side { get; set; }
        public decimal stopPrice { get; set; }
        public decimal icebergQty { get; set; }
        public long time { get; set; }
        public long updateTime { get; set; }
        public bool isWorking { get; set; }
        public decimal origQuoteOrderQty { get; set; }

        public override string ToString()
        {
            toStringBuilder.Clear();

            toStringBuilder.AppendLine($"symbol: {this.symbol}");
            toStringBuilder.AppendLine($"orderId: {this.orderId}");
            // toStringBuilder.AppendLine($"orderListId: {this.orderListId}");
            // toStringBuilder.AppendLine($"clientOrderId: {this.clientOrderId}");
            toStringBuilder.AppendLine($"price: {this.price}");
            toStringBuilder.AppendLine($"origQty: {this.origQty}");
            toStringBuilder.AppendLine($"executedQty: {this.executedQty}");
            toStringBuilder.AppendLine($"cummulativeQuoteQty: {this.cummulativeQuoteQty}");
            toStringBuilder.AppendLine($"status: {this.status}");
            // toStringBuilder.AppendLine($"timeInForce: {this.timeInForce}");
            toStringBuilder.AppendLine($"type: {this.type}");
            toStringBuilder.AppendLine($"side: {this.side}");
            toStringBuilder.AppendLine($"stopPrice: {this.stopPrice}");
            toStringBuilder.AppendLine($"icebergQty: {this.icebergQty}");
            toStringBuilder.AppendLine($"time: {this.time}");
            toStringBuilder.AppendLine($"updateTime: {this.updateTime}");
            // toStringBuilder.AppendLine($"isWorking: {this.isWorking}");
            toStringBuilder.AppendLine($"origQuoteOrderQty: {this.origQuoteOrderQty}");

            return toStringBuilder.ToString();
        }
    }
}