using System.Net;
using System.Net.NetworkInformation;

namespace BinanceApp.PingService
{
    public class BinancePingService : BinanceBaseService
    {
        private readonly string apiPingPath = string.Empty;

        public BinancePingService(IApp app, string apiPingPath) : base(app)
        {
            this.apiPingPath = apiPingPath;
        }

        override public void StartService()
        {

        }

        public async Task<BinancePing?> ApiPingRequest()
        {
            string endPoint = $"{App.BaseUrl}{this.apiPingPath}";

            try
            {
                return await this.SendRequest<BinancePing?>(HttpMethod.Get, this.apiPingPath, null);
            }
            catch (Exception e)
            {
                ExceptionHandler.LogException(e);
            }

            return BinancePing.InvalidPing;
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