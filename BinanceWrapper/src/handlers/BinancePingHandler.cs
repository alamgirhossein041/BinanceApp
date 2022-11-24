using System.Net.NetworkInformation;
using System.Text;

namespace BinanceWrapper
{
    public static class PingReplyHandler
    {
        private static StringBuilder messageBuilder = new StringBuilder();

        public static void LogReply(PingReply reply)
        {
            messageBuilder.AppendLine($"__ADDRESS: {reply.Address}");
            messageBuilder.AppendLine($"__ROUNTRIP TIME: {reply.RoundtripTime}");
            messageBuilder.AppendLine($"__BUFFER: {reply.Buffer.Length}");
            messageBuilder.AppendLine($"__STATUS: {reply.Status}");

            Console.WriteLine(messageBuilder.ToString());
        }
    }
}