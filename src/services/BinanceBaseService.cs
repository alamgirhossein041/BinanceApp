using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;
using BinanceApp.JsonConverters;

namespace BinanceApp
{
    public abstract class BinanceBaseService : ServiceBase
    {
        protected BinanceBaseService(IApp app) : base(app)
        {
        }

        protected string GenerateQueryString(Dictionary<string, object> parameters)
        {
            if (parameters == null)
            {
                return string.Empty;
            }

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

        protected string GenerateSignature(string key, string content)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            using (HMACSHA256 hmacsha256 = new HMACSHA256(keyBytes))
            {
                byte[] sourceBytes = Encoding.UTF8.GetBytes(content);
                byte[] hash = hmacsha256.ComputeHash(sourceBytes);
                return BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
            }
        }

        protected async Task<T?> SendRequest<T>(HttpMethod method, string endpoint, Dictionary<string, object> parameters)
        {
            string queryString = this.GenerateQueryString(parameters);

            string endPoint = $"{App.BaseUrl}{endpoint}";
            string finalUrl = $"{endPoint}?{queryString}";

            Console.WriteLine($"Sending request to {finalUrl}");

            using (HttpRequestMessage request = new HttpRequestMessage() { Method = method, RequestUri = new Uri(finalUrl) })
            {
                HttpResponseMessage apiResult = await this.App.HttpClient.SendAsync(request);
                // HttpResponseMessageHandler.LogResponse(apiAssetResult);
                if (apiResult.StatusCode == HttpStatusCode.OK)
                {
                    return await this.OnRequestSuccess<T?>(apiResult);
                }
                else
                {
                    throw await this.OnRequestError(apiResult);
                }
            }
        }

        protected async Task<T?> SendSignedRequest<T>(HttpMethod method, string endpoint, Dictionary<string, object> parameters)
        {
            parameters.Add("timestamp", new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds().ToString());

            string queryString = this.GenerateQueryString(parameters);
            // Create signature for the parameters.
            parameters.Add("signature", this.GenerateSignature(this.App.SecretKey, queryString));
            // Re-generate query string with signature.
            queryString = this.GenerateQueryString(parameters);

            string endPoint = $"{App.BaseUrl}{endpoint}";
            string finalUrl = $"{endPoint}?{queryString}";

            Console.WriteLine($"Sending request to {finalUrl}");

            using (HttpRequestMessage request = new HttpRequestMessage() { Method = method, RequestUri = new Uri(finalUrl) })
            {
                request.Headers.Add("X-MBX-APIKEY", this.App.ApiKey);
                HttpResponseMessage apiResult = await this.App.HttpClient.SendAsync(request);
                // HttpResponseMessageHandler.LogResponse(apiAssetResult);
                if (apiResult.StatusCode == HttpStatusCode.OK)
                {
                    return await this.OnRequestSuccess<T?>(apiResult);
                }
                else
                {
                    throw await this.OnRequestError(apiResult);
                }
            }
        }

        private async Task<T?> OnRequestSuccess<T>(HttpResponseMessage response)
        {
            string responseContent = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrWhiteSpace(responseContent))
            {
                Console.WriteLine($"{typeof(T).Name}: {responseContent}");

                // Source: https://dotnetfiddle.net/Y4Pzzv
                JsonSerializerOptions jsonOptions = new JsonSerializerOptions();
                jsonOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase; // Always serialize to camel case, as convention for JSON and javascript
                jsonOptions.PropertyNameCaseInsensitive = true; // Unless we can have everyone in organization to emit JSON property name with camel case, then we need to set deserializer to be more forgiving re case sensitivity
                jsonOptions.WriteIndented = true; // SHOULD always set to false (default) for PROD code, keeping payload small. It is set as true here to display serialized JSON to ease readibility.
                jsonOptions.Converters.Add(new JsonStringEnumConverter()); // Always handle enum as string, ie using enum value as as string
                jsonOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault; // always ignore null and default value for small serialized JSON payload

                // This is custom converter to handle deserializing from string JSON value to decimal target property.
                jsonOptions.Converters.Add(new DecimalToStringConverter());

                // This is solution to allow reading string number value to target int/double property value
                jsonOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString |
                     JsonNumberHandling.WriteAsString |
                     JsonNumberHandling.AllowNamedFloatingPointLiterals;

                try
                {
                    T deserializedResponse = JsonSerializer.Deserialize<T>(responseContent, jsonOptions);
                    return deserializedResponse ?? default(T);
                }
                catch (Exception e)
                {
                    ExceptionHandler.LogException(e);
                }
            }

            return default(T);
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
                Dictionary<string, object> values = JsonSerializer.Deserialize<Dictionary<string, object>>(content);

                errorCode = values.TryGetValue("code", out object code) ? code.ToString() : "0";
                errorMessage = values.TryGetValue("msg", out object message) ? message.ToString() : "";
            }

            return new Exception(string.Format("Api Error Code: {0} Message: {1}", errorCode, errorMessage));
        }
    }
}