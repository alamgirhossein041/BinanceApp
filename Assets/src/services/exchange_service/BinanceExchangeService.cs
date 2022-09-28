using System.Net;
using System.Text.Json;

namespace BinanceApp.ExchangeService
{
    public class BinanceExchangeService : BinanceBaseService
    {
        private readonly string assetPath = string.Empty;

        public BinanceExchangeService(IApp app, string assetPath) : base(app)
        {
            this.assetPath = assetPath;
        }

        public override void StartService()
        {

        }

        public async Task<BinanceExchangeInfo?> GetExchangeSymbol(string symbol)
        {
            try
            {
                string endPoint = string.Format($"{App.BaseUrl}{this.assetPath}", symbol);
                HttpResponseMessage apiAssetResult = await App.HttpClient.GetAsync(endPoint);
                // HttpResponseMessageHandler.LogResponse(apiAssetResult);
                if (apiAssetResult.StatusCode == HttpStatusCode.OK)
                {
                    string apiAssetResultContent = await apiAssetResult.Content.ReadAsStringAsync();
                    BinanceExchangeInfo? asset = JsonSerializer.Deserialize<BinanceExchangeInfo>(apiAssetResultContent);
                    return asset ?? BinanceExchangeInfo.InvalidResult;
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