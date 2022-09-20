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
        private readonly string apiSpotPath = "/api/v3/account";

        public BinanceSpotService(IApp app, string apiSpotPath) : base(app)
        {
            this.apiSpotPath = apiSpotPath;
        }

        public async Task<SpotAccountInformation?> GetSpotAccountInformation()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("recvWindow", 5000);

            try
            {
                return await this.SendSignedRequest<SpotAccountInformation?>(HttpMethod.Get, this.apiSpotPath, parameters);
            }
            catch (Exception e)
            {
                ExceptionHandler.LogException(e);
            }

            return SpotAccountInformation.InvalidSpotAccount;
        }

        public async Task<Dictionary<string, string>?> GetAllOrders(string symbol)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("symbol", symbol);
            parameters.Add("recvWindow", 5000);
            parameters.Add("timestamp", new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds().ToString());

            try
            {
                return await this.SendSignedRequest<Dictionary<string, string>?>(HttpMethod.Get, this.apiSpotPath, parameters);
            }
            catch (Exception e)
            {
                ExceptionHandler.LogException(e);
            }

            return new Dictionary<string, string>();
        }

        override public void StartService()
        {

        }
    }
}