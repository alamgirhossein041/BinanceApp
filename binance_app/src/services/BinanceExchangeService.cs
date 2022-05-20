using System.Net;
using System.Text.Json;

namespace BinanceApp
{
    public class BinanceExchangeService
    {
        private readonly HttpClient httpClient = null;
        private readonly string apiBaseUrl = null;
        private readonly string assetPath = null;

        public BinanceExchangeService(HttpClient httpClient, string baseUrl, string assetPath)
        {
            this.httpClient = httpClient;
            this.apiBaseUrl = baseUrl;
            this.assetPath = assetPath;
        }

        public async Task<BinanceExchangeAsset?> GetAsset(string pairName)
        {
            try
            {
                string endPoint = string.Format($"{this.apiBaseUrl}{this.assetPath}", pairName);

                HttpResponseMessage apiAssetResult = await this.httpClient.GetAsync(endPoint);

                // HttpResponseMessageHandler.LogResponse(apiAssetResult);

                if (apiAssetResult.StatusCode == HttpStatusCode.OK)
                {
                    string apiAssetResultContent = await apiAssetResult.Content.ReadAsStringAsync();
                    // Dictionary<string, object> values = JsonSerializer.Deserialize<Dictionary<string, object>>(apiAssetResultContent);

                    Console.WriteLine($"Asset: {apiAssetResultContent}");

                    BinanceExchangeAsset asset = JsonSerializer.Deserialize<BinanceExchangeAsset>(apiAssetResultContent);

                    return null;
                }

                return null;
            }
            catch (Exception e)
            {
                ExceptionHandler.LogException(e);
            }

            return null;
        }
    }
}