using System.Net;
using System.Text.Json;

namespace BinanceApp.ExchangeService
{
    public class BinanceExchangeService : BinanceBaseService
    {
        private readonly string apiExchangeSymbolPath = "/api/v3/exchangeInfo";

        public BinanceExchangeService(IApp app) : base(app)
        {

        }

        public override void StartService()
        {

        }

        public async Task<BinanceExchangeInfo?> GetExchangeSymbol(string symbol)
        {
            // try
            // {
            //     string endPoint = string.Format($"{App.BaseUrl}{this.apiExchangeSymbolPath}", symbol);
            //     HttpResponseMessage apiAssetResult = await App.HttpClient.GetAsync(endPoint);
            //     // HttpResponseMessageHandler.LogResponse(apiAssetResult);
            //     if (apiAssetResult.StatusCode == HttpStatusCode.OK)
            //     {
            //         string apiAssetResultContent = await apiAssetResult.Content.ReadAsStringAsync();
            //         BinanceExchangeInfo? asset = JsonSerializer.Deserialize<BinanceExchangeInfo>(apiAssetResultContent);
            //         return asset ?? BinanceExchangeInfo.InvalidResult;
            //     }

            //     return null;
            // }
            // catch (Exception e)
            // {
            //     ExceptionHandler.LogException(e);
            // }

            // return null;

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("symbol", symbol);

            return await this.SendSignedRequest<BinanceExchangeInfo?>(HttpMethod.Get, this.apiExchangeSymbolPath, parameters);
        }
    }
}