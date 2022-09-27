namespace BinanceApp.SpotService
{
    public class BinanceSpotService : BinanceBaseService
    {
        private readonly string apiSpotAccountInfoPath = "/api/v3/account";
        private readonly string apiSpotAllOrdersPath = "/api/v3/allOrders";
        private readonly string apiSpotAllTradesPath = "/api/v3/myTrades";

        public BinanceSpotService(IApp app, string apiSpotPath) : base(app)
        {
            this.apiSpotAccountInfoPath = apiSpotPath;
        }

        override public void StartService()
        {

        }

        public async Task<SpotAccountInformation?> GetSpotAccountInformation()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("recvWindow", 5000);

            return await this.SendSignedRequest<SpotAccountInformation?>(HttpMethod.Get, this.apiSpotAccountInfoPath, parameters);
        }

        public async Task<IEnumerable<SpotSymbolOrder>?> GetAllOrders(string symbol)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("symbol", symbol.ToUpper());
            parameters.Add("recvWindow", 5000);

            return await this.SendSignedRequest<IEnumerable<SpotSymbolOrder>?>(HttpMethod.Get, this.apiSpotAllOrdersPath, parameters);
        }

        public async Task<IEnumerable<SpotSymbolTrade>?> GetAllTrades(string symbol)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("symbol", symbol.ToUpper());
            parameters.Add("recvWindow", 5000);

            return await this.SendSignedRequest<IEnumerable<SpotSymbolTrade>?>(HttpMethod.Get, this.apiSpotAllTradesPath, parameters);
        }
    }
}