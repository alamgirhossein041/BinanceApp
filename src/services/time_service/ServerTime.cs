namespace BinanceApp.TimeService
{
    public struct ServerTime
    {
        public long serverTime { get; set; }

        public override string ToString()
        {
            return this.serverTime.ToString();
        }
    }
}