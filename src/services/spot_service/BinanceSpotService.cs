using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Web;

namespace BinanceApp.SpotService
{
    public class BinanceSpotService : BinanceBaseService
    {
        private readonly string apiSpotAccountInfoPath = "/api/v3/account";
        private readonly string apiSpotAllOrdersPath = "/api/v3/allOrders";

        public BinanceSpotService(IApp app, string apiSpotPath) : base(app)
        {
            this.apiSpotAccountInfoPath = apiSpotPath;
        }

        public async Task<SpotAccountInformation?> GetSpotAccountInformation()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("recvWindow", 5000);

            return await this.SendSignedRequest<SpotAccountInformation?>(HttpMethod.Get, this.apiSpotAccountInfoPath, parameters);
        }

        public async Task<SpotSymbolOrder[]?> GetAllOrders(string symbol)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("symbol", symbol.ToUpper());
            parameters.Add("recvWindow", 5000);

            return await this.SendSignedRequest<SpotSymbolOrder[]?>(HttpMethod.Get, this.apiSpotAllOrdersPath, parameters);
        }

        override public void StartService()
        {

        }
    }
}