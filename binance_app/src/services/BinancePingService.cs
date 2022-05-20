using System.Net;
using System.Net.NetworkInformation;

namespace BinanceApp
{
    public class BinancePingService : ServiceBase
    {
        private readonly string apiPingPath = string.Empty;

        public BinancePingService(IApp app, string apiPingPath) : base(app)
        {
            this.apiPingPath = apiPingPath;
        }

        public async Task<bool> ApiPingRequest()
        {
            try
            {
                string endPoint = $"{App.BaseUrl}{this.apiPingPath}";
                HttpResponseMessage apiPingresult = await App.HttpClient.GetAsync(endPoint);

                // HttpResponseMessageHandler.LogResponse((apiPingresult));

                return apiPingresult.StatusCode == HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                ExceptionHandler.LogException(e);
            }

            return false;
        }

        public async Task<bool> PingBaseUrl()
        {
            try
            {
                Ping ping = new Ping();
                // Ping url must not contain {https://}.
                PingReply pingResult = await ping.SendPingAsync(App.BaseUrl.Replace("https://", ""));

                // PingReplyHandler.LogReply(pingResult);

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