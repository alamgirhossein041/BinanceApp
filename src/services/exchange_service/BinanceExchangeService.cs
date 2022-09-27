using System.Net;
using System.Text.Json;

namespace BinanceApp.ExchangeService
{
    public class BinanceExchangeService : ServiceBase
    {
        private readonly string assetPath = string.Empty;

        public BinanceExchangeService(IApp app, string assetPath) : base(app)
        {
            this.assetPath = assetPath;
        }

        override public void StartService()
        {

        }

        public async Task<ExchangeInfo?> GetExchangeSymbol(string symbol)
        {
            try
            {
                string endPoint = string.Format($"{App.BaseUrl}{this.assetPath}", symbol);
                HttpResponseMessage apiAssetResult = await App.HttpClient.GetAsync(endPoint);
                // HttpResponseMessageHandler.LogResponse(apiAssetResult);
                if (apiAssetResult.StatusCode == HttpStatusCode.OK)
                {
                    string apiAssetResultContent = await apiAssetResult.Content.ReadAsStringAsync();
                    ExchangeInfo? asset = JsonSerializer.Deserialize<ExchangeInfo>(apiAssetResultContent);
                    return asset ?? ExchangeInfo.InvalidResult;
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