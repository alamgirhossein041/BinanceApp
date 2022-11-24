namespace BinanceWrapper.TimeService
{
    public class BinanceTimeService : BinanceBaseService
    {
        private string apiTimePath = "/api/v3/time";

        public BinanceTimeService(IApp app) : base(app)
        {

        }

        override public void StartService()
        {

        }

        public async Task<DateTime?> GetServerTime()
        {
            BinanceServerTime? serverTime = await this.SendRequest<BinanceServerTime?>(HttpMethod.Get, this.apiTimePath, new Dictionary<string, object>());

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