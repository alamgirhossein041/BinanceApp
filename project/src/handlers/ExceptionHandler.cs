using System.Text;

namespace BinanceApp
{
    public static class ExceptionHandler
    {
        private static StringBuilder messageBuilder = new StringBuilder();

        public static void LogException(Exception e)
        {
            messageBuilder.Clear();
            messageBuilder.AppendLine($"___MESSAGE:{e.Message}.");
            messageBuilder.AppendLine($"___STACKTRACE:{e.StackTrace}.");

            Console.WriteLine(messageBuilder.ToString());
        }
    }
}