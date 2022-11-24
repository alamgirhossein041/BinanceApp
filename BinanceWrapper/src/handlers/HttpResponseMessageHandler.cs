using System.Text;

namespace BinanceWrapper
{
    public static class HttpResponseMessageHandler
    {
        private static StringBuilder messageBuilder = new StringBuilder();

        public static async void LogResponse(HttpResponseMessage response)
        {
            string result = await response.Content.ReadAsStringAsync();

            messageBuilder.AppendLine($"___STATUS:{response.StatusCode}.");
            messageBuilder.AppendLine($"___CONTENT:{result}.");

            Console.WriteLine(messageBuilder.ToString());
        }
    }
}