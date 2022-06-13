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

        override public void StartService()
        {

        }

        public async Task<BinanceExchangeResult?> GetExchangeAsset(string pairName)
        {
            try
            {
                string endPoint = string.Format($"{App.BaseUrl}{this.assetPath}", pairName);
                HttpResponseMessage apiAssetResult = await App.HttpClient.GetAsync(endPoint);
                // HttpResponseMessageHandler.LogResponse(apiAssetResult);
                if (apiAssetResult.StatusCode == HttpStatusCode.OK)
                {
                    string apiAssetResultContent = await apiAssetResult.Content.ReadAsStringAsync();
                    BinanceExchangeResult? asset = JsonSerializer.Deserialize<BinanceExchangeResult>(apiAssetResultContent);
                    return asset ?? BinanceExchangeResult.InvalidResult;
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