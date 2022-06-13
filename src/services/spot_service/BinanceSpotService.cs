using System.Net;
using System.Text.Json;

namespace BinanceApp
{
    public class BinanceSpotService : ServiceBase
    {
        private readonly string apiSpotPath = "/api/v3/account";

        public BinanceSpotService(IApp app, string apiSpotPath) : base(app)
        {
            this.apiSpotPath = apiSpotPath;

            this.App.HttpClient.DefaultRequestHeaders.Add("X-MBX-APIKEY", app.SecretKey);
        }

        override public void StartService()
        {

        }

        public async Task<SpotAccountInformation?> GetSpotAccountInformation()
        {
            try
            {
                string endPoint = $"{App.BaseUrl}{this.apiSpotPath}";
                HttpResponseMessage apiAssetResult = await App.HttpClient.GetAsync(endPoint);
                // HttpResponseMessageHandler.LogResponse(apiAssetResult);
                if (apiAssetResult.StatusCode == HttpStatusCode.OK)
                {
                    string apiAssetResultContent = await apiAssetResult.Content.ReadAsStringAsync();
                    // Dictionary<string, object> values = JsonSerializer.Deserialize<Dictionary<string, object>>(apiAssetResultContent);
                    Console.WriteLine($"Asset: {apiAssetResultContent}");
                    // SpotAccountInformation spotAccount = JsonSerializer.Deserialize<SpotAccountInformation>(apiAssetResultContent);
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