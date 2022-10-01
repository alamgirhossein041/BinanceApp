using System.Net;
using System.Net.NetworkInformation;

namespace BinanceApp.PingService
{
    public class BinancePingService : BinanceBaseService
    {
        private readonly string apiPingPath = "/api/v3/ping";

        public BinancePingService(IApp app) : base(app)
        {

        }

        override public void StartService()
        {

        }

        public async Task<BinancePing?> ApiPingRequest()
        {
            return await this.SendRequest<BinancePing?>(HttpMethod.Get, this.apiPingPath, null);
        }

        public async Task<bool> PingBaseUrl()
        {
            try
            {
                Ping ping = new Ping();
                // Ping url must not contain {https://}.
                PingReply pingResult = await ping.SendPingAsync(App.BaseUrl.Replace("https://", ""));
                return pingResult.Status == IPStatus.Success;
            }
            catch (Exception e)
            {
                ExceptionHandler.LogException(e);
            }

            return false;
        }
    }
}