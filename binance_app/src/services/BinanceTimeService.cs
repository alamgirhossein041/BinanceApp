using System.Net;
using System.Text.Json;

namespace BinanceApp
{
    public class BinanceTimeService
    {
        private const string API_TIME_METHOD = "GET";

        private string apiBaseUrl = string.Empty;
        private string apiTimePath = string.Empty;

        private DateTime invalidTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private HttpClient httpClient = null;

        public BinanceTimeService(HttpClient httpClient, string apiBaseUrl, string apiTimePath)
        {
            this.httpClient = httpClient;
            this.apiBaseUrl = apiBaseUrl;
            this.apiTimePath = apiTimePath;
        }

        public async Task<DateTime> GetServerTime()
        {
            try
            {
                string endPoint = $"{this.apiBaseUrl}{this.apiTimePath}";
                HttpResponseMessage apiTimeResult = await this.httpClient.GetAsync(endPoint);

                // HttpResponseMessageHandler.LogResponse((apiTimeResult));

                if (apiTimeResult.StatusCode == HttpStatusCode.OK)
                {
                    string apiTimeResultContent = await apiTimeResult.Content.ReadAsStringAsync();
                    Dictionary<string, object> values = JsonSerializer.Deserialize<Dictionary<string, object>>(apiTimeResultContent);

                    if (Double.TryParse(values["serverTime"].ToString(), out double asDouble))
                    {
                        DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                        DateTime serverTime = start.AddMilliseconds((long)asDouble);

                        Console.WriteLine("Server time: " + serverTime.ToString());

                        return serverTime;
                    }
                }

                return this.invalidTime;
            }
            catch (Exception e)
            {
                ExceptionHandler.LogException(e);
            }

            return this.invalidTime;
        }
    }
}