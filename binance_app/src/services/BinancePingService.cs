using System.Net;
using System.Net.NetworkInformation;

namespace BinanceApp
{
    public class BinancePingService
    {
        private readonly string apiBaseUrl = string.Empty;
        private readonly string apiPingPath = string.Empty;

        private readonly HttpClient httpClient = null;

        public BinancePingService(HttpClient httpClient, string apiBaseUrl, string apiPingPath)
        {
            this.httpClient = httpClient;
            this.apiBaseUrl = apiBaseUrl;
            this.apiPingPath = apiPingPath;
        }

        public async Task<bool> TryApiPingRequest()
        {
            try
            {
                string endPoint = $"{this.apiBaseUrl}{this.apiPingPath}";
                HttpResponseMessage apiPingresult = await this.httpClient.GetAsync(endPoint);

                // HttpResponseMessageHandler.LogResponse((apiPingresult));

                return apiPingresult.StatusCode == HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                ExceptionHandler.LogException(e);
            }

            return false;
        }

        public async Task<bool> TryPingBaseUrl()
        {
            try
            {
                Ping ping = new Ping();
                // Ping url must not contain {https://}.
                PingReply pingResult = await ping.SendPingAsync(this.apiBaseUrl.Replace("https://", ""));

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