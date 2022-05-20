using System.Net;
using System.Text.Json;

namespace BinanceApp
{
    public class BinanceExchangeService : ServiceBase
    {
        private readonly string assetPath = string.Empty;

        public BinanceExchangeService(IApp app, string assetPath) : base(app)
        {
            this.assetPath = assetPath;
        }

        public async Task<BinanceExchangeAsset?> GetAsset(string pairName)
        {
            try
            {
                string endPoint = string.Format($"{App.BaseUrl}{this.assetPath}", pairName);

                HttpResponseMessage apiAssetResult = await App.HttpClient.GetAsync(endPoint);

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