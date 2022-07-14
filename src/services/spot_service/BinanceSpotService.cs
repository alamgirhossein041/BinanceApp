using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Web;

namespace BinanceApp.SpotService
{
    public class BinanceSpotService : ServiceBase
    {
        private readonly string apiSpotPath = "/api/v3/account";

        public BinanceSpotService(IApp app, string apiSpotPath) : base(app)
        {
            this.apiSpotPath = apiSpotPath;
        }

        public async Task<SpotAccountInformation?> GetSpotAccountInformation()
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("recvWindow", 5000);
                parameters.Add("timestamp", new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds().ToString());

                string queryString = this.GenerateQueryString(parameters);
                // Create signature for the parameters.
                parameters.Add("signature", this.GenerateSignature(this.App.SecretKey, queryString));
                // Re-generate query string with signature.
                queryString = this.GenerateQueryString(parameters);

                string endPoint = $"{App.BaseUrl}{this.apiSpotPath}";
                string finalUrl = $"{endPoint}?{queryString}";

                using (HttpRequestMessage request = new HttpRequestMessage() { Method = HttpMethod.Get, RequestUri = new Uri(finalUrl) })
                {
                    request.Headers.Add("X-MBX-APIKEY", this.App.ApiKey);
                    HttpResponseMessage apiAssetResult = await App.HttpClient.SendAsync(request);
                    // HttpResponseMessageHandler.LogResponse(apiAssetResult);
                    if (apiAssetResult.StatusCode == HttpStatusCode.OK)
                    {
                        return await this.OnRequestSuccess<SpotAccountInformation>(apiAssetResult);
                    }
                    else
                    {
                        throw await this.OnRequestError(apiAssetResult);
                    }
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.LogException(e);
            }

            return null;
        }

        public async Task<Dictionary<string, string>?> GetAllOrders(string symbol)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("symbol", symbol);
                parameters.Add("recvWindow", 5000);
                parameters.Add("timestamp", new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds().ToString());

                string queryString = this.GenerateQueryString(parameters);
                // Create signature for the parameters.
                parameters.Add("signature", this.GenerateSignature(this.App.SecretKey, queryString));
                // Re-generate query string with signature.
                queryString = this.GenerateQueryString(parameters);

                string endPoint = $"{App.BaseUrl}{this.apiSpotPath}";
                string finalUrl = $"{endPoint}?{queryString}";

                Console.WriteLine($"Sending request to {finalUrl}");

                using (HttpRequestMessage request = new HttpRequestMessage() { Method = HttpMethod.Get, RequestUri = new Uri(finalUrl) })
                {
                    request.Headers.Add("X-MBX-APIKEY", this.App.ApiKey);
                    HttpResponseMessage apiAssetResult = await App.HttpClient.SendAsync(request);
                    // HttpResponseMessageHandler.LogResponse(apiAssetResult);
                    if (apiAssetResult.StatusCode == HttpStatusCode.OK)
                    {
                        string apiAssetResultContent = await apiAssetResult.Content.ReadAsStringAsync();
                        // Dictionary<string, object> values = JsonSerializer.Deserialize<Dictionary<string, object>>(apiAssetResultContent);
                        Console.WriteLine($"Asset: {apiAssetResultContent}");
                        // SpotAccountInformation spotAccount = JsonSerializer.Deserialize<SpotAccountInformation>(apiAssetResultContent);
                        return null;
                    }
                    else
                    {
                        throw await this.OnRequestError(apiAssetResult);
                    }
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.LogException(e);
            }

            return null;
        }

        override public void StartService()
        {

        }

        private string GenerateSignature(string key, string content)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            using (HMACSHA256 hmacsha256 = new HMACSHA256(keyBytes))
            {
                byte[] sourceBytes = Encoding.UTF8.GetBytes(content);
                byte[] hash = hmacsha256.ComputeHash(sourceBytes);
                return BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
            }
        }

        private string GenerateQueryString(Dictionary<string, object> parameters)
        {
            StringBuilder queryString = new StringBuilder();
            foreach (KeyValuePair<string, object> parameter in parameters)
            {
                string queryParameterValue = Convert.ToString(parameter.Value, CultureInfo.InvariantCulture);
                if (!string.IsNullOrWhiteSpace(queryParameterValue))
                {
                    if (queryString.Length > 0)
                    {
                        queryString.Append("&");
                    }
                    queryString.Append($"{parameter.Key}={HttpUtility.UrlEncode(queryParameterValue)}");
                }
            }
            return queryString.ToString();
        }

        private async Task<T?> OnRequestSuccess<T>(HttpResponseMessage response) where T : struct
        {
            string responseContent = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrWhiteSpace(responseContent))
            {
                // Console.WriteLine("responseContent: " + responseContent);

                return JsonSerializer.Deserialize<T>(responseContent);
            }

            return null;
        }

        private async Task<Exception> OnRequestError(HttpResponseMessage response)
        {
            // We received an error
            if (response.StatusCode == HttpStatusCode.GatewayTimeout)
            {
                throw new Exception("Api Request Timeout.");
            }

            // Get te error code and message
            string content = await response.Content.ReadAsStringAsync();

            // Error Values
            string errorCode = "";
            string errorMessage = "";
            if (!string.IsNullOrEmpty(content))
            {
                try
                {
                    Dictionary<string, object> values = JsonSerializer.Deserialize<Dictionary<string, object>>(content);

                    errorCode = values.TryGetValue("code", out object code) ? code.ToString() : "0";
                    errorMessage = values.TryGetValue("msg", out object message) ? message.ToString() : "";
                }
                catch { }
            }

            return new Exception(string.Format("Api Error Code: {0} Message: {1}", errorCode, errorMessage));
        }
    }
}