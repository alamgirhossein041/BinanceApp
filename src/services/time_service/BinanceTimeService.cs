namespace BinanceApp.TimeService
{
    public class BinanceTimeService : BinanceBaseService
    {
        private const string API_TIME_METHOD = "GET";

        private string apiTimePath = string.Empty;

        public BinanceTimeService(IApp app, string apiTimePath) : base(app)
        {
            this.apiTimePath = apiTimePath;
        }

        override public void StartService()
        {

        }

        public async Task<DateTime?> GetServerTime()
        {
            ServerTime? serverTime = await this.SendRequest<ServerTime?>(HttpMethod.Get, this.apiTimePath, new Dictionary<string, object>());

            if (long.TryParse(serverTime.ToString(), out long asDouble))
            {
                DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                DateTime dateTime = start.AddMilliseconds((long)asDouble);
                return dateTime;
            }

            return DateTime.MinValue;
        }
    }
}