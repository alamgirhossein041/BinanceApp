namespace BinanceApp.SpotService
{
    public class BinanceSpotService : BinanceBaseService
    {
        private readonly string apiSpotAccountInfoPath = "/api/v3/account";
        private readonly string apiSpotAllOrdersPath = "/api/v3/allOrders";
        private readonly string apiSpotAllTradesPath = "/api/v3/myTrades";

        public BinanceSpotService(IApp app) : base(app)
        {

        }

        public override void StartService()
        {

        }

        public async Task<BinanceSpotAccountInformation?> GetSpotAccountInformation()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("recvWindow", 5000);

            return await this.SendSignedRequest<BinanceSpotAccountInformation?>(HttpMethod.Get, this.apiSpotAccountInfoPath, parameters);
        }

        public async Task<IEnumerable<BinanceSpotSymbolOrder>?> GetAllOrders(string symbol)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("symbol", symbol.ToUpper());
            parameters.Add("recvWindow", 5000);

            return await this.SendSignedRequest<IEnumerable<BinanceSpotSymbolOrder>?>(HttpMethod.Get, this.apiSpotAllOrdersPath, parameters);
        }

        public async Task<IEnumerable<BinanceSpotSymbolTrade>?> GetAllTrades(string symbol)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("symbol", symbol.ToUpper());
            parameters.Add("recvWindow", 5000);

            return await this.SendSignedRequest<IEnumerable<BinanceSpotSymbolTrade>?>(HttpMethod.Get, this.apiSpotAllTradesPath, parameters);
        }
    }
}